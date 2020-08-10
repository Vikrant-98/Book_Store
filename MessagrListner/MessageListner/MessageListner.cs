using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using System;

namespace MessagrListner
{
    public class MessageListner
    {
        public IConfiguration _configuration;

        static void Main(string[] args)
        {
            MessageListner msg = new MessageListner();
            msg.Listner();
        }
        public void Listner()
        {
            //create object of smtp class 
            SMTP smtp = new SMTP(_configuration);

            Console.WriteLine("Message");

            // Message queue
            MessageQueue MyQueue;

            MyQueue = new MessageQueue(@".\Private$\MyQueue");

            //message recieve from the Queue
            Message MyMessage = MyQueue.Receive();

            //Message in binary formate
            MyMessage.Formatter = new BinaryMessageFormatter();

            //Mail send 
            smtp.SendMail(MyMessage.Body.ToString());

            //Print message of the body
            Console.WriteLine(MyMessage.Body.ToString());

        }
    }
}
