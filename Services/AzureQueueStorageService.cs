using Azure.Storage.Queues;
using System.Threading.Tasks;

namespace POE_CLDV_6221_ST10224391.Services
{
    public class AzureQueueStorageService
    {
        private readonly QueueClient _queueClient;

        public AzureQueueStorageService(string storageConnectionString, string queueName)
        {
            _queueClient = new QueueClient(storageConnectionString, queueName);
            _queueClient.CreateIfNotExists();
        }

        public async Task AddMessageAsync(string message)
        {
            await _queueClient.SendMessageAsync(message);
        }

        public async Task<List<string>> ListMessagesAsync()
        {
            var messages = new List<string>();
            var queueMessages = await _queueClient.ReceiveMessagesAsync(maxMessages: 10);
            foreach (var msg in queueMessages.Value)
            {
                messages.Add(msg.MessageText);
            }
            return messages;
        }
    }
}
