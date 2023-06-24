using Transflo.Assessment.Shared.Models;

namespace Transflo.Assessment.Api.Responses
{
    public class FailedWithTechnicalErrorApiResponse<T> : ApiResponse<T>
    {
        public FailedWithTechnicalErrorApiResponse(List<Error> errors)
        {
            Errors = errors;
        }
    }
}
