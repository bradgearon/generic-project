using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GenericProject.Core.Data;
using GenericProject.Core.Model;

namespace GenericProject.Web.ViewModels
{
    public class UserVm
    {
        public long Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Title { get; set; }

        public string EmailAddress { get; set; }

        public IEnumerable<RoleVm> Roles { get; set; }

        public static UserVm Project(User user)
        {
            if (user == null) return null;
            var userVm = new UserVm().InjectFrom(user);
            userVm.Roles = user.Roles.EmptyIfNull().Select(RoleVm.Project);
            return userVm;
        }
    }
}