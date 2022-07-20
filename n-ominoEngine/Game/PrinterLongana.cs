using Table;

namespace Game;

public class PrinterLongana : PrinterDomino
{
    public PrinterLongana(int speed, bool classic = false) : base(speed, classic)
    {
    }

    public override void LocationTable<T>(TableGame<T> table)
    {
        Thread.Sleep(this.Speed);

        IEnumerable<LocationGui> locations = Array.Empty<LocationGui>();

        for (int i = 0; i < table.TableNode[0].Connections.Length; i++)
        {
            IEnumerable<LocationGui> aux = Array.Empty<LocationGui>();

            if (table.TableNode[0].Connections[i] == null) break;

            if (!table.FreeNode.Contains(table.TableNode[0].Connections[i]!))
            {
                aux = DeterminateLocation(table, table.TableNode[0].Connections[i]!, (3 * i, 1),
                    new HashSet<INode<T>>() { table.TableNode[0] }, true);
            }

            TypeToken type = (Classic) ? TypeToken.DominoVC : TypeToken.DominoV;

            var first = new LocationGui((3 * i + 1, 3 * i + 4, 1, 2),
                new[]
                {
                    table.TableNode[0].ValuesConnections[0]!.ToString()!,
                    table.TableNode[0].ValuesConnections[0]!.ToString()!
                },
                type);

            locations = locations.Concat(new[] { first }.Concat(aux));
        }

        Printer.ExecuteTableEvent(locations);
    }

    public override Printer Reset()
    {
        return new PrinterLongana(this.Speed, this.Classic);
    }
}