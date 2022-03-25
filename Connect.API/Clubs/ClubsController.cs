using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Connect.API.Clubs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase
    {
        private readonly ILogger<ClubsController> _logger;
        private readonly IMediator _mediator;
        public ClubsController(
            ILogger<ClubsController> logger, 
            IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<string> Get(CancellationToken cancellationToken)
        {
            await Task.Delay(0, cancellationToken);

            //await _mediator.Send(new AddNewClubCommand()
            //{
            //    Name = "Alpine Club of Canada - Ottawa Chapter",
            //    Description = "A section of Alpine club of Canada"
            //});

            return "string.e";
        }
    }
}
