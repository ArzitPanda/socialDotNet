using sample_one.models;
using sample_one.models.dto;

namespace sample_one.services.Comments
{

public interface ICommentService
{
    public Task<SubComment> AddSubComment(long Cid, long userId,SubCommentRequestDto request);
    public Task<SubComment> UpdateSubComment(long Cid, long userId,SubCommentRequestDto request);
    public Task<List<SubComment>> GetAllSubComments(long Cid);
    public Task<long> DeleteSubComment(long scid, long userId);

    

}



}