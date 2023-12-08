using authorization.services;
using AutoMapper;
using sample_one.models;
using sample_one.models.dto;
using sample_one.utility;

namespace sample_one.services.auth{

    public class AuthService : IAuthService
    {

        private readonly  IFreeSql _DbContext; 
        private readonly  IMapper _Mapper;
        private readonly IPasswordManager _PasswordsManager; 
        private readonly ITokenGenerator _TokenGenerator; 
        public AuthService(IFreeSql dbContext,IMapper mapper,IPasswordManager passwordManager,ITokenGenerator tokenGenerator)
        {
            _DbContext = dbContext;
            _Mapper = mapper;
            _PasswordsManager = passwordManager;
            _TokenGenerator = tokenGenerator;

        }



        public async Task<LoginUserResponseDto> Login(string username, string password)
        {
                 var data =  await _DbContext.Select<User>().LeftJoin(a=>a.Id==a.Bio.Id).Where(u => u.UserName == username).ToListAsync();
                 if(data.Count==0) throw new Exception(username + " is not  found");

                 User a  = data[0] ;
                 var passdata =  await _DbContext.Select<UserPass>().Where(u => u.UserId == a.Id).ToListAsync();
                 if(_PasswordsManager.Check(password,passdata[0].PasswordSalt,passdata[0].PasswordHash))
                 {
                        
                        
                        
                        LoginUserResponseDto b =  _Mapper.Map<LoginUserResponseDto>(a);
                        b.AuthToken = _TokenGenerator.Generate(a);
                            return b;

                 }
                 else
                 {
                        throw new Exception("wrong password") ;


                 }




        }

        public async Task<UserResponseDto>  Signup(UserRequestDto request)
        {
               var data =  await _DbContext.Select<User>().Where(u => u.UserName == request.UserName).ToListAsync();

                if(data.Count!=0)
                throw new Exception("User " + request.UserName + "is already signed up");
                else
                {
                          User a  = _Mapper.Map<User>(request);
                         var userId = await  _DbContext.Insert<User>().AppendData(a).ExecuteIdentityAsync();
                            UserPass userpass = new UserPass();
                            _PasswordsManager.Create(request.Password,out byte[] passwordSalt,out byte[] passwordHash);
                            userpass.UserId = userId;
                            userpass.PasswordHash = passwordHash;
                            userpass.PasswordSalt = passwordSalt;
                            a.Passcode = userpass;
                        await  _DbContext.Insert<UserPass>().AppendData(userpass).ExecuteIdentityAsync();


                            // userpass.UserId = userId;
                             

                            var user  = _DbContext.Select<User>(userId).First();
                         return _Mapper.Map<UserResponseDto>(user);

                }


                // return new UserResponseDto();
            
        }

      
    }


}