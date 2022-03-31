using Connect.Domain.Extensions;
using Connect.Infrastructure.Extensions;
using Connect.Infrastructure.Repositories;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Connect.Web.Registration
{
    public class RegisterForEventCommand : IRequest<RegisterForEventCommandResponse>
    {
        public Guid CorrelationId { get; internal set; } = Guid.NewGuid();
        public Guid ClubId { get; set; }
        public Guid EventId { get; set; }
        [Display(Name = "First Name"), Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "Last Name"), Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = string.Empty;
        [Display(Name = "Email"), Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;
        [Display(Name = "Member Code"), Required(ErrorMessage = "Member code is required")]
        public string MemberCode { get; set; } = string.Empty;
    }

    public class RegisterForEventCommandResponse
    { 
        public Guid RegistrationId { get; set; }
    }

    public class RegisterForMemberCommandHandler : IRequestHandler<RegisterForEventCommand, RegisterForEventCommandResponse>
    {
        private readonly ILogger<RegisterForMemberCommandHandler> _logger;
        private readonly IRegistrationRepository _registrationRepository;

        public RegisterForMemberCommandHandler(
            ILogger<RegisterForMemberCommandHandler> logger,
            IRegistrationRepository registrationRepository)
        {
            _logger = logger.ThrowIfNull(nameof(logger));
            _registrationRepository = registrationRepository.ThrowIfNull(nameof(registrationRepository));
        }

        public async Task<RegisterForEventCommandResponse> Handle(RegisterForEventCommand request, CancellationToken cancellationToken)
        {
            var registration
                = Domain.Registration.Register(
                    request.ClubId, 
                    request.EventId, 
                    request.FirstName, 
                    request.LastName, 
                    request.Email, 
                    request.MemberCode);

            await _registrationRepository.AddAsync(request.CorrelationId, registration, cancellationToken);

            return new RegisterForEventCommandResponse()
            {
                RegistrationId = registration.RowKey.StringToGuidKey()
            };
        }
    }
}
