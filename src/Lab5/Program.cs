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
builder.Services.AddSwaggerGen();

WebApplication application = builder.Build();
application.UseSwagger();
application.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ATM System API v1");
    options.DocumentTitle = "ATM System API";
});
application.MapControllers();
application.Run();
