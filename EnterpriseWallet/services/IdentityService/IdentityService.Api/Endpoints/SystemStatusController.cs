using Microsoft.AspNetCore.Mvc;

namespace EnterpriseWallet.IdentityService.Api.Endpoints;

[ApiController]
[Route("api/[controller]")]
public class SystemStatusController : ControllerBase
{
    [HttpGet("version")]
    public IActionResult GetVersion()
    {
        return Ok(new
        {
            پیام = "سرویس هویت در دسترس است",
            نسخه = "۱.۰.۰"
        });
    }
}
