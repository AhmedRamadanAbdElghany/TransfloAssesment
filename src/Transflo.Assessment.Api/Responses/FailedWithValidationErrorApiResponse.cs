using Transflo.Assessment.Shared.Models;

namespace Transflo.Assessment.Api.Responses
{
    public class FailedWithValidationErrorApiResponse<T> : ApiResponse<T>
    {
        public FailedWithValidationErrorApiResponse(ServiceResponse<T> serviceResponse) : base(serviceResponse)
        {
            Errors = serviceResponse.Errors;
        }
        public FailedWithValidationErrorApiResponse(List<Error> errors) : base()
        {
            Errors = errors;
        }
    }
}
