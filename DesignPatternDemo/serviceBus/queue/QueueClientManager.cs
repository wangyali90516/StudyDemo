using System;
using System.Collections.Concurrent;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace DesignPatternDemo.serviceBus
{
    public class QueueClientManager
    {
        private readonly MessagingFactory messagingFactory;
        private readonly NamespaceManager namespaceManager;
        private readonly ConcurrentDictionary<string, QueueClient> queues;

        /// <summary>
        ///     constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public QueueClientManager(string connectionString)
        {
            //创建链接
            this.namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            Uri address = this.namespaceManager.Address;
            MessagingFactorySettings mfs = new MessagingFactorySettings
            {
                TokenProvider = this.namespaceManager.Settings.TokenProvider,
                NetMessagingTransportSettings = { BatchFlushInterval = TimeSpan.FromMilliseconds(20) } //批处理的时间间隔
            };
            this.messagingFactory = MessagingFactory.Create(address, mfs);
            this.queues = new ConcurrentDictionary<string, QueueClient>();
        }

        /// <summary>
        ///    创建队列
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="autoDelete"></param>
        /// <returns></returns>
        public QueueClient CreateQueue(string queueName, bool autoDelete = false)
        {
            if (string.IsNullOrEmpty(queueName))
            {
                return null;
            }

            return this.queues.GetOrAdd(queueName, s =>
            {
                if (!this.namespaceManager.QueueExists(s))
                {
                    QueueDescription description = new QueueDescription(s);
                    this.namespaceManager.CreateQueue(description);
                }

                QueueClient client = this.messagingFactory.CreateQueueClient(s);
                return client;
            });
        }

        /// <summary>
        ///   删除队列
        /// </summary>
        /// <param name="queueName"></param>
        public void QueueDelete(string queueName)
        {
            QueueClient toRemove;
            if (this.queues.TryRemove(queueName, out toRemove))
            {
                if (this.namespaceManager.QueueExists(queueName))
                {
                    this.namespaceManager.DeleteQueue(queueName);
                }
            }
        }
    }
}