using Rules;

namespace InteractionGui;

public class SelectDealerInt : ISelectVariantGui<IDealer<int>, int>
{
    public List<IDealer<int>> ValueParam { get; } = new List<IDealer<int>>();

    public ISelectVariantGui<IDealer<int>, int>.Select[] Values { get; } =
        new ISelectVariantGui<IDealer<int>, int>.Select[]
        {
            ((comparison, a, b) => (new RandomDealer<int>()))
        };

    public ParamSelect[] Param { get; } = new ParamSelect[]
    {
        new ParamSelect("Repartidor Clasico", "Modo clasico para repartir las fichas", 0),
    };
}