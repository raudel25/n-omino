using InfoGame;
using Table;

namespace Rules;

public class IsValidRule : ActionConditionRule<IValidPlay>
{
    private bool[] _comprobateValid;

    public IsValidRule(IEnumerable<IValidPlay> rules, IEnumerable<ICondition> condition,
        IValidPlay rule) : base(rules, condition, rule)
    {
        this._comprobateValid = new bool[this.Actions.Length + 1];
    }

    public override void RunRule(GameStatus game, GameStatus original, InfoRules rules, int ind)
    {
        bool activate = false;
        for (int i = 0; i < this.Condition.Length; i++)
        {
            this._comprobateValid[i] = false;
            if (this.Condition[i].RunRule(game, ind))
            {
                this._comprobateValid[i] = true;
                activate = true;
            }
        }

        if (!activate) this._comprobateValid[this.Actions.Length] = true;
    }

    /// <summary>Determinar si una jugada es correcta segun las reglas existentes</summary>
    /// <param name="node">Nodo por el que se quiere jugar</param>
    /// <param name="token">Ficha para jugar</param>
    /// <param name="table">Mesa para jugar</param>
    /// <returns>Criterios de jugada valida correspomdientes a la ficha y el nodo</returns>
    public List<int> ValidPlays(INode node, Token token, TableGame table)
    {
        List<int> valid = new List<int>();
        for (int j = 0; j < this.Actions.Length; j++)
        {
            if (this._comprobateValid[j] && this.Actions[j].ValidPlay(node, token, table)) valid.Add(j);
        }

        if (this._comprobateValid[this.Actions.Length] && this.Default!.ValidPlay(node, token, table))
            valid.Add(this.Actions.Length);

        return valid;
    }

    /// <summary>
    /// Determinar los valores a asignar por un criterio de juego determinado
    /// </summary>
    /// <param name="node">Nodo por el que se quiere jugar</param>
    /// <param name="token">Ficha para jugar</param>
    /// <param name="table">Mesa para jugar</param>
    /// <param name="ind">Criterio del juego determinado</param>
    /// <returns>Valores a asignar en el nodo, si devuelve -1 no es posible jugar</returns>
    public int[] AssignValues(INode node, Token token, TableGame table, int ind)
    {
        if (ind == this.Actions.Length) return this.Default!.AssignValues(node, token, table);
        return this.Actions[ind].AssignValues(node, token, table);
    }

    public IsValidRule Clone()
    {
        return new IsValidRule(this.Actions, this.Condition, this.Default!);
    }
}