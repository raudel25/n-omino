using InfoGame;
using Table;

namespace Rules;

public interface IAsignScorePlayer
{
    /// <summary>
    /// Determinar la forma de asignar puntos a un jugador
    /// </summary>
    /// <param name="game">Estado del juego</param>
    /// <param name="ind">Indice del jugador</param>
    /// <param name="rules">Reglas del juego</param>
    public void AsignScore(GameStatus game, InfoRules rules, int ind);
}

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

            game.Players[i].Score = sum;
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

            game.Players[i].Score = sum * game.Players[i].Hand!.Count;
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

            game.Players[i].Score = (double) (sum) / game.Players[i].Hand!.Count;
        }
    }
}

public class AsignScoreSumFreeNode : IAsignScorePlayer
{
    public void AsignScore(GameStatus game, InfoRules rules, int ind)
    {
        game.Players[game.Turns[ind]].Score = AuxTable.SumConectionFree(game.Table);
    }
}