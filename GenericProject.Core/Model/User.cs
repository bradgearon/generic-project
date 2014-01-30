using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GenericProject.Core.Model
{
    public class User : AuditedModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Title { get; set; }

        public string EmailAddress { get; set; }

        public IList<Role> Roles { get; set; }
    }
}