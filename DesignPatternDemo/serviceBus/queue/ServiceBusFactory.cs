using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternDemo.serviceBus
{
    public class ServiceBusFactory
    {
        public static ServiceBusMethodProvider InitServicebusProvider(string connectionString)
        {
            return new ServiceBusMethodProvider(new QueueClientManager(connectionString));
        }
    }
}