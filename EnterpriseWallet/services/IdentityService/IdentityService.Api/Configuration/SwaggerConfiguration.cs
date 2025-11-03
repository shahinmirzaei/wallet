using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EnterpriseWallet.IdentityService.Api.Configuration;

public static class SwaggerConfiguration
{
    public static Action<SwaggerGenOptions> Create()
    {
        return options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "سرویس هویت سازمانی",
                Version = "v1",
                Description = "مستندات فارسی سرویس هویت برای کیف پول سازمانی",
                Contact = new OpenApiContact
                {
                    Name = "تیم معماری کیف پول",
                    Email = "architecture@wallet.local"
                }
            });

            options.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
        };
    }
}
