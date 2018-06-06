using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternDemo
{
    public class _03_02_工厂模式
    {
        //public static void Main()
        //{
        //    Creator04 tomatoScrambledEggsFactory = new TomatoScrambledEggsFactory04();
        //    Creator04 shreddedPorkWithPotatoesFactory = new ShreddedPorkWithPotatoesFactory04();
        //    Creator04 mincedMeatEggplantFactory = new MincedMeatEggplantFactory();
        //    Food04 tomatoFood = tomatoScrambledEggsFactory.CreateFoddFactory();
        //    tomatoFood.Print();

        //    Food04 shreddedFood = shreddedPorkWithPotatoesFactory.CreateFoddFactory();
        //    shreddedFood.Print();

        //    Food04 mincedFood = mincedMeatEggplantFactory.CreateFoddFactory();
        //    mincedFood.Print();
        //    Console.ReadKey();
        //}
    }

    /// <summary>
    /// 抽象工厂类
    /// </summary>
    public abstract class Creator04
    {
        // 工厂方法
        public abstract Food04 CreateFoddFactory();
    }

    /// <summary>
    /// 菜抽象类
    /// </summary>
    public abstract class Food04
    {
        // 输出点了什么菜
        public abstract void Print();
    }

    /// <summary>
    /// 肉末茄子这道菜
    /// </summary>
    public class MincedMeatEggplant : Food04
    {
        public override void Print()
        {
            Console.WriteLine("肉末茄子好了！");
        }
    }

    /// <summary>
    ///  肉末茄子工厂类，负责创建肉末茄子这道菜
    /// </summary>
    public class MincedMeatEggplantFactory : Creator04
    {
        /// <summary>
        /// 负责创建肉末茄子这道菜
        /// </summary>
        /// <returns></returns>
        public override Food04 CreateFoddFactory()
        {
            return new MincedMeatEggplant();
        }
    }

    /// <summary>
    /// 土豆肉丝这道菜
    /// </summary>
    public class ShreddedPorkWithPotatoes04 : Food04
    {
        public override void Print()
        {
            Console.WriteLine("土豆肉丝好了");
        }
    }

    /// <summary>
    /// 土豆肉丝工厂类
    /// </summary>
    public class ShreddedPorkWithPotatoesFactory04 : Creator04
    {
        /// <summary>
        ///负责创建土豆肉丝这道菜
        /// </summary>
        /// <returns></returns>
        public override Food04 CreateFoddFactory()
        {
            return new ShreddedPorkWithPotatoes04();
        }
    }

    /// <summary>
    /// 西红柿炒鸡蛋这道菜
    /// </summary>
    public class TomatoScrambledEggs04 : Food04
    {
        public override void Print()
        {
            Console.WriteLine("西红柿炒蛋好了！");
        }
    }

    /// <summary>
    /// 西红柿炒蛋工厂类
    /// </summary>
    public class TomatoScrambledEggsFactory04 : Creator04
    {
        /// <summary>
        /// 负责创建西红柿炒蛋这道菜
        /// </summary>
        /// <returns></returns>
        public override Food04 CreateFoddFactory()
        {
            return new TomatoScrambledEggs04();
        }
    }
}