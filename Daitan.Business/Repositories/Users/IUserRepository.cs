using Daitan.Data.Dto.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Business.Repositories.Users
{
    public interface IUserRepository
    {
        Task SaveCompanySettings(CompanyDto Dto);
    }
}
