using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEBAPISwagger2.Contracts.v1;
using WEBAPISwagger2.Contracts.v1.Requests;
using WEBAPISwagger2.Contracts.v1.Responses;
using WEBAPISwagger2.Services;

namespace WEBAPISwagger2.Controllers.v1
{
    public class IdentityController : Controller
    {
        public IdentityController(IIdentityService identityService)
        {
            _IdentityService = identityService;
        }

        private readonly IIdentityService _IdentityService;

        [HttpPost(ApiRoutes.Identity.register)]
        public async Task<IActionResult> Register([FromBody] UserRegisterationRequest userRegisterationRequest)
        {
            var authResponse = await _IdentityService.RegisterAsync(userRegisterationRequest.Email, userRegisterationRequest.Password);

            if (!authResponse.Success)
                return BadRequest(new AuthFailedResponse { Errors = authResponse.ErrorMessage });


            return Ok(new AuthSuccessResponse {  Token=authResponse.Token});
        }
    }
}