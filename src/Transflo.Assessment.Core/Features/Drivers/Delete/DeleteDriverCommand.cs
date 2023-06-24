using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transflo.Assessment.Core.Domain;
using Transflo.Assessment.Core.Interfaces.Repositories;
using Transflo.Assessment.Shared.Models;

namespace Transflo.Assessment.Core.Features.Drivers.Delete
{
    public class DeleteDriverCommand : IRequest<ServiceResponse<DeleteDriverCommandResponse>>
    {
        [FromRoute(Name = "id")]
        public int Id { get; set; }
    }
    internal class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, ServiceResponse<DeleteDriverCommandResponse>>
    {
        private readonly IGenericRepository<Driver> _repository;

        public DeleteDriverCommandHandler(IGenericRepository<Driver> repository)
        {
            _repository = repository;
        }
        public async Task<ServiceResponse<DeleteDriverCommandResponse>> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
        {
            Driver? driver = await _repository.GetAsync(drv => drv.Id == request.Id);
            if (driver == null)
            {
                return new ValidationErrorServiceResponse<DeleteDriverCommandResponse>("Driver not found");
            }

            _repository.Delete(driver);
            await _repository.SaveChangesAsync();
            //TODO: use factory or Auto mapper
            return new SuccessServiceResponse<DeleteDriverCommandResponse>(new DeleteDriverCommandResponse()
            {
                Id = driver.Id
            });
        }
    }
}
