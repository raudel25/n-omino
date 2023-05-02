using Rules;
using Table;

namespace Game;

public class BuildGame<T> : IReset<BuildGame<T>>
{
    public BuildGame(InitializerGame<T> game, InfoRules<T> infoRules, Printer print)
    {
        Initializer = game;
        Print = print;
        Rules = infoRules;
    }

    /// <summary>
    ///     Inicializador de juego
    /// </summary>
    public InitializerGame<T> Initializer { get; }

    /// <summary>
    ///     Reglas de juego
    /// </summary>
    public InfoRules<T> Rules { get; }

    /// <summary>
    ///     Printiador del juego
    /// </summary>
    public Printer Print { get; }

    public BuildGame<T> Reset()
    {
        return new BuildGame<T>(Initializer.Reset(), Rules, Print.Reset());
    }
}