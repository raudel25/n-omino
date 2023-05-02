using InfoGame;
using Table;

namespace Rules;

public class AssignScoreClassic<T> : IAssignScorePlayer<T>
{
    public void AssignScore(GameStatus<T> game, IAssignScoreToken<T> rules, int ind)
    {
        game.Players[game.Turns[ind]].Score = 100;
    }
}

public class AssignScoreHands<T> : IAssignScorePlayer<T>
{
    public void AssignScore(GameStatus<T> game, IAssignScoreToken<T> rules, int ind)
    {
        for (var i = 0; i < game.Players.Count; i++)
        {
            var sum = 0;
            foreach (var item in game.Players[i].Hand) sum += rules.ScoreToken(item);

            game.Players[i].Score = sum;
        }
    }
}

public class AssignScoreHandsSmallCant<T> : IAssignScorePlayer<T>
{
    public void AssignScore(GameStatus<T> game, IAssignScoreToken<T> rules, int ind)
    {
        for (var i = 0; i < game.Players.Count; i++)
        {
            var sum = 0;
            foreach (var item in game.Players[i].Hand) sum += rules.ScoreToken(item);

            game.Players[i].Score = sum * game.Players[i].Hand.Count;
        }
    }
}

public class AssignScoreHandsHighCant<T> : IAssignScorePlayer<T>
{
    public void AssignScore(GameStatus<T> game, IAssignScoreToken<T> rules, int ind)
    {
        for (var i = 0; i < game.Players.Count; i++)
        {
            var sum = 0;
            foreach (var item in game.Players[i].Hand) sum += rules.ScoreToken(item);

            game.Players[i].Score = (double)sum / game.Players[i].Hand.Count;
        }
    }
}

public class AssignScoreSumFreeNode : IAssignScorePlayer<int>
{
    public void AssignScore(GameStatus<int> game, IAssignScoreToken<int> rules, int ind)
    {
        game.Players[game.Turns[ind]].Score = AuxTable.SumConnectionFree(game.Table);
    }
}