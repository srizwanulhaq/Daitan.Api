using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Data.Models
{
    public class Machine : BaseEntity
    {
        public string Name { get; set; }
        public string DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Departments { get; set; }
    }
}
