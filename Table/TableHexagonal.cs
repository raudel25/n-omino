namespace Table;

public class TableHexagonal : TableGeometry
{
    public TableHexagonal((int, int)[] coordenates) : base(coordenates)
    {
    }

    protected override void Expand(INode node)
    {
        var geometry = node as NodeGeometry;
        if (geometry == null) return;
        var center = FindCenter(geometry);
        var expand = FindCenterExpand(center);
        for (var i = 0; i < expand.Length; i++) AsignCoordenates(geometry, ExpandGeometry(new[] {expand[i]}));
    }

    protected override (int, int)[] ExpandGeometry((int, int)[] coordenates)
    {
        var expand = new (int, int)[6];
        int[] x = {-1, 1, 2, 1, -1, -2};
        int[] y = {1, 1, 0, -1, -1, 0};
        for (var i = 0; i < expand.Length; i++) expand[i] = (coordenates[0].Item1 + x[i], coordenates[0].Item2 + y[i]);

        return expand;
    }

    /// <summary>Expandir el nodo a partir de las coorenadas de su centro</summary>
    /// <param name="coordenates">Coordenadas del nodo</param>
    /// <returns>Coordenadas de los centros nodos expandidos</returns>
    protected (int, int)[] FindCenterExpand((int, int) coordenates)
    {
        var expand = new (int, int)[6];
        int[] x = {-3, 0, 3, 3, 0, -3};
        int[] y = {-1, -2, -1, 1, 2, 1};
        for (var i = 0; i < expand.Length; i++) expand[i] = (coordenates.Item1 + x[i], coordenates.Item2 + y[i]);

        return expand;
    }

    /// <summary>Buscar las coordenadas del centro del nodo</summary>
    /// <param name="node">Nodo</param>
    /// <returns>Coordenadas del centro</returns>
    protected (int, int) FindCenter(NodeGeometry node)
    {
        var x = 0;
        var y = 0;
        for (var i = 1; i < node.Location.Coord.Length; i++)
        {
            if (node.Location.Coord[i].Item2 == node.Location.Coord[i - 1].Item2)
                x = (node.Location.Coord[i].Item1 + node.Location.Coord[i - 1].Item1) / 2;

            if (i == 1) continue;
            if (node.Location.Coord[i].Item1 == node.Location.Coord[i - 2].Item1)
                y = (node.Location.Coord[i].Item2 + node.Location.Coord[i - 2].Item2) / 2;
        }

        return (x, y);
    }

    public override TableGame Clone()
    {
        var aux = new (int, int)[6];
        Array.Copy(((NodeGeometry) TableNode[0]).Location.Coord, aux, 6);
        TableGame table = new TableHexagonal(aux);
        return AuxClone(table);
    }
}