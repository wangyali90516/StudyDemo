using System;
using Autofac;

namespace DesignPatternDemo.autoFac
{
    public class AutoFacInit
    {
        //public static void Main()
        //{
        //    //注册AutoFac
        //    var builder = new ContainerBuilder();
        //    builder.RegisterType<Show>();
        //    builder.RegisterType<TestService>().As<ITestService>();
        //    //调用方法
        //    using (var container = builder.Build())
        //    {
        //        var manager = container.Resolve<Show>();
        //        Console.WriteLine(manager.Add2(1, 2));
        //    }
        //    Console.ReadKey();
        //}
    }

    public class Show
    {
        public readonly ITestService testService;

        public Show(ITestService testService)
        {
            this.testService = testService;
        }

        public int Add2(int a, int b)
        {
            return this.testService.Add(a, b);
        }
    }
}