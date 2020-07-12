using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagrListner
{
    class SMTP
    {
        public IConfiguration Configuration;

        //Method for sending mail 
        public void SendMail(string data)
        {
            try
            {
                //variable declare for message
                var message = new MimeMessage();

                //message sent by the user email
                message.From.Add(address: new MailboxAddress("Employee Management", "vikrantchitte5398@gmail.com"));

                //messsage recieved by the user email
                message.To.Add(new MailboxAddress("Employee Management", "vikrantchitte5398@gmail.com"));

                //subject of email
                message.Subject = "Registration";

                //body of email
                message.Body = new TextPart("plain")
                {
                    Text = data
                };

                //Connection 
                //Authentication
                //sending email
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("vikrantchitte5398@gmail.com", "");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
