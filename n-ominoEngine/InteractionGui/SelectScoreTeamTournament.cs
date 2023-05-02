using Rules;

namespace InteractionGui;

public class SelectScoreTeamTournament<T> : IVariant<IScoreTeamTournament<T>, T>
{
    public string Description { get; } = "Forma de asignar puntos a los equipos en el torneo";

    public List<IVariant<IScoreTeamTournament<T>, T>.Select> Values { get; } =
        new()
        {
            comp => new ClassicScoreTeamTournament<T>(),
            comp => new PlayersScoreTeamTournament<T>(),
            comp => new PointForTeam<T>()
        };

    public List<ParamSelect> Param { get; } = new()
    {
        new("Score de los equipos clásico",
            "Se le asigna al equipo ganador la suma de los puntos de las manos de los restantes equipos", 0),
        new("Score por la puntuación en el juego",
            "Se le asigna a los equipos la puntuación de sus respectivos miembros durante el juego", 1),
        new("Por puntos",
            "Se le asigna 100 puntos al equipo ganador", 2)
    };
}