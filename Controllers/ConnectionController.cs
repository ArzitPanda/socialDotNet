using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using sample_one.models.dto;
using sample_one.services.Connection;

namespace sample_one.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    [Authorize(Roles ="user")]
    public class ConnectionController : ControllerBase
    {
        private readonly IConnectionService _connectionService;
        public ConnectionController(IConnectionService iconnectionService)
        {

            _connectionService = iconnectionService;

        }


        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Connect(long id, long friendId)
        {         var userId =  HttpContext.Items["UserId"];
                Console.WriteLine(userId);
            try
            {
                var data = await _connectionService.FollowAUser(id, friendId);
                return Ok(data);

            }
            catch (AlreadyConnectionException alce)
            {
                return Ok(alce.Message);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }

        [HttpGet]
        public async Task<ActionResult<UserResponseDto>> GetFollowers(long id)
        {   
            try
            {
                var data = await _connectionService.GetFollowers(id);
                return Ok(data);

            }
            catch (AlreadyConnectionException alce)
            {
                return Ok(alce.Message);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }



         [HttpGet("/followings/{id}")]
        public async Task<ActionResult<UserResponseDto>> GetFollowings([FromRoute]long id)
        {

            try
            {
                var data = await _connectionService.GetFollowings(id);
                return Ok(data);

            }
            catch (AlreadyConnectionException alce)
            {
                return Ok(alce.Message);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete]

        public async Task<ActionResult<UserResponseDto>> Delete(long id,long friendId)
        {

                try
                {
                    var data  = await _connectionService.RemoveFriend(id,friendId);
                    return Ok(data);
                }
                catch (Exception e)
                {
                    
                    return NotFound(e.Message);
                }


        }





    }




}