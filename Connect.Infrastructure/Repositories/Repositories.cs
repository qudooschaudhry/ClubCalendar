// Copyright (C) Julipur Software Inc. - All Rights Reserved
// Unauthorized copying or re-use of this file or any portion thereof via any medium without permission from Julipur Software Inc. is strictly prohibited
// Proprietary and confidential
// THIS CODE WAS GENERATED, DO NOT EDIT.

using Connect.Domain;
using Microsoft.Extensions.Logging;
using Connect.Infrastructure.Configuration;

namespace Connect.Infrastructure.Repositories
{
	public partial interface IClubRepository : ITableStorageRepository<Club> {}
	public partial class ClubRepository : TableStorageRepositoryBase<Club>, IClubRepository 
	{
		public ClubRepository(
            IApplicationSettings applicationSettings, 
            ILogger<ClubRepository> logger) 
            : base(logger, applicationSettings, "Clubs")
        {
        }
	}	
	public partial interface IEventRepository : ITableStorageRepository<Event> {}
	public partial class EventRepository : TableStorageRepositoryBase<Event>, IEventRepository 
	{
		public EventRepository(
            IApplicationSettings applicationSettings, 
            ILogger<EventRepository> logger) 
            : base(logger, applicationSettings, "Events")
        {
        }
	}
    public partial interface IRegistrationRepository : ITableStorageRepository<Registration>
    {
    }
    public partial class RegistrationRepository : TableStorageRepositoryBase<Registration>, IRegistrationRepository 
	{
		public RegistrationRepository(
            IApplicationSettings applicationSettings, 
            ILogger<RegistrationRepository> logger) 
            : base(logger, applicationSettings, "Registrations")
        {
        }
	}	
}
