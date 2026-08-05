using math_game.Lib;

namespace math_game.Helpers;

public class DifficultyHelper
{
    public DifficultyChoices RandomDifficulty()
    {
        DifficultyChoices[] difficultyChoicesArray = Enum.GetValues<DifficultyChoices>();
        
        return difficultyChoicesArray[Random.Shared.Next(difficultyChoicesArray.Length)];;
    }
}