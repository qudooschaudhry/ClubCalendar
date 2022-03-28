using Connect.Domain.Extensions;
using Connect.Infrastructure.Repositories;
using MediatR;

namespace Connect.Web.Pages.Events
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
            string Name,
            string Description,
            DateTimeOffset StartDate,
            DateTimeOffset EndDate,
            int GroupSize,
            decimal Cost)
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
            _logger = logger;
            _eventRepository = eventRepository;
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
                    e.Description,
                    e.StartDate.Date,
                    e.EndDate.Date,
                    e.GroupSize,
                    e.Cost)).ToList()
            };

        }
    }
}
