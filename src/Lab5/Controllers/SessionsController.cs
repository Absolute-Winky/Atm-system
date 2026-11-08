using Lab5.Application.Contracts.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

[ApiController]
[Route("api/sessions")]
public class SessionsController : ControllerBase
{
    private readonly ISessionApplicationService _sessionService;

    public SessionsController(ISessionApplicationService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpPost("user")]
    public ActionResult<CreateSessionResponse> CreateUserSession([FromBody] CreateUserSessionRequest request)
    {
        CreateUserSessionResult result = _sessionService.CreateUserSession(request.AccountNumber, request.PinCode);
        return result switch
        {
            CreateUserSessionResult.Success success => Ok(new CreateSessionResponse(success.SessionKey)),
            CreateUserSessionResult.AccountNotFound => Unauthorized(),
            CreateUserSessionResult.WrongPinCode => Unauthorized(),
            _ => BadRequest(),
        };
    }

    [HttpPost("admin")]
    public ActionResult<CreateSessionResponse> CreateAdminSession([FromBody] CreateAdminSessionRequest request)
    {
        CreateAdminSessionResult result = _sessionService.CreateAdminSession(request.Password);
        return result switch
        {
            CreateAdminSessionResult.Success success => Ok(new CreateSessionResponse(success.SessionKey)),
            CreateAdminSessionResult.WrongPassword => Unauthorized(),
            _ => BadRequest(),
        };
    }

    [HttpDelete("{sessionKey:guid}")]
    public IActionResult CloseSession(Guid sessionKey)
    {
        _sessionService.CloseSession(sessionKey);
        return Ok();
    }
}
