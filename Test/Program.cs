using Table;
using Rules;
using Player;
using Judge;
using Game;

ValidPlayGeometry v = new ValidPlayGeometry();
Token t = new Token(new int[] { 1, 2, 3 });
TableTriangular a = new TableTriangular(new (int, int)[] { (0, 0), (1, 1), (2, 0) });
a.PlayTable(a.TableNode[0], t, v.AsignValues(a.TableNode[0], t, a));
System.Console.WriteLine(v.ValidPlay(a.TableNode[1], new Token(new int[] { 2, 1, 5 }), a));
a.PlayTable(a.TableNode[1], new Token(new int[] { 2, 1, 5 }), v.AsignValues(a.TableNode[1], new Token(new int[] { 2, 1, 5 }), a));
//a.PlayTable(a.TableNode[2], new Token(new int[] { 2, 1, 5, 5 }));



for (int i = 0; i < 3; i++)
{
    System.Console.WriteLine(a.CoordValor[((NodeGeometry)a.TableNode[1]).Ubication.Coord[i]]);
}
// for (int i = 0; i < 4; i++)
// {
//     System.Console.WriteLine(((NodeDimension)a.TableNode[2]).ValuesConections[i]);
// }
System.Console.WriteLine(a.FreeNode.Count);

TableGame b = a.Clone();
System.Console.WriteLine(a.PlayNode.Count);
System.Console.WriteLine("*****");
b.TableNode[2] = b.TableNode[0];
System.Console.WriteLine(b.FreeNode.Count);
for (int i = 0; i < 3; i++)
{
    System.Console.WriteLine(((TableGeometry)a).CoordValor[((NodeGeometry)a.TableNode[2]).Ubication.Coord[i]]);
}
System.Console.WriteLine(b.FreeNode.Count);
System.Console.WriteLine(a.PlayNode.Count);

