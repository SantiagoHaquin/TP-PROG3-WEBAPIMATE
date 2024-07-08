using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        User GetByUserEmail(string email);
        IEnumerable<User> GetUsersByType(string userType);
    }
}
