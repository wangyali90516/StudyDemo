using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace DesignPatternDemo.auzreTable
{
    public class SmsMessage : TableEntity
    {
        /// <summary>
        ///     Gets or sets the application identifier.
        /// </summary>
        /// <value>The application identifier.</value>
        public string AppId { get; set; }

        /// <summary>
        ///     手机号
        /// </summary>
        /// <value>The cellphones.</value>
        public string Cellphones { get; set; }

        /// <summary>
        ///     短信通道
        /// </summary>
        /// <value>The gateway.</value>
        public int Gateway { get; set; }

        /// <summary>
        ///     短信
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        /// <value>The notes.</value>
        public string Notes { get; set; }

        /// <summary>
        ///     响应信息
        /// </summary>
        /// <value>The response.</value>
        public string Response { get; set; }

        /// <summary>
        ///     发送时间
        /// </summary>
        /// <value>The time.</value>
        public DateTime Time { get; set; }
    }
}