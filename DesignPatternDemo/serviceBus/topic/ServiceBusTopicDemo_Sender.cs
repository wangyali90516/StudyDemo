using System;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace DesignPatternDemo.serviceBus
{
    public class ServiceBusTopicDemo_Sender
    {
        // / <summary>
        // / Topic的名称是DataCollectionTopic，订阅是Shanghai和Wuxi
        //  / </summary>
        //public static void Main()
        //{
        //    string connectionString = "Endpoint=sb://jym-dev.servicebus.chinacloudapi.cn/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=hCwV7FZOs/tlS/F48pVqqd6h29Ysu+T8N7tdSXlPzgc=;TransportType=Amqp";

        //    var myNamespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

        //    //Create Topic
        //    if (!myNamespaceManager.TopicExists("DataCollectionTopic"))
        //    {
        //        myNamespaceManager.CreateTopic("DataCollectionTopic");
        //        Console.WriteLine("Service Bus Topic is created");
        //    }

        //    //Create Subscription
        //    if (!myNamespaceManager.SubscriptionExists("DataCollectionTopic", "Shanghai"))
        //    {
        //        myNamespaceManager.CreateSubscription("DataCollectionTopic", "Shanghai", new SqlFilter("site = 'Shanghai'"));
        //        Console.WriteLine("Subscription named Shanghai is created");
        //    }

        //    if (!myNamespaceManager.SubscriptionExists("DataCollectionTopic", "Wuxi"))
        //    {
        //        myNamespaceManager.CreateSubscription("DataCollectionTopic", "Wuxi", new SqlFilter("site = 'Wuxi'"));
        //        Console.WriteLine("Subscription named Wuxi is created");
        //    }

        //    //Begin to send messages
        //    TopicClient client = TopicClient.CreateFromConnectionString(connectionString, "DataCollectionTopic");

        //    for (int i = 0; i < 100; i++)
        //    {
        //        BrokeredMessage message = new BrokeredMessage("Test message " + i);
        //        if (i % 2 == 0)
        //            message.Properties["site"] = "Shanghai";
        //        else
        //            message.Properties["site"] = "Wuxi";
        //        client.Send(message);
        //        Console.WriteLine(i + 1 + " messages are sent");
        //    }
        //}
    }
}