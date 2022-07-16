using Rules;

namespace InteractionGui;

public class SelectScoreTeamTournament<T> : IVariant<IScoreTeamTournament<T>, T>
{
    public string Description { get; } = "Forma de asignar puntos a los equipos en el torneo";

    public List<IVariant<IScoreTeamTournament<T>, T>.Select> Values { get; } =
        new List<IVariant<IScoreTeamTournament<T>, T>.Select>()
        {
            (comp) => new ClassicScoreTeamTournament<T>(),
            (comp) => new PlayersScoreTeamTournament<T>()
        };

    public List<ParamSelect> Param { get; } = new List<ParamSelect>
    {
        new ParamSelect("Score de los equipos clásico",
            "Se le asigna al equipo ganador la suma de los puntos de las manos de los restantes equipos", 0),
        new ParamSelect("Score por la puntuación en el juego",
            "Se le asigna a los equipos la puntuación de sus respectivos miembros durante el juego", 1),
    };
}