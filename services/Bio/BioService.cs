
using AutoMapper;
using sample_one.models;
using sample_one.models.dto;
using sample_one.utility;

namespace sample_one.services.Bios
{


    public class BioService : IBioService
    {

        private readonly IMapper _Mapper;
        private readonly IFreeSql _DbContext;

        private readonly IFileUploader _FileUploader;
        public BioService(IFreeSql dbContext, IMapper mapper,IFileUploader fileUploader) {
            
            _FileUploader = fileUploader;
            _DbContext = dbContext;
            _Mapper = mapper;
        }

        public async Task<UserResponseDto> CreateBio(Bio bio)
        {

            var data = _DbContext.Insert<Bio>().AppendData(bio).ExecuteIdentityAsync();

            var user = _DbContext.Select<User>().InnerJoin(a => a.Id == a.Bio.Id).Where(a => a.Id == bio.Id).ToList()[0];

            return _Mapper.Map<UserResponseDto>(user);


        }

        public async Task<Bio> UploadUserProfile(long uid,IFormFile userphoto)
        {       
                var user = await _DbContext.Select<User>().Where(u => u.Id == uid).FirstAsync();
                if(user != null)
                {     

                        Console.WriteLine("User is not null");

                    if(userphoto is null) 
                    {
                        Console.WriteLine("photo is null");
                        
                    }

                     string img =  await  _FileUploader.UploadImage(userphoto);
                    var bio = await _DbContext.Update<Bio>().Set(a=>a.ImgUrl,img).Where(a=> a.Id == uid).ExecuteAffrowsAsync();

                    Bio bioresponse = await _DbContext.Select<Bio>().Where(a=>a.Id == uid).FirstAsync();

                    return bioresponse;

                }
                else
                {
                    throw new InvalidUserException();
                }
            
            
        }
    }




}