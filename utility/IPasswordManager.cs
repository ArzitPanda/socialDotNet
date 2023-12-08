namespace sample_one.utility{


public interface IPasswordManager
{
    public void Create(string password, out byte[] passwordSalt, out byte[] passwordHash);


    public bool Check(string password,  byte[] passwordSalt,  byte[] passwordHash);
        // bool CheckPassword(string password, byte[] passwordSalt, byte[] passwordHash);
    }





}