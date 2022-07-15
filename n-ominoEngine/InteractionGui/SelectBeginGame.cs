using Rules;

namespace InteractionGui;

public class SelectBeginGame<T> : IVariant<IBeginGame<T>, T>
{
    public List<IVariant<IBeginGame<T>, T>.Select> Values { get; } = new()
    {
        (comp) => new BeginGameToken<T>(comp.Token!),
        (comp) => new BeginGameRandom<T>(),
        (comp) => new BeginGameLastWinner<T>()
    };

    public string Description => "Seleccione el modo de iniciar el juego";

    public List<ParamSelect> Param { get; } = new()
    {
        new ParamSelect("Iniciar el juego con la ficha ?", "", 0, false, false, false, false, true),
        new ParamSelect("Iniciar el juego con cualquier ficha", "", 1),
        new ParamSelect("Inicia el juego el ganador del juego anterior", "", 2),
    };
}