using System.Threading.Tasks;

namespace DesignPatternDemo
{
    public interface IQueueReceiveService
    {
        Task<bool> SendTestAsync(TestRabbitModel input);

        void TestRabbit();
    }
}