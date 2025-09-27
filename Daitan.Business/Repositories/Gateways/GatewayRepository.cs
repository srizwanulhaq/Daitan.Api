using Daitan.Data.Dao.Gateways;
using Daitan.Data.DBContexts;
using Daitan.Data.Dto.Gateways;
using Daitan.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Business.Repositories.Gateways
{
    public class GatewayRepository : IGatewayRepository
    {
        private readonly ApplicationDbContext _context;
        public GatewayRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public string AddOrUpdate(GatewayDto Dto)
        {
            var objGateway = new Gateway
            {
                Name = Dto.Name,
            };

            _context.Gateways.Add(objGateway);
            _context.SaveChanges();

            return objGateway.Id;
        }
        public GatewayDao Edit(string Id)
        {
            return _context.Gateways.Select(x => new GatewayDao
            {
                Id = Id,
                Name = x.Name
            }).FirstOrDefault();

        }
        public List<GatewayDao> GetAll()
        {
            return _context.Gateways.Select(x => new GatewayDao
            {
                Id = x.Id,
                Name = x.Name,
                CreatedDate = x.CreatedDate
            }).ToList();

        }

    }
}
