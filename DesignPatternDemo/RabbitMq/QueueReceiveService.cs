using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JYM.Core.MessageQueue.Rabbit.Config;
using Newtonsoft.Json;
using ServiceStack;

namespace DesignPatternDemo
{
    public class QueueReceiveService : IQueueReceiveService
    {
        #region IQueueReceiveService Members

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> SendTestAsync(TestRabbitModel input)
        {
            return await Task.FromResult(RabbitMqProvider.Send(YemRabbitMqConst.Test_QUEUE_NAME_PRODUCER_KEY, input));
        }

        /// <summary>
        /// 接收
        /// </summary>
        public void TestRabbit()
        {
            //获取所有的当前brokerName下的某个Exchange下的所有消费者配置名称
            List<string> consumerData = ClientConfiguration.Instance.GetConsumerDatas(YemRabbitMqConst.Test_EXCHANGE_NAME);
            foreach (var consummerkey in consumerData)
            {
                RabbitMqProvider.Receive(consummerkey, body =>
                {
                    try
                    {
                        TestRabbitModel test = JsonConvert.DeserializeObject<TestRabbitModel>(body);
                        Console.WriteLine(test.ToJson());
                        return true;
                    }
                    catch (Exception)
                    {
                        return true;
                    }
                });
            }
            Console.ReadKey();
        }

        #endregion IQueueReceiveService Members
    }
}