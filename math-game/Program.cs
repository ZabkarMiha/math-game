using math_game.Models;
using math_game.Helpers;
using math_game.Lib;

Console.WriteLine("Welcome to the MathGame");

QuestionsHelper questionsHelper = new QuestionsHelper();

List<QuestionTierModel> questions = questionsHelper.PopulateList();
DifficultyChoices difficulty = DifficultyChoices.Easy;
int correctAnswers = 0;

bool validDifficultyChoice = false;

while (!validDifficultyChoice)
{
    Console.WriteLine("Choose difficulty (case insensitive) or leave blank for random:");
    Console.WriteLine("Easy");
    Console.WriteLine("Medium");
    Console.WriteLine("Hard");

    switch (Console.ReadLine()?.ToLower())
    {
        case "easy":
            difficulty = DifficultyChoices.Easy;
            validDifficultyChoice  = true;
            break;
        case "medium":
            difficulty = DifficultyChoices.Medium;
            validDifficultyChoice  = true;
            break;
        case "hard":
            difficulty = DifficultyChoices.Hard;
            validDifficultyChoice  = true;
            break;
        case "":
            var rand = new Random();
            var difficultyChoicesArray = Enum.GetNames<DifficultyChoices>();
            
            difficulty = (DifficultyChoices)rand.Next(difficultyChoicesArray.Length);
            
            validDifficultyChoice  = true;
            break;
        default:
            Console.WriteLine("Invalid choice");
            break;
    }
}

Console.WriteLine("Selected difficulty: " + difficulty);

foreach (var questionTier in questions)
{
    if (questionTier.Difficulty == difficulty)
    {
        foreach (var question in questionTier.QuestionsList)
        {
            int answer = 0;
            bool success = false;

            while (!success)
            {
                Console.WriteLine(question.Text);
            
                Console.WriteLine("Your answer: ");
                
                success = int.TryParse(Console.ReadLine(), out answer);

                if (!success)
                    Console.WriteLine("Only round positive and negative numbers are accepted");
            }

            if (answer == question.Answer)
            {
                Console.WriteLine("Correct");
                correctAnswers++;
            }
            else
                Console.WriteLine("Incorrect");
        }
    }
}

Console.WriteLine("Number of correct answers: " + correctAnswers);

Console.ReadLine();