using Rules;
using Table;

namespace Game;

public class BuildGame<T> : IReset<BuildGame<T>>
{
    /// <summary>
    /// Inicializador de juego
    /// </summary>
    public InitializerGame<T> Initializer { get; private set; }

    /// <summary>
    /// Reglas de juego
    /// </summary>
    public InfoRules<T> Rules { get; private set; }

    /// <summary>
    /// Printiador del juego
    /// </summary>
    public Printer Print { get; private set; }

    public BuildGame(InitializerGame<T> game, InfoRules<T> infoRules, Printer print)
    {
        this.Initializer = game;
        this.Print = print;
        this.Rules = infoRules;
    }

    public BuildGame<T> Reset()
    {
        return new BuildGame<T>(Initializer.Reset(), Rules, Print.Reset());
    }
}