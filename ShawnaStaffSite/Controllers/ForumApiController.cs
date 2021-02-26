using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shawna_Staff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shawna_Staff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumApiController : ControllerBase
    {
        private readonly ForumContext _context;

        public ForumApiController(ForumContext context)
        {
            _context = context;
        }
        // GET: api/<ForumApiController>
        public async Task<ActionResult<IEnumerable<ForumPosts>>> GetPosts()
        {
            // converting to a list
            return await _context.ForumPosts.ToListAsync();
        }

        // GET api/<ForumApiController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ForumPosts>> GetPost(int id)
        {
            var post = await _context.ForumPosts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // POST api/<ForumApiController>
        [HttpPost]
        public async Task<ActionResult<ForumPosts>> Post(ForumPosts post)
        {
            _context.ForumPosts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.PostID }, post);
        }

        // PUT api/<ForumApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, ForumPosts post)
        {
            if (id != post.PostID)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<ForumApiController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ForumPosts>> DeletePost(int id)
        {
            var post = await _context.ForumPosts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.ForumPosts.Remove(post);
            await _context.SaveChangesAsync();

            return post;
        }

        private bool PostExists(int id)
        {
            return _context.ForumPosts.Any(e => e.PostID == id);
        }
    }
}
