using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternDemo.quartz
{
    public class TestService : ITestService
    {
        #region Implementation of ITestService

        public int Add(int a, int b)
        {
            return a + b;
        }

        #endregion Implementation of ITestService
    }
}