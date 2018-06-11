using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DesignPatternDemo.quartz;

namespace DesignPatternDemo.dapper
{
    public class DapperInit
    {
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <returns></returns>
        public static bool BatchInsert()
        {
            List<Book> books = new List<Book>();
            for (int i = 0; i < 10; i++)
            {
                Book book = new Book
                {
                    BookName = "测试" + i,
                    PersonID = i + 2
                };
                books.Add(book);
            }
            var conn = new SqlConnection(Settings.SqlDiagnosticsDb);
            string query = "INSERT INTO Book(BookName,PersonID)VALUES(@BookName,@PersonID)";
            int result = conn.Execute(query, books);
            return result > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public static bool Delete()
        {
            Book book = new Book
            {
                PersonID = 1
            };
            var conn = new SqlConnection(Settings.SqlDiagnosticsDb);
            string query = "delete from Book where PersonID=@personId";
            int result = conn.Execute(query, book);
            return result > 0;
        }

        /// <summary>
        /// 查询多张表的记录
        /// </summary>
        /// <returns></returns>
        public static Tuple<List<Book>, List<Person>> GetDoubleTable()
        {
            var conn = new SqlConnection(Settings.SqlDiagnosticsDb);
            string sql = "select * from Person;select * from Book";
            var multiReader = conn.QueryMultiple(sql);
            var personList = multiReader.Read<Person>().ToList();
            var bookList = multiReader.Read<Book>().ToList();
            multiReader.Dispose();

            return Tuple.Create(bookList, personList);
        }

        /// <summary>
        ///获取集合
        /// </summary>
        /// <returns></returns>
        public static List<Book> GetList()
        {
            var conn = new SqlConnection(Settings.SqlDiagnosticsDb);
            return conn.Query<Book>("select * from Book").ToList();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public static bool Insert()
        {
            Book book = new Book
            {
                BookName = "测试",
                PersonID = 1
            };
            var conn = new SqlConnection(Settings.SqlDiagnosticsDb);
            string query = "INSERT INTO Book(BookName,PersonID) VALUES(@bookname,@personId);SELECT @@identity;";
            int result = conn.ExecuteScalar<int>(query, book);
            return result > 0;
        }

        //public static void Main()
        //{
        //    //单条插入
        //    Insert();

        //    //批量插入
        //    //  BatchInsert();

        //    //修改
        //    //  Modify();

        //    //删除
        //    //  Delete();

        //    //查询所有集合
        //    //var bookList = GetList();
        //    //foreach (Book book in bookList)
        //    //{
        //    //    Console.WriteLine(book.ID + "  " + book.BookName + "  " + book.PersonID);
        //    //}

        //    //var tupleResult = GetDoubleTable();
        //    //Console.WriteLine("多表返回输出");
        //    //foreach (Book book in tupleResult.Item1)
        //    //{
        //    //    Console.WriteLine(book.ID + "  " + book.BookName + "  " + book.PersonID);
        //    //}
        //    //foreach (Person person in tupleResult.Item2)
        //    //{
        //    //    Console.WriteLine(person.Id + "  " + person.Name + "  " + person.Remark);
        //    //}

        //    var result = QueryJoin();
        //    foreach (BookWithPerson person in result)
        //    {
        //        Console.WriteLine(person.ID + "  " + person.BookName + "  " + person.Pers.Id);
        //    }
        //    Console.ReadKey();
        //}

        /// <summary>
        /// 更新某行数据
        /// </summary>
        /// <returns></returns>
        public static bool Modify()
        {
            Book book = new Book
            {
                ID = 1,
                BookName = "111"
            };
            var conn = new SqlConnection(Settings.SqlDiagnosticsDb);
            string query = "update Book set BookName=@BookName where ID=@ID";
            int result = conn.Execute(query, book);
            return result > 0;
        }

        /// <summary>
        /// In筛选
        /// </summary>
        /// <returns></returns>
        public static List<Book> QueryIn()
        {
            List<int> ids = new List<int> { 12, 1 };
            var conn = new SqlConnection(Settings.SqlDiagnosticsDb);

            string sql = "select * from Book where ID in @ids";
            return conn.Query<Book>(sql, new { ids }).ToList();
        }

        /// <summary>
        /// 多表查询
        /// </summary>
        /// <returns></returns>
        public static List<BookWithPerson> QueryJoin()
        {
            using (IDbConnection connection = new SqlConnection(Settings.SqlDiagnosticsDb))
            {
                var sql = @"select b.id,b.bookName,p.id,p.name,p.remark
                        from Person as p
                        join Book as b
                        on p.id = b.personId
                        where b.id = @id;";
                //Query 委托回调类型1，委托回调类型2，返回类型
                var result = connection.Query<BookWithPerson, Person, BookWithPerson>(sql,
                    (bookWithPerson, person) =>
                    {
                        bookWithPerson.Pers = person;
                        return bookWithPerson;
                    },
                    new { id = 4 });
                return result.ToList();
            }
        }

        /// <summary>
        /// 事务删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteDapperDemoList(int id)
        {
            using (IDbConnection connection = new SqlConnection(Settings.SqlDiagnosticsDb))
            {
                const string deleteChildSQL = @"DELETE FROM dbo.DapperNETDemo WHERE ID> 0 AND ID = @ID";
                const string deleteParentSQL = @"DELETE FROM dbo.DapperNETDemo WHERE ParentID < 1 AND ID = @ID";

                IDbTransaction transaction = connection.BeginTransaction();
                int rowsAffected = connection.Execute(deleteChildSQL, new { ID = id }, transaction);
                rowsAffected += connection.Execute(deleteParentSQL, new { ID = id }, transaction);
                transaction.Commit();
                return rowsAffected;
            }
        }
    }
}