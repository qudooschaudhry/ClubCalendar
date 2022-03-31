using Connect.Domain.Extensions;
using Microsoft.Azure.Cosmos.Table;

namespace Connect.Domain
{
    public class Registration : TableEntity
    {
        public string ClubId { get; set; }
        public string EventId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MemberCode { get; set; } = string.Empty;


        public static Registration Register(
            Guid clubId,
            Guid eventId,
            string firstName,
            string lastName,
            string email,
            string memberCode)
        {
            var registration = new Registration()
            {
                ClubId = clubId.GuidToStringKey(),
                EventId = eventId.GuidToStringKey(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                MemberCode = memberCode
            };

            registration.PartitionKey = clubId.GuidToStringKey();
            registration.RowKey = Guid.NewGuid().GuidToStringKey();

            return registration;
        }
    }
}