using System;
using Microsoft.ServiceBus.Messaging;

namespace DesignPatternDemo.serviceBus.topic
{
    public class ServiceBusTopicDemo_Consumer
    {
        //public static void Main()
        //{
        //    string connectionString = "Endpoint=sb://jym-dev.servicebus.chinacloudapi.cn/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=hCwV7FZOs/tlS/F48pVqqd6h29Ysu+T8N7tdSXlPzgc=;TransportType=Amqp";
        //    SubscriptionClient subClient = SubscriptionClient.CreateFromConnectionString(connectionString, "DataCollectionTopic", "Shanghai");
        //    subClient.Receive();

        //    while (true)
        //    {
        //        BrokeredMessage message = subClient.Receive();

        //        if (message != null)
        //        {
        //            try
        //            {
        //                // process the message like update the inventory system.
        //                Console.WriteLine("Body: " + message.GetBody<string>());
        //                Console.WriteLine("MessageID: " + message.MessageId);
        //                Console.WriteLine("Site: " + message.Properties["site"]);
        //                message.Complete();
        //            }
        //            catch (Exception)
        //            {
        //                message.Abandon();
        //            }
        //        }
        //        else
        //            break;
        //    }
        //}
    }
}