using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Daitan.Business.Helpers
{
    public class EmailMessage
    {
        public EmailAddress From { get; set; }
        public List<EmailAddress> To { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
    }

    public class EmailAddress
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

    public class EmailUtility
    {
        public void SendEmailAsync(string ToEmail, string Subject, string strMsg)
        {
            try
            {
                string fromEmail = "rizwanmz1000@gmail.com";
                string password = "lsotkbmhggzxjujv"; 
               
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(ToEmail);
                mail.Subject = Subject;
                mail.Body = strMsg;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(fromEmail, password);
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch(Exception ex)
            {

            }

            //    var msg1 = new EmailMessage()
            //{

            //    Category = "test",
            //    From = new EmailAddress
            //    {
            //        Email = "syedrizwan20008@gmail.com",
            //        Name = "Rizwan"
            //    },
            //    Subject = "test",
            //    Text = strMsg,


            //    To = new List<EmailAddress>()
            //    {
            //        new EmailAddress
            //        {
            //            Email = "syedrizwan20008@gmail.com",
            //            Name = "rizwan"

            //        }

            //    },

            //};

            //var client = new RestClient("https://sandbox.api.mailtrap.io/api/send/3475530");
            //var request = new RestRequest();
            //request.AddHeader("Authorization", "Bearer 915575c6685eb4e5eae2a6f63b8ecfa4");
            //request.AddHeader("Content-Type", "application/json");
            ////request.AddParameter("application/json", 
            ////    "{\"from\":{\"email\":\"syedrizwan20008@gmail.com\",\"name\":\"Mqtt Test\"},\"to\":[{\"email\":\"syedrizwan20008@gmail.com\"}],\"subject\":\"mqtt test!\",\"text:"+strMsg+",\"category\":\"Integration Test\"}", ParameterType.RequestBody);

            //request.AddParameter("application/json",
            //  JsonSerializer.Serialize(msg1), ParameterType.RequestBody);

            //var response = client.Post(request);
            //System.Console.WriteLine(response.Content);

        }
    }
}
