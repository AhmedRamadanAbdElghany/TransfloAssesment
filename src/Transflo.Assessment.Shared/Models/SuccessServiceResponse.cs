using Transflo.Assessment.Shared.Enums;

namespace Transflo.Assessment.Shared.Models
{
    public class SuccessServiceResponse<T> : ServiceResponse<T>
    {
        public SuccessServiceResponse(T data)
        {
            ServiceResponseStatus = ServiceResponseStatus.Success;
            Data = data;
        }
    }
}
