using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MillionaireGame.Models
{
    public class QuestionModel
    {
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public string Answer { get; set; }
    }
}