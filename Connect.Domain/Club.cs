using Microsoft.Azure.Cosmos.Table;

namespace Connect.Domain
{
    public class Club : TableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public static Club AddNew(string name, string description)
        {
            return new Club()
            {
                PartitionKey = "Connect",
                RowKey = $"{Guid.NewGuid()}",
                Name = name,
                Description = description
            };
        }

    }


}