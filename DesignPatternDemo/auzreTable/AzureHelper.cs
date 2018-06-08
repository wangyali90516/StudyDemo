using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Microsoft.WindowsAzure.Storage.Table;
using Moe.Lib;

namespace DesignPatternDemo.auzreTable
{
    public class AzureHelper
    {
        public CloudTableClient CloudTableClient => this.InstCloudTableClient();

        public CloudStorageAccount StorageAccount => CloudStorageAccount.Parse("BlobEndpoint=https://jymstoredev.blob.core.chinacloudapi.cn/;QueueEndpoint=https://jymstoredev.queue.core.chinacloudapi.cn/;TableEndpoint=https://jymstoredev.table.core.chinacloudapi.cn/;AccountName=jymstoredev;AccountKey=1dCLRLeIeUlLAIBsS9rYdCyFg3UNU239MkwzNOj3BYbREOlnBmM4kfTPrgvKDhSmh6sRp2MdkEYJTv4Ht3fCcg==");

        /// <summary>
        /// </summary>
        /// <param name="cellPhone"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Task<List<SmsMessage>> GetSmsMessage(string cellPhone, int type)
        {
            AzureHelper azureHelper = new AzureHelper();
            //TableOperation retrieve = TableOperation.Retrieve<SmsMessage>(cellPhone, cellPhone);
            TableQuery<SmsMessage> query = new TableQuery<SmsMessage>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("Cellphones", QueryComparisons.Equal, cellPhone),
                    TableOperators.And,
                    TableQuery.GenerateFilterConditionForInt("Gateway", QueryComparisons.Equal, type)
                ));

            List<SmsMessage> retrieveResult = azureHelper.CloudTableClient.GetTableReference("Test").ExecuteQuery(query).ToList();
            return Task.FromResult(retrieveResult);
        }

        private CloudTableClient InstCloudTableClient()
        {
            CloudTableClient client = this.StorageAccount.CreateCloudTableClient();
            client.DefaultRequestOptions.RetryPolicy = new ExponentialRetry(new TimeSpan(0, 0, 5), 5);
            client.DefaultRequestOptions.MaximumExecutionTime = 2.Minutes();
            client.DefaultRequestOptions.ServerTimeout = 2.Minutes();
            return client;
        }
    }
}