using System.Threading.Tasks;
using Quartz;

namespace DesignPatternDemo.quartz
{
    [DisallowConcurrentExecution]
    public class TestJob : IJob
    {
        private readonly ITestService testService;

        public TestJob(ITestService testService)
        {
            this.testService = testService;
        }

        #region IJob Members

        public async void Execute(IJobExecutionContext context)
        {
            this.testService.Add(1, 1);
            await Task.FromResult(0);
        }

        #endregion IJob Members
    }
}