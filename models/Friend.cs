using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

namespace  sample_one.models.models
{
    

[Table(Name ="Friend")]
public class Friend
{   
    // [JsonIgnore()]
     public User Followin{ get; set; }
     public long FollowingId{ get; set; }
     public User Follower{ get; set; }
     public long FollowersId{ get; set; }
    


}

}