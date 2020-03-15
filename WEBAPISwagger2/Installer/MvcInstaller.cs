using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPISwagger2.options;
using WEBAPISwagger2.Services;

namespace WEBAPISwagger2.Installer
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var jwtsetting = new JwtSetting();
            configuration.Bind(nameof(jwtsetting), jwtsetting);
            services.AddSingleton(jwtsetting);
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x=> {

                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes( jwtsetting.Secert)),
                    ValidateIssuer=false,
                    ValidateAudience=false,
                    ValidateLifetime=true,

                };
            });

      
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(x => {
                x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "TweetApi", Version = "v1 " });
               

                var ApiSecurityScheme = new OpenApiSecurityScheme
                {
                    Description = "ddddd",
                    Name = "Authorizaton",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey


                };
               
                x.AddSecurityDefinition("Bearer", ApiSecurityScheme);
                
                        x.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                ApiSecurityScheme
                                ,
                                new[] { "readAccess", "writeAccess" }
                            }
                         });
            });

         //   services.AddSingleton<IPostService, PostService>();
        }
    }
}
