using Table;
using Rules;
using System.Diagnostics;
using Player;
using Judge;
using Game;

Stopwatch crono = new Stopwatch();
ValidPlayGeometry v = new ValidPlayGeometry(new CongruenceComparation(10));
Token t = new Token(new int[] { 1, 2, 3 });
TableTriangular a = new TableTriangular(new (int, int)[] { (0, 0), (1, 1), (2, 0) });

a.PlayTable(a.TableNode[0], t, v.AsignValues(a.TableNode[0], t, a));

if (v.ValidPlay(a.TableNode[1], new Token(new int[] { 12, 11, 15 }), a)) System.Console.WriteLine(true);
//System.Console.WriteLine(v.ValidPlay(a.TableNode[1], new Token(new int[] { 2, 1, 5 }), a));
//a.PlayTable(a.TableNode[1], new Token(new int[] { 2, 1, 5 }), v.ValidPlay(a.TableNode[1], new Token(new int[] { 2, 1, 5 }), a));
//a.PlayTable(a.TableNode[2], new Token(new int[] { 2, 1, 5, 5 }));



for (int i = 0; i < 3; i++)
{
    System.Console.WriteLine(a.CoordValor[((NodeGeometry)a.TableNode[1]).Ubication.Coord[i]]);
}
// for (int i = 0; i < 4; i++)
// {
//     System.Console.WriteLine(((NodeDimension)a.TableNode[2]).ValuesConections[i]);
// }
GCDComparation ew = new GCDComparation(1);
System.Console.WriteLine(ew.Compare(13, 3));

HashSet<Token> yre = new HashSet<Token>();
yre.Add(t);
System.Console.WriteLine(yre.Contains(new Token(new int[] { 1, 2, 3 })));