using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transflo.Assessment.Core.Domain;
using Transflo.Assessment.Core.Interfaces.Repositories;
using Transflo.Assessment.Shared.Models;

namespace Transflo.Assessment.Core.Features.Drivers.GetDriverById
{
    public class GetDriverByIdQuery : IRequest<ServiceResponse<GetDriverByIdQueryResponse>>
    {
        [FromRoute(Name = "id")]
        public int Id { get; set; }
    }
    internal class GetDriverByIdQueryHandler : IRequestHandler<GetDriverByIdQuery, ServiceResponse<GetDriverByIdQueryResponse>>
    {
        private readonly IReadonlyRepository<Driver> _repository;

        public GetDriverByIdQueryHandler(IReadonlyRepository<Driver> repository)
        {
            _repository = repository;
        }
        public async Task<ServiceResponse<GetDriverByIdQueryResponse>> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
        {
            Driver? driver = await _repository.GetAsync(drv => drv.Id == request.Id);
            if (driver == null)
            {
                return new ValidationErrorServiceResponse<GetDriverByIdQueryResponse>("Driver not found");
            }
            //TODO: use factory or Auto mapper
            return new SuccessServiceResponse<GetDriverByIdQueryResponse>(new GetDriverByIdQueryResponse()
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
