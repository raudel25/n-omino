namespace Table;

public abstract class TableGeometry<T> : TableGame<T>
{
    protected TableGeometry((int, int)[] coordinates) : base(coordinates.Length)
    {
        TableCoord = new Dictionary<Coordinates, INode<T>>();
        CoordValor = new Dictionary<(int, int), ValuesNode<T>>();
        INode<T> node = CreateNode(coordinates);
        FreeTable(node);
    }

    /// <summary>Coordenadas con sus respectivos nodos</summary>
    protected Dictionary<Coordinates, INode<T>> TableCoord { get; set; }

    /// <summary>Valor que contiene cada coordenada</summary>
    protected Dictionary<(int, int), ValuesNode<T>> CoordValor { get; set; }

    /// <summary>Buscar las coordenadas de un nodo expandido</summary>
    /// <param name="coordinates">Coordenadas del nodo a expandir</param>
    /// <returns>Coordenadas del nodo expandido</returns>
    protected abstract (int, int)[] ExpandGeometry((int, int)[] coordinates);

    /// <summary>Asignar las coordenadas obtenidas en ExpandGeometry</summary>
    /// <param name="node">Nodo a asignar</param>
    /// <param name="coordinates">Coordenadas del nodo a asignar</param>
    protected virtual void AssignCoordinates(INode<T> node, (int, int)[] coordinates)
    {
        var aux = new Coordinates(coordinates);
        //Comprobar si ya existe un nodo con esas coordenadas
        if (!TableCoord.ContainsKey(aux)) FreeTable(CreateNode(coordinates));

        var j = 0;
        //Buscar las conexiones libres
        while (node.Connections[j] != null)
        {
            j++;
            if (j == node.Connections.Length) break;
        }

        if (j == node.Connections.Length) return;
        UnionNode(node, TableCoord[aux], j);
    }

    /// <summary>Crear un nodo</summary>
    /// <param name="coordinates">Cooredenadas de la ficha</param>
    /// <returns>Nuevo nodo</returns>
    protected NodeGeometry<T> CreateNode((int, int)[] coordinates)
    {
        var node = new NodeGeometry<T>(coordinates, TableNode.Count);
        TableNode.Add(node);
        TableCoord.Add(node.Location, node);
        //Asignar in valor a cada cordenada
        for (var i = 0; i < coordinates.Length; i++)
            if (!CoordValor.ContainsKey(coordinates[i]))
                CoordValor.Add(coordinates[i], new ValuesNode<T>());

        return node;
    }

    protected override void AssignValues(INode<T> node, T[] values)
    {
        var nodeGeometry = (node as NodeGeometry<T>)!;
        Array.Copy(values, nodeGeometry.ValuesConnections, values.Length);
        //Asignamos los valores
        for (var j = 0; j < values.Length; j++)
        {
            if (!CoordValor[nodeGeometry.Location.Coord[j]].IsAssignValue)
                CoordValor[nodeGeometry.Location.Coord[j]].IsAssignValue = true;

            CoordValor[nodeGeometry.Location.Coord[j]].Values.Add(values[j]);
        }
    }

    public override ValuesNode<T>? ValuesNodeTable(INode<T> node, int ind)
    {
        var nodeGeometry = node as NodeGeometry<T>;
        if (nodeGeometry == null) return null;

        return CoordValor[nodeGeometry.Location.Coord[ind]];
    }
}