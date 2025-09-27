using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Data.Dao.GatewayDevices
{
    public class GatewayDeviceDao
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DeviceReferenceId { get; set; }
        public double? PhaseVoltageUA { get; set; }
        public double? PhaseVoltageUB { get; set; }
        public double? PhaseVoltageUC { get; set; }
        public double? LineVoltageUAB { get; set; }
        public double? LineVoltageUBC { get; set; }
        public double? LineVoltageUCA { get; set; }
        public double? Frequency { get; set; }
        public string GatewayName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class WifiMeterDaoV2
    {
        public string id { get; set; }
        public double ia { get; set; }
        public double ib { get; set; }
        public double ic { get; set; }
        public double ua { get; set; }
        public double ub { get; set; }
        public double uc { get; set; }
        public double pa { get; set; }
        public double pb { get; set; }
        public double pc { get; set; }
        public double qa { get; set; }
        public double qb { get; set; }
        public double qc { get; set; }
        public double sa { get; set; }
        public double sb { get; set; }
        public double sc { get; set; }
        public double pfa { get; set; }
        public double pfb { get; set; }
        public double pfc { get; set; }
        public double uab { get; set; }
        public double ubc { get; set; }
        public double uca { get; set; }
        public double zyggl { get; set; }
        public double zwggl { get; set; }
        public double zszgl { get; set; }
        public double zglys { get; set; }
        public double f { get; set; }
        public string time { get; set; }
        public string isend { get; set; }
    }

    public class WifiMeterDaoV1
    {
        [JsonProperty("id")]
        public string id;

        [JsonProperty("u0")]
        public double u0;

        [JsonProperty("u+")]
        public double u1;

        [JsonProperty("u-")]
        public double u2;

        [JsonProperty("i0")]
        public double i0;

        [JsonProperty("i+")]
        public double i1;

        [JsonProperty("i-")]
        public double i2;

        [JsonProperty("uxja")]
        public double uxja;

        [JsonProperty("uxjb")]
        public double uxjb;

        [JsonProperty("uxjc")]
        public double uxjc;

        [JsonProperty("ixja")]
        public double ixja;

        [JsonProperty("ixjb")]
        public double ixjb;

        [JsonProperty("ixjc")]
        public double ixjc;

        [JsonProperty("unb")]
        public double unb;

        [JsonProperty("inb")]
        public double inb;

        [JsonProperty("pdm")]
        public double pdm;

        [JsonProperty("qdm")]
        public double qdm;

        [JsonProperty("sdm")]
        public double sdm;

        [JsonProperty("time")]
        public string time;

        [JsonProperty("isend")]
        public string isend;
    }



    public class ApiGatewayResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("data")]
        public DeviceInfo Data { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class DeviceInfo
    {
        public int Id { get; set; } // Primary Key
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("serialnumber")]
        public string SerialNumber { get; set; }

        [JsonProperty("firmwareversion")]
        public string FirmwareVersion { get; set; }

        [JsonProperty("kernelversion")]
        public string KernelVersion { get; set; }

        [JsonProperty("localtime")]
        public string LocalTime { get; set; }

        [JsonProperty("uptime")]
        public string Uptime { get; set; }
        public string? IpAddress { get; set; } // Map with IP
    }

   


}
