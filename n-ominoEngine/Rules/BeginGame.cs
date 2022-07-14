using InfoGame;
using Table;

namespace Rules;

public interface IBeginGame<T>
{
    /// <summary>
    /// Determinar como se inicia el juego
    /// </summary>
    /// <param name="tournament">Datos del torneo</param>
    /// <param name="game">Datos del juego</param>
    /// <param name="rules">Reglas del juego</param>
    public void Start(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules);
}

public class BeginGameToken<T> : IBeginGame<T>
{
    private Token<T> _token;

    public BeginGameToken(Token<T> token)
    {
        this._token = token;
    }

    public void Start(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules)
    {
        int id = -1;
        foreach (var item in game.Players)
        {
            if (item.Hand!.Contains(_token))
            {
                id = item.Id;
                break;
            }
        }

        if (id == -1)
        {
            IBeginGame<T> begin = new BeginGameRandom<T>();
            begin.Start(tournament, game, rules);
        }
        else
        {
            game.TokenStart = this._token;
            game.PlayerStart = id;
        }
    }
}

public class BeginGameRandom<T> : IBeginGame<T>
{
    public void Start(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules)
    {
        Random rnd = new Random();
        int ind = rnd.Next(game.Turns.Length);
        game.PlayerStart = game.Players[ind].Id;
    }
}

public class BeginGameLastWinner<T> : IBeginGame<T>
{
    public void Start(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules)
    {
        if (tournament.Index > 0)
        {
            int ind = tournament.ImmediateWinnerTeam;

            Random rnd = new Random();
            int aux = rnd.Next(tournament.Teams[ind].Count);

            game.PlayerStart = tournament.Teams[ind][aux].Id;
        }
    }
}