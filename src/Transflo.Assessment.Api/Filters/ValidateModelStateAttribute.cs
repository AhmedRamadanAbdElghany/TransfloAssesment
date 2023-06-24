using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Transflo.Assessment.Api.Responses;
using Transflo.Assessment.Shared.Enums;
using Transflo.Assessment.Shared.Models;

namespace Transflo.Assessment.Api.Filters
{
    internal class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(x => x.Errors.Count > 0).SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                var response = GetServiceValidationReponse(context, errors);
                context.Result = new JsonResult(response)
                {
                    StatusCode = 400
                };
            }

        }
        private static FailedWithValidationErrorApiResponse<string> GetServiceValidationReponse(ActionExecutingContext context, List<string> errors)
        {
            if (errors.Any(x => x.ToLower().Contains("json") || x.ToLower().Contains("command") || x.ToLower().Contains("sing character") || x.ToLower().Contains("BytePositionInLine".ToLower())))
            {
                var castingError = new List<Error>() { new Error() { Code = (int)ServiceResponseStatus.ValidationError, Message = "Invalid Casting" } };
                return new FailedWithValidationErrorApiResponse<string>(castingError);
            }
            var customErrors = context.ModelState.Values.Where(x => x.Errors.Count > 0).SelectMany(x => x.Errors).Select(x =>
            new Error
            {
                Message = x.ErrorMessage,
                Code = (int)ServiceResponseStatus.ValidationError
            }).ToList();
            return new FailedWithValidationErrorApiResponse<string>(customErrors);
        }
    }
}
