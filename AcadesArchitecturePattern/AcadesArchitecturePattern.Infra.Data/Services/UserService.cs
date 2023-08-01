using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Infra.Data.Repositories
{
    public class UserService : IUserService
    {
        private readonly AcadesArchitecturePatternSqlServerContext _ctx;

        public UserService(AcadesArchitecturePatternSqlServerContext ctx)
        {
            _ctx = ctx;
        }



        // Commands:
        public void Add(User user)
        {
            _ctx.Users.Add(user);
            _ctx.SaveChanges();
        }

        public void Delete(Guid? id)
        {
            _ctx.Users.Remove(SearchById(id));
            _ctx.SaveChanges();
        }



        // Queries:
        public IEnumerable<User> List()
        {
            return _ctx.Users
                .AsNoTracking()
                //.Include(x => x.Carts)
                .ToList();
        }

        public User SearchByEmail(string email)
        {
            return _ctx.Users.FirstOrDefault(x => x.Email == email);
        }

        public User SearchByUserName(string userName)
        {
            return _ctx.Users.FirstOrDefault(x => x.UserName == userName);
        }

        public User SearchById(Guid? id)
        {
            return _ctx.Users.FirstOrDefault(x => x.Id == id);
        }
    }
}
