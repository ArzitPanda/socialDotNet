
using AutoMapper;
using sample_one.models;
using sample_one.models.dto;

namespace sample_one.services.Bios
{


    public class BioService : IBioService
    {

        private readonly IMapper _Mapper;
        private readonly IFreeSql _DbContext;
        public BioService(IFreeSql dbContext, IMapper mapper)
        {
            _DbContext = dbContext;
            _Mapper = mapper;
        }

        public async Task<UserResponseDto> CreateBio(Bio bio)
        {

            var data = _DbContext.Insert<Bio>().AppendData(bio).ExecuteIdentityAsync();

            var user = _DbContext.Select<User>().InnerJoin(a => a.Id == a.Bio.Id).Where(a => a.Id == bio.Id).ToList()[0];

            return _Mapper.Map<UserResponseDto>(user);


        }




    }




}