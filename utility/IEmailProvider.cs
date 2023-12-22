namespace sample_one.utility
{


public interface IEmailProvider
{

public Task<string> SendVerificationEmail(string email);




}


}