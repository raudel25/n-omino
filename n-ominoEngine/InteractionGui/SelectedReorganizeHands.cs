using Rules;

namespace InteractionGui;

public class SelectReorganizeHands<T> : IVariant<IReorganizeHands<T>, T>
{
    public string Description { get; } = "Reorganizar las manos de los jugadores";

    public List<IVariant<IReorganizeHands<T>, T>.Select> Values { get; } = new()
    {
        comp => new ClassicReorganize<T>(),
        comp => new HandsTeamWin<T>()
    };

    public List<ParamSelect> Param { get; } = new()
    {
        new("No reorganiza las manos", "No reorganiza las manos", 0),
        new("El equipo ganador", "Las manos de los integrantes del equipo ganador pierden una ficha", 1)
    };
}