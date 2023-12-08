

namespace sample_one
{


   

public class SubCommentNotFoundException : Exception
{
    public SubCommentNotFoundException() : base("Subcomment not found or user doesn't have permission.")
    {
    }

    public SubCommentNotFoundException(string message) : base(message)
    {
    }

    public SubCommentNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

}
