using System.Collections.Generic;
using System.Data.Entity;
using MillionaireGame.DAL.Entities;

namespace MillionaireGame.DAL
{
    public class MillionaireContext : DbContext
    {
        public MillionaireContext() : base("name=MillionaireGameConnectionString")
        {
            Database.SetInitializer(new MillionaireInitializer());
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Win> Wins { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<ExceptionDetail> ExceptionDetails { get; set; }

        public class MillionaireInitializer : DropCreateDatabaseIfModelChanges<MillionaireContext>
        {
            protected override void Seed(MillionaireContext context)
            {
                List<Win> wins = new List<Win>
                {
                    new Win {Level = 1, Sum = 100},
                    new Win {Level = 2, Sum = 200},
                    new Win {Level = 3, Sum = 300},
                    new Win {Level = 4, Sum = 500},
                    new Win {Level = 5, Sum = 1000},
                    new Win {Level = 6, Sum = 2000},
                    new Win {Level = 7, Sum = 4000},
                    new Win {Level = 8, Sum = 8000},
                    new Win {Level = 9, Sum = 16000},
                    new Win {Level = 10, Sum = 32000},
                    new Win {Level = 11, Sum = 64000},
                    new Win {Level = 12, Sum = 125000},
                    new Win {Level = 13, Sum = 250000},
                    new Win {Level = 14, Sum = 500000},
                    new Win {Level = 15, Sum = 1000000}
                };

                foreach (var win in wins)
                {
                    context.Wins.Add(win);
                }

                context.Questions.Add
                (new Question
                {
                    QuestionText = "Which colour is used as a term to describe an illegal market in rare goods?",
                    Option1 = "Blue",
                    Option2 = "Red",
                    Option3 = "Black",
                    Option4 = "White",
                    Answer = 3,
                    Difficulty = wins[0]
                });

                context.Questions.Add
                (new Question
                {
                    QuestionText = "Which of these is a legendary British king who is said to have drawn a sword from a stone?",
                    Option1 = "Eric",
                    Option2 = "Albert",
                    Option3 = "Ernie",
                    Option4 = "Artur",
                    Answer = 4,
                    Difficulty = wins[0]
                });

                context.Questions.Add
                    (new Question
                    {
                        QuestionText = "Which character was first played by Arnold Schwarzenegger in a 1984 film?",
                        Option1 = "Demonstator",
                        Option2 = "Instigator",
                        Option3 = "Investigator",
                        Option4 = "Terminator",
                        Answer = 4,
                        Difficulty = wins[1]
                    });

                context.Questions.Add
                    (new Question
                    {
                        QuestionText = "Which country is not crossed by the Arctic Circle?",
                        Option1 = "Norway",
                        Option2 = "Finland",
                        Option3 = "Greece",
                        Option4 = "Sweden",
                        Answer = 3,
                        Difficulty = wins[2]
                    });


                context.Questions.Add
                    (new Question
                    {
                        QuestionText = "In which country would you expect to be greeted with the word 'bonjour'?",
                        Option1 = "Italy",
                        Option2 = "France",
                        Option3 = "Spain",
                        Option4 = "Wales",
                        Answer = 2,
                        Difficulty = wins[3]
                    });

                context.Questions.Add
                    (new Question
                    {
                        QuestionText = "Which country is not an island?",
                        Option1 = "Madagascar",
                        Option2 = "Cuba",
                        Option3 = "Germany",
                        Option4 = "Jamaica",
                        Answer = 3,
                        Difficulty = wins[4]
                    });

                context.Questions.Add
                    (new Question
                    {
                        QuestionText = "According to the old adage, how many lives does a cat have?",
                        Option1 = "Five",
                        Option2 = "Seven",
                        Option3 = "Nine",
                        Option4 = "Ten",
                        Answer = 3,
                        Difficulty = wins[5]
                    });

                context.Questions.Add
                    (new Question
                    {
                        QuestionText = "Which country shares a land border with the UK?",
                        Option1 = "Portugal",
                        Option2 = "Libya",
                        Option3 = "Vietnam",
                        Option4 = "Ireland",
                        Answer = 4,
                        Difficulty = wins[6]
                    });

                context.Questions.Add
                    (new Question
                    {
                        QuestionText = "Which of these geographical features is a mountain?",
                        Option1 = "Kilimanjaro",
                        Option2 = "Danube",
                        Option3 = "Amazon",
                        Option4 = "Nile",
                        Answer = 1,
                        Difficulty = wins[6]
                    });

                context.Questions.Add
                    (new Question
                    {
                        QuestionText = "Which part of the body do bronchial infections mainly attack?",
                        Option1 = "Eyes",
                        Option2 = "Liver",
                        Option3 = "Spleen",
                        Option4 = "Lungs",
                        Answer = 4,
                        Difficulty = wins[7]
                    });

                context.Questions.Add
                    (new Question
                    {
                        QuestionText = "Which of the following is not a geological period?",
                        Option1 = "Jurassic",
                        Option2 = "Palaeozoic",
                        Option3 = "Triassic",
                        Option4 = "Boracic",
                        Answer = 4,
                        Difficulty = wins[8]
                    });

                context.Questions.Add
                    (new Question
                    {
                        QuestionText = "A traditional Italian dish is spaghetti ...?",
                        Option1 = "Roma",
                        Option2 = "Bolognese",
                        Option3 = "Firenze",
                        Option4 = "Pisa",
                        Answer = 2,
                        Difficulty = wins[9]
                    });

                context.Questions.Add
                    (new Question
                    {
                        QuestionText = "In the world of video games, who has a brother called Luigi?",
                        Option1 = "Mario",
                        Option2 = "Benito",
                        Option3 = "Carlo",
                        Option4 = "Georgio",
                        Answer = 1,
                        Difficulty = wins[10]
                    });

                context.Questions.Add
                    (new Question
                    {
                        QuestionText = "Which is not a real English county?",
                        Option1 = "West Sussex",
                        Option2 = "South Norfolk",
                        Option3 = "North Yorkshire",
                        Option4 = "East Sussex",
                        Answer = 2,
                        Difficulty = wins[11]
                    });

                context.Questions.Add
                    (new Question
                    {
                        QuestionText = "Rioja is a type of what?",
                        Option1 = "Wine",
                        Option2 = "Nut",
                        Option3 = "Bread",
                        Option4 = "Meat",
                        Answer = 1,
                        Difficulty = wins[12]
                    });

                context.Questions.Add
                    (new Question
                    {
                        QuestionText = "Who created the character Huckleberry Finn?",
                        Option1 = "Henry James",
                        Option2 = "Jack London",
                        Option3 = "Mark Twain",
                        Option4 = "Jonathan Swift",
                        Answer = 3,
                        Difficulty = wins[13]
                    });

                context.Questions.Add
                (new Question
                {
                    QuestionText = "Which of these would a film actor like to receive?",
                    Option1 = "Oliver",
                    Option2 = "Oscar",
                    Option3 = "Oliphan",
                    Option4 = "Osbert",
                    Answer = 2,
                    Difficulty = wins[14]
                });
                context.SaveChanges();

                base.Seed(context);
            }
            
        }
    }
}
