using Transflo.Assessment.Shared.Enums;

namespace Transflo.Assessment.Shared.Models
{
    public class FailedWithTechnicalErrorServiceResponse<T> : ServiceResponse<T>
    {
        public FailedWithTechnicalErrorServiceResponse(string errorMessage)
        {
            Errors = new List<Error>() { new Error()
            {
                Code= (int)ServiceResponseStatus.TechnicalError,
                Message= errorMessage
            } };

            ServiceResponseStatus = ServiceResponseStatus.TechnicalError;
        }
        public FailedWithTechnicalErrorServiceResponse(Error error)
        {
            ServiceResponseStatus = ServiceResponseStatus.TechnicalError;
            Errors = new List<Error>() { error };
        }
    }
}
