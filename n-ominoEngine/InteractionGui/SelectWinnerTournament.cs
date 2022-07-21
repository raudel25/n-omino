using Rules;

namespace InteractionGui;

public class SelectWinnerTournament<T> : IVariant<IWinnerTournament, T>
{
    public string Description { get; } = "Forma de determinar el ganador del torneo";

    public List<IVariant<IWinnerTournament, T>.Select> Values { get; } =
        new List<IVariant<IWinnerTournament, T>.Select>()
        {
            (comp) => new ClassicWinnerTournament(),
            (comp) => new MaxPlayerScore()
        };

    public List<ParamSelect> Param { get; } = new List<ParamSelect>
    {
        new ParamSelect("Ganador del torneo clásico", "El ganador es el que mayor puntuación acumule", 0),
        new ParamSelect("Puntuación por los jugadores",
            "El ganador es el equipo que tenga mas score entre sus miembros", 1),
    };
}