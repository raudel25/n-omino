namespace InteractionGui;

public interface IVariant<T1, T2>
{
    /// <summary>
    /// Delegado para la regla
    /// </summary>
    public delegate T1 Select(ParamSelectFunction<T2> select);

    /// <summary>
    /// Lista de valores para la regla
    /// </summary>
    public List<Select> Values { get; }

    /// <summary>
    /// Descripcion de la regla
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Lista de parametros
    /// </summary>
    public List<ParamSelect> Param { get; }
}