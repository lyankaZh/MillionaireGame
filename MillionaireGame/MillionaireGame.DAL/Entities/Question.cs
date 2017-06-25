using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionaireGame.DAL.Entities
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        [Range(1, 4, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Answer { get; set; }
        public virtual Win Difficulty { get; set; }
    }
}
