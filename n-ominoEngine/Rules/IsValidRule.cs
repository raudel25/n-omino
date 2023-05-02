using InfoGame;
using Table;

namespace Rules;

public class IsValidRule<T> : ActionConditionRule<IValidPlay<T>, T>, ICloneable<IsValidRule<T>>
{
    private readonly bool[] _checkValid;

    public IsValidRule(IEnumerable<IValidPlay<T>> rules, IEnumerable<ICondition<T>> condition,
        IValidPlay<T> rule) : base(rules, condition, rule)
    {
        _checkValid = new bool[Actions.Length + 1];
    }

    public int CantValid => _checkValid.Length;

    public (IValidPlay<T>, bool) this[int index]
    {
        get
        {
            if (index < Actions.Length && index >= 0) return (Actions[index], _checkValid[index]);
            if (index == Actions.Length) return (Default!, _checkValid[index]);
            throw new Exception("Index Out of Range");
        }
    }

    public IsValidRule<T> Clone()
    {
        return new IsValidRule<T>(Actions, Condition, Default!);
    }

    public void RunRule(TournamentStatus tournament, GameStatus<T> original,
        IAssignScoreToken<T> rules, int ind)
    {
        for (var i = 0; i < Condition.Length; i++)
        {
            _checkValid[i] = false;
            if (Condition[i].RunRule(tournament, original, rules, ind)) _checkValid[i] = true;
        }

        _checkValid[Actions.Length] = true;
    }

    /// <summary>Determinar si una jugada es correcta segun las reglas existentes</summary>
    /// <param name="node">Nodo por el que se quiere jugar</param>
    /// <param name="token">Ficha para jugar</param>
    /// <param name="game">Estado del juego</param>
    /// <param name="ind">Indice del jugador relativo a la mesa</param>
    /// <returns>Criterios de jugada valida correspomdientes a la ficha y el nodo</returns>
    public List<int> ValidPlays(INode<T> node, Token<T> token, GameStatus<T> game, int ind)
    {
        var valid = new List<int>();
        for (var j = 0; j < Actions.Length; j++)
            if (_checkValid[j] && Actions[j].ValidPlay(node, token, game, ind))
                valid.Add(j);

        if (_checkValid[Actions.Length] && Default!.ValidPlay(node, token, game, ind))
            valid.Add(Actions.Length);

        return valid;
    }

    /// <summary>Determina si el jugador tiene opciones para jugar</summary>
    /// <param name="tokens">Fichas de las que dispone el jugador</param>
    /// <param name="game">Estado del juego</param>
    /// <param name="ind">Indice del jugador relativo a la mesa</param>
    /// <returns>El jugador tiene opciones para jugar</returns>
    public bool ValidPlayPlayer(Hand<T> tokens, GameStatus<T> game, int ind)
    {
        foreach (var item in game.Table.FreeNode)
        foreach (var token in tokens)
            if (ValidPlays(item, token, game, ind).Count != 0)
                return true;

        return false;
    }

    /// <summary>
    ///     Determinar los valores a asignar por un criterio de juego determinado
    /// </summary>
    /// <param name="node">Nodo por el que se quiere jugar</param>
    /// <param name="token">Ficha para jugar</param>
    /// <param name="table">Mesa para jugar</param>
    /// <param name="ind">Criterio del juego determinado</param>
    /// <returns>Valores a asignar en el nodo, si devuelve un array vacio si no es posible jugar</returns>
    public T[] AssignValues(INode<T> node, Token<T> token, TableGame<T> table, int ind)
    {
        if (ind == Actions.Length) return Default!.AssignValues(node, token, table);
        return Actions[ind].AssignValues(node, token, table);
    }
}