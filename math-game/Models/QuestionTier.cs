using System.Text.Json.Serialization;
using math_game.Lib;

namespace math_game.Models;

public class QuestionTier
{
    public DifficultyChoices Difficulty { get; set; }
    [JsonPropertyName("questions")]
    public List<Question> QuestionsList { get; set; } = [];
}