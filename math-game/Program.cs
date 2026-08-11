using System.Diagnostics;
using math_game.Models;
using math_game.Helpers;
using math_game.Lib;

Console.WriteLine("Welcome to the MathGame \n");

QuestionsHelper questionsHelper = new();
DifficultyHelper difficultyHelper = new();
Stopwatch stopwatch = new();

bool gameloop = true;

List<QuestionTierModel> questionsTiers;
List<ResultModel> results = [];

while (true)
{
    try
    {
        questionsTiers = questionsHelper.ReturnAllQuestionTiers();

        break;
    }
    catch (Exception e)
    {
        Console.WriteLine($"Something went wrong. {e.Message}");
        Console.WriteLine("\nRetry? \nYes \nNo \n");

        switch (Console.ReadLine())
        {
            case "Yes":
                break;
            case "No":
                throw;
        }
    }
}

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
                Console.WriteLine("Invalid choice");
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

            Console.WriteLine("Only round positive and negative numbers are accepted");
        }

        if (answer == question.Answer)
        {
            Console.WriteLine("Correct");
            correctAnswers++;
        }
        else
            Console.WriteLine("Incorrect");

        Thread.Sleep(700);

        Console.Clear();
    }

    stopwatch.Stop();

    ResultModel result = new()
    {
        Difficulty = difficulty,
        Questions = questions,
        Answers = answers,
        TimeTaken = $"{stopwatch.Elapsed.Minutes}min {stopwatch.Elapsed.Seconds}sec",
        CorrectAnswers = correctAnswers
    };

    results.Add(result);

    Console.WriteLine(
        $"Time taken to solve the questions: {stopwatch.Elapsed.Minutes}min {stopwatch.Elapsed.Seconds}sec");

    Console.WriteLine($"Number of correct answers: {correctAnswers}");

    Console.WriteLine("\nGo again? \nYes \nNo \n");

    switch (Console.ReadLine()?.ToLower())
    {
        case "yes":
            Console.Clear();
            break;
        case "no":
            gameloop = false;
            break;
    }
} while (gameloop);

Console.Clear();

Console.WriteLine("Would you like to view your results? \nYes \nNo \n");

switch (Console.ReadLine())
{
    case "yes":
        Console.Clear();
        foreach (var result in results)
        {
            Console.WriteLine(result.ToString());
        }

        break;
    case "no":
        Console.Clear();
        break;
}

Console.WriteLine("Press any key to exit");

Console.ReadLine();