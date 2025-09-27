using Daitan.Data.Dao.GatewayDevices;
using Daitan.Data.Dao.Gateways;
using Daitan.Data.Dao.PagedData;
using Daitan.Data.Dto.GatewayDevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Business.Repositories.GatewayDevices
{
    public interface IGatewayDeviceRepository
    {
        List<GatewayDeviceDao> GetAll();
        GatewayDeviceDao GetById(string Id);
        string AddOrUpdate(GatewayDeviceDto Dto);
        string SaveDeviceReadings(string ipAddress, string json);
        Task<string> SaveDeviceTcpData(ModbusTcpDeviceDto Dto);
        PagedRecords<GatewayDeviceDao> GetPagedRecords(PagedRecordsParams Params);

    }
}
