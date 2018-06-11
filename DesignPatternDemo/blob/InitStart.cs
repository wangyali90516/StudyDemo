using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DesignPatternDemo.blob
{
    public class InitStart
    {
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <returns></returns>
        public static bool DeleteFile()
        {
            CloudBlob cloudBlob = GetContainer().GetBlobReference("a.txt");
            if (cloudBlob.Exists())
            {
                cloudBlob.Delete();
            }
            return true;
        }

        public static bool DownLoad()
        {
            CloudBlob cloudBlob = GetContainer().GetBlobReference("bbb.txt");
            //// 把文件保存到本地。
            cloudBlob.DownloadToFileAsync("D:\\aa\\bbb.txt", FileMode.Create).GetAwaiter().GetResult();

            //Download
            using (var stream = new MemoryStream())
            {
                cloudBlob.DownloadToStreamAsync(stream).GetAwaiter().GetResult();
            }
            return true;
        }

        public static List<string> GetBlobList()
        {
            List<string> blobList = new List<string>();
            // 输出文件大小与路径uri
            foreach (IListBlobItem item in GetContainer().ListBlobs())
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    blobList.Add(blob.Name);
                    //r += string.Format("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);
                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;
                    blobList.Add(pageBlob.Name);
                    // r += string.Format("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;

                    // r += string.Format("Directory: {0}", directory.Uri);
                }
            }
            return blobList;
        }

        public static CloudBlobContainer GetContainer()
        {
            //注意连接字符串中的xxx和yyy，分别对应Access keys中的Storage account name 和 key。
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("BlobEndpoint=https://jymstoredev.blob.core.chinacloudapi.cn/;QueueEndpoint=https://jymstoredev.queue.core.chinacloudapi.cn/;TableEndpoint=https://jymstoredev.table.core.chinacloudapi.cn/;AccountName=jymstoredev;AccountKey=1dCLRLeIeUlLAIBsS9rYdCyFg3UNU239MkwzNOj3BYbREOlnBmM4kfTPrgvKDhSmh6sRp2MdkEYJTv4Ht3fCcg==");

            //CloudBlobClient 类是 Windows Azure Blob Service 客户端的逻辑表示，我们需要使用它来配置和执行对 Blob Storage 的操作。
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            //CloudBlobContainer 表示一个 Blob Container 对象。
            CloudBlobContainer container = cloudBlobClient.GetContainerReference("testblob");

            //如果不存在就创建名为 picturecontainer 的 Blob Container。
            container.CreateIfNotExists();
            //container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            return container;
        }

        //public static void Main()
        //{
        //    //上传
        //    //Upload();

        //    //获取blob下的信息
        //    var blobList = GetBlobList();
        //    foreach (var item in blobList)
        //    {
        //        Console.WriteLine(item);
        //    }

        //    //删除文件
        //    DeleteFile();

        //    //下载文件
        //    DownLoad();
        //    Console.ReadKey();
        //}

        public static bool Upload()
        {
            //上传
            CloudBlockBlob blockBlob = GetContainer().GetBlockBlobReference("bbb.txt");
            string file = "D:\\bbb.txt";
            using (var fileStream = File.OpenRead(file))
            {
                // 这是一个同步执行的方法
                blockBlob.UploadFromStream(fileStream);
            }
            return true;
        }
    }
}