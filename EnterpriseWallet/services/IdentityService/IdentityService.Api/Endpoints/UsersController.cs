using EnterpriseWallet.IdentityService.Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseWallet.IdentityService.Api.Endpoints;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccessful)
        {
            return Conflict(new { پیام = result.Message });
        }

        return CreatedAtAction(nameof(GetById), new { id = result.UserId }, new { پیام = "کاربر با موفقیت ایجاد شد", شناسه = result.UserId });
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(new { پیام = "این نقطه به زودی پیاده‌سازی می‌شود", شناسه = id });
    }
}
