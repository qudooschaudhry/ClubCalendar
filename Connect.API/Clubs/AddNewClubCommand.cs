using Connect.Domain;
using Connect.Infrastructure.Repositories;
using MediatR;

namespace Connect.API.Clubs
{
    public class AddNewClubCommand : IRequest<AddNewClubCommandResponse>
    {
        public Guid CorrelationId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class AddNewClubCommandResponse
    {
        public Guid Id { get; set; }
    }

    public class AddNewClubCommandHandler : IRequestHandler<AddNewClubCommand, AddNewClubCommandResponse>
    {
        private readonly IClubRepository _clubRepository;

        public AddNewClubCommandHandler(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository ?? throw new ArgumentNullException(nameof(clubRepository));
        }

        public async Task<AddNewClubCommandResponse> Handle(AddNewClubCommand request, CancellationToken cancellationToken)
        {
            var club = Club.AddNew(request.Name, request.Description);

            await _clubRepository.AddAsync(request.CorrelationId, club);

            return new AddNewClubCommandResponse();
        }
    }
}
