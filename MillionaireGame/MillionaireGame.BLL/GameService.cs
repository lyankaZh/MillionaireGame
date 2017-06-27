using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using MillionaireGame.DAL.Entities;
using MillionaireGame.DAL.Repository;

namespace MillionaireGame.BLL
{
    public class GameService : IGameService
    {
        private readonly IQuestionRepository _questionRepository;

        public GameService(IQuestionRepository repository)
        {
            _questionRepository = repository;
        }

        public IEnumerable<Win> GetWins()
        {
            return _questionRepository.GetWins().OrderByDescending(x => x.Level);
        }

        public void LogAnswer(int questionId, string username, int selectedOption)
        {
            var question = _questionRepository.GetQuestionById(questionId);
            _questionRepository.InsertRecord(
                new Record
                {
                    Question = question,
                    Answer = selectedOption,
                    Username = username
                });

            _questionRepository.Save();
        }

        public int GetCorrectAnswer(int questionId)
        {
            var question = _questionRepository.GetQuestionById(questionId);
            return question.Answer;
        }

        public void SendEmail(int questionId, string username, string email)
        {
            var question = _questionRepository.GetQuestionById(questionId);
            var messageText = FormTextForMessage(question, username);
            var message = new MailMessage();
            message.To.Add(new MailAddress(email));
            message.Subject = "Millionaire game";
            message.Body = messageText;
            using (var smtp = new SmtpClient())
            {
                smtp.Send(message);
            }
        }

        public string FormTextForMessage(Question question, string username)
        {
            var messageText = new StringBuilder();
            messageText.Append("Hello!" + Environment.NewLine);
            messageText.Append($"Your friend {username} needs help in Game 'Who wants to be a Millionaire'" + Environment.NewLine);
            messageText.Append("Question:" + Environment.NewLine);
            messageText.Append(question.QuestionText + Environment.NewLine);
            messageText.Append($"A: {question.Option1}" + Environment.NewLine);
            messageText.Append($"B: {question.Option2}" + Environment.NewLine);
            messageText.Append($"C: {question.Option3}" + Environment.NewLine);
            messageText.Append($"D: {question.Option4}" + Environment.NewLine);
            return messageText.ToString();
        }

        public List<string> GetFiftyFiftyOptions(int questionId)
        {
            var question = _questionRepository.GetQuestionById(questionId);
            var options = new List<string>
            {
                question.Option1,
                question.Option2,
                question.Option3,
                question.Option4
            };

            var allAnswersForQuestion = _questionRepository.GetRecords().ToList()
                .Where(x => x.Question == question && x.Answer != question.Answer).ToList();
            return allAnswersForQuestion.Any() ? GenerateOptionFromStatistics(question, options, allAnswersForQuestion)
                                               : GenerateRandomOptions(question, options);
        }

        public List<string> GenerateOptionFromStatistics(Question question, List<string> options, List<Record> allAnswers)
        {
            var groupedRecords = from record in allAnswers
                                 group record.Question by record.Answer
                                 into g
                                 select new { Answer = g.Key, AmountOfRecords = g.Count() };
            var mostPopularAnswer = groupedRecords.OrderByDescending(x => x.AmountOfRecords).First().Answer;

            for (var i = 0; i < 4; i++)
            {
                if (i != question.Answer - 1 && i != mostPopularAnswer - 1)
                {
                    options[i] = "";
                }
            }

            return options;
        }

        public List<string> GenerateRandomOptions(Question question, List<string> options)
        {
            var randomList = new List<int>();
            for (var i = 0; i < 4; i++)
            {
                if (i != question.Answer - 1)
                {
                    randomList.Add(i);
                }
            }

            var random = new Random();
            randomList.RemoveAt(random.Next(0, randomList.Count));
            options[randomList[0]] = "";
            options[randomList[1]] = "";
            return options;
        }

        public Question GetQuestionById(int questionId)
        {
            return _questionRepository.GetQuestionById(questionId);
        }

        public Dictionary<string, int> AskAudience(int questionId)
        {
            var question = _questionRepository.GetQuestionById(questionId);
            var allAnswersForQuestion = _questionRepository.GetRecords().ToList()
                .Where(x => x.Question == question).ToList();
            var options = new List<string>
            {
                question.Option1, question.Option2, question.Option3, question.Option4
            };

            return allAnswersForQuestion.Any() ? AskAudienceOnStatistics(allAnswersForQuestion, options)
                                               : AskAudienceOnRandom(allAnswersForQuestion, options, question.Answer);
        }

        public Dictionary<string, int> AskAudienceOnRandom(List<Record> records, List<string> options, int correctAnswer)
        {
            var resultDictionary = new Dictionary<string, int>();
            var numberOfASymbol = 65;
            var random = new Random();
            var percentageForAnswer = random.Next(40, 101);
            var restPercentage = 100 - percentageForAnswer;

            for (var i = 0; i < 4; i++)
            {
                var text = $"{(char)numberOfASymbol}: {options[i]}";
                if (i == correctAnswer - 1)
                {
                    resultDictionary.Add(text, percentageForAnswer);
                }
                else if (i == 3)
                {
                    resultDictionary.Add(text, restPercentage);
                }
                else
                {
                    var currentPercentage = random.Next(0, restPercentage);
                    resultDictionary.Add(text, currentPercentage);
                    restPercentage -= currentPercentage;
                }

                numberOfASymbol++;
            }

            return resultDictionary;
        }

        public Dictionary<string, int> AskAudienceOnStatistics(List<Record> records, List<string> options)
        {
            var resultDictionary = new Dictionary<string, int>();
            var numberOfASymbol = 65;
            var groupedRecords = (from record in records
                                  group record.Question by record.Answer into g
                                  select new { Answer = g.Key, AmountOfRecords = g.Count() }).ToList();

            for (var i = 1; i <= 4; i++)
            {
                if (!groupedRecords.Exists(x => x.Answer == i))
                {
                    groupedRecords.Add(new { Answer = i, AmountOfRecords = 0 });
                }
            }

            var totalAmountOfRecords = records.Count;

            foreach (var element in groupedRecords.OrderBy(x => x.Answer))
            {
                double percentage = (double)element.AmountOfRecords / totalAmountOfRecords * 100.0;
                resultDictionary.Add($"{(char)numberOfASymbol}: {options[element.Answer - 1]}", (int)percentage);
            }

            return resultDictionary;
        }

        public Question GetRandomCurrentQuestion(int questionNumber)
        {
            var random = new Random();
            var allQuestionsFromLevel = _questionRepository.GetQuestions().Where(x => x.Difficulty.Level == questionNumber).ToList();
            return allQuestionsFromLevel[random.Next(0, allQuestionsFromLevel.Count)];
        }
    }
}
