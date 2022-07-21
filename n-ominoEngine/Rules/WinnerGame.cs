using InfoGame;

namespace Rules;

public class WinnerGameHigh<T> : IWinnerGame<T>
{
    public void Winner(GameStatus<T> game, int ind)
    {
        InfoPlayer<T>[] aux = game.Players.ToArray();
        Array.Sort(aux, (o1, o2) => (o2.Score.CompareTo(o1.Score)));
        game.PlayerWinner = aux[0].Id;
        game.TeamWinner = game.Teams[game.FindTeamPlayer(aux[0].Id)].Id;
    }
}

public class WinnerGameSmall<T> : IWinnerGame<T>
{
    public void Winner(GameStatus<T> game, int ind)
    {
        InfoPlayer<T>[] aux = game.Players.ToArray();
        Array.Sort(aux, (o1, o2) => (o1.Score.CompareTo(o2.Score)));
        game.PlayerWinner = aux[0].Id;
        game.TeamWinner = game.Teams[game.FindTeamPlayer(aux[0].Id)].Id;
    }
}

public class WinnerGameTeamHigh<T> : IWinnerGame<T>
{
    public void Winner(GameStatus<T> game, int ind)
    {
        double max = double.MinValue;
        int win = 0;
        for (int i = 0; i < game.Teams.Count; i++)
        {
            double sum = 0;
            for (int j = 0; j < game.Teams[i].Count; j++)
            {
                sum += game.Teams[i][j].Score;
            }

            if (sum > max)
            {
                max = sum;
                win = i;
            }
        }

        game.TeamWinner = game.Teams[win].Id;
    }
}

public class WinnerGameTeamSmall<T> : IWinnerGame<T>
{
    public void Winner(GameStatus<T> game, int ind)
    {
        double min = double.MaxValue;
        int win = 0;
        for (int i = 0; i < game.Teams.Count; i++)
        {
            double sum = 0;
            for (int j = 0; j < game.Teams[i].Count; j++)
            {
                sum += game.Teams[i][j].Score;
            }

            if (sum < min)
            {
                min = sum;
                win = i;
            }
        }

        game.TeamWinner = game.Teams[win].Id;
    }
}