using MediatR;
using Transflo.Assessment.Core.Domain;
using Transflo.Assessment.Core.Interfaces.Repositories;
using Transflo.Assessment.Shared.Models;

namespace Transflo.Assessment.Core.Features.Drivers.Create
{
    public class CreateDriverCommand : IRequest<ServiceResponse<CreateDriverCommandResponse>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

    }
    internal class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, ServiceResponse<CreateDriverCommandResponse>>
    {
        private readonly IGenericRepository<Driver> _repository;

        public CreateDriverCommandHandler(IGenericRepository<Driver> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<CreateDriverCommandResponse>> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {
            Driver? driver = await _repository.AddAsync(Driver.Create(request.FirstName, request.LastName, request.PhoneNumber, request.EmailAddress));
            await _repository.SaveChangesAsync();
            return new SuccessServiceResponse<CreateDriverCommandResponse>(new CreateDriverCommandResponse() { Id = driver.Id });
        }
    }

}
