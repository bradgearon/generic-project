using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GenericProject.Core.Model
{
    public class Role : LookupModel
    {
        public IList<User> Users { get; set; }
    }
}