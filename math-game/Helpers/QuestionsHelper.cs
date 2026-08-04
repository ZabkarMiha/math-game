using System.Text.Json;
using System.Text.Json.Serialization;
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

    public List<QuestionTierModel> PopulateList()
    {
        string questionsJson = File.ReadAllText("Data/questions.json");
        
        return JsonSerializer.Deserialize<List<QuestionTierModel>>(questionsJson, Options)!;
    }

    public List<QuestionModel> RandomizeList(List<QuestionModel> questions)
    {
        List<QuestionModel> randomizedList = questions.OrderBy(x => Guid.NewGuid()).ToList();
        
        return randomizedList;
    }
}