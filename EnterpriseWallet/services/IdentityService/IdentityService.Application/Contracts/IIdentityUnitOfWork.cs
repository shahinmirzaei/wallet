using EnterpriseWallet.IdentityService.Domain.Entities;

namespace EnterpriseWallet.IdentityService.Application.Contracts;

public interface IIdentityUnitOfWork
{
    Task<User?> FindUserByEmailAsync(string email, CancellationToken token = default);
    Task AddUserAsync(User user, CancellationToken token = default);
    Task<int> SaveChangesAsync(CancellationToken token = default);
}
