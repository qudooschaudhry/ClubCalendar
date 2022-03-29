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
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetClubEvents(CancellationToken cancellationToken)
        {
            await Task.Delay(0, cancellationToken);

            var response = await _mediator.Send(new GetEventsByClubQuery() { ClubId = new Guid("635bf54c-e5ba-4d79-808c-da7a982ec396") }, cancellationToken );

            return new JsonResult(response.Events);
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}
