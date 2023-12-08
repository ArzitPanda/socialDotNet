using Microsoft.AspNetCore.Mvc;
using sample_one.models;
using sample_one.models.dto;
using sample_one.services.Bios;

namespace sample_one.Controllers
{

[ApiController]
[Route("/api/[Controller]")]
public class BioController : ControllerBase
{
    private readonly IBioService _service; 

    public BioController(IBioService service)
    {
        _service = service;


    }


    [HttpPost]
    public async  Task<ActionResult<UserResponseDto>> CreateBio(Bio bio)
    {
            var data = await _service.CreateBio(bio);

            return data;


    }




    
}


}