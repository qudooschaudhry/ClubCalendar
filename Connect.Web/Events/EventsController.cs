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

        public IActionResult Index()
        {
            return View();
        }
    }
}
