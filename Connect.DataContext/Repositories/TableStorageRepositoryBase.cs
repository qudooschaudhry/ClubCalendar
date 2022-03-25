using Connect.Infrastructure.Configuration;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Infrastructure.Repositories
{

    public interface ITableStorageRepository<T>
    {
        /// <summary>
        /// Adds a new entity to table storage
        /// </summary>
        /// <param name="correlationId">The correlation Id of the request</param>
        /// <param name="entity">The table storage entity</param>
        Task AddAsync(Guid correlationId, T entity);

        /// <summary>
        /// Saves an existing entity to table storage
        /// </summary>
        /// <param name="correlationId">The correlation Id of the request</param>
        /// <param name="entity">The table storage entity</param>
        Task SaveAsync(Guid correlationId, T entity);

        /// <summary>
        /// Deletes the entity
        /// </summary>
        /// <param name="correlationId">The correlation Id of the request</param>
        /// <param name="entity">The table storage entity</param>
        Task DeleteAsync(Guid correlationId, T entity);

        /// <summary>
        /// Gets the entity from Table Storage
        /// </summary>
        /// <param name="correlationId">The correlation Id of the request</param>
        /// <param name="partitionKey">The Partition Key of the entity</param>
        /// <param name="rowKey">The Row key of the entity</param>
        /// <returns>Entity</returns>
        Task<T> GetAsync(Guid correlationId, string partitionKey, string rowKey);

        /// <summary>
        /// Gets all the entities of a type
        /// </summary>
        /// <param name="correlationId">The correlation Id of the request</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(Guid correlationId);

        /// <summary>
        /// Gets all the entities of a type in a partition
        /// </summary>
        /// <param name="correlationId">The correlatin Id of the request</param>
        /// <param name="partitionKey">The partition key</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(Guid correlationId, string partitionKey, string startRowKey, string endRowKey);
    }

    public abstract class TableStorageRepositoryBase<T> : ITableStorageRepository<T>
        where T : TableEntity, new()
    {
        protected readonly IApplicationSettings _applicationSettings;
        protected readonly ILogger<TableStorageRepositoryBase<T>> _logger;
        protected readonly AzureStorageAccountContext _context;

        private readonly CloudTable _tableInstance;

        public TableStorageRepositoryBase(
            ILogger<TableStorageRepositoryBase<T>> logger,
            IApplicationSettings applicationSettings,
            string tableName)
        {
            _logger = logger;
            _applicationSettings = applicationSettings;

            _context = new AzureStorageAccountContext(logger, applicationSettings.BaseStorageAccount);

            var cloudTableClient = _context.StorageAccount.CreateCloudTableClient();

            _tableInstance = cloudTableClient.GetTableReference(tableName);
            _tableInstance.CreateIfNotExists();
        }

        public async Task AddAsync(Guid correlationId, T entity)
        {
            _logger.LogInformation($"{correlationId} - Adding entity to table storage");
            try
            {
                TableOperation insertOperation = TableOperation.InsertOrMerge(entity);
                await _tableInstance.ExecuteAsync(insertOperation);
                _logger.LogInformation($"{correlationId} - Successfully added table storage entity");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{correlationId} - Exception occurred while trying to add entity to table storage: {ex.Message}");
                throw;
            }
        }

        public async Task SaveAsync(Guid correlationId, T entity)
        {
            TableOperation mergeOpertion = TableOperation.Merge(entity);
            await _tableInstance.ExecuteAsync(mergeOpertion);
        }

        public Task DeleteAsync(Guid correlationId, T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Guid correlationId)
        {
            TableContinuationToken? token = null;
            var entities = new List<T>();
            do
            {
                var queryResult = await _tableInstance.ExecuteQuerySegmentedAsync(new TableQuery<T>(), token);
                entities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            } while (token != null);

            return entities;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Guid correlationId, string partitionKey, string startRowKey, string endRowKey)
        {
            TableContinuationToken? token = null;
            var entities = new List<T>();
            do
            {
                string pkFilter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey);
                string rkLowerFilter = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.GreaterThanOrEqual, startRowKey);
                string rkUpperFilter = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.LessThanOrEqual, endRowKey);

                // Note CombineFilters has the effect of “([Expression1]) Operator (Expression2]), as such passing in a complex expression will result in a logical grouping. 
                string combinedRowKeyFilter = TableQuery.CombineFilters(rkLowerFilter, TableOperators.And, rkUpperFilter);
                string combinedFilter = TableQuery.CombineFilters(pkFilter, TableOperators.And, combinedRowKeyFilter);

                var queryResult = await _tableInstance.ExecuteQuerySegmentedAsync(new TableQuery<T>().Where(combinedFilter), token);
                entities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            } while (token != null);

            return entities;
        }

        public async Task<T> GetAsync(Guid correlationId, string partitionKey, string rowKey)
        {
            T entity;
            _logger.LogInformation($"{correlationId} - Loading entity from table storage");
            try
            {
                TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);
                var result = await _tableInstance.ExecuteAsync(retrieveOperation);
                entity = (T)result.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{correlationId} - Exception occurred while trying to load entity from table storage: {ex.Message}");
                throw;
            }
            return entity;
        }
    }
}