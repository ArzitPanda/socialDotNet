using Microsoft.AspNetCore.Mvc;
using sample_one.models.dto;
using sample_one.services.auth;
using sample_one.utility;

namespace sample_one.Controllers {


[ApiController]
[Route("/api/[Controller]")]
public class AuthController : ControllerBase
{


    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {   
            _authService = authService;


    }

    [HttpGet]
    public async Task<ActionResult<UserResponseDto>> LoginUser(string username, string password)
    {
        try
        {
        UserResponseDto a =await  _authService.Login(username, password);
            return Ok(a);
        }
        catch (Exception e)
        {
            return Unauthorized(e.Message);
            
        }


    } 





    [HttpPost]
    public async Task<ActionResult<UserResponseDto>> SignupUser(UserRequestDto userRequestDto)
    {
       try
       {
         UserResponseDto a = await _authService.Signup(userRequestDto);
         return Ok(a);
       }
       catch (Exception e)
       {
            return Unauthorized(e.Message);
        
       }
        

    

        // return Ok();

    }

    [HttpPost("sendmail")]
    public async Task<ActionResult> SendMail(string mail)
    {
    IEmailProvider emailProvider = new EmailProvider();
    emailProvider.SendVerificationEmail(mail);
    return Ok();


    }







}









}