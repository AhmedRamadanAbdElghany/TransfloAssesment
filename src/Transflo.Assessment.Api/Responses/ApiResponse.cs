using Transflo.Assessment.Shared.Models;

namespace Transflo.Assessment.Api.Responses
{
    public class ApiResponse<T>
    {
        public ApiResponse() { }
        public ApiResponse(ServiceResponse<T> serviceResponse)
        {
            Data = serviceResponse.Data;
            Errors = serviceResponse.Errors;

        }
        public T Data { get; set; }
        public List<Error> Errors { get; set; }
    }
}
