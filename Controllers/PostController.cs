using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using sample_one.hubs;
using sample_one.models;
using sample_one.models.dto;
using sample_one.services.Connection;
using sample_one.services.post;
using sample_one.utility;

namespace sample_one.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(Roles = "user")]
    public class PostController:ControllerBase
    {
        private readonly IPostService _service;
        private readonly IConnectionService _connectionService;
        private readonly IHubContext<NotificationHub> _SocketContext;

        private readonly IFileUploader _fileUploader;
        public PostController(IPostService postService, IHubContext<NotificationHub> context,IConnectionService connectionService) 
        {
                _service = postService;
                _SocketContext = context;
                _connectionService= connectionService;

        }


        [HttpPost]
        public async Task<ActionResult<Post>> AddPost(IFormFile _file, [FromQuery]PostRequestDto post)
        {
                try
                {
                      long userId = (long) HttpContext.Items["UserId"];

                if(userId != post.UserId)
                {
                    return Unauthorized("invalid token");
                }
                 var data = await _service.AddPost(post,_file);
                 var followersList = await _connectionService.GetFollowers(post.UserId);
                    List<string> ids = new List<string>();
                        foreach (var item in followersList)
                        {   
                            ids.Add(item.Id.ToString());
                            
                        }
                         string postJson = JsonConvert.SerializeObject(data);

                 
                    await  _SocketContext.Clients.Groups(ids).SendAsync("ReceivePostNotification",postJson);


                    return Ok(data);
                }
                catch (InvalidUserException exception)
                {
                    
                   return  Unauthorized(exception.Message);
                }



        }






        [HttpDelete("{pid}")]
    public async Task<IActionResult> DeletePost(long pid)
    {
        try
        {
             long userId = (long) HttpContext.Items["UserId"];
            var affectedRows = await _service.DeletePost(userId, pid);
            if (affectedRows > 0)
            {
                return NoContent();
            }
            else
            {
                return NotFound("Post not found.");
            }
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllPosts(long id)
    {
        try
        {
            var posts = await _service.GetAllPost(id);
            return Ok(posts);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("getPost/{pid}")]
    public async Task<IActionResult> GetPostById(long pid)
    {

        long userId = (long) HttpContext.Items["UserId"];




        Console.WriteLine(userId);

        try
        {
            var post = await _service.GetPostById(userId, pid);
            return Ok(post);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    
    }

    [HttpPut("{pid}")]
    public async Task<IActionResult> UpdatePost(long pid, [FromBody] PostRequestDto updatedPost)
    {
        try
        {
            long userId = (long) HttpContext.Items["UserId"];
            var updatedPostResult = await _service.UpdatePost(userId, pid, updatedPost);
            return Ok(updatedPostResult);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    
    }




    }




}