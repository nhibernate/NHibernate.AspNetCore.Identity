using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebTest.Entities;
using WebTest.Models;
using WebTest.Repositories;

namespace WebTest.Controllers;

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
        this.repo = repo ?? throw new ArgumentNullException(nameof(repo));
        this.userMgr = userMgr ?? throw new ArgumentNullException(nameof(userMgr));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    protected override void Dispose(
        bool disposing
    ) {
        if (disposing) {
            // dispose managed resource here.
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
            logger!.LogError(ex, "Can not search todo items.");
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
            logger?.LogError(ex, "Can not read todo items.");
            return StatusCode(500);
        }
    }

    [HttpPost("")]
    public async Task<ActionResult<TodoItemModel>> Save(
        [FromBody]TodoItemModel model
    ) {
        try {
            var identity = User.Identity;
            if (identity == null) {
                return Unauthorized();
            }
            var username = identity.Name;
            if (string.IsNullOrEmpty(username)) {
                return Unauthorized();
            }
            var user = await userMgr.FindByNameAsync(username);
            model.UserId = user?.Id ?? string.Empty;
            model.UserName = user?.UserName ?? string.Empty;
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
