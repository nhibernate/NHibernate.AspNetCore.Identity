using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebTest.Entities;
using WebTest.Models;
using WebTest.Repositories;

namespace WebTest.Controllers {

    [Route("api/[controller]")]
    public class TodoController : Controller {

        private ITodoItemRepository repo;
        private UserManager<AppUser> userMgr;
        private ILogger<TodoController> logger;

        public TodoController(
            ITodoItemRepository repo,
            UserManager<AppUser> userMgr,
            ILogger<TodoController> logger
        ) {
            if (repo == null) {
                throw new ArgumentNullException(nameof(repo));
            }
            if (userMgr == null) {
                throw new ArgumentNullException(nameof(userMgr));
            }
            if (logger == null) {
                throw new ArgumentNullException(nameof(logger));
            }
            this.repo = repo;
            this.userMgr = userMgr;
            this.logger = logger;
        }

        protected override void Dispose(
            bool disposing
        ) {
            if (disposing) {
                repo = null;
                userMgr = null;
                logger = null;
            }
            base.Dispose(disposing);
        }

        [HttpGet("")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PagedResultModel<TodoItemModel>>> Search(
            TodoItemSearchModel model
        ) {
            try {
                var result = await repo.SearchAsync(model);
                return result;
            }
            catch (Exception ex) {
                logger.LogError(ex, "Can not search todo items.");
                return StatusCode(500);
            }
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TodoItemModel>> GetById(long id) {
            try {
                var model = await repo.GetByIdAsync(id);
                if (model == null) {
                    return NotFound();
                }
                return model;
            }
            catch (Exception ex) {
                logger.LogError(ex, "Can not read todo items.");
                return StatusCode(500);
            }
        }

        [HttpPost("")]
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
                logger.LogError(ex, "Can not save todo items.");
                return StatusCode(500);
            }
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<TodoItemModel>> Update(
            [FromRoute]long id,
            [FromBody]TodoItemModel model
        ) {
            try {
                await repo.UpdateAsync(id, model);
                return model;
            }
            catch (Exception ex) {
                logger.LogError(ex, "Can not update todo items.");
                return StatusCode(500);
            }
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id) {
            try {
                await repo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex) {
                logger.LogError(ex, "Can not delete todo items.");
                return StatusCode(500);
            }
        }

    }

}
