namespace Table;

public class TableSquare<T> : TableGeometry<T>
{
    public TableSquare((int, int)[] coordinates) : base(coordinates)
    {
    }

    protected override void Expand(INode<T> node)
    {
        var geometry = node as NodeGeometry<T>;
        if (geometry == null) return;
        var center = FindCenter(geometry);
        var expand = FindCenterExpand(center);
        for (var i = 0; i < expand.Length; i++) AssignCoordinates(geometry, ExpandGeometry(new[] { expand[i] }));
    }

    protected override (int, int)[] ExpandGeometry((int, int)[] coordinates)
    {
        var expand = new (int, int)[4];
        int[] x = { -1, 1, 1, -1 };
        int[] y = { 1, 1, -1, -1 };
        for (var i = 0; i < expand.Length; i++) expand[i] = (coordinates[0].Item1 + x[i], coordinates[0].Item2 + y[i]);

        return expand;
    }

    /// <summary>Expandir el nodo a partir de las coorenadas de su centro</summary>
    /// <param name="coordinates">Coordenadas del nodo</param>
    /// <returns>Coordenadas de los nodos expandidos</returns>
    protected (int, int)[] FindCenterExpand((int, int) coordinates)
    {
        var expand = new (int, int)[4];
        int[] x = { -2, 0, 2, 0 };
        int[] y = { 0, -2, 0, 2 };
        for (var i = 0; i < expand.Length; i++) expand[i] = (coordinates.Item1 + x[i], coordinates.Item2 + y[i]);

        return expand;
    }

    /// <summary>Buscar las coordenadas del centro del nodo</summary>
    /// <param name="node">Nodo</param>
    /// <returns>Coordenadas del centro</returns>
    protected (int, int) FindCenter(NodeGeometry<T> node)
    {
        var x = 0;
        var y = 0;
        for (var i = 1; i < node.Location.Coord.Length; i++)
        {
            if (node.Location.Coord[i].Item2 == node.Location.Coord[i - 1].Item2)
                x = (node.Location.Coord[i].Item1 + node.Location.Coord[i - 1].Item1) / 2;

            if (node.Location.Coord[i].Item1 == node.Location.Coord[i - 1].Item1)
                y = (node.Location.Coord[i].Item2 + node.Location.Coord[i - 1].Item2) / 2;
        }

        return (x, y);
    }

    public override TableGame<T> Clone()
    {
        var aux = new (int, int)[4];
        Array.Copy(((NodeGeometry<T>)TableNode[0]).Location.Coord, aux, 4);
        TableGame<T> table = new TableSquare<T>(aux);
        return AuxClone(table);
    }

    public override TableGame<T> Reset()
    {
        var aux = new (int, int)[4];
        Array.Copy(((NodeGeometry<T>)TableNode[0]).Location.Coord, aux, 4);
        return new TableSquare<T>(aux);
    }
}