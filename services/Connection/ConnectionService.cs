using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using sample_one.hubs;
using sample_one.models;
using sample_one.models.dto;
using sample_one.models.models;

namespace sample_one.services.Connection
{


    public class ConnectionService : IConnectionService
    {

        private readonly IFreeSql _Dbcontext;
        private readonly IMapper _Mapper;


        private readonly IHubContext<NotificationHub> _HubContext;
        public ConnectionService(IFreeSql dbcontext, IMapper mapper, IHubContext<NotificationHub> hubContext)
        {
            _Dbcontext = dbcontext;
            _HubContext = hubContext;
            _Mapper = mapper;
        }



        // public   Task<List<User>> GetUsers(long id);
        public async Task<List<UserResponseDto>>? FollowAUser(long id, long friendId)
        {
            var user = await _Dbcontext.Select<User>().Where(u => u.Id == id).ToListAsync();

            var data = await _Dbcontext.Select<User>().Where(u => u.Id == friendId).ToListAsync();

            long a = await _Dbcontext.Select<Friend>().Where(f => f.FollowingId == friendId).Where(f => f.FollowersId == id).CountAsync();

            if (a == 1)
            {
                throw new AlreadyConnectionException(id, friendId);
            }


            Friend f = new Friend();
            //  f.FId = id;
            f.FollowingId = friendId;
            f.Followin = data[0];
            f.FollowersId = id;
            f.Follower = user[0];
            var friends = await _Dbcontext.Insert<Friend>().AppendData(f).ExecuteIdentityAsync();
            Console.WriteLine(friends);

            var data2 = await _Dbcontext.Select<User, Friend>().InnerJoin((a) => a.t1.Id == a.t2.FollowingId).Where(a => a.t2.FollowersId == id).ToListAsync();
            var friendsdata = _Mapper.Map<List<UserResponseDto>>(data2);
            Console.WriteLine(data2);

            await  _HubContext.Clients.Groups(friendId.ToString()).SendAsync("receivedFollowingNotification",user[0].FirstName);


            return friendsdata;
        }

        public async Task<List<UserResponseDto>> GetFollowings(long id)
        {
            var data2 = await _Dbcontext.Select<User, Friend>().InnerJoin((a) => a.t1.Id == a.t2.FollowingId).Where(a => a.t2.FollowersId == id).ToListAsync();
            var friendsdata = _Mapper.Map<List<UserResponseDto>>(data2);
            Console.WriteLine(data2);
            return friendsdata;

        }

        public async Task<List<UserResponseDto>>? GetFollowers(long id)
        {
            var data2 = await _Dbcontext.Select<User, Friend>().InnerJoin((a) => a.t1.Id == a.t2.FollowersId).Where(a => a.t2.FollowingId == id).ToListAsync();
            var friendsdata = _Mapper.Map<List<UserResponseDto>>(data2);
            Console.WriteLine(data2);
            return friendsdata;
        }

        public async Task<int> RemoveFriend(long id, long friendId)
        {
            // Assuming _DbContext is your FreeSql.DbContext
            var result = await _Dbcontext.Delete<Friend>().Where(a => a.FollowingId == id && a.FollowersId == friendId).ExecuteAffrowsAsync();

            if (result <= 0)
            {
                throw new Exception("Friend connection not found.");
            }
            return result;
        }








    }
}


