using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UserRepositoryEf : EfRepository<User>, IUserRepository
    {
        public UserRepositoryEf(ApplicationDbContext context) : base(context) { }

        public User GetByUsername(string username)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == username);
        }

        public IEnumerable<User> GetUsersByType(string userType)
        {
            return _context.Users.Where(u => u.UserType == userType).ToList();
        }

    }
}


