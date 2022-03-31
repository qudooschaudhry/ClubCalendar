using Connect.Domain.Extensions;
using Connect.Infrastructure.Extensions;
using Connect.Infrastructure.Repositories;
using MediatR;

namespace Connect.Web.Events

{
    public class GetEventsByClubQuery : IRequest<GetEventsByClubQueryResponse>
    {
        public Guid CorrelationId { get; set; } = Guid.NewGuid();
        public Guid ClubId { get; set; }
    }

    public class GetEventsByClubQueryResponse
    {
        public List<EventDto> Events { get; set; } = new List<EventDto>();
        public record EventDto(
            Guid Id,
            string Title,
            ExtendedProperties ExtendedProps,
            DateTimeOffset Start,
            DateTimeOffset End)
        {
        }

        public record ExtendedProperties (
            string Description, 
            int groupSize, 
            decimal cost)
        {
        }
    }

    public class GetEventsByClubQueryHandler : IRequestHandler<GetEventsByClubQuery, GetEventsByClubQueryResponse>
    {
        private readonly ILogger<GetEventsByClubQueryHandler> _logger;
        private readonly IEventRepository _eventRepository;

        public GetEventsByClubQueryHandler(
            ILogger<GetEventsByClubQueryHandler> logger,
            IEventRepository eventRepository)
        {
            _logger = logger.ThrowIfNull(nameof(logger));
            _eventRepository = eventRepository.ThrowIfNull(nameof(eventRepository));
        }


        public async Task<GetEventsByClubQueryResponse> Handle(GetEventsByClubQuery request, CancellationToken cancellationToken)
        {
            var events = await _eventRepository.GetAllAsync(request.CorrelationId, request.ClubId.GuidToStringKey(), cancellationToken);

            return new GetEventsByClubQueryResponse()
            {
                Events = events.Select(e =>
                new GetEventsByClubQueryResponse.EventDto(
                    e.RowKey.StringToGuidKey(),
                    e.Name,
                    new GetEventsByClubQueryResponse.ExtendedProperties(
                        e.Description,
                        e.GroupSize,
                        e.Cost),
                    e.StartDate.Date,
                    e.EndDate.Date)).ToList()
            };

        }
    }
}
