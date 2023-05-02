using Rules;

namespace InteractionGui;

public class SelectTeamGameTournament<T> : IVariant<ITeamsGame, T>
{
    public string Description { get; } = "Determinar los equipos que participan en el juego";

    public List<IVariant<ITeamsGame, T>.Select> Values { get; } = new()
    {
        comp => new ClassicTeam(),
        comp => new LeagueTeam(comp.Cant)
    };

    public List<ParamSelect> Param { get; } = new()
    {
        new("Asignación clásica para los equipos en el juego", "Todos los equipos participan en el juego",
            0),
        new("Liga de n-omino", "Liga donde se produce un todos contra todos",
            1, false, true)
    };
}