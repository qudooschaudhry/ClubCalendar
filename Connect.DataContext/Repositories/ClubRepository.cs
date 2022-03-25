using Connect.Domain;
using Connect.Infrastructure.Configuration;
using Microsoft.Extensions.Logging;

namespace Connect.Infrastructure.Repositories
{
    public interface IClubRepository : ITableStorageRepository<Club>
    {
    }
    public class ClubRepository : TableStorageRepositoryBase<Club>, IClubRepository
    {
        public ClubRepository(
            ILogger<ClubRepository> logger,
            IApplicationSettings applicationSettings) : base(logger, applicationSettings, "Clubs")
        {
        }
    }
}
