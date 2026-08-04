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
        List<QuestionTierModel> questions = [];

        bool retry = true;

        while (retry)
        {
            try
            {
                retry = false;

                string questionsJson = File.ReadAllText("Data/questions.json");

                questions.AddRange(JsonSerializer.Deserialize<List<QuestionTierModel>>(questionsJson, Options)!);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading questions.json " + e.Message);
                Console.WriteLine("Retry?");
                Console.WriteLine("Yes");
                Console.WriteLine("No");

                switch (Console.ReadLine())
                {
                    case "Yes":
                        retry = true;
                        break;
                    case "No":
                        throw;
                }
            }
        }

        return questions;
    }

    public List<QuestionModel> RandomizeList(List<QuestionModel> questions)
    {
        IOrderedEnumerable<QuestionModel> randomizedList = questions.OrderBy(x => Guid.NewGuid());

        return randomizedList.ToList();
    }
}