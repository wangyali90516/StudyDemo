using System;
using Autofac;
using Topshelf;
using Topshelf.Autofac;

namespace DesignPatternDemo
{
    public class RabbitMq
    {
        //public static void Main(string[] args)
        //{
        //    var builder = new ContainerBuilder();
        //    builder.RegisterType<BankMqService>().AsSelf();
        //    // AutoMappingConfig.Register();
        //    AutoFacConfig.RegisterComponents(builder);
        //    var container = builder.Build();
        //    // ConfigManager.Init(container);

        //    // ExceptionlessClient.Default.Configuration.ServerUrl = ConfigManager.ExceptionLessServer;
        //    // ExceptionlessClient.Default.Configuration.ApiKey = ConfigManager.ExceptionLessKey;

        //    HostFactory.Run(c =>
        //    {
        //        // Pass it to Topshelf
        //        c.UseAutofacContainer(container);
        //        c.SetServiceName("测试RabbitMq");
        //        c.SetDisplayName("测试RabbitMq");
        //        c.SetDescription("测试RabbitMq");

        //        c.Service<BankMqService>(s =>
        //        {
        //            s.ConstructUsingAutofacContainer();
        //            s.WhenStarted(service => service.Start());
        //            s.WhenStopped(service => service.Stop());
        //        });
        //    });
        //    Console.ReadKey();
        //}
    }
}