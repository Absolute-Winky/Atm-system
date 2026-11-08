using Lab5.Application.Contracts.Sessions;
using Lab5.Application.Extensions;
using Lab5.Infrastructure.DataAccess.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<AdminAuthOptions>()
    .Bind(builder.Configuration.GetSection(AdminAuthOptions.SectionName))
    .Validate(options => !string.IsNullOrWhiteSpace(options.SystemPassword),
        "Set AdminAuth__SystemPassword to a non-empty value before starting the application.")
    .ValidateOnStart();
builder.Services.AddApplication();
builder.Services.AddInfrastructureDataAccess();
builder.Services.AddControllers();

WebApplication application = builder.Build();
application.MapControllers();
application.Run();
