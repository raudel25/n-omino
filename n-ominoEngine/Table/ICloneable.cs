namespace Table;

public interface ICloneable<T>
{
    /// <summary>
    ///     Devolver una copia exacta del objeto
    /// </summary>
    /// <returns>Copia exacta del objeto</returns>
    public T Clone();
}