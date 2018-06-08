namespace DesignPatternDemo.autoFac
{
    public class TestService : ITestService
    {
        #region ITestService Members

        #region Implementation of ITestService

        public int Add(int i, int j)
        {
            return i + j;
        }

        #endregion Implementation of ITestService

        #endregion ITestService Members
    }
}