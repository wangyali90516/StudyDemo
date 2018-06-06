using System;
using System.Collections.Generic;
using System.Linq;
using JYM.Core.MessageQueue.Rabbit.Config;

namespace DesignPatternDemo
{
    public static class RabbitMqConfigExtension
    {
        /// <summary>
        ///     获取所有的数据
        /// </summary>
        /// <param name="clientConfiguration"></param>
        /// <param name="brokerName"></param>
        /// <param name="exchangeName"></param>
        /// <returns></returns>
        public static List<string> GetConsumerDatas(this ClientConfiguration clientConfiguration, string exchangeName, string brokerName = YemRabbitMqConst.BrokerName)
        {
            try
            {
                List<string> consumerNames = new List<string>();
                BrokerConfigurationCollection brokerConfigurationCollection = clientConfiguration.Brokers;
                foreach (ExchangeConfiguration brokerConfigurationExchangeConfig in brokerConfigurationCollection.Cast<BrokerConfiguration>().Where(brokerConfiguration => brokerConfiguration.Name == brokerName)
                    .SelectMany(brokerConfiguration => brokerConfiguration.ExchangeConfigs.Cast<ExchangeConfiguration>()))
                {
                    //获取那个exchangeName
                    if (exchangeName == brokerConfigurationExchangeConfig.Name)
                    {
                        consumerNames.AddRange(from ConsumerConfiguration consumerConfig in brokerConfigurationExchangeConfig.ConsumerConfigs select consumerConfig.Name);
                    }
                }
                return consumerNames;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}