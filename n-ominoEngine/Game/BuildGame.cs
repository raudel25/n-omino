using Rules;

namespace Game;

public class BuildGame<T>
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
}