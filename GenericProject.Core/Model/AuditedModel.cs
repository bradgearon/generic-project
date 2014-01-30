using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericProject.Core.Model
{
    public abstract class AuditedModel : ModelBase
    {
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public string UpdatedBy { get; set; }

        protected AuditedModel()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            CreatedBy = "system";
            UpdatedBy = "system";
        }
    }
}