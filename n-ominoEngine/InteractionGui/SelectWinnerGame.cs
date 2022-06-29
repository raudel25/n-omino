using Rules;

namespace InteractionGui;

public class SelectWinnerGameInt : ISelectVariantGui<IWinnerGame<int>>
{
    public List<IWinnerGame<int>> ValueParam { get; } = new List<IWinnerGame<int>>();

    public IWinnerGame<int>[] Values { get; } = new IWinnerGame<int>[]
    {
        new WinnerGameHigh<int>(), new WinnerGameSmall<int>(),
        new WinnerGameTeamHigh<int>(), new WinnerGameSmall<int>()
    };

    public ParamSelect[] Param { get; } = new ParamSelect[]
    {
        new ParamSelect("El ganador es el que mayor puntuacion acumule", 0),
        new ParamSelect("El ganador es el que menor puntuacion acumule", 1),
        new ParamSelect("El ganador es el equipo que mayor puntuacion acumule", 2),
        new ParamSelect("El ganador es el equipo que menor puntuacion acumule", 3)
    };
}