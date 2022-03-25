using Microsoft.Azure.Cosmos.Table;

namespace Connect.Domain
{
    public class Event : TableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int Capacity { get; set; }
        public List<Registration> Registrations { get; set; } = new List<Registration>();
    }
}