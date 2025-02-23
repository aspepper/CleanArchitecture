using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Infra.Data.SQLite.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AcadesArchitecturePattern.Infra.Data.SQLite.Services
{
    public class UserService(AcadesArchitecturePatternSQLiteContext ctx) : IUserService
    {
        private readonly AcadesArchitecturePatternSQLiteContext ctx = ctx;

        // Commands:
        public void Add(User user)
        {
            ctx.Users.Add(user);
            ctx.SaveChanges();
        }

        public void Delete(Guid? id)
        {
            var user = SearchById(id);
            if (user != null)
            {
                ctx.Users.Remove(user);
                ctx.SaveChanges();
            }
        }



        // Queries:
        public IEnumerable<User> List()
        {
            return [.. ctx.Users.AsNoTracking()];
        }

        public User? SearchByEmail(string email)
        {
            return ctx.Users.FirstOrDefault(x => x.Email == email);
        }

        public User? SearchByUserName(string userName)
        {
            return ctx.Users.FirstOrDefault(x => x.UserName == userName);
        }

        public User? SearchById(Guid? id)
        {
            return ctx.Users.FirstOrDefault(x => x.Id == id);
        }
    }
}