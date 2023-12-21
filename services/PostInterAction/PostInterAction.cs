using AutoMapper;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.AspNetCore.SignalR;
using sample_one.hubs;
using sample_one.models;
using sample_one.models.dto;

namespace sample_one.services.post
{



    public class PostInterAction : IPostInterAction
    {
        private readonly IFreeSql _connection;
        private readonly IMapper _mapper;
        private readonly  IHubContext<NotificationHub> _hubContext;
        public PostInterAction(IFreeSql connection, IMapper mapper,IHubContext<NotificationHub> hubContext)
        {
            _connection = connection;
            _mapper = mapper;
            _hubContext=hubContext;
        }


        public async Task<Comment> AddComment(long postId, long InteractorId, CommentRequestDto comment)
        {
            long postPresent = await _connection.Select<Post>().Where(p => p.PostId == postId).CountAsync();
            long userPresent = await _connection.Select<User>().Where(u => u.Id == InteractorId).CountAsync();

            


            if (postPresent == 0)
            {

                throw new PostNotFoundException();
            }

            if (userPresent == 0)
            {
                throw new InvalidUserException();

            }
        Post post  = await  _connection.Select<Post>().Where(p => p.PostId == postId).FirstAsync();  
                  User user = await _connection.Select<User>().Where(u => u.Id == InteractorId).FirstAsync();   

            Comment c = _mapper.Map<Comment>(comment);
            c.PostId = postId;
            c.Uid = InteractorId;

            var data = await _connection.Insert<Comment>().AppendData(c).ExecuteIdentityAsync();
            await _hubContext.Clients.Group(post.UserId.ToString()).SendAsync("receivedPostNotification",$"{post.PostId}-{user.UserName}");
            c.CommentId = data;
            return c;


        }

        public async Task<List<Comment>> GetCommentsByPost(long postId)
        {
            var comments = await _connection
         .Select<Comment>()
         .Where(c => c.PostId == postId)
         .ToListAsync();

            return comments;
        }

        public async Task<long> AddLikeToPost(long postId, long uid)
        {
            User a = await _connection.Select<User>().Where(u => u.Id == uid).FirstAsync() ?? throw new InvalidUserException();
            var userdata = await _connection.Select<User_post_like>().Where(u => u.Pid == postId && u.Uid == uid).FirstAsync();
            if (userdata != null)
            {
                throw new AlreadyLikeException();
            }
            var user_post_like_data = new User_post_like { Pid = postId, Uid = uid };
            return await _connection.Insert<User_post_like>().AppendData(user_post_like_data).ExecuteIdentityAsync();



        }

        public async Task<List<UserResponseDto>> GetLikesByPost(long postId)
        {
            Post a = await _connection.Select<Post>().Where(u => u.PostId == postId).FirstAsync() ?? throw new PostNotFoundException();
            List<User> likers = await _connection.Select<User, User_post_like>().InnerJoin((a) => a.t1.Id == a.t2.Uid).Where(u => u.t2.Pid == postId).ToListAsync();

            var data = _mapper.Map<List<UserResponseDto>>(likers);
            return data.ToList();





            // throw new NotImplementedException();
        }

        public async Task<long> RemoveLikeToPost(long postId, long uid)
        {
            User a = await _connection.Select<User>().Where(u => u.Id == uid).FirstAsync();
            if (a is null)
            {
                throw new InvalidUserException();
            }
            var userdata = await _connection.Select<User_post_like>().Where(u => u.Pid == postId && u.Uid == uid).FirstAsync();
            if (userdata == null)
            {
                throw new NotLikedException();
            }
            var user_post_like_data = new User_post_like { Pid = postId, Uid = uid };
            return await _connection.Delete<User_post_like>().Where((a) => a.Pid == postId && a.Uid == uid).ExecuteAffrowsAsync();
        }

        public async Task<long> AddLikeToComment(long cId, long uid)
        {
            User a = await _connection.Select<User>().Where(u => u.Id == uid).FirstAsync() ?? throw new InvalidUserException();
            var userdata = await _connection.Select<User_comment_like>().Where(u => u.Cid == cId && u.Uid == uid).FirstAsync();
            if (userdata != null)
            {
                throw new AlreadyLikeException();
            }
            var User_comment_like_data = new User_comment_like { Cid = cId, Uid = uid };
            return await _connection.Insert<User_comment_like>().AppendData(User_comment_like_data).ExecuteIdentityAsync();
        }

        public async Task<long> RemoveLikeToComment(long cId, long uid)
        {
            var userData = await _connection.Select<User_comment_like>()
          .Where(u => u.Cid == cId && u.Uid == uid)
          .FirstAsync() ?? throw new NotLikedException();

            // Remove the like

            var data = await _connection.Delete<User_comment_like>()
         .Where(u => u.Cid == cId && u.Uid == uid).ExecuteAffrowsAsync();
            return data;

        }
    }


}