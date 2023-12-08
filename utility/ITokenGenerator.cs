using sample_one.models;

namespace authorization.services
{
    
public interface ITokenGenerator
{
    public string Generate(User user);



}


}