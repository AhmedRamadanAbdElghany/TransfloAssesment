using Microsoft.AspNetCore.Mvc;
using Transflo.Assessment.Core.Features.Drivers.Create;
using Transflo.Assessment.Core.Features.Drivers.Delete;
using Transflo.Assessment.Core.Features.Drivers.GetDriverById;
using Transflo.Assessment.Core.Features.Drivers.Update;

namespace Transflo.Assessment.Api.Controllers
{
    public class DriverController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDriver(GetDriverByIdQuery request)
        {
            return GetAPIResponse(await Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDriver([FromBody] CreateDriverCommand request)
        {
            return GetAPIResponse(await Send(request));
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateDriver([FromRoute(Name = "id")] int id, [FromBody] UpdateDriverCommand request)
        {
            request.SetId(id);
            return GetAPIResponse(await Send(request));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDriver(DeleteDriverCommand request)
        {
            return GetAPIResponse(await Send(request));
        }
    }
}