using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Data.Dao.Settings
{
    public class CompanyDao
    {
        public string CompanyName { get; set; }
        public double? EmissionFactor { get; set; }
    }

    public class DepartmentDao
    {
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class MachineDao
    {
        public string MachineId { get; set; }
        public string MachineName { get; set; }
        public DateTime CreatedDate { get; set; }
    }



}
