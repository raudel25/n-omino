using InfoGame;
using Table;

namespace Rules;

public interface IAssignScorePlayer<T> where T : ICloneable<T>
{
    /// <summary>
    /// Determinar la forma de asignar puntos a un jugador
    /// </summary>
    /// <param name="game">Estado del juego</param>
    /// <param name="ind">Indice del jugador</param>
    /// <param name="rules">Reglas del juego</param>
    public void AssignScore(GameStatus<T> game, InfoRules<T> rules, int ind);
}

public class AssignScoreClassic<T> : IAssignScorePlayer<T> where T : ICloneable<T>
{
    public void AssignScore(GameStatus<T> game, InfoRules<T> rules, int ind)
    {
        game.Players[game.Turns[ind]].Score = 100;
    }
}

public class AssignScoreHands<T> : IAssignScorePlayer<T> where T : ICloneable<T>
{
    public void AssignScore(GameStatus<T> game, InfoRules<T> rules, int ind)
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

public class AssignScoreHandsSmallCant<T> : IAssignScorePlayer<T> where T : ICloneable<T>
{
    public void AssignScore(GameStatus<T> game, InfoRules<T> rules, int ind)
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

public class AssignScoreHandsHighTokens<T> : IAssignScorePlayer<T> where T : ICloneable<T>
{
    public void AssignScore(GameStatus<T> game, InfoRules<T> rules, int ind)
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

public class AssignScoreSumFreeNode : IAssignScorePlayer<int> 
{
    public void AssignScore(GameStatus<int> game, InfoRules<int> rules, int ind)
    {
        game.Players[game.Turns[ind]].Score = AuxTable.SumConnectionFree(game.Table);
    }
}