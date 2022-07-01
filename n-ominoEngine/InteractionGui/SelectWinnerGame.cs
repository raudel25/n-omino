using Rules;

namespace InteractionGui;

public class SelectWinnerGame<T> : IVariant<IWinnerGame<T>,T>
{
    public string Description { get; } = "Forma de seleccionar el jugador del juego";

    public List<IVariant<IWinnerGame<T>,T>.Select> Values { get; }= new List<IVariant<IWinnerGame<T>,T>.Select>()
    {
        (comp)=>new WinnerGameHigh<T>(),
        (comp)=>new WinnerGameSmall<T>(),
        (comp)=>new WinnerGameTeamHigh<T>(),
        (comp)=>new WinnerGameSmall<T>()
    };

    public List<ParamSelect> Param { get; } = new List<ParamSelect>
    {
        new ParamSelect("Mayor puntuacion", "El ganador es el que mayor puntuacion acumule", 0),
        new ParamSelect("Menor puntuacion", "El ganador es el que menor puntuacion acumule", 1),
        new ParamSelect("Mayor puntuacion (team)", "El ganador es el equipo que mayor puntuacion acumule", 2),
        new ParamSelect("Menor puntuacion (team)", "El ganador es el equipo que menor puntuacion acumule", 3)
    };
}