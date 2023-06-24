using MediatR;
using Transflo.Assessment.Core.Domain;
using Transflo.Assessment.Core.Interfaces.Repositories;
using Transflo.Assessment.Shared.Models;

namespace Transflo.Assessment.Core.Features.Drivers.Update
{
    public class UpdateDriverCommand : IRequest<ServiceResponse<UpdateDriverCommandResponse>>
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public void SetId(int id)
        {
            Id = id;
        }
    }
    internal class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, ServiceResponse<UpdateDriverCommandResponse>>
    {
        private readonly IGenericRepository<Driver> _repository;

        public UpdateDriverCommandHandler(IGenericRepository<Driver> repository)
        {
            _repository = repository;
        }
        public async Task<ServiceResponse<UpdateDriverCommandResponse>> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            Driver? driver = await _repository.GetAsync(drv => drv.Id == request.Id);
            if (driver == null)
            {
                return new ValidationErrorServiceResponse<UpdateDriverCommandResponse>("Driver not found");
            }

            _repository.Update(Driver.Update(driver, request.FirstName, request.LastName, request.PhoneNumber, request.EmailAddress));
            await _repository.SaveChangesAsync();

            //TODO: use factory or Auto mapper
            return new SuccessServiceResponse<UpdateDriverCommandResponse>(new UpdateDriverCommandResponse()
            {
                EmailAddress = driver.EmailAddress,
                FirstName = driver.FirstName,
                Id = driver.Id,
                LastName = driver.LastName,
                PhoneNumber = driver.PhoneNumber
            });
        }
    }
}
