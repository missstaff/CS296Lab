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
    public class ForumApiController : ControllerBase
    {
        private readonly IForums repo;

        public ForumApiController(IForums r)
        {
            repo = r;
        }
        // GET: api/<ForumApiController>
        public async Task<ActionResult<IEnumerable<ForumPosts>>> GetPosts()
        {
            // converting to a list
            return await repo.Posts.ToListAsync();
        }

        // GET api/<ForumApiController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ForumPosts>> GetPost(int id)
        {
            var post = await repo.Posts.FirstAsync(e => e.PostID == id);

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
            await repo.AddPostAsync(post);
            await repo.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.PostID }, post);
        }

        // PUT api/<ForumApiController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ForumPosts>> PutPost(int id, ForumPosts post)
        {
            if (id != post.PostID)
            {
                return NotFound();
            }
            repo.UpdatePostAsync(post, id);
            await repo.SaveChangesAsync();

            return post;
        }

        // DELETE api/<ForumApiController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ForumPosts>> DeletePost(int id)
        {
            var post = await repo.Posts.FirstAsync(e => e.PostID == id);
            if (post == null)
            {
                return NotFound();
            }

            await repo.DeletePostAsync(id);
            await repo.SaveChangesAsync();

            return post;
        }

        private bool PostExists(int id)
        {
            return repo.Posts.Any(e => e.PostID == id);
        }
    }
}
