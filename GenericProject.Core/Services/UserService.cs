using System;
using System.Linq;
using GenericProject.Core.Injection;
using GenericProject.Core.Model;
using GenericProject.Data;

namespace GenericProject.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWorkFactory _UnitOfWorkFactory;
        private readonly IRepositoryFactory<User> _UserRepositoryFactory;
        private readonly IRepositoryFactory<Role> _RoleRepositoryFactory;

        public UserService(IUnitOfWorkFactory unitOfWorkFactory, IRepositoryFactory<User> userRepositoryFactory, IRepositoryFactory<Role> roleRepositoryFactory)
        {
            _UnitOfWorkFactory = unitOfWorkFactory;
            _UserRepositoryFactory = userRepositoryFactory;
            _RoleRepositoryFactory = roleRepositoryFactory;
        }

        public IQueryable<User> GetAllUsers()
        {
            var repo = GetRepo();

            return repo.AsQueryable();
        }

        public User GetUser(long userId)
        {
            var repo = GetRepo();

            return repo.Single(x => x.Id == userId);
        }

        public void Create(User user)
        {
            using (var unitOfWork = _UnitOfWorkFactory.StartNew())
            {
                var repo = _UserRepositoryFactory.GetRepository(unitOfWork);
                repo.Add(user);
                unitOfWork.SubmitChanges();
            }
        }

        public void UpdateUser(User user)
        {
            var repo = GetRepo();
            var storedUser = repo.Single(x => x.Id == user.Id);
            //storedUser.Name = user.Name;
            //storedUser.Title = user.Title;
            //storedUser.SystemRole = user.SystemRole;
            //storedUser.Name = user.EmailAddress;
        }

        public Role GetRole(long roleId)
        {
            return GetRoleRepo().FirstOrDefault(r => r.Id == roleId);
        }

        public IQueryable<Role> GetAllRoles()
        {
            return GetRoleRepo().GetAll().AsQueryable();
        }


        public User GetUserByUserName(string userName)
        {
            var repo = GetRepo();
            return repo.FirstOrDefault(x => x.UserName == userName);
        }

        private IRepository<User> GetRepo()
        {
            return Injector.Get<IRepository<User>>();
        }

        private IRepository<Role> GetRoleRepo()
        {
            return Injector.Get<IRepository<Role>>();
        }
    }
}