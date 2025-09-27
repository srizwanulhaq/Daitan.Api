using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Data.Models
{
    public class Gateway : BaseEntity
    {
        public string? Name { get; set; }
        public string? ClientId { get; set; }
        public string? IPAddress { get; set; }
        public string? KernelVersion { get; set; }
        public string? SerialNumber { get; set; }
        public string? FirmwareVersion { get; set; }
        public string? Uptime { get; set; }
        public string? LocalTime { get; set; }
    }


}
