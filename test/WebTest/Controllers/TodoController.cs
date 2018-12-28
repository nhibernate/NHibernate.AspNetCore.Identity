using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using WebTest.Models;

namespace WebTest.Controllers {

    [Route("api/[controller]")]
    public class TodoController : Controller {

        private static IList<ToDoItemModel> items;

        public TodoController() {
            if (items == null) {
                items = new List<ToDoItemModel> {
                    new ToDoItemModel {
                        Id = 1,
                        Title = "To do item 1",
                        Completed = false
                    },
                    new ToDoItemModel {
                        Id = 2,
                        Title = "To do item 2",
                        Completed = false
                    },
                    new ToDoItemModel {
                        Id = 3,
                        Title = "To do item 3",
                        Completed = false
                    }
                };
            }
        }

        [HttpGet("")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "todo.read")]
        public ActionResult<List<ToDoItemModel>> GetAll() {
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "todo.read")]
        public ActionResult<ToDoItemModel> GetById(int id) {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null) {
                return NotFound();
            }
            return item;
        }

        [HttpPost("")]
        [Authorize(Roles = "todo.save")]
        public ActionResult<ToDoItemModel> Save([FromBody]ToDoItemModel item) {
            item.Id = items.Max(i => i.Id) + 1;
            items.Add(item);
            // return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
            return item;
            // return Ok(item);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Authorize(Roles = "todo.update")]
        public ActionResult<ToDoItemModel> Update(int id, [FromBody]ToDoItemModel item) {
            var todo = items.FirstOrDefault(i => i.Id == id);
            if (todo == null) {
                return NotFound();
            }
            todo.Title = item.Title;
            todo.Description = item.Description;
            todo.Completed = item.Completed;
            //
            return todo;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [Authorize(Roles = "todo.delete")]
        public IActionResult Delete(int id) {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item != null) {
                items.Remove(item);
            }
            return NoContent();
        }
    }

}
