using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericProject.Core.Model
{
    public abstract class LookupModel : AuditedModel
    {
        [Required]
        public string Name { get; set; }

        public override string ToString() {  return Name; }
    }
}
