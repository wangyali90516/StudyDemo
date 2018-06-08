using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace DesignPatternDemo.autoFac
{
    public class AutoFacConfig
    {
        public static void RegisterComponents(ContainerBuilder container)
        {
            container.RegisterType<TestService>().As<ITestService>();
        }
    }
}