

using FreeSql.DataAnnotations;

namespace  sample_one.models
{


[Table(Name ="User_post_like")]
public class User_post_like
{

    public long Uid { get; set;}
    public User User { get; set; }

    public long Pid{ get; set; }
    public Post post { get; set; }






}



}