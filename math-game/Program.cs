using System.Diagnostics;
using math_game.Models;
using math_game.Helpers;
using math_game.Lib;

Console.WriteLine("Welcome to the MathGame");

QuestionsHelper questionsHelper = new();
Stopwatch stopwatch = new();

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
            difficulty = DifficultyChoices.Random;
            break;
        default:
            Console.WriteLine("Invalid choice");
            continue;
    }

    break;
}

Console.WriteLine($"Selected difficulty: {difficulty}");

List<QuestionModel> questions;

while (true)
{
    try
    {
        questions = questionsHelper.ReturnPopulatedList(difficulty);

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

stopwatch.Start();

Console.WriteLine($"Number of questions: {questions.Count}");

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

Console.WriteLine($"Time taken to solve the questions: {stopwatch.Elapsed.Minutes}min {stopwatch.Elapsed.Seconds}sec");

Console.WriteLine($"Number of correct answers: {correctAnswers}");

Console.ReadLine();