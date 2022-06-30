using Rules;

namespace InteractionGui;

public interface ISelectVariantGui<T1, T2>
{
    public delegate T1 Select(IComparison<T2> comparison, int a, int b);

    public List<T1> ValueParam { get; }

    public Select[] Values { get; }

    public ParamSelect[] Param { get; }
}