using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPISwagger2.Domain;

namespace WEBAPISwagger2.Services
{
    public class CosmosPostService : IPostService
    {
        public Task<bool> DeleteAsync(string postId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Post>> GetPostAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetPostByIdAsync(string postId)
        {
            throw new NotImplementedException();
        }

        public Task<Post> PostCreateAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostUpateAsync(Post postUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
