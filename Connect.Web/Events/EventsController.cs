using Connect.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Connect.Web.Events
{
    public class EventsController : Controller
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IMediator _mediator;

        public EventsController(ILogger<EventsController> logger, IMediator mediator)
        {
            _logger = logger.ThrowIfNull(nameof(logger));
            _mediator = mediator.ThrowIfNull(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            await Task.Delay(0, cancellationToken);
            return View();
        }

        [HttpGet("events/getbyid/{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetEventByIdQuery()
            {
                ClubId = new Guid("635bf54c-e5ba-4d79-808c-da7a982ec396"),
                Id = id
            }, cancellationToken);
            return new JsonResult(response.Event);
        }


        [HttpGet("events/getclubevents")]
        public async Task<IActionResult> GetClubEvents(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetEventsByClubQuery() { ClubId = new Guid("635bf54c-e5ba-4d79-808c-da7a982ec396") }, cancellationToken);

            return new JsonResult(response.Events);
        }

        
    }
}
