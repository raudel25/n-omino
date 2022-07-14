namespace Table;

public class TableHexagonal<T> : TableGeometry<T>
{
    public TableHexagonal((int, int)[] coordinates) : base(coordinates)
    {
    }

    protected override void Expand(INode<T> node)
    {
        NodeGeometry<T>? geometry = node as NodeGeometry<T>;
        if (geometry == null) return;
        (int, int) center = FindCenter(geometry);
        (int, int)[] expand = FindCenterExpand(center);
        for (int i = 0; i < expand.Length; i++) AssignCoordinates(geometry, ExpandGeometry(new[] { expand[i] }));
    }

    protected override (int, int)[] ExpandGeometry((int, int)[] coordinates)
    {
        (int, int)[] expand = new (int, int)[6];
        int[] x = { -1, 1, 2, 1, -1, -2 };
        int[] y = { 1, 1, 0, -1, -1, 0 };
        for (int i = 0; i < expand.Length; i++) expand[i] = (coordinates[0].Item1 + x[i], coordinates[0].Item2 + y[i]);

        return expand;
    }

    /// <summary>Expandir el nodo a partir de las coorenadas de su centro</summary>
    /// <param name="coordinates">Coordenadas del nodo</param>
    /// <returns>Coordenadas de los centros nodos expandidos</returns>
    protected (int, int)[] FindCenterExpand((int, int) coordinates)
    {
        (int, int)[] expand = new (int, int)[6];
        int[] x = { -3, 0, 3, 3, 0, -3 };
        int[] y = { -1, -2, -1, 1, 2, 1 };
        for (int i = 0; i < expand.Length; i++) expand[i] = (coordinates.Item1 + x[i], coordinates.Item2 + y[i]);

        return expand;
    }

    /// <summary>Buscar las coordenadas del centro del nodo</summary>
    /// <param name="node">Nodo</param>
    /// <returns>Coordenadas del centro</returns>
    protected (int, int) FindCenter(NodeGeometry<T> node)
    {
        int x = 0;
        int y = 0;
        for (int i = 1; i < node.Location.Coord.Length; i++)
        {
            if (node.Location.Coord[i].Item2 == node.Location.Coord[i - 1].Item2)
                x = (node.Location.Coord[i].Item1 + node.Location.Coord[i - 1].Item1) / 2;

            if (i == 1) continue;
            if (node.Location.Coord[i].Item1 == node.Location.Coord[i - 2].Item1)
                y = (node.Location.Coord[i].Item2 + node.Location.Coord[i - 2].Item2) / 2;
        }

        return (x, y);
    }

    public override TableGame<T> Clone()
    {
        (int, int)[] aux = new (int, int)[6];
        Array.Copy(((NodeGeometry<T>)TableNode[0]).Location.Coord, aux, 6);
        TableGame<T> table = new TableHexagonal<T>(aux);
        return AuxClone(table);
    }

    public override TableGame<T> Reset()
    {
        (int, int)[] aux = new (int, int)[6];
        Array.Copy(((NodeGeometry<T>)TableNode[0]).Location.Coord, aux, 6);
        return new TableHexagonal<T>(aux);
    }
}