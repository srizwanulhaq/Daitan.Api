using Daitan.Data.DBContexts;
using Daitan.Data.Dto.Settings;
using Daitan.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Business.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(UserManager<ApplicationUser> userManager, 
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task SaveCompanySettings(CompanyDto Dto)
        {
            var user = await _userManager.FindByIdAsync(Dto.UserId);

            user.CompanyName = Dto.CompanyName;
            user.EmissionFactor = Dto.EmissionFactor;
            await _userManager.UpdateAsync(user);
        }

     
        

    }
}
