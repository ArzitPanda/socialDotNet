using sample_one.models;
using sample_one.models.dto;
using sample_one.models.models;

namespace sample_one.services.Connection
{


public interface IConnectionService
{


        

    public   Task<List<UserResponseDto>>? GetFollowers(long id);
    public    Task<List<UserResponseDto>>? FollowAUser(long id,long friendId);
    public   Task<int> RemoveFriend(long id,long friendId);

    public Task<List<UserResponseDto>> GetFollowings(long id);
    // public   Task<List<User>> GetUsers(long id);




}



}

