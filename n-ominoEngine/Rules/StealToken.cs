using InfoGame;
using Table;

namespace Rules;

public class NoStealToken<T> : IStealToken<T>
{
    public NoStealToken()
    {
        CantMax = 0;
    }

    public int CantMax { get; }

    public void Steal(GameStatus<T> game, GameStatus<T> original, IsValidRule<T> rules, int ind, ref bool play)
    {
        game.TokensTable = new List<Token<T>>();
    }
}

public class ClassicStealToken<T> : IStealToken<T>
{
    public ClassicStealToken()
    {
        CantMax = 0;
    }

    public int CantMax { get; }

    public void Steal(GameStatus<T> game, GameStatus<T> original, IsValidRule<T> rules, int ind, ref bool play)
    {
        var rnd = new Random();
        while (true)
        {
            if (game.TokensTable.Count == 0) break;

            var aux = game.TokensTable[rnd.Next(game.TokensTable.Count)];

            //Actualizar la mano
            game.Players[original.Turns[ind]].Hand.Add(aux);
            original.Players[original.Turns[ind]].Hand.Add(aux);
            game.TokensTable.Remove(aux);
            original.TokensTable.Remove(aux);

            var hand = new Hand<T>();
            hand.Add(aux);
            play = rules.ValidPlayPlayer(hand, game, ind);

            if (play) break;
        }

        game.TokensTable = new List<Token<T>>();
    }
}

public class ChooseStealToken<T> : IStealToken<T>
{
    public ChooseStealToken(int a)
    {
        CantMax = a;
    }

    public int CantMax { get; }

    public void Steal(GameStatus<T> game, GameStatus<T> original, IsValidRule<T> rules, int ind, ref bool play)
    {
        for (var i = 0; i < game.TokensTable.Count; i++)
            foreach (var item in game.Table.FreeNode)
            {
                if (rules.ValidPlays(item, game.TokensTable[i], game, ind).Count != 0) break;
                play = true;
            }
    }
}