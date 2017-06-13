﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MillionaireGame.Models
{
    public class QuestionModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public string AnswerText { get; set; }
        public int AnswerId { get; set; }
        public int QuestionNumber { get; set; }

    }
}