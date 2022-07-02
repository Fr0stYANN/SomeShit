using Server.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Business.Interfaces
{
    public interface IUserRepository
    {
        int Create(User user);
        User GetById(int userId);
        void Delete(int userId);
        void Update(User user);
        User GetByEmail(string email);
    }
}
