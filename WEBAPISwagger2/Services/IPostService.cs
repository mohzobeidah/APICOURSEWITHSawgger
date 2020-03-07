using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPISwagger2.Domain;

namespace WEBAPISwagger2.Services
{
    public interface IPostService
    {
        List<Post> GetPostAll();
        Post GetPostById(string postId);
        Post PostCreate(Post post);

    }
}
