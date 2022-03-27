using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Connect.API.Clubs.GetClubsQueryResponse;

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
        public async Task<List<ClubDto>> Get(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetClubsQuery());

            return response.Clubs;
        }

        [HttpPost]
        public async Task<Guid> Post(AddNewClubCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            return response.Id;
        }
    }
}
