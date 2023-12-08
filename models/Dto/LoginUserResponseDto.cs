namespace sample_one.models.dto
{
    


    public class LoginUserResponseDto:UserResponseDto
    {
        public string AuthToken { get; set; } = string.Empty;


    }




}