namespace sample_one.models.dto
{


public class UserResponseDto
{


    public long Id { get; set; }
    public string UserName { get; set; } =string.Empty;
    public string LastName { get; set; } =string.Empty;
    public string FirstName { get; set; } =string.Empty;
    public string Email { get; set; } =string.Empty;
    public Bio Bio { get; set; } = new Bio();
    public List<User> Friends { get; set; } =new List<User>();


}


    
}