using Table;
using Rules;
using Player;
using Game;

Token<int> t = new Token<int>(new[] {1, 2, 3});

TableGeometry<int> table = new TableTriangular<int>(new []{(0,0),(1,1),(2,0)});

IValidPlay<int> v = new ValidPlayGeometry<int>(new ClassicComparison<int>());
int[] we = v.AssignValues(table.TableNode[0], t,table);
table.PlayTable(table.TableNode[0], t,we );
TableGame<int> a = table.Clone();
//table.PlayTable(table.TableNode[1],new Token<int>(new []{4,5,1}),v.AssignValues(table.TableNode[1],new Token<int>(new []{4,5,1}),table));
for (int i = 0; i < 3; i++)
{
    //Console.WriteLine(((TableGeometry<int>) a).CoordValor[((NodeGeometry<int>)a.TableNode[1]).Location.Coord[i]].Item1);
    Console.WriteLine(table.TableNode[1].ValuesConnections[i]);
}
Console.WriteLine(v.ValidPlay(a.TableNode[1],new Token<int>(new []{1,2,4}),table));

Console.Write("hola".Equals("hola"));