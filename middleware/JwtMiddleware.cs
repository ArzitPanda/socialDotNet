
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace sample_one.middleware{


public  class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

     public async Task Invoke(HttpContext context)
    {
        Console.WriteLine("hello");
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        Console.WriteLine(token);

        if (token != null)
            AttachUserToContext(context, token);

        await _next(context);
    }

    private void AttachUserToContext(HttpContext context, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ersion=v4.7.2, .NETFramework,Version=v4.8, .NETFramework,Version=v4.8.1' instead of the project target framework 'net7.0'. This package may not be fully compatible with your project.");
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

                Console.WriteLine("here");
            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == ClaimTypes.PrimarySid).Value;
            // Console.WriteLine(userId);

        
            context.Items["UserId"] =long.Parse(userId);
        }
        catch
        {
            
        }
    }




}




}