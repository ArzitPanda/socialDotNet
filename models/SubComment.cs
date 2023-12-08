using FreeSql.DataAnnotations;

namespace sample_one.models
{
    public class SubComment
    {   [Column(IsPrimary =true,IsIdentity =true)]
        public long SubCommentId { get; set; }

        public User User { get; set; }

        public long CommentId{ get; set; }
        public Comment Comment { get; set; }

        public long Uid { get; set; }

        public string Content { get; set; }
        public DateTime SubCommentCreated { get; set; } =DateTime.Now;



    }



}