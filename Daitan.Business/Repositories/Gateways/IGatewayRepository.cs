using Daitan.Data.Dao.Gateways;
using Daitan.Data.Dto.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Business.Repositories.Gateways
{
    public interface IGatewayRepository
    {
        string AddOrUpdate(GatewayDto Dto);
        GatewayDao Edit(string Id);
        List<GatewayDao> GetAll();
    }
}
