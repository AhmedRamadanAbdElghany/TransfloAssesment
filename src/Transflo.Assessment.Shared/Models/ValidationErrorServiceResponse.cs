using Transflo.Assessment.Shared.Enums;

namespace Transflo.Assessment.Shared.Models
{
    public class ValidationErrorServiceResponse<T> : ServiceResponse<T>
    {
        public ValidationErrorServiceResponse(List<Error> errors)
        {
            ServiceResponseStatus = ServiceResponseStatus.ValidationError;
            Errors = errors;
        }
        public ValidationErrorServiceResponse(Error error)
        {
            ServiceResponseStatus = ServiceResponseStatus.ValidationError;
            Errors = new List<Error>() { error };
        }
        public ValidationErrorServiceResponse(string error)
        {
            ServiceResponseStatus = ServiceResponseStatus.ValidationError;
            Errors = new List<Error>() { new Error
            {
                Code= (int) ServiceResponseStatus.ValidationError,
                Message= error
        }
            };
        }
    }
}
