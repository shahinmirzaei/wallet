using EnterpriseWallet.IdentityService.Application.Contracts;
using EnterpriseWallet.IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseWallet.IdentityService.Infrastructure.Persistence;

public class IdentityUnitOfWork : IIdentityUnitOfWork
{
    private readonly IdentityDbContext _context;

    public IdentityUnitOfWork(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(User user, CancellationToken token = default)
    {
        await _context.Users.AddAsync(user, token);
    }

    public async Task<User?> FindUserByEmailAsync(string email, CancellationToken token = default)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email, token);
    }

    public Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return _context.SaveChangesAsync(token);
    }
}
