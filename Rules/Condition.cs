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

public class ClassicWin : ICondition
{
    public bool RunRule(GameStatus game, int ind)
    {
        return game.Players[game.Turns[ind]].Hand!.Count == 0;
    }
}

public class ClassicTeamWin : ICondition
{
    public bool RunRule(GameStatus game, int ind)
    {
        bool win = true;
        for (int i = 0; i < game.Teams.Length; i++)
        {
            for (int j = 0; j < game.Teams[i].Count; j++)
            {
                if (game.Teams[game.Turns[i]][j].Hand!.Count != 0) win = false;
            }
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
        for (int i = 0; i < game.Teams.Length; i++)
        {
            for (int j = 0; j < game.Teams[i].Count; j++)
            {
                if (game.Teams[i][j].Passes == this.Cant) condition = false;
            }
        }

        return condition;
    }
}

public class ImmediatePass : ICondition
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
    private IComparison _comparison;

    public SumFreeNode(int value, IComparison comparison)
    {
        this._comparison = comparison;
        this.Value = value;
    }

    public bool RunRule(GameStatus game, int ind)
    {
        return this._comparison.Compare(AuxTable.SumConnectionFree(game.Table), this.Value);
    }
}

public class ConditionDefault : ICondition
{
    public bool RunRule(GameStatus game, int ind)
    {
        return true;
    }
}