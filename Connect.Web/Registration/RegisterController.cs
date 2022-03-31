using Connect.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Connect.Web.Registration
{
    public class RegisterController : Controller
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly IMediator _mediator;

        public RegisterController(ILogger<RegisterController> logger, IMediator mediator)
        {
            _logger = logger.ThrowIfNull(nameof(logger));
            _mediator = mediator.ThrowIfNull(nameof(mediator));
        }



        [HttpGet("/register/{id}")]
        public async Task<IActionResult> Index(Guid id, CancellationToken cancellationToken)
        {
            await Task.Delay(0, cancellationToken);
            return View();
        }

        [HttpPost("/register/{id}")]
        public async Task<IActionResult> Index(Guid id, RegisterForEventCommand command, CancellationToken cancellationToken)
        {
            command.ClubId = new Guid("635bf54c-e5ba-4d79-808c-da7a982ec396");
            command.EventId = id;
            

            await _mediator.Send(command, cancellationToken);

            return View(command);
        }
    }

    
}
