using Table;

namespace Game;

public class LocationGui
{
    public delegate void BindLocationTable(IEnumerable<LocationGui> location);

    public delegate void BindLocationHand(IEnumerable<LocationGui> location, LocationGui? play, string action,
        string player);

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

        //Buscamos las cooredenadas extremas
        int left = int.MaxValue;
        int top = int.MinValue;
        for (int i = 0; i < table.TableNode.Count; i++)
        {
            left = Math.Min(((NodeGeometry<int>)table.TableNode[i]).Location.BorderLeft, left);
            top = Math.Max(((NodeGeometry<int>)table.TableNode[i]).Location.BorderTop, top);
        }

        (int row, int column) = DeterminateIncrement(table);

        IEnumerable<LocationGui> locationGui = AssignFindLocationGui(table, row, column, top, left);

        // foreach (var item in table.FreeNode)
        // {
        //     NodeGeometry<T>? node = item as NodeGeometry<T>;
        //     if (node == null) return;
        //
        //     locationGui[cont++] = CreateLocationTable(table, node, row, column, top, left, false);
        // }

        BindTableEvent!(locationGui);
    }

    /// <summary>
    /// Asignar los valores para las coordenadas en la GUI
    /// </summary>
    /// <param name="table">Mesa</param>
    /// <param name="row">Incremento en las filas</param>
    /// <param name="column">Incremento en las columnas</param>
    /// <param name="top">CoorY mas grande</param>
    /// <param name="left">CoorX mas pequena</param>
    /// <typeparam name="T">Tipo para el juego</typeparam>
    private static IEnumerable<LocationGui> AssignFindLocationGui<T>(TableGame<T> table, int row, int column,
        int top,
        int left)
    {
        foreach (var item in table.PlayNode)
        {
            NodeGeometry<T>? node = item as NodeGeometry<T>;
            if (node == null) yield break;

            yield return CreateLocationTable(table, node, row, column, top, left, true);
        }
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
        if (table is TableSquare<T>) return (2, 2);
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
            int ind = ReorganizeCoordinates(node.Location.Coord);
            T[] auxValues = AuxTable.CircularArray(node.ValuesConnections, ind).ToArray();
            for (int i = 0; i < values.Length; i++)
            {
                //values[i] = ((TableGeometry<T>) table).CoordValor[coordinatesAux[i]].Item1!.ToString()!;
                values[i] = auxValues[i]!.ToString()!;
            }
        }

        return new LocationGui(aux, values, !node.Location.Coord.Contains((n, m)), play);
    }

    /// <summary>
    /// Reorganizar los valores de la ficha empezando por la coordenada inferior izquierda
    /// </summary>
    /// <param name="coordinates">Coordenadas de la ficha</param>
    /// <returns>Reorganizacion de las coordenadas</returns>
    private static int ReorganizeCoordinates((int, int)[] coordinates)
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

        return ind;
    }

    /// <summary>
    /// Determinar la posicion de la mano de los jugadores
    /// </summary>
    /// <param name="tokens">Lista de fichas</param>
    /// <param name="play">Jugada actual</param>
    /// <param name="table">Mesa</param>
    /// <param name="player">Jugador</param>
    /// <typeparam name="T">Tipo de ficha para el juego</typeparam>
    /// <returns>Ubicacion en la GUI para la mano del play</returns>
    public static void FindLocationHand<T>(List<Token<T>> tokens, Token<T>? play, TableGame<T> table, string player)
    {
        (int row, int column) = DeterminateIncrement(table);
        int indColumn = 0;
        int indRow = 0;

        IEnumerable<LocationGui> location = AssignValues(tokens, indRow, indColumn, row, column);

        string action = "Jugada";
        LocationGui? locationPlay = null;

        if (play != null)
        {
            string[] valuesPlay = new string[play.CantValues];
            for (int i = 0; i < valuesPlay.Length; i++)
            {
                valuesPlay[i] = play[i]!.ToString()!;
            }

            locationPlay = new LocationGui((0, 0, 0, 0), valuesPlay, true, true);
        }
        else action = "Pase";

        BindHandEvent!(location, locationPlay, action, "Jugador " + player);
    }

    /// <summary>
    /// Asignar los valores a las fichas
    /// </summary>
    /// <param name="tokens">Lista de fichas</param>
    /// <param name="indRow">Incremento en las filas</param>
    /// <param name="indColumn">Incremento en las columnas</param>
    /// <param name="row">Filas</param>
    /// <param name="column">Columnas</param>
    /// <typeparam name="T">Tipo de juego</typeparam>
    private static IEnumerable<LocationGui> AssignValues<T>(List<Token<T>> tokens, int indRow, int indColumn,
        int row, int column)
    {
        foreach (var item in tokens)
        {
            string[] values = new string[item.CantValues];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = item[i]!.ToString()!;
            }

            yield return new LocationGui(
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
    }
}