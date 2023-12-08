using AutoMapper;
using sample_one.models;
using sample_one.models.dto;

namespace sample_one.services.Comments
{


    public class CommentService : ICommentService
    {
        private readonly IFreeSql _DbContext;
        private readonly IMapper _Mapper;
        public CommentService(IFreeSql dbContext, IMapper mapper)
        {
            _DbContext = dbContext;
            _Mapper = mapper;

        }



        public async Task<SubComment> AddSubComment(long Cid, long userId, SubCommentRequestDto request)
        {
            User a = await _DbContext.Select<User>().Where(u => u.Id == userId).FirstAsync() ?? throw new InvalidUserException();
            Comment b = await _DbContext.Select<Comment>().Where(u => u.CommentId == Cid).FirstAsync() ?? throw new InvalidUserException();
            SubComment reply = _Mapper.Map<SubComment>(request);
            reply.CommentId = Cid;
            reply.Uid = userId;

            var id = await _DbContext.Insert<SubComment>().AppendData(reply).ExecuteIdentityAsync();
            reply.SubCommentId = id;
            return reply;



        }

        public async Task<long> DeleteSubComment(long scid, long userId)
        {
            User user = await _DbContext.Select<User>().Where(u => u.Id == userId).FirstAsync() ?? throw new InvalidUserException();

            // Check if the subcomment exists
            SubComment subComment = await _DbContext.Select<SubComment>().Where(s => s.SubCommentId == scid && s.Uid == userId).FirstAsync();

            if (subComment == null)
            {
                // Subcomment not found or user doesn't have permission
                throw new SubCommentNotFoundException();
            }

            // Delete the subcomment
            var data = await _DbContext.Delete<SubComment>().Where(s => s.SubCommentId == scid && s.Uid == userId).ExecuteAffrowsAsync();
            return data;
        }

        public async Task<List<SubComment>> GetAllSubComments(long Cid)
        {
            return await _DbContext.Select<SubComment>().Where(s => s.CommentId == Cid).ToListAsync();
        }

        public async Task<SubComment> UpdateSubComment(long Cid, long userId, SubCommentRequestDto request)
        {

            User user = await _DbContext.Select<User>().Where(u => u.Id == userId).FirstAsync() ?? throw new InvalidUserException();

            // Check if the subcomment exists
            SubComment subComment = await _DbContext.Select<SubComment>().Where(s => s.CommentId == Cid && s.Uid == userId).FirstAsync();

            if (subComment == null)
            {
                // Subcomment not found or user doesn't have permission
                throw new SubCommentNotFoundException();
            }

            // Update subcomment properties
            subComment.Content = request.Content;

            // Execute the update
            await _DbContext.Update<SubComment>().Set(s => s.Content, request.Content).Where(s => s.CommentId == Cid && s.Uid == userId).ExecuteAffrowsAsync();

            return subComment;

        }
    }





}