using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Connect.API.Events.GetEventsByClubQueryResponse;

namespace Connect.API.Events
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IMediator _mediator;

        public EventsController(ILogger<EventsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        [HttpGet("/clubs/events/{clubId}")]
        public async Task<List<EventDto>> Get(Guid clubId)
        {
            var response = await _mediator.Send(new GetEventsByClubQuery() { ClubId = clubId});

            return response.Events;
        }

        [HttpPost]
        public async Task<Guid> Post(AddNewEventCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            return response.Id;
        }
        
        
    }
}
