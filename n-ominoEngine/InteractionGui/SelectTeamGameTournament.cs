using Rules;

namespace InteractionGui;

public class SelectTeamGameTournament<T> : IVariant<ITeamsGame, T>
{
    public string Description { get; } = "Determinar los equipos que participan en el juego";

    public List<IVariant<ITeamsGame, T>.Select> Values { get; } = new List<IVariant<ITeamsGame, T>.Select>()
    {
        (comp) => new ClassicTeam()
    };

    public List<ParamSelect> Param { get; } = new List<ParamSelect>
    {
        new ParamSelect("Asignación clásica para los equipos en el juego", "Todos los equipos participan en el juego",
            0),
    };
}