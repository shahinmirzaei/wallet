using System.Globalization;
using EnterpriseWallet.IdentityService.Api.Configuration;
using EnterpriseWallet.IdentityService.Application;
using EnterpriseWallet.IdentityService.Infrastructure;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((_, configuration) =>
{
    configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    configuration.AddEnvironmentVariables();
});

builder.Services.AddControllers().AddDataAnnotationsLocalization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(SwaggerConfiguration.Create());

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);

var app = builder.Build();

var supportedCultures = new[] { new CultureInfo("fa-IR"), new CultureInfo("en-US") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("fa-IR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "سرویس هویت سازمانی - نسخه ۱");
        c.DocumentTitle = "اسناد سرویس هویت";
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/سلامت");

app.Run();
