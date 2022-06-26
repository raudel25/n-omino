using InfoGame;
using Table;

namespace Rules;

public interface IBeginGame<T> where T : struct
{
    public void Start(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules);
}

public class BeginGameToken<T> : IBeginGame<T> where T : struct
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
            game.Players[id].Hand!.Remove(_token);
            game.Table.PlayTable(game.Table.TableNode[0], _token,
                rules.IsValidPlay.Default!.AssignValues(game.Table.TableNode[0], _token, game.Table));

            for (int i = 0; i < game.Turns.Length; i++)
            {
                if (id == game.Turns[i])
                {
                    game.PlayerStart = (id == game.Turns.Length - 1) ? 0 : id + 1;
                    break;
                }
            }
        }
    }
}

public class BeginGameRandom<T> : IBeginGame<T> where T : struct
{
    public void Start(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules)
    {
        Random rnd = new Random();
        game.PlayerStart = rnd.Next(game.Turns.Length);
    }
}