using sample_one.models;
using sample_one.models.dto;

namespace sample_one.services.Bios
{


public interface IBioService
{
    public Task<UserResponseDto> CreateBio(Bio bio);




}




}
