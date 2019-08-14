using System;
using System.Threading.Tasks;
using WebTest.Models;

namespace WebTest.Repositories {

    public interface ITodoItemRepository {

        Task CreateAsync(TodoItemModel model);

        Task DeleteAsync(long id);

        Task<TodoItemModel> GetByIdAsync(long id);

        Task UpdateAsync(long id, TodoItemModel model);

        Task<PagedResultModel<TodoItemModel>> SearchAsync(TodoItemSearchModel model);

    }

}
