using System.Text.Json.Serialization;
using math_game.Lib;

namespace math_game.Models;

public class QuestionTierModel
{
    public DifficultyChoices Difficulty { get; set; }
    [JsonPropertyName("questions")]
    public List<QuestionModel> QuestionsList { get; set; } = [];
}