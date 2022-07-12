using Table;
using InfoGame;

namespace Game;

public class PrinterDomino : Printer
{
    /// <summary>
    /// Ultima ficha refernte a la cabeza del juego
    /// </summary>
    public int IdLastHead;

    private delegate bool Parity(int n);

    protected bool _classic;

    public PrinterDomino(int speed, bool classic = false) : base(speed)
    {
        this._classic = classic;
    }

    public override void LocationTable<T>(TableGame<T> table)
    {
        Thread.Sleep(this.Speed);

        if (table.TableNode.Count == 0) return;
        Printer.ExecuteTableEvent(DeterminateLocation(table, DeterminateHead(table), (0, 0), new HashSet<INode<T>>()));
    }

    public override void LocationHand<T>(InfoPlayer<T> player, Token<T>? play, TableGame<T> table)
    {
        if (_classic)
        {
            DeterminateLocationHand(play, table, player, 3, 1, TypeToken.DominoVC);
        }
        else
        {
            DeterminateLocationHand(play, table, player, 3, 1, TypeToken.DominoV);
        }

        Thread.Sleep(this.Speed);
    }

    private INode<T> DeterminateHead<T>(TableGame<T> table)
    {
        INode<T> actualhead = table.TableNode[this.IdLastHead];
        INode<T> head = actualhead;

        //Determinar la cabeza acual correspondiente al juego
        foreach (var item in actualhead.Connections)
        {
            if (table.FreeNode.Contains(item!)) break;

            foreach (var itemConnection in item!.Connections)
            {
                if (table.FreeNode.Contains(itemConnection!))
                {
                    head = item;
                    break;
                }
            }
        }

        this.IdLastHead = head.Id;

        return head;
    }

    /// <summary>
    /// Detrminar la ubicacion de las fichas
    /// </summary>
    /// <param name="table">Mesa</param>
    /// <param name="node">Nodo que de encuentra en la cabeza</param>
    /// <param name="increment">Incremento en las coordenadas</param>
    /// <param name="visited">Nodos visitados</param>
    /// <param name="longana">Si estamos printiando la longana</param>
    /// <typeparam name="T">Tipo que de utiliza en el juego</typeparam>
    /// <returns>Ubicacion de las fichas</returns>
    protected IEnumerable<LocationGui> DeterminateLocation<T>(TableGame<T> table, INode<T> node,
        (int, int) increment, HashSet<INode<T>> visited, bool longana = false)
    {
        int cant = 0;
        INode<T> aux = node;
        int column = 1 + increment.Item2;

        bool condition = table.FreeNode.Contains(node.Connections[0]!);

        if (longana) condition = !condition;

        Parity func = (condition) ? (n) => ((n & 1) == 1) : (n) => ((n & 1) == 0);

        //Distribuir la ubicacion de la ficha en la Gui
        while (cant < table.PlayNode.Count)
        {
            if (visited.Contains(aux)) break;

            cant++;
            visited.Add(aux);

            (string values1, string values2) = (func(cant))
                ? (aux.ValuesConnections[0]!.ToString()!, aux.ValuesConnections[1]!.ToString()!)
                : (aux.ValuesConnections[1]!.ToString()!, aux.ValuesConnections[0]!.ToString()!);
            string[] values = new[] { values1, values2 };

            if (aux.ValuesConnections[0]!.Equals(aux.ValuesConnections[1]) && !longana)
            {
                if (_classic)
                {
                    yield return new LocationGui((1 + increment.Item1, 4 + increment.Item1, column, column + 1), values,
                        TypeToken.DominoVC);
                }
                else
                {
                    yield return new LocationGui((1 + increment.Item1, 4 + increment.Item1, column, column + 1), values,
                        TypeToken.DominoV);
                }

                column++;
            }
            else
            {
                if (_classic)
                {
                    yield return new LocationGui((2 + increment.Item1, 3 + increment.Item1, column, column + 3), values,
                        TypeToken.DominoHC);
                }
                else
                {
                    yield return new LocationGui((2 + increment.Item1, 3 + increment.Item1, column, column + 3), values,
                        TypeToken.DominoH);
                }

                column += 3;
            }

            foreach (var item in aux.Connections)
            {
                if (table.FreeNode.Contains(item!)) continue;
                if (visited.Contains(item!)) continue;
                aux = item!;
            }
        }
    }
}