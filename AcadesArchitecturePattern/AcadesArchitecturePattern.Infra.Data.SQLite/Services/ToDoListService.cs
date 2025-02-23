using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Infra.Data.SQLite.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AcadesArchitecturePattern.Infra.Data.SQLite.Services
{
    public class ToDoListService(AcadesArchitecturePatternSQLiteContext ctx) : IToDoListService
    {
        private readonly AcadesArchitecturePatternSQLiteContext ctx = ctx;



        // Commands:
        public void Add(ToDoList user)
        {
            ctx.ToDoLists.Add(user);
            ctx.SaveChanges();
        }

        public void Delete(Guid? id)
        {
            var toDoList = SearchById(id);
            if (toDoList != null)
            {
                ctx.ToDoLists.Remove(toDoList);
                ctx.SaveChanges();
            }
        }

        public void DeleteListsByIdUser(Guid? idUser)
        {
            var lists = ctx.ToDoLists.Where(x => x.IdUser == idUser);
            ctx.ToDoLists.RemoveRange(lists);
            ctx.SaveChanges();
        }



        // Queries:
        public IEnumerable<ToDoList> List()
        {
            return [.. ctx.ToDoLists.AsNoTracking()];
        }

        public ToDoList? SearchById(Guid? id)
        {
            return ctx.ToDoLists.FirstOrDefault(x => x.Id == id);
        }
    }
}
