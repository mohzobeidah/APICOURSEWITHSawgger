using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPISwagger2.Domain;

namespace WEBAPISwagger2.Services
{
    public interface IPostService
    {
       Task <List<Post>> GetPostAllAsync();
        Task< Post> GetPostByIdAsync(string postId);
        Task<Post> PostCreateAsync(Post post);
        Task<bool> PostUpateAsync(Post postUpdate);
        Task<bool> DeleteAsync(string postId);


    }
}
