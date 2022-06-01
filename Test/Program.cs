using Table;
using Rules;
using Player;
using Judge;
using Game;

ValidPlayDimension v = new ValidPlayDimension();
Token t = new Token(new int[] { 1, 2, 3, 4 });
TableDimension a = new TableDimension(4);
a.PlayTable(a.TableNode[0], t, v.AsignValues(a.TableNode[0], t, a));
System.Console.WriteLine(v.ValidPlay(a.TableNode[1], new Token(new int[] { 2, 1, 5, 5 }), a));
a.PlayTable(a.TableNode[2], new Token(new int[] { 2, 1, 5, 5 }), v.AsignValues(a.TableNode[2], new Token(new int[] { 2, 1, 5, 5 }), a));
//a.PlayTable(a.TableNode[2], new Token(new int[] { 2, 1, 5, 5 }));



// for (int i = 0; i < 4; i++)
// {
//     System.Console.WriteLine(a.CoordValor[((NodeGeometry)a.TableNode[1]).Ubication.Coord[i]]);
// }
for (int i = 0; i < 4; i++)
{
    System.Console.WriteLine(((NodeDimension)a.TableNode[2]).ValuesConections[i]);
}
System.Console.WriteLine(a.FreeNode.Count);


