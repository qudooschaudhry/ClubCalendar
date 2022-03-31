using Connect.Domain.Extensions;
using Connect.Infrastructure.Extensions;
using Connect.Infrastructure.Repositories;
using MediatR;

namespace Connect.Web.Events

{
    public class GetEventByIdQuery : IRequest<GetEventByIdQueryResponse>
    {
        public Guid CorrelationId { get; set; } = Guid.NewGuid();
        public Guid ClubId { get; set; }
        public Guid Id { get; set; }
    }

    public class GetEventByIdQueryResponse
    {
        public EventDto Event { get; set; }
        public record EventDto(
            Guid Id,
            string Title,
            string Description,
            DateTimeOffset Start,
            DateTimeOffset End,
            int spotsLeft)
        {
        }
    }

    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, GetEventByIdQueryResponse>
    {
        private readonly ILogger<GetEventByIdQueryHandler> _logger;
        private readonly IEventRepository _eventRepository;
        private readonly IRegistrationRepository _registrationRepository;

        public GetEventByIdQueryHandler(
            ILogger<GetEventByIdQueryHandler> logger,
            IEventRepository eventRepository, 
            IRegistrationRepository registrationRepository)
        {
            _logger = logger.ThrowIfNull(nameof(logger));
            _eventRepository = eventRepository.ThrowIfNull(nameof(eventRepository));
            _registrationRepository = registrationRepository.ThrowIfNull(nameof(registrationRepository));
        }


        public async Task<GetEventByIdQueryResponse> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var @event
                = await _eventRepository.GetAsync(
                    request.CorrelationId,
                    request.ClubId.GuidToStringKey(),
                    request.Id.GuidToStringKey(),
                    cancellationToken);

            //var registrations = await _registrationRepository.GetAsync(request.CorrelationId, request.ClubId)

            return new GetEventByIdQueryResponse()
            {
                Event
                = new GetEventByIdQueryResponse.EventDto(
                    @event.RowKey.StringToGuidKey(),
                    @event.Name,
                    @event.Description,
                    @event.StartDate.Date,
                    @event.EndDate.Date,
                    0)
            };
        }
    }
}
