using System.Collections.Generic;

namespace MillionaireGame.Models
{
    public class QuestionModel
    {
        public int QuestionId { get; set; }

        public string QuestionText { get; set; }

        public List<string> Options { get; set; }

        public int QuestionNumber { get; set; }
    }
}