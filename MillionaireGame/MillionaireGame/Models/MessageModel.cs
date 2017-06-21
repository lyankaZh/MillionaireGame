using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MillionaireGame.Models
{
    public class MessageModel
    {
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "Please enter correct email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}