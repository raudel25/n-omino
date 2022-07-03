using InfoGame;
using Table;

namespace Rules;

public interface IStealToken<T>
{
    /// <summary>
    /// Cantidad de fichas maximas que puede robar el jugador
    /// </summary>
    int CantMax { get; }

    /// <summary>
    /// Determinar las condiciones bajo las cuales se puede robar en el juego
    /// </summary>
    /// <param name="game">Estado del juego</param>
    /// <param name="ind">Indice del jugador que le corresponde jugar</param>
    /// <param name="rules">Reglas del juego</param>
    /// <param name="original">Estado Original del juego</param>
    /// <param name="play">Determinar si es posible jugar</param>
    public void Steal(GameStatus<T> game, GameStatus<T> original, InfoRules<T> rules, int ind, ref bool play);
}

public class NoStealToken<T> : IStealToken<T>
{
    public int CantMax { get; private set; }

    public NoStealToken()
    {
        this.CantMax = 0;
    }

    public void Steal(GameStatus<T> game, GameStatus<T> original, InfoRules<T> rules, int ind, ref bool play)
    {
        game.TokensTable = null;
    }
}

public class ClassicStealToken<T> : IStealToken<T>
{
    public int CantMax { get; private set; }

    public ClassicStealToken()
    {
        this.CantMax = 0;
    }

    public void Steal(GameStatus<T> game, GameStatus<T> original, InfoRules<T> rules, int ind, ref bool play)
    {
        Random rnd = new Random();
        while (true)
        {
            if (game.TokensTable!.Count == 0) break;
            
            Token<T> aux = game.TokensTable![rnd.Next(game.TokensTable.Count)];
            
            //Actualizar la mano
            game.Players[original.Turns[ind]].Hand!.Add(aux);
            original.Players[original.Turns[ind]].Hand!.Add(aux);
            game.TokensTable!.Remove(aux);
            original.TokensTable!.Remove(aux);
            
            foreach (var item in game.Table.FreeNode)
            {
                if (rules.IsValidPlay.ValidPlays(item, aux, game.Table).Count != 0)
                {
                    play = true;
                    break;
                }
            }
            
            if(play) break;
        }

        game.TokensTable = null;
    }
}

public class ChooseStealToken<T> : IStealToken<T>
{
    public int CantMax { get; private set; }

    public ChooseStealToken(int a)
    {
        this.CantMax = a;
    }

    public void Steal(GameStatus<T> game, GameStatus<T> original, InfoRules<T> rules, int ind, ref bool play)
    {
        for (int i = 0; i < game.TokensTable!.Count; i++)
        {
            foreach (var item in game.Table.FreeNode)
            {
                if (rules.IsValidPlay.ValidPlays(item, game.TokensTable[i], game.Table).Count != 0) break;
                play = true;
            }
        }
    }
}