using FreeSql.DataAnnotations;

namespace sample_one.models
{




[Table(Name ="User_comment_like")]
public class User_comment_like
{

    public long Uid { get; set; }
    public User User { get; set; }

    public long Cid { get; set; }
    public Comment comment { get; set; }



}





}