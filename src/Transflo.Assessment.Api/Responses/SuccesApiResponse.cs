using Transflo.Assessment.Shared.Models;

namespace Transflo.Assessment.Api.Responses
{
    public class SuccesApiResponse<T> : ApiResponse<T>
    {
        public SuccesApiResponse(ServiceResponse<T> serviceResponse)
        {
            Data = serviceResponse.Data;
            Errors = serviceResponse.Errors;

        }
    }
}
