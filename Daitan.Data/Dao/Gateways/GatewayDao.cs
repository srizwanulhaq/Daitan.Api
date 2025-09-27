using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Data.Dao.Gateways
{
    public class GatewayDao
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Voltage { get; set; } 
        public string GatewayName { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}
