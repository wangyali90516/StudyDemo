using System;

namespace DesignPatternDemo
{
    /// <summary>
    /// 菜抽象类
    /// </summary>
    public abstract class Food_02
    {
        /// <summary>
        ///  输出点了什么菜
        /// </summary>
        public abstract void Print();
    }

    /// <summary>
    /// 简单工厂类, 负责 炒菜
    /// </summary>
    public class FoodSimpleFactory
    {
        /// <summary>
        /// 烧菜方法
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Food_02 CreateFood(string type)
        {
            Food_02 food = null;
            // 客户A说：我想吃西红柿炒蛋怎么办？
            // 客户B说：那你就自己烧啊
            // 客户A说： 好吧，那就自己做吧
            if (type.Equals("西红柿炒蛋"))
            {
                food = new TomatoScrambledEggs_02();
            }
            // 我又想吃土豆肉丝, 这个还是得自己做
            // 我觉得自己做好累哦，如果能有人帮我做就好了？
            else if (type.Equals("土豆肉丝"))
            {
                food = new ShreddedPorkWithPotatoes_02();
            }
            return food;
        }
    }

    /// <summary>
    /// 土豆肉丝这道菜
    /// </summary>
    public class ShreddedPorkWithPotatoes_02 : Food_02
    {
        #region Overrides of Food

        public override void Print()
        {
            Console.WriteLine("一份土豆肉丝！");
        }

        #endregion Overrides of Food
    }

    /// <summary>
    /// 西红柿炒鸡蛋这道菜
    /// </summary>
    public class TomatoScrambledEggs_02 : Food_02
    {
        #region Overrides of Food

        public override void Print()
        {
            Console.WriteLine("一份西红柿炒蛋！");
        }

        #endregion Overrides of Food
    }

    /// <summary>
    /// 顾客充当客户端，负责调用简单工厂来生产对象
    /// 即客户点菜，厨师（相当于简单工厂）负责烧菜(生产的对象)
    /// </summary>
    internal class _02_02_简单工厂模式
    {
        //private static void Main(string[] args)
        //{
        //    // 客户想点一个西红柿炒蛋
        //    Food_02 food1 = FoodSimpleFactory.CreateFood("西红柿炒蛋");
        //    food1.Print();

        //    // 客户想点一个土豆肉丝
        //    Food_02 food2 = FoodSimpleFactory.CreateFood("土豆肉丝");
        //    food2.Print();

        //    Console.Read();
        //}
    }
}