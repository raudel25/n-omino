using Rules;

namespace InteractionGui;

public class SelectSteal<T>: IVariant<IStealToken<T>, T>
{
    public List<IVariant<IStealToken<T>, T>.Select> Values { get; } = new List<IVariant<IStealToken<T>, T>.Select>()
    {
        (comp) => new NoStealToken<T>(),
        (comp) => new ClassicStealToken<T>()
    };
    public string Description { get; } = "Seleccionar la forma de robar";

    public List<ParamSelect> Param { get; } = new List<ParamSelect>()
    {
        new ParamSelect("No robar", "No esta permitido robar", 0),
        new ParamSelect("Robar si no se puede jugar", "Se le da a al jugador una ficha aleatoria cuando este no lleva",
            1)
    };
}