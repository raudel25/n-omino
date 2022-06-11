using Table;
using Rules;
using System.Diagnostics;
using Player;
using Game;

ValidPlayDimension v = new ValidPlayDimension(new CongruenceComparation(10));
Token t = new Token(new int[] { 1, 2, 3, 4 });

// TableSquare a = new TableSquare(new (int, int)[] {(0, 0), (0, 2), (2, 2), (2, 0)});
TableDimension a = new TableDimension(4);
int[]? aux = v.AsignValues(a.TableNode[0], t, a);
a.PlayTable(a.TableNode[0], t,aux );
Console.WriteLine(t.Values[2]);
Console.WriteLine(aux[0]);


if (v.ValidPlay(a.TableNode[1], new Token(new int[] { 12, 11, 15,3 }), a)) System.Console.WriteLine(true);
//System.Console.WriteLine(v.ValidPlay(a.TableNode[1], new Token(new int[] { 2, 1, 5 }), a));
//a.PlayTable(a.TableNode[1], new Token(new int[] { 2, 1, 5 }), v.ValidPlay(a.TableNode[1], new Token(new int[] { 2, 1, 5 }), a));
//a.PlayTable(a.TableNode[2], new Token(new int[] { 2, 1, 5, 5 }));



// for (int i = 0; i < 4; i++)
// {
//     System.Console.WriteLine(a.CoordValor[((NodeGeometry)a.TableNode[1]).Ubication.Coord[i]]);
//}
for (int i = 0; i < 4; i++)
{
    System.Console.WriteLine(((NodeDimension)a.TableNode[1]).ValuesConections[i]);
}

