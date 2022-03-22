using Connect.Infrastructure.DataContext;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.ApplicationService.Queries
{
    public class GetClubsQuery : IRequest<GetClubsQueryResponse>
    {
    }

    public class GetClubsQueryResponse
    {
        public List<ClubDto> Clubs { get; set; } = new List<ClubDto>();

        public record ClubDto(int Id, string Name, string Description);
    }

    public class GetClubsQueryHandler : BaseQueryHandler, IRequestHandler<GetClubsQuery, GetClubsQueryResponse>
    {
        private readonly ILogger<GetClubsQueryHandler> _logger;

        public GetClubsQueryHandler(ILogger<GetClubsQueryHandler> logger, ConnectContext context) : base(context)
        {
            _logger = logger;
        }

        public async Task<GetClubsQueryResponse> Handle(GetClubsQuery request, CancellationToken cancellationToken)
        {
            await Task.Delay(0, cancellationToken);

            var clubs = Context.Clubs.ToList();

            return new GetClubsQueryResponse 
            { 
                Clubs = clubs.Select(c => new GetClubsQueryResponse.ClubDto(c.Id, c.Name, c.Description)).ToList()
            };
        }
    }
}
