using Connect.Domain;
using Connect.Domain.Extensions;
using Connect.Infrastructure.Repositories;
using MediatR;

namespace Connect.API.Events
{
    public class AddNewEventCommand : IRequest<AddNewEventCommandResponse>
    {
        public Guid CorrelationId { get; set; } = Guid.NewGuid();
        public Guid ClubId { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GroupSize { get; set; }
        public decimal Cost { get; set; }
    }

    public class AddNewEventCommandResponse
    {
        public Guid Id { get; set; }
    }

    public class AddNewEventCommandHandler : IRequestHandler<AddNewEventCommand, AddNewEventCommandResponse>
    {
        private readonly ILogger<AddNewEventCommandHandler> _logger;
        private readonly IEventRepository _eventRepository;

        public AddNewEventCommandHandler(
            ILogger<AddNewEventCommandHandler> logger,
            IEventRepository eventRepository)
        {
            _logger = logger;
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        public async Task<AddNewEventCommandResponse> Handle(AddNewEventCommand request, CancellationToken cancellationToken)
        {
            var @event 
                = Event.AddEvent(
                    request.ClubId, 
                    request.Name, 
                    request.Description, 
                    request.StartDate, 
                    request.EndDate, 
                    request.GroupSize,
                    request.Cost);

            await _eventRepository.AddAsync(request.CorrelationId, @event, cancellationToken);

            return new AddNewEventCommandResponse()
            { 
                Id = @event.RowKey.StringToGuidKey()
            };
        }
    }
}
