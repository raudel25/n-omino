using Rules;

namespace InteractionGui;

public class SelectDealer<T> : IVariant<IDealer<T>, T>
{
    public string Description { get; } = "Forma de repartir fichas";

    public List<IVariant<IDealer<T>, T>.Select> Values { get; } = new()
    {
        comp => new RandomDealer<T>()
    };

    public List<ParamSelect> Param { get; } = new()
    {
        new("Repartidor clásico", "Modo clásico para repartir las fichas", 0)
    };
}