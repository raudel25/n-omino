using InfoGame;
using Table;

namespace Rules;

public class BeginGameToken<T> : IBeginGame<T>
{
    private readonly Token<T> _token;

    public BeginGameToken(Token<T> token)
    {
        _token = token;
    }

    public void Start(TournamentStatus tournament, GameStatus<T> game)
    {
        var id = -1;
        foreach (var item in game.Players)
            if (item.Hand.Contains(_token))
            {
                id = item.Id;
                break;
            }

        if (id == -1)
        {
            IBeginGame<T> begin = new BeginGameRandom<T>();
            begin.Start(tournament, game);
        }
        else
        {
            game.TokenStart = _token;
            game.PlayerStart = id;
        }
    }
}

public class BeginGameRandom<T> : IBeginGame<T>
{
    public void Start(TournamentStatus tournament, GameStatus<T> game)
    {
        var rnd = new Random();
        var ind = rnd.Next(game.Turns.Length);
        game.PlayerStart = game.Players[ind].Id;
    }
}

public class BeginGameLastWinner<T> : IBeginGame<T>
{
    public void Start(TournamentStatus tournament, GameStatus<T> game)
    {
        if (tournament.ImmediateWinnerTeam != -1)
        {
            var ind = game.FindTeamById(tournament.ImmediateWinnerTeam);

            //Si no se encuentra el equipo ganador determinamos uno random
            if (ind == -1)
            {
                IBeginGame<T> begin = new BeginGameRandom<T>();
                begin.Start(tournament, game);
                return;
            }

            var rnd = new Random();
            var aux = rnd.Next(tournament.Teams[ind].Count);

            game.PlayerStart = tournament.Teams[ind][aux].Id;
        }

        if (game.PlayerStart == -1)
        {
            IBeginGame<T> begin = new BeginGameRandom<T>();
            begin.Start(tournament, game);
        }
    }
}