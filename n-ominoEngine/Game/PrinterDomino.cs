using Table;
using InfoGame;

namespace Game;

public class PrinterDomino : Printer
{
    public int IdLastHead;

    private delegate bool Parity(int n);

    public PrinterDomino(int speed) : base(speed)
    {
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

    public override void LocationHand<T>(Hand<T> tokens, Token<T>? play, TableGame<T> table, string player)
    {
        DeterminateLocationHand(tokens, play, table, player, 3, 1, TypeToken.DominoV);

        Thread.Sleep(this.Speed);
    }

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
                yield return new LocationGui((1, 4, column, column + 1), values, TypeToken.DominoV);
                column++;
            }
            else
            {
                yield return new LocationGui((2, 3, column, column + 3), values, TypeToken.DominoH);
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