using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBAPISwagger2.Contracts.v1;
using WEBAPISwagger2.Contracts.v1.Requests;
using WEBAPISwagger2.Contracts.v1.Responses;
using WEBAPISwagger2.Domain;
using WEBAPISwagger2.Services;

namespace WEBAPISwagger2.Controllers.v1
{
     
   // [Route("api/[controller]")]
   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class PostsController : ControllerBase
    {
        public readonly IPostService _PostService;
        public PostsController(IPostService IPostService)
        {
        this._PostService=IPostService;
        }

       


            [HttpGet(ApiRoutes.Posts.GetAll)]
        public async  Task<IActionResult> GetAll()
        {
            return Ok(await _PostService.GetPostAllAsync());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute]string postId)
        {
            if (string.IsNullOrEmpty(postId))
                return BadRequest();
            var post = await _PostService.GetPostByIdAsync(postId);
            if (post == null)
                return NotFound();


            return Ok(post);
         }

       [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] PostRequestCreate postRequestCreate)
        {
            
            var post = new Post
            {
                Id = postRequestCreate.Id,
                Name = postRequestCreate.Name
            };

            if (string.IsNullOrEmpty(post.Id))
                post.Id = Guid.NewGuid().ToString();

            post =await _PostService.PostCreateAsync(post);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id);

            var responsePost = new ResponsePost
            {
                Id = post.Id,
                Name = post.Name
            };
            return Created(locationUrl, responsePost);


        }

        [HttpPut(ApiRoutes.Posts.Upate)]
         public async Task<IActionResult> PostAsync ([FromRoute] string postId, [FromBody] UpdtePostRequest updtePostRequest)
        {
            var post = new Post
            {
                Id = updtePostRequest.Id,
                Name = updtePostRequest.Name,
            };

            var updated = await _PostService.PostUpateAsync(post);

            if (updated)
                return Ok(post);

            return NotFound();
        }
        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string postId)
        {
            if (string.IsNullOrEmpty(postId))
            {
                return  BadRequest();
            }
            if (await _PostService.DeleteAsync(postId))
                return NoContent();
            else
             return NotFound();
            
        }
        }
}