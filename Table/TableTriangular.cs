namespace Table;

public class TableTriangular : TableGeometry
{
    public TableTriangular((int, int)[] coordenates) : base(coordenates)
    {

    }
    protected override void Expand(INode node)
    {
        NodeGeometry? geometry = (node as NodeGeometry);
        if (geometry == null) return;
        for (int i = 0; i < node.Conections.Length; i++)
        {
            (int, int)[] coordenates = ExpandGeometry(CircularArray<(int, int)>.Circular(geometry.Ubication.Coord, i));
            AsignCoordenates(node, coordenates);
        }
    }
    protected override (int, int)[] ExpandGeometry((int, int)[] coordenates)
    {
        (int, int) coord1 = coordenates[0];
        (int, int) coord2 = coordenates[1];
        (int, int) coord3 = coordenates[2];
        bool nw = coord2.Item1 > coord1.Item1 && coord2.Item2 > coord1.Item2;
        bool w = coord2.Item2 == coord1.Item2 && coord2.Item1 > coord1.Item1;
        bool sw = coord2.Item1 > coord1.Item1 && coord2.Item2 < coord1.Item2;
        if (nw)
        {
            if (coord3.Item1 > coord1.Item1) return new [] { coord1, (coord2.Item1 - 2, coord2.Item2), coord2 };
            return new [] { coord1, coord2, (coord1.Item1 + 2, coord1.Item2) };
        }
        else if (w)
        {
            if (coord3.Item2 > coord1.Item2) return new [] { coord1, coord2, (coord3.Item1, coord3.Item2 - 2) };
            return new [] { coord1, coord2, (coord3.Item1, coord3.Item2 + 2) };
        }
        else if (sw)
        {
            if (coord3.Item1 < coord1.Item1) return new [] { coord1, (coord1.Item1 + 2, coord1.Item1), coord2 };
            return new [] { coord1, coord2, (coord2.Item1 - 2, coord2.Item2) };
        }
        return ExpandGeometry(new [] { coord2, coord1, coord3 });
    }
    public override TableGame Clone()
    {
        (int, int)[] aux = new (int, int)[3];
        Array.Copy(((NodeGeometry)this.TableNode[0]).Ubication.Coord, aux, 3);
        TableGame table = new TableTriangular(aux);
        return this.AuxClone(table);
    }
}
