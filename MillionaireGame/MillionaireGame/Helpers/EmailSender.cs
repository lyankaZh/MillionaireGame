using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MillionaireGame.DAL.Repository;

namespace MillionaireGame.Helpers
{
    public class EmailSender : IMessageSender
    {
        private IQuestionRepository _repository;

        public EmailSender(IQuestionRepository repository)
        {
            _repository = repository;
        }
        public async void SendMessage(string messageText, string to, string username)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(to));
            message.Subject = "Millionaire game";
            message.Body = messageText;
            using (var smtp = new SmtpClient())
            {
                await smtp.SendMailAsync(message);
            }
        }
    }
}