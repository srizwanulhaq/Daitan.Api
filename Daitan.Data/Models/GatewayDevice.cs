using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Data.Models
{
    public class GatewayDevice : BaseEntity
    {
        public string? Name { get; set; }
        public string? DeviceReferenceId { get; set; }
        public double? PhaseVoltageUA { get; set; }
        public double? PhaseVoltageUB { get; set; }
        public double? PhaseVoltageUC { get; set; }
        public double? LineVoltageUAB { get; set; }
        public double? LineVoltageUBC { get; set; }
        public double? LineVoltageUCA { get; set; }
        public double? Frequency { get; set; }
        public string? GatewayId { get; set; }

        [ForeignKey("GatewayId")]
        public Gateway Gateways { get; set; }
        public string GatewayJson { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }

    }
}
