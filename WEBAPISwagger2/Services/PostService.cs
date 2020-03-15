using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPISwagger2.Data;
using WEBAPISwagger2.Domain;

namespace WEBAPISwagger2.Services
{
    public class PostService : IPostService
    {
        //private List<Post> _post;
        private readonly DataContext _dataContext;

        public PostService(DataContext dataContext)
        {
            this._dataContext = dataContext;
            //_post = new List<Post>();
            //for (int i = 0; i < 5; i++)
            //{
            //    _post.Add(new Post
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Name = $"this is post NO{ i}"
            //    });
            //}
        }

        public async Task<bool> DeleteAsync(string postId)
        {
            var post =  await _dataContext.posts.FirstOrDefaultAsync(x=>x.Id==postId);
            if (post != null)
            {
                 _dataContext.posts.Remove(post);
                var effectedItem=await _dataContext.SaveChangesAsync();
                if (effectedItem > 0)
                    return true;

            }
            return false;
        }

        public async Task<List<Post>> GetPostAllAsync()
        {
            return await _dataContext.posts.ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(string postId)
        {
            return await _dataContext.posts.SingleOrDefaultAsync(x => x.Id == postId);
        }

        public async Task< Post> PostCreateAsync(Post post)
        {
            await _dataContext.posts.AddAsync(post);
            await _dataContext.SaveChangesAsync();
            return await _dataContext.posts.SingleOrDefaultAsync(x => x.Id == post.Id);
        }

        public async Task<bool > PostUpateAsync(Post postUpdate)
        {
            var postexist =await GetPostByIdAsync(postUpdate.Id);
            if (postexist == null)
                return false;
            else
            {
                var post = await _dataContext.posts.FindAsync(postUpdate.Id);
                post.Name = postUpdate.Name;
                 _dataContext.posts.Update(post);
                var effectedItem =await  _dataContext.SaveChangesAsync();
                if (effectedItem > 0)
                return true;
                
            }
            return false;
        }
    }
}
