using InfoGame;
using Table;

namespace Rules;

public class NoStealToken<T> : IStealToken<T>
{
    public int CantMax { get; private set; }

    public NoStealToken()
    {
        this.CantMax = 0;
    }

    public void Steal(GameStatus<T> game, GameStatus<T> original, IsValidRule<T> rules, int ind, ref bool play)
    {
        game.TokensTable = new List<Token<T>>();
    }
}

public class ClassicStealToken<T> : IStealToken<T>
{
    public int CantMax { get; private set; }

    public ClassicStealToken()
    {
        this.CantMax = 0;
    }

    public void Steal(GameStatus<T> game, GameStatus<T> original, IsValidRule<T> rules, int ind, ref bool play)
    {
        Random rnd = new Random();
        while (true)
        {
            if (game.TokensTable.Count == 0) break;

            Token<T> aux = game.TokensTable[rnd.Next(game.TokensTable.Count)];

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
    public int CantMax { get; private set; }

    public ChooseStealToken(int a)
    {
        this.CantMax = a;
    }

    public void Steal(GameStatus<T> game, GameStatus<T> original, IsValidRule<T> rules, int ind, ref bool play)
    {
        for (int i = 0; i < game.TokensTable.Count; i++)
        {
            foreach (var item in game.Table.FreeNode)
            {
                if (rules.ValidPlays(item, game.TokensTable[i], game, ind).Count != 0) break;
                play = true;
            }
        }
    }
}