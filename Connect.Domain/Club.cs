using Connect.Domain.Extensions;
using Microsoft.Azure.Cosmos.Table;

namespace Connect.Domain
{
    public class Club : TableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public static Club AddNew(string name, string description)
        {
            
            return new Club()
            {
                PartitionKey = "Connect",
                RowKey = Guid.NewGuid().GuidToStringKey(),
                Name = name,
                Description = description
            };
        }
    }


}