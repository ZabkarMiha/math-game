using math_game.Lib;

namespace math_game.Models;

public class ResultModel
{
    public int Id { get; set; } = 0;
    public DifficultyChoices Difficulty { get; init; }
    public List<QuestionModel> Questions { get; init; } = [];
    public List<int> Answers { get; init; } = [];
    public string TimeTaken { get; init; } = string.Empty;
    public int CorrectAnswers { get; init; }
    private int QuestionsCount => Questions.Count;
    private int IncorrectAnswers => QuestionsCount - CorrectAnswers;

    public override string ToString()
    {
        string questionAnswer = string.Empty;

        for (int i = 0; i < QuestionsCount; i++)
        {
            questionAnswer +=
                $"Question: {Questions[i].Text} Your answer: {Answers[i]} Correct answer: {Questions[i].Answer}\n";
        }

        return
            $"Difficulty: {Difficulty} \nTime taken: {TimeTaken} \nNumber of questions: {QuestionsCount} \nCorrect answers: {CorrectAnswers} \nIncorrect answers: {IncorrectAnswers} \nList of questions:\n{questionAnswer} \n";
    }
}