using Table;
using InfoGame;

namespace Game;

public abstract class Printer
{
    /// <summary>
    /// Tipos de ficha
    /// </summary>
    public enum TypeToken
    {
        TriangleTop,
        TriangleBottom,
        Square,
        Hexagon,
        NDimension,
        DominoV,
        DominoH
    }

    public int Speed { get; protected set; }

    public Printer(int speed)
    {
        this.Speed = speed;
    }

    public delegate void BindLocationTable(IEnumerable<LocationGui> location);

    public delegate void BindLocationHand(IEnumerable<LocationGui> location, LocationGui? play, string action,
        string player);

    /// <summary>
    /// Bindiar el tablero logico con el front-end
    /// </summary>
    public static event BindLocationTable? BindTableEvent;

    /// <summary>
    /// Bindiar la mano del jugador con el front-end
    /// </summary>
    public static event BindLocationHand? BindHandEvent;

    public static void ExecuteTableEvent(IEnumerable<LocationGui> location)
    {
        BindTableEvent!(location);
    }

    public static void ExecuteHandEvent(IEnumerable<LocationGui> location, LocationGui? play, string action,
        string player)
    {
        BindHandEvent!(location, play, action, "Jugador " + player);
    }

    /// <summary>
    /// Determinar la distribucion de la mesa en la GUI
    /// </summary>
    /// <param name="table">Mesa</param>
    /// <typeparam name="T">Tipo para el juego</typeparam>
    /// <returns>Distribucion de la mesa para la GUI</returns>
    public abstract void LocationTable<T>(TableGame<T> table) ;

    /// <summary>
    /// Determinar la posicion de la mano de los jugadores
    /// </summary>
    /// <param name="tokens">Lista de fichas</param>
    /// <param name="play">Jugada actual</param>
    /// <param name="table">Mesa</param>
    /// <param name="player">Jugador</param>
    /// <typeparam name="T">Tipo de ficha para el juego</typeparam>
    /// <returns>Ubicacion en la GUI para la mano del play</returns>
    public abstract void LocationHand<T>(Hand<T> tokens, Token<T>? play, TableGame<T> table, string player)
        ;

    /// <summary>
    /// Asignar los valores a las fichas
    /// </summary>
    /// <param name="tokens">Lista de fichas</param>
    /// <param name="type">Tipo de ficha</param>
    /// <param name="row">Filas</param>
    /// <param name="column">Columnas</param>
    /// <typeparam name="T">Tipo de juego</typeparam>
    protected IEnumerable<LocationGui> AssignValues<T>(Hand<T> tokens, int row, int column, TypeToken type)
        
    {
        int indColumn = 0;
        int indRow = 0;
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
                type);
            indColumn++;
            if (indColumn == 10)
            {
                indColumn = 0;
                indRow++;
            }
        }
    }

    protected void DeterminateLocationHand<T>(Hand<T> tokens, Token<T>? play, TableGame<T> table, string player,
        int row, int column, TypeToken type) 
    {
        IEnumerable<LocationGui> location = AssignValues(tokens, row, column, type);

        string action = "Jugada";
        LocationGui? locationPlay = null;

        if (play != null)
        {
            string[] valuesPlay = new string[play.CantValues];
            for (int i = 0; i < valuesPlay.Length; i++)
            {
                valuesPlay[i] = play[i]!.ToString()!;
            }

            locationPlay = new LocationGui((0, 0, 0, 0), valuesPlay, type);
        }
        else action = "Pase";

        Printer.ExecuteHandEvent(location, locationPlay, action, player);
    }
}