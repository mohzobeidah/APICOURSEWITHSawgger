using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [ApiController]
    public class PostsController : ControllerBase
    {
        public readonly IPostService _IPostService;
        public PostsController(IPostService IPostService)
        {
        this._IPostService=IPostService;
        }

       


            [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_IPostService.GetPostAll());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public IActionResult Get([FromRoute]string postId)
        {
            if (string.IsNullOrEmpty(postId))
                return BadRequest();
            var post = _IPostService.GetPostById(postId);
            if (post == null)
                return NotFound();


            return Ok(post);
         }

       [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] PostRequestCreate postRequestCreate)
        {
            
            var post = new Post
            {
                Id = postRequestCreate.Id,
                Name = postRequestCreate.Name
            };

            if (string.IsNullOrEmpty(post.Id))
                post.Id = Guid.NewGuid().ToString();

            post =_IPostService.PostCreate(post);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id);

            var responsePost = new ResponsePost
            {
                Id = post.Id,
                Name = post.Name
            };
            return Created(locationUrl, responsePost);


        }
    }
}