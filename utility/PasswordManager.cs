using System.Linq;
using System.Security.Cryptography;

namespace sample_one.utility
{

    public class PasswordManager : IPasswordManager
    {
        public bool Check(string password, byte[] passwordSalt, byte[] passwordHash)
        {           
                
             using (var client = new HMACSHA512(passwordSalt))
                {    
                    
                    
                    var  passwordHash_generate = client.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    return passwordHash_generate.SequenceEqual(passwordHash);
                      



                }


                

      
    }

        public void Create(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
                using (var client = new HMACSHA512())
                {    passwordSalt =  client.Key ;
                       passwordHash = client.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));



                }



        }
    }





}