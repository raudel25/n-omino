using InfoGame;
using Table;

namespace Rules;

public interface IStealToken
{
    /// <summary>
    /// Cantidad de fichas minimas que puede robar el jugador
    /// </summary>
    int CantMin { get; }

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
    public void Steal(GameStatus game, GameStatus original, InfoRules rules, int ind, ref bool play);
}

public class NoStealToken : IStealToken
{
    public int CantMin { get; private set; }
    public int CantMax { get; private set; }

    public NoStealToken()
    {
        this.CantMax = 0;
        this.CantMin = 0;
    }

    public void Steal(GameStatus game, GameStatus original, InfoRules rules, int ind, ref bool play)
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

    public void Steal(GameStatus game, GameStatus original, InfoRules rules, int ind, ref bool play)
    {
        Random rnd = new Random();
        while (true)
        {
            Token aux = game.TokensTable![rnd.Next(game.TokensTable.Count)];
            //Actualizar la mano
            game.Players[original.Turns[ind]].Hand!.Add(aux);
            original.Players[original.Turns[ind]].Hand!.Add(aux);
            game.TokensTable!.Remove(aux);
            original.TokensTable!.Remove(aux);
            foreach (var item in game.Table.FreeNode)
            {
                if (rules.ValidPlays(item, aux, game.Table).Count != 0) break;
                play = true;
            }

            if (game.TokensTable.Count == 0) break;
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

    public void Steal(GameStatus game, GameStatus original, InfoRules rules, int ind, ref bool play)
    {
    }
}