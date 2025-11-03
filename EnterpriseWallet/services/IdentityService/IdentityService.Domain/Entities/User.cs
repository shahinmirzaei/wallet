namespace EnterpriseWallet.IdentityService.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid TenantId { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Mobile { get; private set; }
    public string Status { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    private User()
    {
    }

    public User(Guid id, Guid customerId, Guid tenantId, string fullName, string email, string mobile, string status)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        CustomerId = customerId;
        TenantId = tenantId;
        FullName = string.IsNullOrWhiteSpace(fullName) ? throw new ArgumentException("نام کامل ضروری است", nameof(fullName)) : fullName;
        Email = email;
        Mobile = mobile;
        Status = status;
        CreatedAtUtc = DateTime.UtcNow;
    }
}
