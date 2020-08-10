using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessagrListner
{
    public class SMTP
    {
        private IConfiguration _configuration;

        public SMTP(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Method for sending mail 
        public void SendMail(string data)
        {
            try
            {
                //variable declare for message
                var message = new MimeMessage();

                //message sent by the user email
                message.From.Add(address: new MailboxAddress("Book Store", "chittevikey5@gmal.com"));

                //messsage recieved by the user email
                message.To.Add(new MailboxAddress("Book Store", "chittevikey5@gmail.com"));

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
                    client.Authenticate("chittevikey5@gmail.com", "nandlalv");
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
