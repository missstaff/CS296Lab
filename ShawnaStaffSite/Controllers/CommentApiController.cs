using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shawna_Staff.Models;
using Shawna_Staff.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shawna_Staff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentApiController : ControllerBase
    {
        private readonly IComment repo;

        public CommentApiController(IComment r)
        {
            repo = r;
        }

        // GET: api/<CommentApiController>
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            // converting to a list
            return await repo.Comments.ToListAsync();
        }

        // GET api/<CommentApiController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await repo.Comments.FirstAsync(e => e.ID == id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // POST api/<CommentApiController>
        [HttpPost]
        public async Task<ActionResult<Comment>> Comment(Comment comment)
        {
            await repo.AddCommentAsync(comment);
            await repo.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.ID }, comment);
        }

        // PUT api/<CommentsApiController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Comment>> PutComment(Comment comment, int id)
        {
            if (id != comment.ID)
            {
                return NotFound();
            }
            repo.UpdatCommentAsync(comment, id);
            await repo.SaveChangesAsync();

            return comment;
        }

        // DELETE api/<CommentsApiController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> DeleteComment(int id)
        {
            var comment = await repo.Comments.FirstAsync(e => e.ID == id);
            if (comment == null)
            {
                return NotFound();
            }

            await repo.DeleteCommentAsync(id);
            await repo.SaveChangesAsync();

            return comment;
        }

        private bool CommentExists(int id)
        {
            return repo.Comments.Any(e => e.ID == id);
        }
    }
}
