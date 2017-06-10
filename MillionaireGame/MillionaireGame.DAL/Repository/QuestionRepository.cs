using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MillionaireGame.DAL.Entities;

namespace MillionaireGame.DAL.Repository
{
    public class QuestionRepository: IQuestionRepository
    {
        private readonly MillionaireContext _context;
        private bool _disposed;

        public QuestionRepository(MillionaireContext context)
        {
            _context = context;
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Question> GetQuestions()
        {
            return _context.Questions.ToList();
        }

        public Question GetQuestionById(int questionId)
        {
            return _context.Questions.Find(questionId);
        }

        public void InsertQuestion(Question question)
        {
            _context.Questions.Add(question);
        }

        public void DeleteQuestion(int questionId)
        {
            Question questionToDelete = _context.Questions.Find(questionId);
            if (questionToDelete != null)
            {
                _context.Questions.Remove(questionToDelete);
            }
        }

        public void UpdateExcursion(Question question)
        {
            _context.Entry(question).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
