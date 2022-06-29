namespace InteractionGui;

public interface ISelectVariantGui<T>
{
    public List<T> ValueParam { get; }

    public T[] Values { get; }

    public ParamSelect[] Param { get; }
}