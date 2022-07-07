using Rules;

namespace InteractionGui;

public class SelectVisibilityPlayer<T> : IVariant<IVisibilityPlayer<T>, T>
{
    public List<IVariant<IVisibilityPlayer<T>, T>.Select> Values { get; } =
        new List<IVariant<IVisibilityPlayer<T>, T>.Select>()
        {
            (comp) => new ClassicVisibilityPlayer<T>(),
            (comp) => new TeamVisibilityPlayer<T>()
        };

    public string Description { get; } = "Seleccionar la visibilidad de los jugadores";

    public List<ParamSelect> Param { get; } = new List<ParamSelect>()
    {
        new ParamSelect("Su mano", "El jugador solo ve su mano", 0),
        new ParamSelect("Las manos de su equipo", "El jugador puede ver todas las manos de los miembros de su equipo",
            1)
    };
}