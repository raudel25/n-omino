using Rules;

namespace InteractionGui;

public class SelectMaker<T> : IVariant<ITokensMaker<T>, T>
{
    public string Description { get; } = "Forma de crear fichas";

    public List<IVariant<ITokensMaker<T>, T>.Select> Values { get; } = new()
    {
        comp => new ClassicTokensMaker<T>(),
        comp => new CircularTokensMaker<T>()
    };

    public List<ParamSelect> Param { get; } = new()
    {
        new("Generador clásico", "Modo clásico de generar las fichas", 0),
        new("Generador circular", "Permutación circular sobre las fichas", 1)
    };
}