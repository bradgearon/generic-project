using System.Linq;
using GenericProject.Core.Model;

namespace GenericProject.Core.Services
{
    public interface IUserService
    {
        IQueryable<User> GetAllUsers();

        User GetUser(long userId);

        User GetUserByUserName(string userName);

        void Create(User user);

        void UpdateUser(User user);

        Role GetRole(long roleId);

        IQueryable<Role> GetAllRoles();
    }
}