using math_game.Models;
using math_game.Helpers;
using math_game.Lib;

Console.WriteLine("Welcome to the MathGame");

PopulateQuestions populateQuestionsHelper = new PopulateQuestions();

List<QuestionTier> questions = [];
DifficultyChoices difficulty;
int correctAnswers = 0;

populateQuestionsHelper.PopulateList(questions);

Console.WriteLine("Choose difficulty:");
Console.WriteLine("Easy");
Console.WriteLine("Medium");
Console.WriteLine("Hard");

switch (Console.ReadLine())
{
    case "Easy":
        difficulty = DifficultyChoices.Easy;
        break;
    case "Medium":
        difficulty = DifficultyChoices.Medium;
        break;
    case "Hard":
        difficulty = DifficultyChoices.Hard;
        break;
    default:
        difficulty = DifficultyChoices.Easy;
        break;
}

foreach (var questionTier in questions)
{
    if (questionTier.Difficulty == difficulty)
    {
        foreach (var question in questionTier.QuestionsList)
        {
            Console.WriteLine(question.Text);
            
            Console.WriteLine("Your answer: ");
            
            int answer = short.Parse(Console.ReadLine());

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