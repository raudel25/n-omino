using Table;
using InfoGame;

namespace Game;

public class PrinterDimension : Printer
{
    public PrinterDimension(int speed) : base(speed)
    {
    }

    public override void LocationTable<T>(TableGame<T> table)
    {
        Thread.Sleep(this.Speed);
        // foreach (var item in Bfs(table.TableNode[0]))
        // {
        //     
        // }
        int height = Dfs(table.TableNode[0], new HashSet<INode<T>>());

        Printer.ExecuteTableEvent(Bfs(table.TableNode[0], height, new HashSet<INode<T>>()));
        // foreach (var item in Bfs(table.TableNode[0],height,new HashSet<INode<T>>()))
        // {
        //     
        // }
    }

    public override void LocationHand<T>(Hand<T> tokens, Token<T>? play, TableGame<T> table, string player)
    {
        Thread.Sleep(this.Speed);
    }

    private int Dfs<T>(INode<T> node, HashSet<INode<T>> visited) where T : struct
    {
        int max = 0;
        visited.Add(node);
        foreach (var item in node.Connections)
        {
            if (item == null) continue;
            if (visited.Contains(item)) continue;
            max = Math.Max(max, Dfs(item, visited));
        }

        return max + 1;
    }

    private IEnumerable<LocationGui> Bfs<T>(INode<T> node, int height, HashSet<INode<T>> visited) where T : struct
    {
        TypeToken type = TypeToken.NDimension;
        Queue<INode<T>> queue1 = new Queue<INode<T>>();
        Queue<INode<T>> queue2 = new Queue<INode<T>>();
        queue1.Enqueue(node);

        int row = 0;
        int column = 0;
        int dimension = node.Connections.Length;
        while (queue1.Count != 0 || queue2.Count != 0)
        {
            row++;
            column = (int) Math.Pow(dimension, height - row);
            while (queue1.Count != 0)
            {
                INode<T> element = queue1.Peek();
                queue1.Dequeue();

                yield return new LocationGui((row, row + 1, column, column + 2), new string[dimension], type);
                //Console.WriteLine(column+"*****");
                visited.Add(element);
                column += 2;

                for (int i = 0; i < element.Connections.Length; i++)
                {
                    if (element.Connections[i] == null) continue;
                    if (visited.Contains(element.Connections[i]!)) continue;
                    queue2.Enqueue(element.Connections[i]!);
                }
            }

            row++;
            column = (int) Math.Pow(dimension, height - row);
            while (queue2.Count != 0)
            {
                INode<T> element = queue2.Peek();
                queue2.Dequeue();

                yield return new LocationGui((row, row + 1, column, column + 2), new string[dimension], type);
                //Console.WriteLine(column+"*****");
                visited.Add(element);
                column += 2;

                for (int i = 0; i < element.Connections.Length; i++)
                {
                    if (element.Connections[i] == null) continue;
                    if (visited.Contains(element.Connections[i]!)) continue;
                    queue1.Enqueue(element.Connections[i]!);
                }
            }
        }
    }
}