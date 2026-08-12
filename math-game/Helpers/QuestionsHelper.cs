using System.Text.Json;
using System.Text.Json.Serialization;
using math_game.Lib;
using math_game.Models;

namespace math_game.Helpers;

public static class QuestionsHelper
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        }
    };

    public static List<QuestionTierModel> ReturnAllQuestionTiers()
    {
        List<QuestionTierModel> questionsTierList = [];
        
        string questionsJson = File.ReadAllText("Data/questions.json");
        
        questionsTierList.AddRange(JsonSerializer.Deserialize<List<QuestionTierModel>>(questionsJson, Options)!);

        return questionsTierList;
    }

    public static List<QuestionModel> ReturnCorrectDifficultyQuestionsList(List<QuestionTierModel> questionsTiers,
        DifficultyChoices difficulty)
    {
        List<QuestionModel> questions = [];

        PopulateFromDifficultyChoice(questionsTiers, difficulty, questions);

        return RandomizeQuestionsList(questions);
    }
    
    private static void PopulateFromDifficultyChoice(List<QuestionTierModel> questionTierList, DifficultyChoices difficultyChoice,
        List<QuestionModel> questionsList)
    {
        if (difficultyChoice is DifficultyChoices.Easy or DifficultyChoices.Medium or DifficultyChoices.Hard)
        {
            foreach (var questionTier in questionTierList)
            {
                if (questionTier.Difficulty != difficultyChoice) continue;

                questionsList.AddRange(questionTier.QuestionsList);
            }
        }

        if (difficultyChoice == DifficultyChoices.Mixed)
        {
            foreach (var questionTier in questionTierList)
            {
                questionsList.AddRange(questionTier.QuestionsList);
            }
        }
    }

    private static List<QuestionModel> RandomizeQuestionsList(List<QuestionModel> questions)
    {
        return questions.OrderBy(x => Guid.NewGuid()).ToList();
    }
    
}