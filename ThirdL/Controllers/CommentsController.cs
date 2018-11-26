using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThirdL.Data;
using ThirdL.Models;

namespace ThirdL.Controllers
{
    [Produces("application/json")]
    [Route("api/Patients/{PatientId}/Comments")]
    public class CommentsController : Controller
    {
        private readonly PatientContext _patientContext;
        
        public CommentsController(PatientContext patientContext)
        {
            _patientContext = patientContext;
        }


        [HttpGet]
        public IEnumerable<Comment> GetPatientComments([FromRoute] int patientId)
        {
            return _patientContext.Patients.SingleOrDefault(p => p.Id == patientId)?.Comments;
        }

        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] Comment comment,[FromRoute] int patientId)
        {
            comment.PatientId = patientId;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _patientContext.Comments.Add(comment);
          
             await _patientContext.SaveChangesAsync();
            
            return CreatedAtAction("GetCommentById", new { id = comment.Id }, comment);
        }
        
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int CommentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workout = await _patientContext.Comments.SingleOrDefaultAsync(m => m.Id == CommentId);

            if (workout == null)
            {
                return NotFound();
            }

            return Ok(workout);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _patientContext.Comments.SingleOrDefaultAsync(c => c.Id == id);
            if (comment == null)
            {
                return NotFound();
            }
            _patientContext.Comments.Remove(comment);
            await _patientContext.SaveChangesAsync();

            return Ok(comment);
        }
      
    }
}