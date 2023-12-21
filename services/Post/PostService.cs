using System.Text;
using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using sample_one.models;
using sample_one.models.dto;
using sample_one.utility;

namespace sample_one.services.post
{


    public class PostService : IPostService
    {
        private readonly IFreeSql _DbContext;
        private readonly IMapper _Mapper;
        private readonly IDistributedCache _cache;

        // private readonly IFileUploader _FileUploader;
        public PostService(IFreeSql freeSql,IMapper impMapper,IDistributedCache cache)
        {
            _DbContext = freeSql;
            _cache = cache;

            _Mapper = impMapper;
            _cache = cache;
            // _FileUploader= fileUploader;

        }

        public async Task<Post> AddPost(PostRequestDto post)
        {

          var count =   await    _DbContext.Select<User>().Where(A=>A.Id ==post.UserId).CountAsync();
            if(count == 0)
            {
                    throw new InvalidUserException();
            }


            //  string url = _FileUploader.UploadImage(file);

             Post p =    _Mapper.Map<Post>(post);
                // p.Content = url;

                long pid = await  _DbContext.Insert<Post>().AppendData(p).ExecuteIdentityAsync();
                p.PostId = pid;


                return p;
             



        }

      public async Task<int> DeletePost(long id, long pid)
    {
        var count = await _DbContext.Select<Post>().Where(p => p.UserId == id && p.PostId == pid).CountAsync();
        if (count == 0)
        {
            throw new Exception("Post not found.");
        }

        var affectedRows = await _DbContext.Delete<Post>().Where(p => p.UserId == id && p.PostId == pid).ExecuteAffrowsAsync();
        return affectedRows;
    }

    public async Task<List<Post>> GetAllPost(long id)
    {
        var posts = await _DbContext.Select<Post>().Where(p => p.UserId == id).ToListAsync();
        return posts;
    }

    public async Task<Post> GetPostById(long id, long pid)
    {
         string cacheKey = $"post:id:{id}:pid:{pid}";
 

        var cachedData  = await  _cache.GetAsync(cacheKey);
              Post cachedPost = null;
    if (cachedData != null)
    {
        cachedPost = JsonSerializer.Deserialize<Post>(cachedData);

    }

    if (cachedPost != null)
    {
        Console.WriteLine("from cached data");
        return cachedPost;
    }


        var post = await _DbContext.Select<Post>().Where(p => p.UserId == id && p.PostId == pid).FirstAsync();
        if (post == null)
        {
            throw new Exception("Post not found.");
        }

            var json = JsonSerializer.Serialize(post);
            var bytedata =  Encoding.UTF8.GetBytes(json);

         await _cache.SetAsync(cacheKey, bytedata, new DistributedCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromSeconds(5))); // Adjust expiration as needed
        return post;
    }

    public async Task<Post> UpdatePost(long id, long pid, PostRequestDto updatedPost)
    {
        var count = await _DbContext.Select<Post>().Where(p => p.UserId == id && p.PostId == pid).CountAsync();
        if (count == 0)
        {
            throw new Exception("Post not found.");
        }

        var existingPost = await _DbContext.Select<Post>().Where(p => p.UserId == id && p.PostId == pid).FirstAsync();
        _Mapper.Map(updatedPost, existingPost);

        await _DbContext.Update<Post>().SetSource(existingPost).ExecuteAffrowsAsync();

        return existingPost;
    }

     
    }




}