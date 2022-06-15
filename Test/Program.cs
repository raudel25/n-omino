using Table;
using Rules;
using Player;
using Game;

/*Token<int> t = new Token<int>(new[] {1, 2, 3});

//TableDimension<int> table = new TableDimension<int>(3);
TableTriangular<int> table = new TableTriangular<int>(new[] {(0, 0), (1, 1), (2, 0)});

IValidPlay<int> v = new ValidPlayGeometry<int>(new ClassicComparison<int>());
int[] we = v.AssignValues(table.TableNode[0], t,table);
table.PlayTable(table.TableNode[0], t,we );
Console.WriteLine(we[0]);
TableGame<int> a = table.Clone();
//table.PlayTable(table.TableNode[1],new Token<int>(new []{4,5,1}),v.AssignValues(table.TableNode[1],new Token<int>(new []{4,5,1}),table));
for (int i = 0; i < 3; i++)
{
   Console.WriteLine(((TableGeometry<int>) a).CoordValor[((NodeGeometry<int>)a.TableNode[1]).Location.Coord[i]].Item1);
   // Console.WriteLine(((NodeDimension<int>)a.TableNode[1]).ValuesConnections[i]);
}
we = v.AssignValues(table.TableNode[1], new Token<int>(new[] {1, 2, 1}),table);
table.PlayTable(table.TableNode[1], new Token<int>(new[] {1, 2, 1}), we);
Console.WriteLine(v.ValidPlay(a.TableNode[1],new Token<int>(new []{1,2,1}),a));
Console.WriteLine(a.PlayNode.Count);*/

IValidPlay<int> v2 = new ValidPlayDimension<int>(new ClassicComparison<int>());
IValidPlay<int> v = new ValidPlayLongana<int>(new ClassicComparison<int>(),v2,0);
TableLongana<int> table = new TableLongana<int>(3, 4);
Token<int> t = new Token<int>(new[] {1, 1, 1});
var t1 = new Token<int>(new[] {2, 2, 3});

table.PlayTable(table.TableNode[0],t,v.AssignValues(table.TableNode[0],t,table));
table.PlayTable(table.TableNode[1],t1,v.AssignValues(table.TableNode[1],t1,table));
Console.WriteLine(v.ValidPlay(table.TableNode[1], t1, table));
for (int i = 0; i < table.TableNode[1].Connections.Length; i++)
{
   Console.WriteLine(table.TableNode[1].ValuesConnections[i]);
}
Console.WriteLine(table.BranchNode[table.TableNode[4]]);