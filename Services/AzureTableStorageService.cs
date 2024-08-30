using Microsoft.Azure.Cosmos.Table;
using POE_CLDV_6221_ST10224391.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POE_CLDV_6221_ST10224391.Services
{
    public class AzureTableStorageService
    {
        private readonly CloudTableClient _tableClient;
        private readonly CloudTable _customerTable;

        public AzureTableStorageService(string storageConnectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            _tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

            _customerTable = _tableClient.GetTableReference("CustomerProfiles");
            _customerTable.CreateIfNotExists();
        }

        public async Task InsertCustomerProfileAsync(CustomerProfile profile)
        {
            var insertOperation = TableOperation.Insert(profile);
            await _customerTable.ExecuteAsync(insertOperation);
        }

        public async Task<List<CustomerProfile>> GetCustomerProfilesAsync()
        {
            var query = new TableQuery<CustomerProfile>();
            var result = new List<CustomerProfile>();
            TableContinuationToken token = null;

            do
            {
                var queryResult = await _customerTable.ExecuteQuerySegmentedAsync(query, token);
                result.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            } while (token != null);

            return result;
        }
    }
}
