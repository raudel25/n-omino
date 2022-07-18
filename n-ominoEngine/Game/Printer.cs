using Table;
using InfoGame;

namespace Game;

public abstract class Printer : IReset<Printer>
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
        DominoH,
        DominoVC,
        DominoHC
    }

    /// <summary>
    /// Velocidad del juego
    /// </summary>
    public int Speed { get; protected set; }

    public Printer(int speed)
    {
        this.Speed = speed;
    }

    public delegate void BindLocationTable(IEnumerable<LocationGui> location);

    public delegate void BindLocationHand(IEnumerable<LocationGui> location, LocationGui? play, string action,
        InfoPlayerGui player);

    public delegate void BindMessage(string winner);

    public delegate void GameOver();

    /// <summary>
    /// Bindiar el tablero logico con el front-end
    /// </summary>
    public static event BindLocationTable? BindTableEvent;

    /// <summary>
    /// Bindiar la mano del jugador con el front-end
    /// </summary>
    public static event BindLocationHand? BindHandEvent;

    /// <summary>
    /// Mostrar un mensaje en el front-end
    /// </summary>
    public static event BindMessage? BindMessageEvent;

    /// <summary>
    /// Indicar el fin del juego
    /// </summary>
    public static event GameOver? GameOverEvent;

    public static void ExecuteMessageEvent(string message)
    {
        Thread.Sleep(1000);
        BindMessageEvent!(message);
    }

    public static void ExecuteTableEvent(IEnumerable<LocationGui> location)
    {
        BindTableEvent!(location);
    }

    public static void ExecuteHandEvent(IEnumerable<LocationGui> location, LocationGui? play, string action,
        InfoPlayerGui player)
    {
        BindHandEvent!(location, play, action, player);
    }

    public static void ExecuteResetEvent()
    {
        Thread.Sleep(1000);
        GameOverEvent!();
    }

    /// <summary>
    /// Determinar la distribucion de la mesa en la GUI
    /// </summary>
    /// <param name="table">Mesa</param>
    /// <typeparam name="T">Tipo para el juego</typeparam>
    /// <returns>Distribucion de la mesa para la GUI</returns>
    public abstract void LocationTable<T>(TableGame<T> table);

    /// <summary>
    /// Determinar la posicion de la mano de los jugadores
    /// </summary>
    /// <param name="play">Jugada actual</param>
    /// <param name="table">Mesa</param>
    /// <param name="player">Jugador</param>
    /// <typeparam name="T">Tipo de ficha para el juego</typeparam>
    /// <returns>Ubicacion en la GUI para la mano del play</returns>
    public abstract void LocationHand<T>(InfoPlayer<T> player, Token<T>? play, TableGame<T> table);

    /// <summary>
    /// Asignar los valores a las fichas
    /// </summary>
    /// <param name="tokens">Lista de fichas</param>
    /// <param name="type">Tipo de ficha</param>
    /// <param name="row">Filas</param>
    /// <param name="column">Columnas</param>
    /// <typeparam name="T">Tipo de juego</typeparam>
    protected IEnumerable<LocationGui> AssignValues<T>(IEnumerable<Token<T>> tokens, int row, int column,
        TypeToken type)
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

    /// <summary>
    /// Determina las posiciones de las fichas en la mano
    /// </summary>
    /// <param name="play">Ficha a jugar</param>
    /// <param name="table">Mesa</param>
    /// <param name="player">Datos del jugador</param>
    /// <param name="row">Fila</param>
    /// <param name="column">Columna</param>
    /// <param name="type">Tipo de ficha</param>
    /// <typeparam name="T">Tipo que se utiliza en el juego</typeparam>
    protected void DeterminateLocationHand<T>(Token<T>? play, TableGame<T> table, InfoPlayer<T> player,
        int row, int column, TypeToken type)
    {
        IEnumerable<LocationGui> location = AssignValues(player.Hand, row, column, type);

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

        InfoPlayerGui playerInfo = new InfoPlayerGui(player.Name, player.Passes, player.Score);

        Printer.ExecuteHandEvent(location, locationPlay, action, playerInfo);
    }

    public abstract Printer Reset();

    Printer IReset<Printer>.Reset()
    {
        return this.Reset();
    }
}