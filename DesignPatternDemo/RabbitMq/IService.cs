using System;
using JYM.Core.MessageQueue.Rabbit;

namespace DesignPatternDemo
{
    public interface IService
    {
        bool Start();

        bool Stop();
    }

    public class BankMqService : IService
    {
        private readonly IQueueReceiveService queueReceiveService;

        public BankMqService(IQueueReceiveService queueReceiveService)
        {
            this.queueReceiveService = queueReceiveService;
        }

        #region IService Members

        public bool Start()
        {
            #region 报备

            try
            {
                this.queueReceiveService.TestRabbit();

                this.queueReceiveService.SendTestAsync(new TestRabbitModel { Username = "汪" });
                return true;

                #endregion 报备
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool Stop()
        {
            Client.Dispose();
            return true;
        }

        #endregion IService Members
    }
}