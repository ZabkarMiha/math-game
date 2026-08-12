using math_game.Lib;

namespace math_game.Helpers;

public static class DifficultyHelper
{
    public static DifficultyChoices ReturnRandomDifficulty()
    {
        DifficultyChoices[] difficultyChoicesArray = Enum.GetValues<DifficultyChoices>();
        
        return difficultyChoicesArray[Random.Shared.Next(difficultyChoicesArray.Length)];;
    }
}