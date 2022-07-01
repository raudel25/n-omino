using Rules;

namespace InteractionGui;

public class SelectMaker<T> : IVariant<ITokensMaker<T>,T>
{
    public string Description { get; } = "Forma de crear fichas";

    public List<IVariant<ITokensMaker<T>,T>.Select> Values { get; }= new List<IVariant<ITokensMaker<T>, T>.Select>()
    {
        (comp)=>new ClassicTokensMaker<T>(),
        (comp)=>new CircularTokensMaker<T>()
    };

    public List<ParamSelect> Param { get; } = new List<ParamSelect>()
    {
        new ParamSelect("Repartidor clasico", "Modo clasico de generar las fichas", 0),
        new ParamSelect("Repartidor circular", "Permutacion circular sobre las fichas", 1)
    };
}