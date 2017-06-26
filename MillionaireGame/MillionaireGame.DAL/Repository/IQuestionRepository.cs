﻿using System;
using System.Collections.Generic;
using MillionaireGame.DAL.Entities;

namespace MillionaireGame.DAL.Repository
{
    public interface IQuestionRepository: IDisposable
    {
        IEnumerable<Question> GetQuestions();
        Question GetQuestionById(int questionId);
        void InsertQuestion(Question question);
        void DeleteQuestion(int questionId);
        void UpdateExcursion(Question question);

        IEnumerable<Record> GetRecords();
        Record GetRecordById(int recordId);
        void InsertRecord(Record record);

        void Save();
    }    
}
