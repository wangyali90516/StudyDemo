using System;

namespace DesignPatternDemo
{
    /// <summary>
    /// 抽象工厂类
    /// </summary>
    public abstract class Creator
    {
        // 工厂方法
        public abstract Food03 CreateFoddFactory();
    }

    /// <summary>
    /// 菜抽象类
    /// </summary>
    public abstract class Food03
    {
        // 输出点了什么菜
        public abstract void Print();
    }

    /// <summary>
    /// 土豆肉丝这道菜
    /// </summary>
    public class ShreddedPorkWithPotatoes03 : Food03
    {
        public override void Print()
        {
            Console.WriteLine("土豆肉丝好了");
        }
    }

    /// <summary>
    /// 土豆肉丝工厂类
    /// </summary>
    public class ShreddedPorkWithPotatoesFactory : Creator
    {
        /// <summary>
        ///负责创建土豆肉丝这道菜
        /// </summary>
        /// <returns></returns>
        public override Food03 CreateFoddFactory()
        {
            return new ShreddedPorkWithPotatoes03();
        }
    }

    /// <summary>
    /// 西红柿炒鸡蛋这道菜
    /// </summary>
    public class TomatoScrambledEggs03 : Food03
    {
        public override void Print()
        {
            Console.WriteLine("西红柿炒蛋好了！");
        }
    }

    /// <summary>
    /// 西红柿炒蛋工厂类
    /// </summary>
    public class TomatoScrambledEggsFactory : Creator
    {
        /// <summary>
        /// 负责创建西红柿炒蛋这道菜
        /// </summary>
        /// <returns></returns>
        public override Food03 CreateFoddFactory()
        {
            return new TomatoScrambledEggs03();
        }
    }

    public class 工厂模式
    {
        //public static void Main()
        //{
        //    Creator tomatoScrambledEggsFactory = new TomatoScrambledEggsFactory();
        //    Creator shreddedPorkWithPotatoesFactory = new ShreddedPorkWithPotatoesFactory();
        //    Food03 tomatoFood = tomatoScrambledEggsFactory.CreateFoddFactory();
        //    tomatoFood.Print();

        //    Food03 shreddedFood = shreddedPorkWithPotatoesFactory.CreateFoddFactory();
        //    shreddedFood.Print();
        //    Console.ReadKey();
        //}
    }
}