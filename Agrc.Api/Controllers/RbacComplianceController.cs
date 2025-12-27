using Agrc.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agrc.Api.Controllers
{
    [ApiController]
    [Route("api/compliance/rbac")]
    public class RbacComplianceController : ControllerBase
    {
        private readonly RbacComplianceService _service;

        public RbacComplianceController(RbacComplianceService service)
        {
            _service = service;
        }

        [HttpGet("{appId}")]
        public async Task<IActionResult> Check(string appId)
        {
            var result = await _service.EvaluateAsync(appId);
            return Ok(result);
        }
    }

}
