using Connect.Domain.Extensions;
using Microsoft.Azure.Cosmos.Table;

namespace Connect.Domain
{
    public class Event : TableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int GroupSize { get; set; }
        public decimal Cost { get; set; }


        public static Event AddEvent(
            Guid clubId,
            string name,
            string description,
            DateTimeOffset startDate,
            DateTimeOffset endDate,
            int groupSize,
            decimal cost)
        {
            return new Event()
            {
                PartitionKey = clubId.GuidToStringKey(),
                RowKey = Guid.NewGuid().GuidToStringKey(),
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                GroupSize = groupSize,
                Cost = cost
            };
        }
    }
}