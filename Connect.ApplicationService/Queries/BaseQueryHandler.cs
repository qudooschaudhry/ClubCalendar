using Connect.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Connect.ApplicationService.Queries
{
    public abstract class BaseQueryHandler
    {
        protected readonly ConnectContext Context;
        public BaseQueryHandler(ConnectContext context)
        {
            Context = context;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}
