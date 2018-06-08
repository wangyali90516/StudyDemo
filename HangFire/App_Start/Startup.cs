using System;
using System.Configuration;
using Hangfire;
using Hangfire.SqlServer;
using HangFire.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace HangFire.App_Start
{
    public class Startup : JYMNLogger
    {
        public void Configuration(IAppBuilder app)
        {
            //配置轮询间隔
            SqlServerStorageOptions options = new SqlServerStorageOptions
            {
                QueuePollInterval = TimeSpan.FromSeconds(15) //队列
                //TransactionTimeout = TimeSpan.FromMinutes(30) //超时时间
            };

            //指定hangfire使用数据库存储后台任务执行
            GlobalConfiguration.Configuration.UseSqlServerStorage(ConfigurationManager.AppSettings["EfConnectString"], options);

            //启用hangfireserver中间件（它会自动释放）
            BackgroundJobServerOptions backgroundOptions = new BackgroundJobServerOptions { WorkerCount = Environment.ProcessorCount * 10 }; //设置处理的并发数
            app.UseHangfireServer(backgroundOptions);

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                AuthorizationFilters = new[] { new IpAuthorizationFilter() }  //设置能访问的IP
            });
            //启用Hangfire的仪表盘（可以看到任务状态，进度等信息）
            app.UseHangfireDashboard();

            //设置定时的任务
            RecurringJob.AddOrUpdate("OnTimeTimerTest", () => this.SetTestRegisterJobs(), "* */6 * * *",
                TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));
        }

        public bool OnTimeTestRegister()
        {
            this.Logger.Info("已成功处理");
            return true;
        }

        public void SetTestRegisterJobs()
        {
            BackgroundJob.Schedule(() => this.OnTimeTestRegister(), TimeSpan.FromSeconds(5));
        }
    }
}