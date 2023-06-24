using Transflo.Assessment.Shared.Enums;

namespace Transflo.Assessment.Shared.Models
{
    public class ServiceResponse<T>
    {
        public ServiceResponseStatus ServiceResponseStatus { get; set; }
        public T Data { get; set; }
        public List<Error> Errors { get; set; }
    }
}
