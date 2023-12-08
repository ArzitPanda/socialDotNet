using sample_one.models;
using sample_one.models.dto;

namespace sample_one.services.post
{
public interface IPostInterAction
{
    public Task<List<Comment>> GetCommentsByPost(long postId);
    public Task<Comment> AddComment(long postId,long InteractorId,CommentRequestDto comment);
     public  Task<long> AddLikeToPost(long postId,long uid);

     public Task<List<UserResponseDto>> GetLikesByPost(long postId);
     public  Task<long> RemoveLikeToPost(long postId,long uid);
     public  Task<long> AddLikeToComment(long cId,long uid);
     public  Task<long> RemoveLikeToComment(long cId,long uid);


}



}