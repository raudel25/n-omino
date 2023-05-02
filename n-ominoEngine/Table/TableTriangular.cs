namespace Table;

public class TableTriangular<T> : TableGeometry<T>
{
    public TableTriangular((int, int)[] coordinates) : base(coordinates)
    {
    }

    protected override void Expand(INode<T> node)
    {
        var geometry = node as NodeGeometry<T>;
        if (geometry == null) return;
        for (var i = 0; i < node.Connections.Length; i++)
        {
            var coordinates = ExpandGeometry(AuxTable.CircularArray(geometry.Location.Coord, i).ToArray());
            AssignCoordinates(node, coordinates);
        }
    }

    protected override (int, int)[] ExpandGeometry((int, int)[] coordinates)
    {
        var coord1 = coordinates[0];
        var coord2 = coordinates[1];
        var coord3 = coordinates[2];
        var ne = coord2.Item1 > coord1.Item1 && coord2.Item2 > coord1.Item2;
        var e = coord2.Item1 > coord1.Item1 && coord2.Item2 == coord1.Item2;
        var se = coord2.Item1 > coord1.Item1 && coord2.Item2 < coord1.Item2;
        var sw = coord2.Item1 < coord1.Item1 && coord2.Item2 < coord1.Item2;
        var w = coord2.Item1 < coord1.Item1 && coord2.Item2 == coord1.Item2;
        var nw = coord2.Item1 < coord1.Item1 && coord2.Item2 > coord1.Item2;

        if (ne)
        {
            if (coord3.Item1 > coord1.Item1) return new[] { coord1, (coord2.Item1 - 2, coord2.Item2), coord2 };
            return new[] { coord1, coord2, (coord1.Item1 + 2, coord1.Item2) };
        }

        if (e)
        {
            if (coord3.Item2 < coord1.Item2) return new[] { coord1, (coord3.Item1, coord3.Item2 + 2), coord2 };
            return new[] { coord1, coord2, (coord3.Item1, coord3.Item2 - 2) };
        }

        if (se)
        {
            if (coord3.Item1 < coord1.Item1) return new[] { coord1, (coord1.Item1 + 2, coord1.Item2), coord2 };
            return new[] { coord1, coord2, (coord2.Item1 - 2, coord2.Item2) };
        }

        if (sw)
        {
            if (coord3.Item1 < coord1.Item1) return new[] { coord1, (coord2.Item1 + 2, coord2.Item2), coord2 };
            return new[] { coord1, coord2, (coord1.Item1 - 2, coord1.Item2) };
        }

        if (w)
        {
            if (coord3.Item2 < coord1.Item2) return new[] { coord1, coord2, (coord3.Item1, coord3.Item2 + 2) };
            return new[] { coord1, (coord3.Item1, coord3.Item2 - 2), coord2 };
        }

        if (coord3.Item1 > coord1.Item1) return new[] { coord1, (coord1.Item1 - 2, coord1.Item2), coord2 };
        return new[] { coord1, coord2, (coord2.Item1 + 2, coord2.Item2) };
    }

    public override TableGame<T> Clone()
    {
        var aux = new (int, int)[3];
        Array.Copy(((NodeGeometry<T>)TableNode[0]).Location.Coord, aux, 3);
        TableGame<T> table = new TableTriangular<T>(aux);
        return AuxClone(table);
    }

    public override TableGame<T> Reset()
    {
        var aux = new (int, int)[3];
        Array.Copy(((NodeGeometry<T>)TableNode[0]).Location.Coord, aux, 3);
        return new TableTriangular<T>(aux);
    }
}