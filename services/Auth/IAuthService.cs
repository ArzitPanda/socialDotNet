using sample_one.models.dto;

namespace sample_one.services.auth{




public interface IAuthService
{

    public Task<UserResponseDto> Signup(UserRequestDto request);
    public Task<LoginUserResponseDto> Login(string username,string password);


}






}