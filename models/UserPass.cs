using FreeSql.DataAnnotations;

namespace sample_one.models
{

public class UserPass
{   
    [Column(IsPrimary =true)]
    public long UserId { get; set; }

    public byte[] PasswordHash { get; set; } 
    public byte[] PasswordSalt { get; set; } 


}


}