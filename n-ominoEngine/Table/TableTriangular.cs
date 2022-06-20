namespace Table;

public class TableTriangular<T> : TableGeometry<T>
{
    public TableTriangular((int, int)[] coordinates) : base(coordinates)
    {
    }

    protected override void Expand(INode<T> node)
    {
        NodeGeometry<T>? geometry = (node as NodeGeometry<T>);
        if (geometry == null) return;
        for (int i = 0; i < node.Connections.Length; i++)
        {
            (int, int)[] coordinates = ExpandGeometry(AuxTable.CircularArray(geometry.Location.Coord, i));
            AssignCoordinates(node, coordinates);
        }
    }

    protected override (int, int)[] ExpandGeometry((int, int)[] coordinates)
    {
        (int, int) coord1 = coordinates[0];
        (int, int) coord2 = coordinates[1];
        (int, int) coord3 = coordinates[2];
        bool ne = coord2.Item1 > coord1.Item1 && coord2.Item2 > coord1.Item2;
        bool e = coord2.Item1 > coord1.Item1 && coord2.Item2 == coord1.Item2;
        bool se = coord2.Item1 > coord1.Item1 && coord2.Item2 < coord1.Item2;
        bool sw = coord2.Item1 < coord1.Item1 && coord2.Item2 < coord1.Item2;
        bool w = coord2.Item1 < coord1.Item1 && coord2.Item2 == coord1.Item2;
        bool nw = coord2.Item1 < coord1.Item1 && coord2.Item2 > coord1.Item2;

        if (ne)
        {
            if (coord3.Item1 > coord1.Item1) return new[] {coord1, (coord2.Item1 - 2, coord2.Item2), coord2};
            return new[] {coord1, coord2, (coord1.Item1 + 2, coord1.Item2)};
        }
        else if (e)
        {
            if (coord3.Item2 < coord1.Item2) return new[] {coord1, (coord3.Item1, coord3.Item2 + 2), coord2};
            return new[] {coord1, coord2, (coord3.Item1, coord3.Item2 - 2)};
        }
        else if (se)
        {
            if (coord3.Item1 < coord1.Item1) return new[] {coord1, (coord1.Item1 + 2, coord1.Item2), coord2};
            return new[] {coord1, coord2, (coord2.Item1 - 2, coord2.Item2)};
        }
        else if (sw)
        {
            if (coord3.Item1 < coord1.Item1) return new[] {coord1, (coord2.Item1 + 2, coord2.Item2), coord2};
            return new[] {coord1, coord2, (coord1.Item1 - 2, coord1.Item2)};
        }
        else if (w)
        {
            if (coord3.Item2 < coord1.Item2) return new[] {coord1, coord2, (coord3.Item1, coord3.Item2 + 2)};
            return new[] {coord1, (coord3.Item1, coord3.Item2 - 2), coord2};
        }
        else
        {
            if (coord3.Item1 > coord1.Item1) return new[] {coord1, (coord1.Item1 - 2, coord1.Item2), coord2};
            return new[] {coord1, coord2, (coord2.Item1 + 2, coord2.Item2)};
        }
    }

    public override TableGame<T> Clone()
    {
        (int, int)[] aux = new (int, int)[3];
        Array.Copy(((NodeGeometry<T>) this.TableNode[0]).Location.Coord, aux, 3);
        TableGame<T> table = new TableTriangular<T>(aux);
        return this.AuxClone(table);
    }
}