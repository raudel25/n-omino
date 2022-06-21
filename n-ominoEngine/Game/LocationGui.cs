using Table;

namespace Game;

public class LocationGui
{
    public delegate void BindLocationTable(LocationGui[] location);
    public delegate void BindLocationHand(LocationGui[] location,string player);
    /// <summary>
    /// Bindiar el tablero logico con el front-end
    /// </summary>
    public static event BindLocationTable? BindTableEvent;
    
    public static event BindLocationHand? BindHandEvent;

    /// <summary>
    /// Filas y las columnas para ubicar cada ficha
    /// </summary>
    public (int, int, int, int) Location { get; set; }

    /// <summary>
    /// Valores de la ficha
    /// </summary>
    public string[] Values { get; set; }

    /// <summary>
    /// Condicion para el tipo de visualizacion 
    /// </summary>
    public bool Condition { get; set; }

    /// <summary>
    /// Determinar si la ficha fue jugada o es un espacio libre
    /// </summary>
    public bool Play { get; set; }

    public LocationGui((int, int, int, int) location, string[] values, bool condition, bool play)
    {
        this.Location = location;
        this.Values = values;
        this.Condition = condition;
        this.Play = play;
    }

    /// <summary>
    /// Determinar la distribucion de la mesa en la GUI
    /// </summary>
    /// <param name="table">Mesa</param>
    /// <typeparam name="T">Tipo para el juego</typeparam>
    /// <returns>Distribucion de la mesa para la GUI</returns>
    public static void FindLocationTable<T>(TableGame<T> table)
    {
        LocationGui[] locationGui = new LocationGui[table.TableNode.Count];

        //Buscamos las cooredenadas extremas
        int left = int.MaxValue;
        int top = int.MinValue;
        for (int i = 0; i < table.TableNode.Count; i++)
        {
            left = Math.Min(((NodeGeometry<int>) table.TableNode[i]).Location.BorderLeft, left);
            top = Math.Max(((NodeGeometry<int>) table.TableNode[i]).Location.BorderTop, top);
        }

        (int row, int column) = DeterminateIncrement(table);

        //Asignar los valores
        int cont = 0;
        foreach (var item in table.PlayNode)
        {
            NodeGeometry<T>? node = item as NodeGeometry<T>;
            if (node == null) return;

            locationGui[cont++] = CreateLocationTable(table, node, row, column, top, left, true);
        }

        foreach (var item in table.FreeNode)
        {
            NodeGeometry<T>? node = item as NodeGeometry<T>;
            if (node == null) return;

            locationGui[cont++] = CreateLocationTable(table, node, row, column, top, left, false);
        }

        BindTableEvent!(locationGui);
    }

    /// <summary>
    /// Determinar el incremento en filas y columnas
    /// </summary>
    /// <param name="table">Mesa</param>
    /// <typeparam name="T">Tipo para el juego</typeparam>
    private static (int, int) DeterminateIncrement<T>(TableGame<T> table)
    {
        if (table is TableTriangular<T>) return (1, 2);
        if (table is TableHexagonal<T>) return (2, 4);
        return (0, 0);
    }

    /// <summary>
    /// Crear la distribucion en la GUI de un ficha
    /// </summary>
    /// <param name="table">Mesa</param>
    /// <param name="node">Nodo</param>
    /// <param name="row">Incremento en las filas</param>
    /// <param name="column">Incremento en las columnas</param>
    /// <param name="top">CoorY mas grande</param>
    /// <param name="left">CoorX mas pequena</param>
    /// <param name="play">Si el nodo esta ocupado</param>
    /// <typeparam name="T">Tipo para el juego</typeparam>
    /// <returns>Distribucion de la ficha</returns>
    private static LocationGui CreateLocationTable<T>(TableGame<T> table, NodeGeometry<T> node, int row, int column,
        int top,
        int left, bool play)
    {
        int n = node.Location.BorderLeft;
        int m = node.Location.BorderTop;
        (int, int, int, int) aux = (top - m + 1, top - m + row + 1, n - left + 1, n - left + column + 1);

        string[] values = new string[node.Connections.Length];

        if (play)
        {
            (int, int)[] coordinatesAux = ReorganizeCoordinates(node.Location.Coord);
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = ((TableGeometry<T>) table).CoordValor[coordinatesAux[i]].Item1!.ToString()!;
            }
        }

        return new LocationGui(aux, values, !node.Location.Coord.Contains((n, m)), play);
    }

    /// <summary>
    /// Reorganizar los valores de la ficha empezando por la coordenada inferior izquierda
    /// </summary>
    /// <param name="coordinates">Coordenadas de la ficha</param>
    /// <returns>Reorganizacion de las coordenadas</returns>
    private static (int, int)[] ReorganizeCoordinates((int, int)[] coordinates)
    {
        int minX = int.MaxValue;
        int minY = int.MaxValue;
        int ind = 0;

        //Buscar el indice donde se encuentra la coordenada inferior izquierda
        for (int i = 0; i < coordinates.Length; i++)
        {
            if (minY >= coordinates[i].Item2)
            {
                if (minY == coordinates[i].Item2)
                {
                    (minX, ind) = (minX > coordinates[i].Item1) ? (coordinates[i].Item1, i) : (minX, ind);
                }
                else (minX, minY, ind) = (coordinates[i].Item1, coordinates[i].Item2, i);
            }
        }

        return AuxTable.CircularArray(coordinates, ind);
    }

    /// <summary>
    /// Determinar la posicion de la mano de los jugadores
    /// </summary>
    /// <param name="tokens">Lista de fichas</param>
    /// <param name="table">Mesa</param>
    /// <typeparam name="T">Tipo de ficha para el juego</typeparam>
    /// <returns>Ubicacion en la GUI para la mano del play</returns>
    public static void FindLocationHand<T>(List<Token<T>> tokens, TableGame<T> table,string player)
    {
        (int row, int column) = DeterminateIncrement(table);
        int indColumn = 0;
        int indRow = 0;

        LocationGui[] location = new LocationGui[tokens.Count];
        for(int j=0;j<tokens.Count;j++)
        {
            string[] values = new string[tokens[j].Values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = tokens[j].Values[i]!.ToString()!;
            }

            location[j]= new LocationGui(
                (indRow * row + 1, indRow * row + row + 1, indColumn * column + 1, indColumn * column + column + 1),
                values,
                true,
                true);
            indColumn++;
            if (indColumn == 10)
            {
                indColumn = 0;
                indRow++;
            }
        }

        BindHandEvent!(location,"Jugador "+player);
    }
}