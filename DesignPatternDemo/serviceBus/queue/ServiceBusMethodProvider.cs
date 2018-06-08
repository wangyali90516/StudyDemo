using System;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace DesignPatternDemo.serviceBus
{
    public class ServiceBusMethodProvider
    {
        private readonly QueueClientManager queueClientManager;

        /// <summary>
        ///     constructor
        /// </summary>
        /// <param name="queueClientManager"></param>
        public ServiceBusMethodProvider(QueueClientManager queueClientManager)
        {
            this.queueClientManager = queueClientManager;
        }

        /// <summary>
        ///     根据name获取queueclinet
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public QueueClient GetQueue(string queueName)
        {
            return this.queueClientManager.CreateQueue(queueName);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queueName"></param>
        /// <param name="func"></param>
        public void ReceiveMessage<T>(string queueName, Func<T, bool> func) where T : class
        {
            QueueClient queueClient = this.GetQueue(queueName);
            queueClient.OnMessageAsync(async queueMessage =>
            {
                try
                {
                    //data类型
                    string objMsg = queueMessage.GetBody<string>();
                    T tObj = JsonConvert.DeserializeObject<T>(objMsg);
                    bool result = func(tObj);
                    if (!result)
                    {
                        //失败了重新放进去
                        await queueMessage.DeadLetterAsync();
                    }
                }
                catch (Exception)
                {
                    //string msg = e.Message;
                }
            });
        }

        /// <summary>
        ///     发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue"></param>
        /// <param name="message"></param>
        public bool Send<T>(string queue, T message) where T : class
        {
            try
            {
                QueueClient declaredQueue = this.GetQueue(queue);
                string content = JsonConvert.SerializeObject(message);
                BrokeredMessage queueMessage = new BrokeredMessage(content);
                declaredQueue.Send(queueMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}