using Table;
using InfoGame;

namespace InteractionGui;

public class PrinterDomino : Printer
{
    /// <summary>
    /// Ultima ficha refernte a la cabeza del juego
    /// </summary>
    public int IdLastHead;

    private delegate bool Parity(int n);

    private bool _classic;

    public PrinterDomino(int speed,bool classic) : base(speed)
    {
        this._classic = classic;
    }

    public override void LocationTable<T>(TableGame<T> table)
    {
        Thread.Sleep(this.Speed);

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

        Printer.ExecuteTableEvent(DeterminateLocation(table, head));
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

    /// <summary>
    /// Detrminar la ubicacion de las fichas
    /// </summary>
    /// <param name="table">Mesa</param>
    /// <param name="node">Nodo que de encuentra en la cabeza</param>
    /// <typeparam name="T">Tipo que de utiliza en el juego</typeparam>
    /// <returns>Ubicacion de las fichas</returns>
    private IEnumerable<LocationGui> DeterminateLocation<T>(TableGame<T> table, INode<T> node)
    {
        HashSet<INode<T>> visited = new HashSet<INode<T>>();

        int cant = 0;
        INode<T> aux = node;
        int column = 1;
        Parity func = (table.FreeNode.Contains(node.Connections[0]!)) ? (n) => ((n & 1) == 1) : (n) => ((n & 1) == 0);

        //Distribuir la ubicacion de la ficha en la Gui
        while (cant < table.PlayNode.Count)
        {
            cant++;
            visited.Add(aux);

            (string values1, string values2) = (func(cant))
                ? (aux.ValuesConnections[0]!.ToString()!, aux.ValuesConnections[1]!.ToString()!)
                : (aux.ValuesConnections[1]!.ToString()!, aux.ValuesConnections[0]!.ToString()!);
            string[] values = new[] {values1, values2};

            if (aux.ValuesConnections[0]!.Equals(aux.ValuesConnections[1]))
            {
                if (_classic)
                {
                    yield return new LocationGui((1, 4, column, column + 1), values, TypeToken.DominoVC);
                }
                else
                {
                    yield return new LocationGui((1, 4, column, column + 1), values, TypeToken.DominoV);
                }
                column++;
            }
            else
            {
                if (_classic)
                {
                    yield return new LocationGui((2, 3, column, column + 3), values, TypeToken.DominoHC);
                }
                else
                {
                    yield return new LocationGui((2, 3, column, column + 3), values, TypeToken.DominoH);
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