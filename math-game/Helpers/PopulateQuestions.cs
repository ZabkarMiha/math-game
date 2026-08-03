using System.Text.Json;
using System.Text.Json.Serialization;
using math_game.Models;

namespace math_game.Helpers;

public class PopulateQuestions
{
    public void PopulateList(List<Questions> questions)
    {
        string questionsJson = File.ReadAllText("Data/questions.json");
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive =  true,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };

        questions.AddRange(JsonSerializer.Deserialize<List<Questions>>(questionsJson, options)!);
    }
}