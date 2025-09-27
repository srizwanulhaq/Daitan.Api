using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Data.Dto.Settings
{
    public class CompanyDto
    {
        public string CompanyName { get; set; }
        public double EmissionFactor { get; set; }
        public string? UserId { get; set; }
    }

    public class DepartmentDto
    {
        public string DepartmentId { get; set; }
        public string Name { get; set; }
    }

    public class MachineDto
    {
        public string MachineId { get; set; }
        public string Name { get; set; }
        public string DepartmentId { get; set; }
    }



}
