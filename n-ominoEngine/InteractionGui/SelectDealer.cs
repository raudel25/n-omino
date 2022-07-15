using Rules;

namespace InteractionGui;

public class SelectDealer<T> : IVariant<IDealer<T>, T>
{
    public string Description { get; } = "Forma de repartir fichas";

    public List<IVariant<IDealer<T>, T>.Select> Values { get; } = new List<IVariant<IDealer<T>, T>.Select>()
    {
        (comp) => new RandomDealer<T>()
    };

    public List<ParamSelect> Param { get; } = new List<ParamSelect>()
    {
        new ParamSelect("Repartidor Clasico", "Modo clasico para repartir las fichas", 0),
    };
}