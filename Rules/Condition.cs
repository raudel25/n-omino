using InfoGame;
using Table;

namespace Rules;

public interface ICondition
{
    /// <summary>
    /// Determinar bajo que condiciones se ejecuta una regla
    /// </summary>
    /// <param name="game">Estado del juego</param>
    /// <param name="ind">Indice del jugador que le corresponde jugar</param>
    /// <returns>Si es valido que se ejecute la regla</returns>
    public bool RunRule(GameStatus game, int ind);
}

public class ClasicWin : ICondition
{
    public bool RunRule(GameStatus game, int ind)
    {
        return game.Players[game.Turns[ind]].Hand!.Count == 0;
    }
}

public class ClasicTeamWin : ICondition
{
    public bool RunRule(GameStatus game, int ind)
    {
        bool win = true;
        for (int i = 0; i < game.Teams[game.Turns[ind]].Count; i++)
        {
            if (game.Teams[game.Turns[ind]][i].Hand!.Count != 0) win = false;
        }

        return win;
    }
}

public class CantToPass : ICondition
{
    public int Cant { get; private set; }

    public CantToPass(int cant)
    {
        this.Cant = cant;
    }

    public bool RunRule(GameStatus game, int ind)
    {
        return game.Players[game.Turns[ind]].Passes == this.Cant;
    }
}

public class CantToPassTeam : ICondition
{
    public int Cant { get; private set; }

    public CantToPassTeam(int cant)
    {
        this.Cant = cant;
    }

    public bool RunRule(GameStatus game, int ind)
    {
        bool condition = true;
        for (int i = 0; i < game.Teams[game.Turns[ind]].Count; i++)
        {
            if (game.Teams[game.Turns[ind]][i].Passes == this.Cant) condition = false;
        }

        return condition;
    }
}

public class InmediatePass : ICondition
{
    public bool RunRule(GameStatus game, int ind)
    {
        return game.InmediatePass;
    }
}

public class NoValidPLay : ICondition
{
    public bool RunRule(GameStatus game, int ind)
    {
        return game.NoValidPlay;
    }
}

public class SumFreeNode : ICondition
{
    public int Value { get; private set; }
    private IComparation _comparation;

    public SumFreeNode(int value, IComparation comparation)
    {
        this._comparation = comparation;
        this.Value = value;
    }

    public bool RunRule(GameStatus game, int ind)
    {
        return this._comparation.Compare(AuxTable.SumConectionFree(game.Table), this.Value);
    }
}