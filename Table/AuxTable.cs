namespace Table;

public static class AuxTable
{
    public static T[] CircularArray<T>(T[] array, int ind)
    {
        T[] aux = new T[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            if (ind == array.Length) ind = 0;
            aux[i] = array[ind];
            ind++;
        }

        return aux;
    }
    public static int SumConnectionFree(TableGame<int> table)
    {
        int sum = 0;
        foreach (var item in table.FreeNode)
        {
            if (table is TableGeometry<int>)
            {
                NodeGeometry<int> node = (NodeGeometry<int>) item;
                TableGeometry<int> tableGeometry = (TableGeometry<int>) table;
                for (int i = 0; i < node.Location.Coord.Length; i++)
                {
                    if (tableGeometry.CoordValor[node.Location.Coord[i]].Item2)
                    {
                        sum += item.ValuesConnections[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < item.ValuesConnections.Length; i++)
                {
                    if (item.ValuesConnections[i] != -1)
                    {
                        sum += item.ValuesConnections[i];
                    }
                }
            }
        }

        return sum;
    }
}