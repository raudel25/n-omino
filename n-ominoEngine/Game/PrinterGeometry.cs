using InfoGame;
using Table;

namespace Game;

public class PrinterGeometry : Printer
{
    public PrinterGeometry(int speed) : base(speed)
    {
    }

    public override void LocationTable<T>(TableGame<T> table)
    {
        Thread.Sleep(Speed);

        //Buscamos las cooredenadas extremas
        var left = int.MaxValue;
        var top = int.MinValue;
        for (var i = 0; i < table.TableNode.Count; i++)
        {
            left = Math.Min(((NodeGeometry<T>)table.TableNode[i]).Location.BorderLeft, left);
            top = Math.Max(((NodeGeometry<T>)table.TableNode[i]).Location.BorderTop, top);
        }

        var type = TypeToken.TriangleTop;

        (var row, var column) = DeterminateTypeToken(table, ref type);

        var locationGui = AssignFindLocationGui(table, row, column, top, left, type);

        ExecuteTableEvent(locationGui);
    }

    public override void LocationHand<T>(InfoPlayer<T> player, Token<T>? play, TableGame<T> table)
    {
        var type = TypeToken.TriangleTop;
        (var row, var column) = DeterminateTypeToken(table, ref type);
        DeterminateLocationHand(play, table, player, row, column, type);

        Thread.Sleep(Speed);
    }

    /// <summary>
    ///     Asignar los valores para las coordenadas en la GUI
    /// </summary>
    /// <param name="table">Mesa</param>
    /// <param name="row">Incremento en las filas</param>
    /// <param name="column">Incremento en las columnas</param>
    /// <param name="top">CoorY mas grande</param>
    /// <param name="left">CoorX mas pequena</param>
    /// <param name="type">Tipo de ficha</param>
    /// <typeparam name="T">Tipo para el juego</typeparam>
    private IEnumerable<LocationGui> AssignFindLocationGui<T>(TableGame<T> table, int row, int column,
        int top,
        int left, TypeToken type)
    {
        foreach (var item in table.PlayNode)
        {
            var node = item as NodeGeometry<T>;
            if (node == null) yield break;

            yield return CreateLocationTable(node, row, column, top, left, type);
        }
    }

    /// <summary>
    ///     Determinar el incremento en filas y columnas
    /// </summary>
    /// <param name="table">Mesa</param>
    /// <param name="type">Tipo de ficha</param>
    /// <typeparam name="T">Tipo para el juego</typeparam>
    private static (int, int) DeterminateTypeToken<T>(TableGame<T> table, ref TypeToken type)
    {
        if (table is TableTriangular<T>)
        {
            type = TypeToken.TriangleTop;
            return (1, 2);
        }

        if (table is TableHexagonal<T>)
        {
            type = TypeToken.Hexagon;
            return (2, 4);
        }

        if (table is TableSquare<T>)
        {
            type = TypeToken.Square;
            return (2, 2);
        }

        return (0, 0);
    }

    /// <summary>
    ///     Crear la distribucion en la GUI de un ficha
    /// </summary>
    /// <param name="node">Nodo</param>
    /// <param name="row">Incremento en las filas</param>
    /// <param name="column">Incremento en las columnas</param>
    /// <param name="top">CoorY mas grande</param>
    /// <param name="left">CoorX mas pequena</param>
    /// <param name="type">Typo de ficha</param>
    /// <typeparam name="T">Tipo para el juego</typeparam>
    /// <returns>Distribucion de la ficha</returns>
    private LocationGui CreateLocationTable<T>(NodeGeometry<T> node, int row, int column,
        int top,
        int left, TypeToken type)
    {
        var n = node.Location.BorderLeft;
        var m = node.Location.BorderTop;
        var aux = (top - m + 1, top - m + row + 1, n - left + 1, n - left + column + 1);

        var values = new string[node.Connections.Length];

        var ind = ReorganizeCoordinates(node.Location.Coord);
        var auxValues = AuxTable.CircularArray(node.ValuesConnections, ind).ToArray();

        for (var i = 0; i < values.Length; i++) values[i] = auxValues[i]!.ToString()!;

        if (type == TypeToken.TriangleTop)
            if (node.Location.Coord.Contains((n, m)))
                type = TypeToken.TriangleBottom;

        return new LocationGui(aux, values, type);
    }

    /// <summary>
    ///     Reorganizar los valores de la ficha empezando por la coordenada inferior izquierda
    /// </summary>
    /// <param name="coordinates">Coordenadas de la ficha</param>
    /// <returns>Reorganizacion de las coordenadas</returns>
    private int ReorganizeCoordinates((int, int)[] coordinates)
    {
        var minX = int.MaxValue;
        var minY = int.MaxValue;
        var ind = 0;

        //Buscar el indice donde se encuentra la coordenada inferior izquierda
        for (var i = 0; i < coordinates.Length; i++)
            if (minY >= coordinates[i].Item2)
            {
                if (minY == coordinates[i].Item2)
                    (minX, ind) = minX > coordinates[i].Item1 ? (coordinates[i].Item1, i) : (minX, ind);
                else (minX, minY, ind) = (coordinates[i].Item1, coordinates[i].Item2, i);
            }

        return ind;
    }

    public override Printer Reset()
    {
        return new PrinterGeometry(Speed);
    }
}