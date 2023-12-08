using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

namespace sample_one.models
{

public class Post 
{
[Column(IsPrimary =true,IsIdentity =true)]
 public long PostId { get; set; }

    public string Title { get; set; } =string.Empty;

    public string Content { get; set; } =string.Empty ;

    public DateTime CreatedAt { get; set; } =DateTime.Now;

    public DateTime? UpdatedAt { get; set; } =DateTime.Now;

    // public  List<User> Likes { get; set; }

    // public int Dislikes { get; set; }

    // public bool IsPublished { get; set; }
     [JsonIgnore]
    public User User {get; set; }
    public long UserId { get; set; }
     [JsonIgnore]
    public List<Comment> Comments { get; set; }

}





}