using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using sample_one.models;

namespace authorization.services
{


    public class JwtTokenGenerator : ITokenGenerator
    {

        public string Generate(User user)
        {
            List<Claim> claims = new List<Claim>
                                     {
                                         new  Claim(ClaimTypes.Name,user.UserName),
                                         
                                         new Claim(ClaimTypes.Role,"user"),
                                         new Claim(ClaimTypes.PrimarySid ,user.Id.ToString()),

                                         };
           var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("ersion=v4.7.2, .NETFramework,Version=v4.8, .NETFramework,Version=v4.8.1' instead of the project target framework 'net7.0'. This package may not be fully compatible with your project."));
           var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims:claims,
                    expires:DateTime.Now.AddDays(2),
                    signingCredentials:cred
                    
                );

                var jwt  = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
           

        }
    }


}