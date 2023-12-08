using FreeSql.DataAnnotations;

namespace sample_one.models
{


public class Comment
{   
    [Column(IsPrimary =true,IsIdentity =true)]
    public long CommentId { get; set; }

    public string Text { get; set; }
    public long PostId{ get; set; }
    public Post post { get; set; }
    // public string AuthorName { get; set; }
    public long Uid { get; set; }
    public User user { get; set; }

    public List<SubComment> SubComments { get; set; }

    public DateTime CreatedAt { get; set; } =DateTime.Now;


}


}