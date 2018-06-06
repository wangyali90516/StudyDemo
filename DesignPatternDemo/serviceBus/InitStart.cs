using System;

namespace DesignPatternDemo.serviceBus
{
    public class InitStart
    {
        /// <summary>
        ///     ServiceBus连接字符串
        /// </summary>
        public static string ServiceBusConnectionString = "Endpoint=sb://jym-dev.servicebus.chinacloudapi.cn/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=hCwV7FZOs/tlS/F48pVqqd6h29Ysu+T8N7tdSXlPzgc=;TransportType=Amqp";

        /// <summary>
        ///     service bus provider
        /// </summary>
        private static Lazy<ServiceBusMethodProvider> lazyServiceBusMethodProvider;

        /// <summary>
        ///     get rpchttpclient
        /// </summary>
        public static ServiceBusMethodProvider ServiceBusMethodProvider => lazyServiceBusMethodProvider.Value;

        //public static void Main()
        //{
        //    //service bus初始化
        //    lazyServiceBusMethodProvider = new Lazy<ServiceBusMethodProvider>(() => ServiceBusFactory.InitServicebusProvider(ServiceBusConnectionString));

        //    //发送消息
        //    SendMessage();

        //    ReceiveMessage();
        //    Console.ReadKey();
        //}

        /// <summary>
        /// 接收队列
        /// </summary>
        /// <returns></returns>
        public static bool ReceiveMessage()
        {
            ServiceBusMethodProvider.ReceiveMessage<string>("test-queue", body =>
            {
                var reseiveMessage = body;
                Console.WriteLine(reseiveMessage);
                return true;
            });
            return true;
        }

        /// <summary>
        /// 发送队列
        /// </summary>
        /// <returns></returns>
        public static bool SendMessage()
        {
            return ServiceBusMethodProvider.Send("test-queue", "bbb");
        }
    }
}