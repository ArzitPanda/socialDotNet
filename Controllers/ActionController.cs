using Microsoft.AspNetCore.Mvc;
using sample_one.models;
using sample_one.models.dto;
using sample_one.services.post;

namespace sample_one.Controllers
{

[ApiController]
[Route("/api/[controller]")]
public class ActionController : ControllerBase
{
    private readonly IPostInterAction _postInteraction;
    public ActionController(IPostInterAction postInterAction)
    {   
        _postInteraction = postInterAction;

    }
    [HttpPost("comment/{postId}/{InteractorId}")]
    public async Task<ActionResult<Comment>> AddCommentPost(long postId, long InteractorId,[FromBody] CommentRequestDto comment)
    {
            try
            {
                
           var data =  await _postInteraction.AddComment(postId,InteractorId ,comment);
           return Ok(data);
            }
            catch (Exception e) 
            {
                return BadRequest(e.Message);
                
            }



    }

    [HttpPost("like/{postId}/{uid}")]
    public async Task<ActionResult<long>> AddLikeToPost(long postId,long uid)
    {
        try
        {
          long data = await   _postInteraction.AddLikeToPost(postId,uid);
          return Ok(data);

        }
        catch(AlreadyLikeException ale)
        {
            return Ok("already liked");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);

        }



    } 

       [HttpDelete("like/remove/{postId}/{uid}")]
    public async Task<ActionResult<long>> RemoveLikeToPost(long postId,long uid)
    {
        try
        {
          long data = await  _postInteraction.RemoveLikeToPost(postId,uid);
          return Ok(data);

        }
        catch(AlreadyLikeException ale)
        {
            return Ok("already liked");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);

        }



    } 


    [HttpGet("like/{postId}")]
    public async Task<ActionResult<List<UserResponseDto>>> GetLikeUsers(long postId)
    {
        try
        {
           List<UserResponseDto> users =   await  _postInteraction.GetLikesByPost(postId);
           return Ok(users);
        }
        catch (Exception e)
        {   
            
            return BadRequest(e.Message);
        }

    }



    [HttpGet("comment/{postId}")]
    public async Task<ActionResult<List<Comment>>> GetCommentsByPost(long postId)
    {


             
             var data  =await _postInteraction.GetCommentsByPost(postId);

             return Ok(data);

    }




    [HttpPost("{cId}/comment/{uid}")]
    public async Task<ActionResult<long>> AddLikeToComment(long cId,long uid)
    {
        try
        {
         long data = await   _postInteraction.AddLikeToComment(cId,uid);
          return Ok(data);

        }
        catch(Exception e)
        {
            return BadRequest(e.Message);

        }



    } 

    [HttpDelete("{cId}/comment/{uid}")]
    public async Task<ActionResult<long>> RemoveLikeToComment(long cid,long uid)
    {
          try
        {
         long data = await   _postInteraction.RemoveLikeToComment(cid,uid);
          return Ok(data);

        }
        catch(Exception e)
        {
            return BadRequest(e.Message);

        }


    }



}

}