using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AcadesArchitecturePattern.Infra.Data.Services
{
    public class ToDoListService : IToDoListService
    {
        private readonly AcadesArchitecturePatternSqlServerContext _ctx;

        public ToDoListService(AcadesArchitecturePatternSqlServerContext ctx)
        {
            _ctx = ctx;
        }



        // Commands:
        public void Add(ToDoList user)
        {
            _ctx.ToDoLists.Add(user);
            _ctx.SaveChanges();
        }

        public void Delete(Guid? id)
        {
            _ctx.ToDoLists.Remove(SearchById(id));
            _ctx.SaveChanges();
        }

        public void DeleteListsByIdUser(Guid? idUser)
        {
            var lists = _ctx.ToDoLists.Where(x => x.IdUser == idUser);
            _ctx.ToDoLists.RemoveRange(lists);
            _ctx.SaveChanges();
        }



        // Queries:
        public IEnumerable<ToDoList> List()
        {
            return _ctx.ToDoLists
                .AsNoTracking()
                //.Include(x => x.Carts)
                .ToList();
        }

        public ToDoList SearchById(Guid? id)
        {
            return _ctx.ToDoLists.FirstOrDefault(x => x.Id == id);
        }
    }
}
