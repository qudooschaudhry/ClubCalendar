using Connect.Domain;
using Connect.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Infrastructure.Repositories
{
    public interface IEventRepository : ITableStorageRepository<Event>
    { 
    }
    public class EventRepository : TableStorageRepositoryBase<Event>, IEventRepository
    {
        public EventRepository(
            ILogger<EventRepository> logger,
            IApplicationSettings applicationSettings) : base(logger, applicationSettings, "Events")
        {
        }
    }
}
