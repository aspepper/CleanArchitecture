using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AcadesArchitecturePattern.Infra.Data.Services
{
    public class TaskService : ITaskService
    {
        private readonly AcadesArchitecturePatternSqlServerContext _ctx;

        public TaskService(AcadesArchitecturePatternSqlServerContext ctx)
        {
            _ctx = ctx;
        }



        // Commands:
        public void Add(Domain.Entities.ToDoTask user)
        {
            _ctx.Tasks.Add(user);
            _ctx.SaveChanges();
        }

        public void Delete(Guid? id)
        {
            _ctx.Tasks.Remove(SearchById(id));
            _ctx.SaveChanges();
        }

        public void DeleteTasksByIdList(Guid? idList)
        {
            var tasks = _ctx.Tasks.Where(x => x.IdList == idList);
            _ctx.Tasks.RemoveRange(tasks);
            _ctx.SaveChanges();
        }

        public void Update(Domain.Entities.ToDoTask task)
        {
            _ctx.Entry(task).State = EntityState.Modified;
            _ctx.SaveChanges();
        }



        // Queries:
        public IEnumerable<Domain.Entities.ToDoTask> List()
        {
            return _ctx.Tasks
                .AsNoTracking()
                //.Include(x => x.Lists)
                .ToList();
        }

        public Domain.Entities.ToDoTask SearchById(Guid? id)
        {
            return _ctx.Tasks.FirstOrDefault(x => x.Id == id);
        }
    }
}
