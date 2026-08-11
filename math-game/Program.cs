using System.Diagnostics;
using math_game.Models;
using math_game.Helpers;
using math_game.Lib;

Console.WriteLine("Welcome to the MathGame");

QuestionsHelper questionsHelper = new();
DifficultyHelper difficultyHelper = new();
Stopwatch stopwatch = new();

bool gameloop = true;

List<QuestionTierModel> questionsTiers;

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
        Console.WriteLine("Retry? \nYes \nNo");

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
        Console.WriteLine("Easy \nMedium \nHard \nMixed \nRandom");

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

    Console.WriteLine($"\nSelected difficulty: {difficulty}");

    List<QuestionModel> questions = questionsHelper.ReturnCorrectDifficultyQuestionsList(questionsTiers, difficulty);

    Console.WriteLine($"Number of questions: {questions.Count}");

    Console.WriteLine("\nGame starting in... \n");

    for (int i = 3; i > 0; i--)
    {
        Console.WriteLine($"{i}...");

        Thread.Sleep(1000);
    }

    Console.WriteLine("Go\n");

    stopwatch.Start();

    int correctAnswers = 0;

    foreach (var question in questions)
    {
        int answer;

        while (true)
        {
            Console.WriteLine(question.Text);

            Console.WriteLine("Your answer: ");

            if (int.TryParse(Console.ReadLine(), out answer))
            {
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
    }


    stopwatch.Stop();

    Console.WriteLine(
        $"\nTime taken to solve the questions: {stopwatch.Elapsed.Minutes}min {stopwatch.Elapsed.Seconds}sec");

    Console.WriteLine($"Number of correct answers: {correctAnswers} \n");
    
    Console.WriteLine("Go again? \nYes \nNo");

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

Console.WriteLine("\nPress any key to exit");

Console.ReadLine();