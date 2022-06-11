namespace Rules;

public interface IToPassToken
{
    /// <summary>
    /// Determinar si el jugador se puede pasar con fichas
    /// </summary>
    /// <returns></returns>
    public bool ToPass();
}

public class YesToPassToken : IToPassToken
{
    public bool ToPass()
    {
        return true;
    }
}

public class NoToPassToken : IToPassToken
{
    public bool ToPass()
    {
        return false;
    }
}