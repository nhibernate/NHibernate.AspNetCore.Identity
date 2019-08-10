using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTest.Entities;
using WebTest.Models;
using WebTest.Repositories;

namespace WebTest.Controllers {

    [Route("api/[controller]")]
    public class TodoController : Controller {

        private ITodoItemRepository repo;
        private UserManager<AppUser> userMgr;

        public TodoController(
            ITodoItemRepository repo,
            UserManager<AppUser> userMgr
        ) {
            if (repo == null) {
                throw new ArgumentNullException(nameof(repo));
            }
            if (userMgr == null) {
                throw new ArgumentNullException(nameof(userMgr));
            }
            this.repo = repo;
            this.userMgr = userMgr;
        }

        protected override void Dispose(
            bool disposing
        ) {
            if (disposing) {
                repo = null;
                userMgr = null;
            }
            base.Dispose(disposing);
        }

        [HttpGet("")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "todo.read")]
        public async Task<ActionResult<PagedResultModel<TodoItemModel>>> Search(
            TodoItemSearchModel model
        ) {
            try {
                var result = await repo.SearchAsync(model);
                return result;
            }
            catch (Exception ex) {
                return StatusCode(500);
            }
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "todo.read")]
        public async Task<ActionResult<TodoItemModel>> GetById(long id) {
            try {
                var model = await repo.GetByIdAsync(id);
                if (model == null) {
                    return NotFound();
                }
                return model;
            }
            catch (Exception ex) {
                return StatusCode(500);
            }
        }

        [HttpPost("")]
        [Authorize(Roles = "todo.save")]
        public async Task<ActionResult<TodoItemModel>> Save(
            [FromBody]TodoItemModel model
        ) {
            try {
                var user = await userMgr.FindByNameAsync(User.Identity.Name);
                model.UserId = user.Id;
                model.UserName = user.UserName;
                await repo.CreateAsync(model);
                return model;
            }
            catch (Exception ex) {
                return StatusCode(500);
            }
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "todo.update")]
        public async Task<ActionResult<TodoItemModel>> Update(
            [FromRoute]long id,
            [FromBody]TodoItemModel model
        ) {
            try {
                await repo.UpdateAsync(id, model);
                return model;
            }
            catch (Exception ex) {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(204)]
        [Authorize(Roles = "todo.delete")]
        public async Task<IActionResult> Delete(long id) {
            try {
                await repo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex) {
                return StatusCode(500);
            }
        }

    }

}
