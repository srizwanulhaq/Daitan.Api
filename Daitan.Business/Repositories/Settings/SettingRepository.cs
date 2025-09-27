using Daitan.Data.Dao.Settings;
using Daitan.Data.DBContexts;
using Daitan.Data.Dto.Settings;
using Daitan.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Business.Repositories.Settings
{
    public class SettingRepository : ISettingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public SettingRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<string> AddOrUpdateDepartment(DepartmentDto Dto)
        {
            var objDept = _context.Departments.FirstOrDefault(x => x.Id == Dto.DepartmentId);

            if (objDept == null)
            {
                objDept = new Department
                {
                    Name = Dto.Name,
                };

                _context.Departments.Add(objDept);
            }
            else
            {
                objDept.Name = Dto.Name;
                _context.Departments.Update(objDept);
            }

            await _context.SaveChangesAsync();
            return objDept.Id;
        }
        public async Task<List<DepartmentDao>> GetAllDepartments()
        {
            var lstDepartments = await _context.Departments.Select(x => new DepartmentDao
            {
                DepartmentId = x.Id,
                DepartmentName = x.Name,
                CreatedDate = DateTime.Now,
            }).ToListAsync();

            return lstDepartments;
        }
        public async Task<DepartmentDao> EditDepartment(string DepartmentId)
        {
            return await _context.Departments
                .Where(x => x.Id == DepartmentId)
                .Select(x => new DepartmentDao
                {
                    DepartmentId = x.Id,
                    DepartmentName = x.Name
                }).FirstOrDefaultAsync();
        }
        public async Task DeleteDepartment(string DepartmentId)
        {
            var objDept = _context.Departments.FirstOrDefault(x => x.Id == DepartmentId);
            objDept.Active = false;
            _context.Departments.Update(objDept);
            await _context.SaveChangesAsync();
        }
        public async Task<string> AddOrUpdateMachine(MachineDto Dto)
        {
            var objMachine = _context.Machines.FirstOrDefault(x => x.Id == Dto.MachineId);

            if (objMachine == null)
            {
                objMachine = new Machine
                {
                    Name = Dto.Name,
                };

                _context.Machines.Add(objMachine);
            }
            else
            {
                objMachine.Name = Dto.Name;
                _context.Machines.Update(objMachine);
            }

            await _context.SaveChangesAsync();
            return objMachine.Id;
        }
        public async Task<List<MachineDao>> GetAllMachines()
        {
            return await _context.Machines.Select(x => new MachineDao
            {
                MachineId = x.Id,
                MachineName = x.Name,
                CreatedDate = x.CreatedDate
            }).ToListAsync();
        }
        public async Task<MachineDao> EditMachine(string MachineId)
        {
            return await _context.Machines.Where(x => x.Id == MachineId)
                .Select(x => new MachineDao
                {
                    MachineId = x.Id,
                    MachineName = x.Name
                }).FirstOrDefaultAsync();
        }
        public async Task DeleteMachine(string MachineId)
        {
            var objMachine = _context.Machines.FirstOrDefault(x => x.Id == MachineId);
            objMachine.Active = false;
            _context.Machines.Update(objMachine);
            await _context.SaveChangesAsync();
        }
        public async Task<CompanyDao> GetCompanyInfo(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            return new CompanyDao
            {
                CompanyName = user.CompanyName,
                EmissionFactor = user.EmissionFactor
            };
        }
    
    }
}
