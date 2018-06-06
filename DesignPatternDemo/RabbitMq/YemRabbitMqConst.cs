namespace DesignPatternDemo
{
    public class YemRabbitMqConst
    {
        /// <summary>
        ///     余额猫的Broker Name
        /// </summary>
        public const string BrokerName = "yem";

        /// <summary>
        ///    交换机名称
        /// </summary>
        public const string Test_EXCHANGE_NAME = "exchange.direct.yem.Test001";

        /// <summary>
        ///  生产者
        /// </summary>
        public const string Test_QUEUE_NAME_PRODUCER_KEY = "yem_Test001_pro";
    }
}