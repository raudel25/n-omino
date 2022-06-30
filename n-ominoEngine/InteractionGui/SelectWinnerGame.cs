using Rules;

namespace InteractionGui;

public class SelectWinnerGameInt : ISelectVariantGui<IWinnerGame<int>, int>
{
    public List<IWinnerGame<int>> ValueParam { get; } = new List<IWinnerGame<int>>();

    public ISelectVariantGui<IWinnerGame<int>, int>.Select[] Values { get; } =
        new ISelectVariantGui<IWinnerGame<int>, int>.Select[]
        {
            ((comparison, a, b) => (new WinnerGameHigh<int>())),
            ((comparison, a, b) => (new WinnerGameSmall<int>())),
            ((comparison, a, b) => (new WinnerGameTeamHigh<int>())),
            ((comparison, a, b) => (new WinnerGameSmall<int>()))
        };

    public ParamSelect[] Param { get; } = new ParamSelect[]
    {
        new ParamSelect("Mayor puntuacion", "El ganador es el que mayor puntuacion acumule", 0),
        new ParamSelect("Menor puntuacion", "El ganador es el que menor puntuacion acumule", 1),
        new ParamSelect("Mayor puntuacion (team)", "El ganador es el equipo que mayor puntuacion acumule", 2),
        new ParamSelect("Menor puntuacion (team)", "El ganador es el equipo que menor puntuacion acumule", 3)
    };
}