namespace Rules;

public interface IComparation
{
    /// <summary>Criterio de comparacion</summary>
    /// <param name="a">Entero a comparar</param>
    /// <param name="b">Entero a comparar</param>
    /// <returns>Si el criterio es valido</returns>
    public bool Compare(int a, int b);
}