using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Transflo.Assessment.Api.Responses;
using Transflo.Assessment.Shared.Enums;
using Transflo.Assessment.Shared.Models;

namespace Transflo.Assessment.Api.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>() ?? throw new NotImplementedException("couldn't resolve mediatR in base controller");
        protected IActionResult GetAPIResponse<T>(ServiceResponse<T> result) where T : class
        {
            var response = new ApiResponse<T>(result);
            return result.ServiceResponseStatus switch
            {
                ServiceResponseStatus.Success => Ok(new SuccesApiResponse<T>(result)),
                ServiceResponseStatus.ValidationError => BadRequest(new FailedWithValidationErrorApiResponse<T>(result)),
                ServiceResponseStatus.TechnicalError => StatusCode((int)HttpStatusCode.InternalServerError, response),
                _ => BadRequest(response)
            };
        }
        protected async Task<T> Send<T>(IRequest<T> r)
        {
            return await Mediator.Send(r);
        }
    }
}