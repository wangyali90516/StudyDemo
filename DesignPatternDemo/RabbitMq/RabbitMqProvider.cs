using System;
using JYM.Core.MessageQueue.Rabbit;
using Newtonsoft.Json;

namespace DesignPatternDemo
{
    /// <summary>
    ///     Rabbit send and receive
    /// </summary>
    public class RabbitMqProvider
    {
        static RabbitMqProvider()
        {
            //初始化RabbitMQ
            Client.Init();
        }

        /// <summary>
        ///     接收业务日志队列
        /// </summary>
        public static void Receive(string consumName, Func<string, bool> func, string brokerName = YemRabbitMqConst.BrokerName)
        {
            Client.GetInstance(brokerName).Consumer.OnReceived(consumName, func);
        }

        /// <summary>
        ///     发送
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="producerKey"></param>
        /// <param name="messageBody"></param>
        /// <param name="brokerName"></param>
        /// <returns></returns>
        public static bool Send<T>(string producerKey, T messageBody, string brokerName = YemRabbitMqConst.BrokerName)
        {
            try
            {
                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.None
                };

                string messageBodyStr = JsonConvert.SerializeObject(messageBody, jsonSerializerSettings);
                return Client.GetInstance(brokerName).Producer.Send(producerKey, messageBodyStr);
            }
            catch (Exception e)
            {
                //后续补充异常日志
                return false;
            }
        }
    }
}