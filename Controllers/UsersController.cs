using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookapp.Core.IConfiguration;
using bookapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bookapp.Controllers
{

    [Route("api/[controller]/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _uwork;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUnitOfWork uwork, ILogger<UsersController> logger)
        {
            this._uwork = uwork;
            this._logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                await _uwork.Users.Add(user);
                await _uwork.CompleteChangesAsync();
                return CreatedAtAction(nameof(GetItem), new { id = user.Id }, user);
            }
            return new JsonResult("something went wrong")
            {
                StatusCode = 500
            };
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<User>> GetItem(Guid id)
        {
            var result = await _uwork.Users.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var result = await _uwork.Users.GetAll();
            return Ok(result);
        }
        [HttpPost("{id}")]
        public async Task<ActionResult<User>> UpsertItem(Guid id, User user)
        {
            if (id != user.Id) return BadRequest();
            await _uwork.Users.Upsert(user);
            await _uwork.CompleteChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _uwork.Users.GetById(id);
            if (item == null) return BadRequest();
            await _uwork.Users.Delete(id);
            await _uwork.CompleteChangesAsync();
            return Ok(new
            {
                message = "user deleted",
                user = item
            });
        }
    }
}