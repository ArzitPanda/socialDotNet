using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sample_one.models.dto;
using sample_one.services.Comments;

namespace sample_one.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "user")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
        }

        [HttpPost("{Cid}/subcomment")]
        public async Task<IActionResult> AddSubComment(long Cid, [FromBody] SubCommentRequestDto request)
        {
            try
            {
                var userId = GetUserId(); // Assuming you have a method to get the user ID
                var subComment = await _commentService.AddSubComment(Cid, userId, request);
                return Ok(subComment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Cid}/subcomment/{scid}")]
        public async Task<IActionResult> UpdateSubComment(long Cid, long scid, [FromBody] SubCommentRequestDto request)
        {
            try
            {
                var userId = GetUserId(); // Assuming you have a method to get the user ID
                var subComment = await _commentService.UpdateSubComment(Cid, userId, request);
                return Ok(subComment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Cid}/subcomments")]
        public async Task<IActionResult> GetAllSubComments(long Cid)
        {
            try
            {
                var subComments = await _commentService.GetAllSubComments(Cid);
                return Ok(subComments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Cid}/subcomment/{scid}")]
        public async Task<IActionResult> DeleteSubComment(long scid)
        {
            try
            {
                var userId = GetUserId(); // Assuming you have a method to get the user ID
                await _commentService.DeleteSubComment(scid, userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // This is a placeholder method; replace it with your actual logic to get the user ID
        private long GetUserId()
        {   
            return (long)HttpContext.Items["UserId"];
           
            // return 123; //
        }
    }
}
