using Rules;

namespace InteractionGui;

public class SelectWinnerGame<T> : IVariant<IWinnerGame<T>, T>
{
    public string Description { get; } = "Forma de seleccionar el jugador del juego";

    public List<IVariant<IWinnerGame<T>, T>.Select> Values { get; } = new()
    {
        comp => new WinnerGameHigh<T>(),
        comp => new WinnerGameSmall<T>(),
        comp => new WinnerGameTeamHigh<T>(),
        comp => new WinnerGameSmall<T>()
    };

    public List<ParamSelect> Param { get; } = new()
    {
        new("Mayor puntuación", "El ganador es el que mayor puntuación acumule", 0),
        new("Menor puntuación", "El ganador es el que menor puntuación acumule", 1),
        new("Mayor puntuación (team)", "El ganador es el equipo que mayor puntuación acumule", 2),
        new("Menor puntuación (team)", "El ganador es el equipo que menor puntuación acumule", 3)
    };
}