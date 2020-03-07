using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPISwagger2.Domain;

namespace WEBAPISwagger2.Services
{
    public class PostService : IPostService
    {
        private List<Post> _post;

        public PostService()
        {
            _post = new List<Post>();
            for (int i = 0; i < 5; i++)
            {
                _post.Add(new Post
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = $"this is post NO{ i}"
                });
            }
        }
        public List<Post> GetPostAll()
        {
            return _post;
        }

        public Post GetPostById(string postId)
        {
            return _post.SingleOrDefault(x => x.Id == postId);
        }

        public Post PostCreate(Post post)
        {
            _post.Add(post);
            return _post.SingleOrDefault(x => x.Id == post.Id);
        }
    }
}
