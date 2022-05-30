using Table;
using Rules;
using Player;
using Judge;
using Game;

ValidPlayGeometry v = new ValidPlayGeometry();
Token t = new Token(new int[] { 1, 2, 3, 4 });
TableSquare a = new TableSquare(t, new (int, int)[] { (0, 0), (0, 2), (2, 2), (2, 0) });
System.Console.WriteLine(v.ValidPlay(a.TableNode[1], new Token(new int[] { 2, 1, 5, 5 }), a));
a.PlayTable(a.TableNode[1], new Token(new int[] { 2, 1, 5, 5 }));
//a.PlayTable(a.TableNode[2], new Token(new int[] { 2, 1, 5, 5 }));



for (int i = 0; i < 4; i++)
{
    System.Console.WriteLine(a.CoordValor[((NodeGeometry)a.TableNode[2]).Ubication.Coord[i]]);
}
System.Console.WriteLine(a.FreeNode.Count);
// a.PlayTable(a.TableNode[1], new Token(new int[] { 0, 0, 0 }));
// System.Console.WriteLine(a.FreeNode.Count);
// a.PlayTable(a.TableNode[3], new Token(new int[] { 0, 0, 0 }));
// System.Console.WriteLine(a.FreeNode.Count);
// System.Console.WriteLine(a.FreeNode.Contains(a.TableNode[1]));


