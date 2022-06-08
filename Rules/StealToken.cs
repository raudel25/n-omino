using InfoGame;
using Table;

namespace Rules;

public class NoStealToken : IStealToken
{
    public int CantMin { get; private set; }
    public int CantMax { get; private set; }

    public NoStealToken()
    {
        this.CantMax = 0;
        this.CantMin = 0;
    }

    public void Steal(GameStatus game, GameStatus original, InfoRules rules, int player)
    {
        game.TokensTable = null;
    }
}

public class ClasicStealToken : IStealToken
{
    public int CantMin { get; private set; }
    public int CantMax { get; private set; }

    public ClasicStealToken()
    {
        this.CantMax = 0;
        this.CantMin = 0;
    }

    public void Steal(GameStatus game, GameStatus original, InfoRules rules, int player)
    {
        Random rnd = new Random();
        while (true)
        {
            Token aux = game.TokensTable![rnd.Next(game.TokensTable.Count)];
            //Actualizar la mano
            game.Players[player].Hand!.Add(aux);
            original.Players[player].Hand!.Add(aux);
            game.TokensTable!.Remove(aux);
            original.TokensTable!.Remove(aux);
            foreach (var item in game.Table.FreeNode)
            {
                if(rules.ValidPlays(item,aux,game.Table).Count!=0) break;                
            }
            if(game.TokensTable.Count==0) break;
        }

        game.TokensTable = null;
    }
}

public class ChooseStealToken : IStealToken
{
    public int CantMin { get; private set; }
    public int CantMax { get; private set; }

    public ChooseStealToken(int a, int b)
    {
        this.CantMax = a;
        this.CantMin = b;
    }

    public void Steal(GameStatus game, GameStatus original, InfoRules rules, int player)
    {
        
    }
}