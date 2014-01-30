using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GenericProject.Core.Data;
using GenericProject.Core.Model;

namespace GenericProject.Web.ViewModels
{
    public class RoleVm
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public static RoleVm Project(Role role)
        {
            if (role == null) return null;
            return new RoleVm().InjectFrom(role);
        }
    }
}