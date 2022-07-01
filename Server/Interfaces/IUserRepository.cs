using Server.Models;

namespace Server.Interfaces
{
    public interface IUserRepository
    {
        int CreateUser(User user);
        User GetUserById(int userId);
        void DeleteUser(int userId);
        void UpdateUser(User user);
    }
}
