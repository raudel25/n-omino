using InfoGame;

namespace Rules;

public class AsignScoreClasic : IAsignScorePlayer
{
    public void AsignScore(GameStatus game, InfoRules rules, int ind)
    {
        game.Players[game.Turns[ind]].Score = 100;
    }
}

public class AsignScoreHands : IAsignScorePlayer
{
    public void AsignScore(GameStatus game, InfoRules rules, int ind)
    {
        for (int i = 0; i < game.Players.Length; i++)
        {
            int sum = 0;
            foreach (var item in game.Players[i].Hand!)
            {
                sum += rules.ScoreToken.ScoreToken(item);
            }

            game.Players[i].Score = -1 * sum;
        }
    }
}

public class AsignScoreHandsMenorCant : IAsignScorePlayer
{
    public void AsignScore(GameStatus game, InfoRules rules, int ind)
    {
        for (int i = 0; i < game.Players.Length; i++)
        {
            int sum = 0;
            foreach (var item in game.Players[i].Hand!)
            {
                sum += rules.ScoreToken.ScoreToken(item);
            }

            game.Players[i].Score = -1 * sum * game.Players[i].Hand!.Count;
        }
    }
}

public class AsignScoreHandsMayorTokens : IAsignScorePlayer
{
    public void AsignScore(GameStatus game, InfoRules rules, int ind)
    {
        for (int i = 0; i < game.Players.Length; i++)
        {
            int sum = 0;
            foreach (var item in game.Players[i].Hand!)
            {
                sum += rules.ScoreToken.ScoreToken(item);
            }

            game.Players[i].Score = (double)(-1 * sum) / game.Players[i].Hand!.Count;
        }
    }
}