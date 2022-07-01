using Rules;

namespace InteractionGui;

public interface IVariant<T1,T2>
{
    public delegate T1 Select(ParamSelectFunction<T2> select);
    
    public List<Select> Values { get; }
    public string Description { get; }
    public List<ParamSelect> Param { get; }
}
