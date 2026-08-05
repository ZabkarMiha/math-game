using System.Text.Json;
using System.Text.Json.Serialization;
using math_game.Lib;
using math_game.Models;

namespace math_game.Helpers;

public class QuestionsHelper
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        }
    };

    public List<QuestionModel> ReturnPopulatedList(DifficultyChoices difficulty)
    {
        List<QuestionTierModel> questions = [];

        string questionsJson = File.ReadAllText("Data/questions.json");

        questions.AddRange(JsonSerializer.Deserialize<List<QuestionTierModel>>(questionsJson, Options)!);

        return ReturnCorrectDifficultyList(questions, difficulty);
    }

    private List<QuestionModel> ReturnCorrectDifficultyList(List<QuestionTierModel> questionsTiers,
        DifficultyChoices difficulty)
    {
        List<QuestionModel> questions = [];

        if (difficulty is DifficultyChoices.Easy or DifficultyChoices.Medium or DifficultyChoices.Hard
            or DifficultyChoices.Mixed)
        {
            PopulateDifficulty(questionsTiers, difficulty, questions);
        }
        else if (difficulty == DifficultyChoices.Random)
        {
            DifficultyChoices[] difficultyChoicesArray = Enum.GetValues<DifficultyChoices>()
                .Where(x => x != DifficultyChoices.Random).ToArray();

            DifficultyChoices randomDifficulty =
                difficultyChoicesArray[Random.Shared.Next(difficultyChoicesArray.Length)];

            PopulateDifficulty(questionsTiers, randomDifficulty, questions);
        }

        return RandomizeList(questions);
    }

    private List<QuestionModel> RandomizeList(List<QuestionModel> questions)
    {
        List<QuestionModel> randomizedList = questions.OrderBy(x => Guid.NewGuid()).ToList();

        return randomizedList;
    }

    private void PopulateDifficulty(List<QuestionTierModel> questionTierModels, DifficultyChoices difficultyChoices,
        List<QuestionModel> questionModels)
    {
        if (difficultyChoices is DifficultyChoices.Easy or DifficultyChoices.Medium or DifficultyChoices.Hard)
        {
            foreach (var questionTier in questionTierModels)
            {
                if (questionTier.Difficulty != difficultyChoices) continue;

                questionModels.AddRange(questionTier.QuestionsList);
            }
        }

        if (difficultyChoices == DifficultyChoices.Mixed)
        {
            foreach (var questionTier in questionTierModels)
            {
                questionModels.AddRange(questionTier.QuestionsList);
            }
        }
    }
}