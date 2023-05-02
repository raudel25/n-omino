using InfoGame;

namespace Rules;

public class WinnerGameHigh<T> : IWinnerGame<T>
{
    public void Winner(GameStatus<T> game, int ind)
    {
        var aux = game.Players.ToArray();
        Array.Sort(aux, (o1, o2) => o2.Score.CompareTo(o1.Score));
        game.PlayerWinner = aux[0].Id;
        game.TeamWinner = game.Teams[game.FindTeamPlayer(aux[0].Id)].Id;
    }
}

public class WinnerGameSmall<T> : IWinnerGame<T>
{
    public void Winner(GameStatus<T> game, int ind)
    {
        var aux = game.Players.ToArray();
        Array.Sort(aux, (o1, o2) => o1.Score.CompareTo(o2.Score));
        game.PlayerWinner = aux[0].Id;
        game.TeamWinner = game.Teams[game.FindTeamPlayer(aux[0].Id)].Id;
    }
}

public class WinnerGameTeamHigh<T> : IWinnerGame<T>
{
    public void Winner(GameStatus<T> game, int ind)
    {
        var max = double.MinValue;
        var win = 0;
        for (var i = 0; i < game.Teams.Count; i++)
        {
            double sum = 0;
            for (var j = 0; j < game.Teams[i].Count; j++) sum += game.Teams[i][j].Score;

            if (sum > max)
            {
                max = sum;
                win = i;
            }
        }

        var rnd = new Random();
        game.PlayerWinner = game.Teams[win][rnd.Next(game.Teams[win].Count)].Id;
        game.TeamWinner = game.Teams[win].Id;
    }
}

public class WinnerGameTeamSmall<T> : IWinnerGame<T>
{
    public void Winner(GameStatus<T> game, int ind)
    {
        var min = double.MaxValue;
        var win = 0;
        for (var i = 0; i < game.Teams.Count; i++)
        {
            double sum = 0;
            for (var j = 0; j < game.Teams[i].Count; j++) sum += game.Teams[i][j].Score;

            if (sum < min)
            {
                min = sum;
                win = i;
            }
        }

        var rnd = new Random();
        game.PlayerWinner = game.Teams[win][rnd.Next(game.Teams[win].Count)].Id;
        game.TeamWinner = game.Teams[win].Id;
    }
}