using System.Diagnostics;
using math_game.Models;
using math_game.Helpers;
using math_game.Lib;

Console.WriteLine("Welcome to the MathGame \n");

//Initialize helpers and lists
QuestionsHelper questionsHelper = new();
DifficultyHelper difficultyHelper = new();
Stopwatch stopwatch = new();
List<QuestionTierModel> questionsTiers;
List<ResultModel> results = [];

//Populate list and handle potential error
while (true)
{
    try
    {
        questionsTiers = questionsHelper.ReturnAllQuestionTiers();
        break;
    }
    catch (Exception e)
    {
        if (!AskYesNo($"Something went wrong. {e.Message} \nRetry?"))
        {
            throw;
        }
    }
}

//Game loop
bool gameloop;

do
{
    DifficultyChoices difficulty;

    while (true)
    {
        Console.WriteLine("Choose difficulty (case insensitive)");
        Console.WriteLine("\nEasy \nMedium \nHard \nMixed \nRandom \n");

        switch (Console.ReadLine()?.ToLower())
        {
            case "easy":
                difficulty = DifficultyChoices.Easy;
                break;
            case "medium":
                difficulty = DifficultyChoices.Medium;
                break;
            case "hard":
                difficulty = DifficultyChoices.Hard;
                break;
            case "mixed":
                difficulty = DifficultyChoices.Mixed;
                break;
            case "random":
                difficulty = difficultyHelper.ReturnRandomDifficulty();
                break;
            default:
                Console.WriteLine("\nInvalid choice");
                Thread.Sleep(1500);
                Console.Clear();
                continue;
        }

        break;
    }

    Console.Clear();
    Console.WriteLine($"Selected difficulty: {difficulty}");

    List<QuestionModel> questions = questionsHelper.ReturnCorrectDifficultyQuestionsList(questionsTiers, difficulty);
    List<int> answers = [];

    Console.WriteLine($"Number of questions: {questions.Count}");
    Console.WriteLine("\nGame starting in...");

    for (int i = 3; i > 0; i--)
    {
        Console.WriteLine($"{i}...");
        Thread.Sleep(1000);
    }

    Console.WriteLine("\nGo");
    Thread.Sleep(1000);
    Console.Clear();

    stopwatch.Start();
    int correctAnswers = 0;

    foreach (var question in questions)
    {
        int answer;

        while (true)
        {
            Console.WriteLine($"{question.Text}");
            Console.WriteLine("Your answer: ");

            if (int.TryParse(Console.ReadLine(), out answer))
            {
                answers.Add(answer);
                break;
            }

            stopwatch.Stop();

            Console.WriteLine("\nOnly round positive and negative numbers are accepted");
            Thread.Sleep(1500);
            Console.Clear();

            stopwatch.Start();
        }

        if (answer == question.Answer)
        {
            Console.WriteLine("Correct");
            correctAnswers++;
        }
        else
        {
            Console.WriteLine("Incorrect");
        }

        Thread.Sleep(500);
        Console.Clear();
    }

    stopwatch.Stop();

    string stopwatchTime = $"{stopwatch.Elapsed.Minutes}min {stopwatch.Elapsed.Seconds}sec";

    ResultModel result = new()
    {
        Difficulty = difficulty,
        Questions = questions,
        Answers = answers,
        TimeTaken = stopwatchTime,
        CorrectAnswers = correctAnswers
    };

    stopwatch.Reset();
    results.Add(result);

    Console.WriteLine($"Time taken to solve the questions: {stopwatchTime}");
    Console.WriteLine($"Number of correct answers: {correctAnswers} \n");

    gameloop = AskYesNo("Go again?");
    
    Console.Clear();
} while (gameloop);

//Results option
if (AskYesNo("Would you like to view your results?"))
{
    Console.Clear();
    foreach (var result in results)
    {
        Console.WriteLine(result.ToString());
    }
}
else
{
    Console.Clear();
}

Console.WriteLine("Press any key to exit");
Console.ReadLine();
return;

//Yes or No prompt method
bool AskYesNo(string prompt)
{
    while (true)
    {
        Console.WriteLine($"{prompt} \nYes \nNo \n");
        string input = Console.ReadLine()?.Trim() ?? "";

        if (input.Equals("yes", StringComparison.OrdinalIgnoreCase)) return true;
        if (input.Equals("no", StringComparison.OrdinalIgnoreCase)) return false;

        Console.WriteLine("\nInvalid choice");
        Thread.Sleep(1200);
        Console.Clear();
    }
}