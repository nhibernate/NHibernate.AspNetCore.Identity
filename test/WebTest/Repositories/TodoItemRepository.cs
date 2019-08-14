using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using NHibernate;
using NHibernate.Linq;
using WebTest.Entities;
using WebTest.Models;

namespace WebTest.Repositories {

    public class TodoItemRepository : ITodoItemRepository, IDisposable {

        private ISessionFactory factory;
        private IMapper mapper;
        private bool disposed;

        public TodoItemRepository(
            ISessionFactory factory,
            IMapper mapper
        ) {
            this.factory = factory;
            this.mapper = mapper;
        }

        ~TodoItemRepository() {
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
        }

        public void Dispose(bool disposing) {
            if (disposed) {
                return;
            }
            if (disposing) {
                factory = null;
                mapper = null;
            }
            //
            disposed = true;
        }

        public async Task CreateAsync(TodoItemModel model) {
            var entity = mapper.Map<TodoItem>(model);
            using (var session = factory.OpenSession()) {
                var user = await session.LoadAsync<AppUser>(model.UserId);
                if (user == null) {
                    throw new InvalidOperationException("user is null!");
                }
                entity.User = user;
                await session.SaveAsync(entity);
                await session.FlushAsync();
                mapper.Map(entity, model);
            }
        }

        public async Task DeleteAsync(long id) {
            using (var session = factory.OpenSession()) {
                var entity = await session.LoadAsync<TodoItem>(id);
                if (entity != null) {
                    await session.DeleteAsync(entity);
                }
            }
        }

        public async Task<TodoItemModel> GetByIdAsync(long id) {
            using (var session = factory.OpenSession()) {
                var entity = await session.LoadAsync<TodoItem>(id);
                if (entity == null) {
                    return null;
                }
                var model = mapper.Map<TodoItemModel>(entity);
                return model;
            }
        }

        public async Task<PagedResultModel<TodoItemModel>> SearchAsync(TodoItemSearchModel model) {
            using (var session = factory.OpenSession()) {
                var result = new PagedResultModel<TodoItemModel>();
                var query = session.Query<TodoItem>();
                // add custom query here;
                if (model.Completed.HasValue) {
                    var val = model.Completed.Value;
                    query = query.Where(x => x.Completed == val);
                }
                if (!string.IsNullOrEmpty(model.UserId)) {
                    query = query.Where(x => x.User.Id == model.UserId);
                }
                //
                result.Total = await query.LongCountAsync();
                result.Data = await query.ProjectTo<TodoItemModel>(mapper.ConfigurationProvider)
                    .Skip(model.Skip)
                    .Take(model.Take)
                    .ToListAsync();
                result.Skip = model.Skip;
                result.Take = model.Take;
                return result;
            }
        }

        public async Task UpdateAsync(long id, TodoItemModel model) {
            using (var session = factory.OpenSession()) {
                var entity = await session.LoadAsync<TodoItem>(id);
                if (entity == null) {
                    throw new InvalidOperationException(
                        $"TodoItem {id} does not exists!"
                    );
                }
                mapper.Map(model, entity);
                await session.UpdateAsync(entity);
                await session.FlushAsync();
            }
        }

    }

}
