using sample_one.models;
using sample_one.models.dto;

namespace sample_one.services.post
{

public interface IPostService
{
    public Task<List<Post>> GetAllPost(long id);
    public Task<Post> GetPostById(long id,long pid);

    public Task<Post> AddPost(PostRequestDto post);

    public Task<Post> UpdatePost(long id,long pid,PostRequestDto updatedPost);

    public Task<int> DeletePost(long id,long pid);

    
    



}




}