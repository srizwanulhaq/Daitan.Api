using Daitan.Data.Dao.GatewayDevices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Daitan.Business.Helpers
{
    public class HttpHelper
    {
       
        public async Task<string> PostFormData(string apiUrl, string ipAddress, Dictionary<string, string> formData)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(formData);
                    var response = await _httpClient.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        return responseBody;
                    }
                }

            }
            catch(Exception ex)
            {

            }

            return null;
        }
    }
}
