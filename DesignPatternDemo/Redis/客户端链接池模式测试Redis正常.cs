namespace DesignPatternDemo.Redis
{
    /// <summary>
    /// RedisCacheHelper 使用的是客户端链接池模式，这样的存取效率应该是最高的。同时也更方便的支持读写分离，均衡负载。
    /// </summary>
    public class 客户端链接池模式测试Redis正常
    {
        //private static void Main(string[] args)
        //{
        //    Console.WriteLine("Redis写入缓存：zhong");

        //    RedisCacheHelper.Add("zhong", "zhongzhongzhong", DateTime.Now.AddDays(1));

        //    Console.WriteLine("Redis获取缓存：zhong");

        //    string str3 = RedisCacheHelper.Get<string>("zhong");

        //    Console.WriteLine(str3);

        //    Console.WriteLine("Redis获取缓存：nihao");
        //    string str = RedisCacheHelper.Get<string>("nihao");
        //    Console.WriteLine(str);

        //    Console.WriteLine("Redis获取缓存：wei");
        //    string str1 = RedisCacheHelper.Get<string>("wei");
        //    Console.WriteLine(str1);

        //    Console.ReadKey();
        //}
    }
}