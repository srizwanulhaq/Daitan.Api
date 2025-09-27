using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Data.Models
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
    }
}
