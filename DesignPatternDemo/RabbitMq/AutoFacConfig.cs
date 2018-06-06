using Autofac;

namespace DesignPatternDemo
{
    public class AutoFacConfig
    {
        public static void RegisterComponents(ContainerBuilder container)
        {
            container.RegisterType<QueueReceiveService>().As<IQueueReceiveService>();
        }
    }
}