using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Auth;

namespace QueueUpYourMessages
{
    public partial class Form1 : Form
    {
        //string storageAccountName = "augappdiag253";
        //string accountAccountKey = "aArrnNVENF5D/9oK3R5FfAWCINp2+eenDpdIkxOje/SJy0i4gr8KnYySuBKsEcER7W5jwUpXsG8eTwve/7Yn5w==";
        CloudStorageAccount account = new CloudStorageAccount(new StorageCredentials("augappdiag253","6w6ranWlhfOyWrorrZIUGLQ6KmECvJZNIEkVSpVS9A0jIW9FOw7EPvNq8pSk1n3gMrFaz3O2KeJJs4lbSmyahQ=="), useHttps: true);
        
        public Form1()
        {
            InitializeComponent();
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CloudQueueClient cloudQueueClient = account.CreateCloudQueueClient();
            string queueName = txtQueue.Text;
            CloudQueue azureQueue = cloudQueueClient.GetQueueReference(queueName);
            azureQueue.CreateIfNotExists();
            MessageBox.Show("Azure Queue created");

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CloudQueueClient cloudQueueClient = account.CreateCloudQueueClient();
            string message = txtMessage.Text;
            CloudQueueMessage cloudQueueMessage = new CloudQueueMessage(message);
            CloudQueue azureQueue = cloudQueueClient.GetQueueReference("queue1");
            azureQueue.AddMessage(cloudQueueMessage);
            txtMessage.Text = "";
        }

        private void btnMessages_Click(object sender, EventArgs e)
        {
            CloudQueueClient cloudQueueClient = account.CreateCloudQueueClient();
            CloudQueue azureQueue = cloudQueueClient.GetQueueReference("queue1");
            var messages = azureQueue.GetMessages(10);
            foreach (var message in messages)
            {
                lstMessage.Items.Add(message.AsString);
            }
        }
    }
    
}
