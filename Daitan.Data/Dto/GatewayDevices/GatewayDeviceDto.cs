using Daitan.Data.Dao.GatewayDevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Data.Dto.GatewayDevices
{
    public class GatewayDeviceDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Voltage { get; set; }
    }

    public class MeterReading
    {
        public float Value { get; set; }
    }

    public class ModbusTcpDeviceDto
    {
        public string IpAddress { get; set; }   
        public List<MeterReading> MeterReadingData { get; set; }
        public ApiGatewayResponse GatewayData { get; set; }
    }
}
