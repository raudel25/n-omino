using Rules;

namespace InteractionGui;

public class SelectMakerInt : ISelectVariantGui<ITokensMaker<int>, int>
{
    public List<ITokensMaker<int>> ValueParam { get; } = new List<ITokensMaker<int>>();

    public ISelectVariantGui<ITokensMaker<int>, int>.Select[] Values { get; } =
        new ISelectVariantGui<ITokensMaker<int>, int>.Select[]
        {
            ((comparison, a, b) => (new ClassicTokensMaker<int>())),
            ((comparison, a, b) => (new CircularTokensMaker<int>()))
        };

    public ParamSelect[] Param { get; } = new ParamSelect[]
    {
        new ParamSelect("Repartidor clasico", "Modo clasico de generar las fichas", 0),
        new ParamSelect("Repartidor circular", "Permutacion circular sobre las fichas", 1)
    };
}