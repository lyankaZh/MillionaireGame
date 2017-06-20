using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MillionaireGame.Helpers
{
    public interface IMessageSender
    {
        void SendMessage(string messageText, string to, string username);
    }
}