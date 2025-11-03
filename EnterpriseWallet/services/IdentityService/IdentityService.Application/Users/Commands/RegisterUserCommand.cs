using EnterpriseWallet.IdentityService.Application.Contracts;
using EnterpriseWallet.IdentityService.Domain.Entities;
using FluentValidation;
using MediatR;

namespace EnterpriseWallet.IdentityService.Application.Users.Commands;

public record RegisterUserCommand(string FullName, string Email, string Mobile, Guid TenantId) : IRequest<RegisterUserResult>;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("نام کامل الزامی است");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("ایمیل معتبر وارد کنید");
        RuleFor(x => x.Mobile).NotEmpty().WithMessage("شماره تلفن همراه الزامی است");
    }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    private readonly IIdentityUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IIdentityUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _unitOfWork.FindUserByEmailAsync(request.Email, cancellationToken);
        if (existingUser is not null)
        {
            return RegisterUserResult.Failure("کاربری با این ایمیل وجود دارد");
        }

        var user = new User(Guid.NewGuid(), Guid.NewGuid(), request.TenantId, request.FullName, request.Email, request.Mobile, "فعال");
        await _unitOfWork.AddUserAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return RegisterUserResult.Success(user.Id, "کاربر با موفقیت ایجاد شد");
    }
}

public record RegisterUserResult
{
    public bool IsSuccessful { get; init; }
    public Guid? UserId { get; init; }
    public string Message { get; init; } = string.Empty;

    public static RegisterUserResult Success(Guid id, string message)
        => new() { IsSuccessful = true, UserId = id, Message = message };

    public static RegisterUserResult Failure(string message)
        => new() { IsSuccessful = false, Message = message };
}
