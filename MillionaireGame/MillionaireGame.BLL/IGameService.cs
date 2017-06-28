using System.Collections.Generic;
using MillionaireGame.DAL.Entities;

namespace MillionaireGame.BLL
{
    public interface IGameService
    {
        void LogAnswer(int questionId, string username, int selectedOption);

        int GetCorrectAnswer(int questionId);

        IEnumerable<Win> GetWins();

        void SendEmail(int questionId, string username, string email);

        List<string> GetFiftyFiftyOptions(int questionId);

        Question GetQuestionById(int questionId);

        Dictionary<string, int> AskAudience(int questionId);

        Question GetRandomCurrentQuestion(int questionNumber);
    }
}