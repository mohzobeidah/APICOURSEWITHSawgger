using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPISwagger2.Domain;
using WEBAPISwagger2.Contracts.v1.Responses;
using System.IdentityModel.Tokens.Jwt;
using WEBAPISwagger2.options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace WEBAPISwagger2.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSetting _jwtSetting;

        public IdentityService(UserManager<IdentityUser>  userManager ,JwtSetting jwtSetting)
        {
            this._userManager = userManager;
            this._jwtSetting = jwtSetting;
        }
        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            var exisituser = await _userManager.FindByEmailAsync(email);

            if (exisituser != null)
            {

                return new AuthenticationResult
                {

                    ErrorMessage = new[] { "user with this email is aleady exists" }


                };

            }
            var newUser = new IdentityUser
            {
                Email = email,
                UserName = email

            };

            var createdUser = await _userManager.CreateAsync(newUser, password);
            if (!createdUser.Succeeded)
            {


                
                return new AuthenticationResult
                {


                    ErrorMessage = createdUser.Errors.Select(X=>X.Description).ToArray(),

                };


             
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSetting.Secert);
            var tokenDescriptior = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[]

                {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim("id", newUser.Id),

                }), Expires = DateTime.Now.AddHours(1), 
                SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)


            };


            var token = tokenHandler.CreateToken(tokenDescriptior);
            return new AuthenticationResult
            {
                Token = tokenHandler.WriteToken(token), Success=true,

            };



            }


    }

}



