﻿using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AcadesArchitecturePattern.Infra.Data.Services
{
    public class TaskService(AcadesArchitecturePatternSqlServerContext ctx) : ITaskService
    {
        private readonly AcadesArchitecturePatternSqlServerContext ctx = ctx;



        // Commands:
        public void Add(Domain.Entities.ToDoTask user)
        {
            ctx.Tasks.Add(user);
            ctx.SaveChanges();
        }

        public void Delete(Guid? id)
        {
            var toDoTask = SearchById(id);
            if (toDoTask != null)
            {
                ctx.Tasks.Remove(toDoTask);
                ctx.SaveChanges();
            }
        }

        public void DeleteTasksByIdList(Guid? idList)
        {
            var tasks = ctx.Tasks.Where(x => x.IdList == idList);
            ctx.Tasks.RemoveRange(tasks);
            ctx.SaveChanges();
        }

        public void Update(Domain.Entities.ToDoTask task)
        {
            ctx.Entry(task).State = EntityState.Modified;
            ctx.SaveChanges();
        }



        // Queries:
        public IEnumerable<Domain.Entities.ToDoTask> List()
        {
            return [.. ctx.Tasks.AsNoTracking()];
        }

        public Domain.Entities.ToDoTask? SearchById(Guid? id)
        {
            return ctx.Tasks.FirstOrDefault(x => x.Id == id);
        }
    }
}
