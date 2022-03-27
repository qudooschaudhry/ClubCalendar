using Connect.Domain.Extensions;
using Connect.Infrastructure.Extensions;
using Connect.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.API.Clubs
{
    public class GetClubsQuery : IRequest<GetClubsQueryResponse>
    {
        public Guid CorrelationId { get; set; } = Guid.NewGuid();
    }

    public class GetClubsQueryResponse
    {
        public List<ClubDto> Clubs { get; set; } = new List<ClubDto>();

        public record ClubDto(Guid Id, string Name, string Description);
    }

    public class GetClubsQueryHandler : IRequestHandler<GetClubsQuery, GetClubsQueryResponse>
    {
        private readonly ILogger<GetClubsQueryHandler> _logger;
        private readonly IClubRepository _clubRepository;

        public GetClubsQueryHandler(
            ILogger<GetClubsQueryHandler> logger, 
            IClubRepository clubRepository)
        {
            _logger = logger.ThrowIfNull(nameof(logger));
            _clubRepository = clubRepository.ThrowIfNull(nameof(clubRepository));
        }

        public async Task<GetClubsQueryResponse> Handle(GetClubsQuery request, CancellationToken cancellationToken)
        {
            var clubs = await _clubRepository.GetAllAsync(request.CorrelationId, cancellationToken);

            return new GetClubsQueryResponse
            {
                Clubs = clubs.Select(c => new GetClubsQueryResponse.ClubDto(c.RowKey.StringToGuidKey(), c.Name, c.Description)).ToList()
            };
        }
    }
}
