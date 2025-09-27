using Azure.Core;
using Daitan.Business.Helpers;
using Daitan.Data.Dao.GatewayDevices;
using Daitan.Data.Dao.Gateways;
using Daitan.Data.Dao.PagedData;
using Daitan.Data.DBContexts;
using Daitan.Data.Dto.GatewayDevices;
using Daitan.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Daitan.Business.Repositories.GatewayDevices
{
    public class GatewayDeviceRepository : IGatewayDeviceRepository
    {
        private readonly ApplicationDbContext _context;
       
        public GatewayDeviceRepository(ApplicationDbContext context)
        {
            _context = context;
          
        }
        public PagedRecords<GatewayDeviceDao> GetPagedRecords(PagedRecordsParams Params)
        {
            var query = _context.GatewayDevices.Select(x => new GatewayDeviceDao
            {
                Id = x.Id,
                Name = x.Name
            });

            var result = new PagedRecords<GatewayDeviceDao>
            {
                Items = query.Skip((Params.PageNo - 1) * Params.PageSize).ToList(),
                TotalCount = query.Count()
            };

            return result;
        }
        public GatewayDeviceDao GetById(string Id)
        {
            return _context.GatewayDevices.Include(x => x.Gateways)
                .Where(x => x.Id == Id)
                .Select(x => new GatewayDeviceDao
                {
                    Id = x.Id,
                    Name = x.Name,
                   
                }).FirstOrDefault();
        }
        public string AddOrUpdate(GatewayDeviceDto Dto)
        {
            var objDevice = new GatewayDevice();

            if (string.IsNullOrEmpty(Dto.Id))
            {
                objDevice = new GatewayDevice
                {
                    Name = Dto.Name,
                };
                _context.GatewayDevices.Add(objDevice);
                _context.SaveChanges();
            }
            else
            {
                objDevice = _context.GatewayDevices.FirstOrDefault(x => x.Id == Dto.Id);
                objDevice.Name = Dto.Name;
                _context.GatewayDevices.Update(objDevice);
                _context.SaveChanges();
            }

            return objDevice.Id;
        }
        public List<GatewayDeviceDao> GetAll()
        {
            return _context.GatewayDevices.Include(x => x.Gateways)
                .Select(x => new GatewayDeviceDao
                {
                    Id = x.Id,
                    Name = x.Name,
                    DeviceReferenceId = x.DeviceReferenceId,
                    GatewayName = x.Gateways.Name,
                    PhaseVoltageUA = x.PhaseVoltageUA,
                    PhaseVoltageUB = x.PhaseVoltageUB,
                    PhaseVoltageUC = x.PhaseVoltageUC,
                    LineVoltageUAB = x.LineVoltageUAB,
                    LineVoltageUBC = x.LineVoltageUBC,
                    LineVoltageUCA = x.LineVoltageUCA,
                    Frequency = x.Frequency,
                    CreatedDate = x.CreatedDate
                }).ToList();
        }




        public string SaveDeviceReadings(string ipAddress, string json)
        {
            //  DeleteOldReadings();

            // var res1 = SaveSimpleMeterData(json);
            // var res2 = SaveWifiMeterDataV1(json);

            return "Repo Response: " + json;
        }
        public async Task<string> SaveDeviceTcpData(ModbusTcpDeviceDto dto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(dto.MeterReadingData);
                var jsonArray = JsonNode.Parse(json).AsArray();
                double[] values = jsonArray.Select(node => (double)node["Value"]).ToArray();

                var gatewayData = dto.GatewayData.Data;

                var objGateway = _context.Gateways.FirstOrDefault(x => x.SerialNumber == gatewayData.SerialNumber);

                if (objGateway == null)
                {
                    if (dto.GatewayData?.Code == "SUCCESS" && dto.GatewayData.Data != null)
                    {
                        objGateway = new Gateway
                        {
                            Name = gatewayData.Model,
                            IPAddress = dto.IpAddress,
                            FirmwareVersion = gatewayData.FirmwareVersion,
                            KernelVersion = gatewayData.KernelVersion,
                            LocalTime = dto.GatewayData.Data.LocalTime,
                            SerialNumber = gatewayData.SerialNumber,
                            Uptime = gatewayData.Uptime
                        };

                        _context.Gateways.Add(objGateway);
                        _context.SaveChanges();
                    }
                }

                string gatewayId = objGateway?.Id;

                var objDevice = new GatewayDevice
                {
                    GatewayId = gatewayId,
                    Temperature = values[0],
                    Humidity = values[1],
                    PhaseVoltageUA = values[2],
                    PhaseVoltageUB = values[3],
                    PhaseVoltageUC = values[4],
                    LineVoltageUAB = values[5],
                    LineVoltageUBC = values[6],
                    LineVoltageUCA = values[7],
                    GatewayJson = json,
                };

                _context.GatewayDevices.Add(objDevice);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "success";

        }
        private string SaveSimpleMeterData(string? json)
        {
            try
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                var isData = root.TryGetProperty("data", out JsonElement data);

                if (!isData)
                    return string.Empty;

                // var data = root.GetProperty("data");
                var deviceId = root.GetProperty("deviceId").GetString();
                var clientId = root.GetProperty("clientId").GetString();

                var objGatewayDevice = _context.GatewayDevices
                    .FirstOrDefault(x => x.DeviceReferenceId == deviceId);

                if (objGatewayDevice == null)
                    objGatewayDevice = new GatewayDevice();

                var objReadingHistory = new DeviceReadingHistory();

                foreach (var device in data.EnumerateArray())
                {
                    var deviceKey = device.GetProperty("deviceKey").GetString();
                    var time = device.GetProperty("time").GetString();

                    int index = 0;
                    // Manually extract values — assuming all keys are "0" (this is hacky)
                    foreach (var element in device.EnumerateObject())
                    {
                        if (element.Name == "deviceKey" || element.Name == "time") continue;

                        string value = element.Value.GetString();
                        var isSuccess = double.TryParse(value, out double decimalVal);

                        if (index == 3 && decimalVal > 190 && isSuccess)
                        {
                            objGatewayDevice.PhaseVoltageUA = decimalVal;
                            objReadingHistory.PhaseVoltageUA = decimalVal;
                        }

                        if (index == 4 && decimalVal > 190 && isSuccess)
                        {
                            objGatewayDevice.PhaseVoltageUB = decimalVal;
                            objReadingHistory.PhaseVoltageUB = decimalVal;
                        }

                        if (index == 5 && decimalVal > 190 && isSuccess)
                        {
                            objGatewayDevice.PhaseVoltageUC = decimalVal;
                            objReadingHistory.PhaseVoltageUC = decimalVal;
                        }


                        if (index == 6 && decimalVal > 190 && isSuccess)
                        {
                            objGatewayDevice.LineVoltageUAB = decimalVal;
                            objReadingHistory.LineVoltageUAB = decimalVal;
                            //  Console.WriteLine($"LineToLineVoltage: {decimalVal}");
                        }

                        if (index == 7 && decimalVal > 190 && isSuccess)
                        {
                            objGatewayDevice.LineVoltageUBC = decimalVal;
                            objReadingHistory.LineVoltageUBC = decimalVal;
                            Console.WriteLine($"LineToLineVoltage: {decimalVal}");
                        }

                        if (index == 8 && decimalVal > 190 && isSuccess)
                        {
                            objGatewayDevice.LineVoltageUCA = decimalVal;
                            objReadingHistory.LineVoltageUCA = decimalVal;
                            Console.WriteLine($"LineToLineVoltage: {decimalVal}");
                        }

                        if (index == 7 && decimalVal > 10 && isSuccess)
                        {
                            objGatewayDevice.Frequency = decimalVal;
                            objReadingHistory.Frequency = decimalVal;
                            Console.WriteLine($"frequency: {decimalVal}");
                        }

                        index++;
                    }
                }

                var objGateway = _context.Gateways.FirstOrDefault(x => x.ClientId == clientId);

                if (objGateway == null)
                {
                    objGateway = new Gateway
                    {
                        Name = clientId + " Gateway",
                        ClientId = clientId,
                    };

                    _context.Gateways.Add(objGateway);
                    _context.SaveChanges();
                }

                if (string.IsNullOrEmpty(objGatewayDevice.DeviceReferenceId))
                {
                    objGatewayDevice.DeviceReferenceId = deviceId;
                    objGatewayDevice.GatewayId = objGateway.Id;
                    objGatewayDevice.Name = "Smart Power Meter";
                    _context.GatewayDevices.Add(objGatewayDevice);
                }
                else
                {
                    objGatewayDevice.UpdatedDate = DateTime.Now;
                    _context.GatewayDevices.Update(objGatewayDevice);
                }

                if (objReadingHistory.PhaseVoltageUA > 0)
                {
                    objReadingHistory.Name = objGatewayDevice.Name;
                    objReadingHistory.DeviceReferenceId = deviceId;
                    objReadingHistory.GatewayId = objGateway.Id;
                    _context.DeviceReadingHistory.Add(objReadingHistory);
                }

                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                //  return ex.Message;
            }

            return "success1";

        }
        private string SaveWifiMeterDataV1(string json)
        {
            try
            {
                var objJson = JsonConvert.DeserializeObject<WifiMeterDaoV1>(json);

                if (objJson?.id == null)
                    return string.Empty;

                var deviceId = objJson?.id;

                var objGatewayDevice = _context.GatewayDevices
                    .FirstOrDefault(x => x.DeviceReferenceId == deviceId);

                if (objGatewayDevice == null)
                {
                    objGatewayDevice = new GatewayDevice
                    {
                        Name = "Smart Meter",
                        PhaseVoltageUA = objJson.uxja,
                        PhaseVoltageUB = objJson.uxjb,
                        PhaseVoltageUC = objJson.uxjc,
                        DeviceReferenceId = deviceId,
                    };

                    _context.GatewayDevices.Add(objGatewayDevice);
                }
                else
                {
                    objGatewayDevice.PhaseVoltageUA = objJson.uxja;
                    objGatewayDevice.PhaseVoltageUB = objJson.uxjb;
                    objGatewayDevice.PhaseVoltageUC = objJson.uxjc;
                    objGatewayDevice.UpdatedDate = DateTime.Now;
                    _context.GatewayDevices.Update(objGatewayDevice);
                }

                if (objJson.uxja > 0 || objJson.uxjb > 0 || objJson.uxjc > 0)
                {
                    var objReading = new DeviceReadingHistory
                    {
                        Name = "Smart Meter",
                        PhaseVoltageUA = objJson.uxja,
                        PhaseVoltageUB = objJson.uxjb,
                        PhaseVoltageUC = objJson.uxjc,
                        DeviceReferenceId = deviceId,
                    };
                    _context.DeviceReadingHistory.Add(objReading);
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return "error 222";
            }

            return "success 2";

        }
        private void SaveWifiMeterDataV11(string json)
        {
            var objJson = JsonConvert.DeserializeObject<WifiMeterDaoV2>(json);
        }
        private void DeleteOldReadings()
        {
            var dt = DateTime.Parse(DateTime.Now.AddDays(-1).ToShortDateString() + " 11:59:59 PM");

            var count = _context.GatewayDevices.Where(x => x.CreatedDate < dt).Count();

            if (count > 0)
            {
                var lst = _context.GatewayDevices.Where(x => x.CreatedDate < dt).ToList();
                _context.GatewayDevices.RemoveRange(lst);
                _context.SaveChanges();
            }

        }
    }
}

