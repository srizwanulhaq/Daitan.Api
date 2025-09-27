using Daitan.Data.Dao.Settings;
using Daitan.Data.Dto.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Business.Repositories.Settings
{
    public interface ISettingRepository
    {
        Task<string> AddOrUpdateDepartment(DepartmentDto Dto);
        Task<List<DepartmentDao>> GetAllDepartments();
        Task<DepartmentDao> EditDepartment(string DepartmentId);
        Task DeleteDepartment(string DepartmentId);
        Task<string> AddOrUpdateMachine(MachineDto Dto);
        Task<MachineDao> EditMachine(string MachineId);
        Task DeleteMachine(string MachineId);
        Task<List<MachineDao>> GetAllMachines();
    }
}
