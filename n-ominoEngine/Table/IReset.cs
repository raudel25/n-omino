namespace Table;

public interface IReset<T>
{
    /// <summary>
    /// Volver el objeto a su estado inicial
    /// </summary>
    /// <returns>Objeto con su estado inicial</returns>
    public T Reset();
}