namespace Rules;

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