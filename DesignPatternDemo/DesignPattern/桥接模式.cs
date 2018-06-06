using System;

namespace DesignPatternDemo
{
    /// <summary>
    /// 长虹牌电视机，重写基类的抽象方法
    /// 提供具体的实现
    /// </summary>
    public class ChangHong : TV
    {
        public override void Off()
        {
            Console.WriteLine("长虹牌电视机已经关掉了");
        }

        public override void On()
        {
            Console.WriteLine("长虹牌电视机已经打开了");
        }

        public override void tuneChannel()
        {
            Console.WriteLine("长虹牌电视机换频道");
        }
    }

    /// <summary>
    /// 抽象概念中的遥控器，扮演抽象化角色
    /// </summary>
    public class RemoteControl
    {
        public TV Tv { get; set; }

        /// <summary>
        /// 关电视机
        /// </summary>
        public virtual void Off()
        {
            this.Tv.Off();
        }

        /// <summary>
        /// 开电视机，这里抽象类中不再提供实现了，而是调用实现类中的实现
        /// </summary>
        public virtual void On()
        {
            this.Tv.On();
        }
    }

    /// <summary>
    /// 三星牌电视机，重写基类的抽象方法
    /// </summary>
    public class Samsung : TV
    {
        public override void Off()
        {
            Console.WriteLine("三星牌电视机已经关掉了");
        }

        public override void On()
        {
            Console.WriteLine("三星牌电视机已经打开了");
        }

        public override void tuneChannel()
        {
            Console.WriteLine("三星牌电视机换频道");
        }
    }

    /// <summary>
    /// 电视机，提供抽象方法
    /// </summary>
    public abstract class TV
    {
        public abstract void Off();

        public abstract void On();

        public abstract void tuneChannel();
    }

    public class 桥接模式
    {
        //public static void Main()
        //{
        //    RemoteControl remoteControl = new RemoteControl();
        //    remoteControl.Tv = new ChangHong();
        //    remoteControl.On();
        //    remoteControl.Off();

        //    remoteControl.Tv = new Samsung();
        //    remoteControl.On();
        //    remoteControl.Off();
        //    Console.ReadKey();
        //}
    }
}