using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Logging;

namespace Connect.Infrastructure.Repositories
{
    public class AzureStorageAccountContext
    {
        public CloudStorageAccount StorageAccount { get; private set; }

        public AzureStorageAccountContext(
            ILogger logger,
            string storageAccount)
        {
            if (string.IsNullOrEmpty(storageAccount))
                throw new ArgumentException(storageAccount);

            StorageAccount = CloudStorageAccount.Parse(storageAccount);
        }
    }
}
