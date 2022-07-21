using InfoGame;
using Table;

namespace Rules;

public class ValidPlayDimension<T> : IValidPlay<T>
{
    /// <summary>
    /// Criterio para comparar los valores de las fichas
    /// </summary>
    public readonly IComparison<T> Comparison;

    public ValidPlayDimension(IComparison<T> comp)
    {
        this.Comparison = comp;
    }

    public bool ValidPlay(INode<T> node, Token<T> token, GameStatus<T> game, int ind)
    {
        if (token.CantValues != node.Connections.Length) return false;
        if (!game.Table.FreeNode.Contains(node)) return false;

        NodeDimension<T>? nodeDimension = node as NodeDimension<T>;
        if (nodeDimension == null) return false;

        int connection = nodeDimension.FirstConnection;
        if (connection == -1) return true;

        ValuesNode<T> value = game.Table.ValuesNodeTable(nodeDimension, connection)!;

        foreach (var item in token)
        {
            if (this.Comparison.Compare(item, value.Values[0])) return true;
        }

        return false;
    }

    public T[] AssignValues(INode<T> node, Token<T> token, TableGame<T> table)
    {
        if (token.CantValues != node.Connections.Length) return Array.Empty<T>();
        if (!table.FreeNode.Contains(node)) return Array.Empty<T>();

        NodeDimension<T>? nodeDimension = node as NodeDimension<T>;
        if (nodeDimension == null) return Array.Empty<T>();

        T[] values = new T[token.CantValues];
        Array.Copy(token.ToArray(), values, token.CantValues);

        int ind = nodeDimension.FirstConnection;
        if (ind == -1) return values;

        ValuesNode<T> value = table.ValuesNodeTable(nodeDimension, ind)!;

        for (int i = 0; i < values.Length; i++)
        {
            if (this.Comparison.Compare(values[i], value.Values[0]))
            {
                //Realizamos el cambio correspondiente con el valor preasignado
                (values[i], values[ind]) = (values[ind], values[i]);
                break;
            }
        }

        return values;
    }
}

public class ValidPlayGeometry<T> : IValidPlay<T>
{
    public readonly IComparison<T> Comparison;

    public ValidPlayGeometry(IComparison<T> comp)
    {
        this.Comparison = comp;
    }

    public bool ValidPlay(INode<T> node, Token<T> token, GameStatus<T> game, int ind)
    {
        return AssignValues(node, token, game.Table).Length != 0;
    }

    public T[] AssignValues(INode<T> node, Token<T> token, TableGame<T> table)
    {
        if (token.CantValues != node.Connections.Length) return Array.Empty<T>();
        if (!table.FreeNode.Contains(node)) return Array.Empty<T>();

        TableGeometry<T>? tableGeometry = table as TableGeometry<T>;
        NodeGeometry<T>? nodeGeometry = node as NodeGeometry<T>;
        if (nodeGeometry == null || tableGeometry == null) return Array.Empty<T>();

        for (int i = 0; i < token.CantValues; i++)
        {
            T[] circular = AuxTable.CircularArray(token, i).ToArray();
            if (ValuesEquals(nodeGeometry, tableGeometry, circular)) return circular;
        }

        return Array.Empty<T>();
    }

    private bool ValuesEquals(NodeGeometry<T> nodeGeometry, TableGeometry<T> tableGeometry, T[] circular)
    {
        for (int j = 0; j < circular.Length; j++)
        {
            ValuesNode<T> valuesNode = tableGeometry.ValuesNodeTable(nodeGeometry, j)!;
            if (valuesNode.IsAssignValue)
            {
                foreach (var item in valuesNode.Values)
                {
                    if (!this.Comparison.Compare(item, circular[j]))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}

public class ComodinToken<T> : IValidPlay<T>
{
    private Token<T> _comodinToken;

    public ComodinToken(Token<T> token)
    {
        this._comodinToken = token;
    }

    public bool ValidPlay(INode<T> node, Token<T> token, GameStatus<T> game, int ind)
    {
        return _comodinToken.Equals(token);
    }

    public T[] AssignValues(INode<T> node, Token<T> token, TableGame<T> table)
    {
        return this._comodinToken.ToArray();
    }
}

public class ValidPlayLongana<T> : IValidPlay<T>
{
    /// <summary>
    /// Criterior valido para comparar
    /// </summary>
    private IValidPlay<T> _valid;

    public ValidPlayLongana(IComparison<T> comp)
    {
        this._valid = new ValidPlayDimension<T>(comp);
    }

    public bool ValidPlay(INode<T> node, Token<T> token, GameStatus<T> game, int ind)
    {
        TableLongana<T>? tableLongana = game.Table as TableLongana<T>;

        if (tableLongana == null) return false;

        if (tableLongana.PlayNode.Count == 0)
        {
            T aux = token[0];
            foreach (var item in token)
            {
                if (!aux!.Equals(item)) return false;
            }

            return true;
        }
        else if (tableLongana.BranchNode[node] == game.Turns[ind])
        {
            return this._valid.ValidPlay(node, token, game, ind);
        }

        return false;
    }

    public T[] AssignValues(INode<T> node, Token<T> token, TableGame<T> table)
    {
        TableLongana<T>? tableLongana = table as TableLongana<T>;
        if (tableLongana == null) return Array.Empty<T>();

        if (tableLongana.PlayNode.Count == 0)
        {
            T[] aux = new T[node.Connections.Length];
            for (int i = 0; i < node.Connections.Length; i++)
            {
                aux[i] = token[0];
            }

            return aux;
        }

        return this._valid.AssignValues(node, token, table);
    }
}

public class ValidPlayLonganaComplement<T> : IValidPlay<T>
{
    /// <summary>
    /// Criterior valido para comparar
    /// </summary>
    private IValidPlay<T> _valid;

    public ValidPlayLonganaComplement(IComparison<T> comp)
    {
        this._valid = new ValidPlayDimension<T>(comp);
    }

    public bool ValidPlay(INode<T> node, Token<T> token, GameStatus<T> game, int ind)
    {
        TableLongana<T>? tableLongana = game.Table as TableLongana<T>;

        if (tableLongana == null) return false;

        ind = (ind == 0) ? game.Turns.Length - 1 : ind - 1;

        if (tableLongana.BranchNode[node] == game.Turns[ind])
        {
            return this._valid.ValidPlay(node, token, game, ind);
        }

        return false;
    }

    public T[] AssignValues(INode<T> node, Token<T> token, TableGame<T> table)
    {
        return this._valid.AssignValues(node, token, table);
    }
}