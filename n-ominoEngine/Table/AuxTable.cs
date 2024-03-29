namespace Table;

public static class AuxTable
{
    /// <summary>
    ///     Rotar un array
    /// </summary>
    /// <param name="array">Array para rotar</param>
    /// <param name="ind">Indice desde el cual queremos emepezar arotar</param>
    /// <typeparam name="T">Tipo de las Fichas</typeparam>
    /// <returns></returns>
    public static IEnumerable<T> CircularArray<T>(IEnumerable<T> array, int ind)
    {
        foreach (var item in array.Skip(ind).Concat(array.Take(ind))) yield return item;
    }

    /// <summary>
    ///     Determinar la suma de los valores situados en los nodos libres
    /// </summary>
    /// <param name="table">Mesa</param>
    /// <returns>Suma de los valores situados en los nodos libres</returns>
    public static int SumConnectionFree(TableGame<int> table)
    {
        var sum = 0;
        foreach (var item in table.FreeNode)
            if (table is TableGeometry<int>)
            {
                var node = (NodeGeometry<int>)item;
                var tableGeometry = (TableGeometry<int>)table;
                for (var i = 0; i < node.Location.Coord.Length; i++)
                {
                    var aux = table.ValuesNodeTable(item, i)!;
                    if (!aux.IsAssignValue) continue;
                    sum += aux.Values[0];
                }
            }
            else
            {
                for (var i = 0; i < item.ValuesConnections.Length; i++)
                {
                    var aux = table.ValuesNodeTable(item, i)!;
                    if (!aux.IsAssignValue) continue;
                    sum += aux.Values[0];
                }
            }

        return sum;
    }
}