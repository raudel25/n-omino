namespace Table;

public abstract class TableGeometry<T> : TableGame<T>
{
    // public override HashSet<Node> PlayNode { get; protected set; }
    // public override HashSet<Node> FreeNode { get; protected set; }
    // public override List<Node> TableNode { get; protected set; }
    /// <summary>Coordenadas con sus respectivos nodos</summary>
    public Dictionary<Coordinates, INode<T>> TableCoord { get; protected set; }

    /// <summary>Valor que contiene cada coordenada</summary>
    public Dictionary<(int, int), (T?, bool)> CoordValor { get; protected set; }

    protected TableGeometry((int, int)[] coordinates)
    {
        this.TableCoord = new Dictionary<Coordinates, INode<T>>();
        this.CoordValor = new Dictionary<(int, int), (T?, bool)>();
        INode<T> node = this.CreateNode(coordinates);
        this.FreeTable(node);
    }

    /// <summary>Buscar las coordenadas de un nodo expandido</summary>
    /// <param name="coordinates">Coordenadas del nodo a expandir</param>
    /// <returns>Coordenadas del nodo expandido</returns>
    protected abstract (int, int)[] ExpandGeometry((int, int)[] coordinates);

    /// <summary>Asignar las coordenadas obtenidas en ExpandGeometry</summary>
    /// <param name="node">Nodo a asignar</param>
    /// <param name="coordinates">Coordenadas del nodo a asignar</param>
    protected virtual void AssignCoordinates(INode<T> node, (int, int)[] coordinates)
    {
        Coordinates aux = new Coordinates(coordinates);
        //Comprobar si ya existe un nodo con esas coordenadas
        if (!this.TableCoord.ContainsKey(aux))
        {
            this.FreeTable(this.CreateNode(coordinates));
        }

        int j = 0;
        //Buscar las conexiones libres
        while (node.Connections[j] != null)
        {
            j++;
            if (j == node.Connections.Length) break;
        }

        if (j == node.Connections.Length) return;
        this.UnionNode(node, this.TableCoord[aux], j);
    }

    /// <summary>Crear un nodo</summary>
    /// <param name="coordinates">Cooredenadas de la ficha</param>
    /// <returns>Nuevo nodo</returns>
    protected NodeGeometry<T> CreateNode((int, int)[] coordinates)
    {
        NodeGeometry<T> node = new NodeGeometry<T>(coordinates, this.TableNode.Count);
        this.TableNode.Add(node);
        this.TableCoord.Add(node.Location, node);
        //Asignar in valor a cada cordenada
        for (int i = 0; i < coordinates.Length; i++)
        {
            if (!this.CoordValor.ContainsKey(coordinates[i]))
            {
                this.CoordValor.Add(coordinates[i], (default, false));
            }
        }

        return node;
    }

    protected override void AssignValues(INode<T> node, T[] values)
    {
        NodeGeometry<T> nodeGeometry = (node as NodeGeometry<T>)!;
        Array.Copy(values, nodeGeometry.ValuesConnections, values.Length);
        //Asignamos los valores
        for (int j = 0; j < values.Length; j++)
        {
            if (!this.CoordValor[nodeGeometry.Location.Coord[j]].Item2)
            {
                this.CoordValor[nodeGeometry.Location.Coord[j]] = (values[j], true);
            }
        }
    }
}