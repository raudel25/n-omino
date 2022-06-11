namespace Rules;

public interface IComparison
{
    /// <summary>Criterio de comparacion</summary>
    /// <param name="a">Entero a comparar</param>
    /// <param name="b">Entero a comparar</param>
    /// <returns>Si el criterio es valido</returns>
    public bool Compare(int a, int b);
}

public class ClassicComparison : IComparison
{
    public bool Compare(int a, int b)
    {
        return a == b;
    }
}

public class CongruenceComparison : IComparison
{
    private int _congruence;

    public CongruenceComparison(int n)
    {
        this._congruence = n;
    }

    public bool Compare(int a, int b)
    {
        return a % this._congruence == b % this._congruence;
    }
}

public class HighNumberComparison : IComparison
{
    private int _mayorNumber;

    public HighNumberComparison(int n)
    {
        this._mayorNumber = n;
    }

    public bool Compare(int a, int b)
    {
        return a > this._mayorNumber && b > this._mayorNumber;
    }
}

public class SmallNumberComparison : IComparison
{
    private int _smallNumber;

    public SmallNumberComparison(int n)
    {
        this._smallNumber = n;
    }

    public bool Compare(int a, int b)
    {
        return a > this._smallNumber && b > this._smallNumber;
    }
}

public class ComodinComparison : IComparison
{
    private int _comodin;

    public ComodinComparison(int n)
    {
        this._comodin = n;
    }

    public bool Compare(int a, int b)
    {
        if (a == this._comodin || b == this._comodin) return true;
        return a == b;
    }
}

public class DivisibleComparison : IComparison
{
    private int _divisible;

    public DivisibleComparison(int n)
    {
        this._divisible = n;
    }

    public bool Compare(int a, int b)
    {
        return a % this._divisible == 0 && b % this._divisible == 0;
    }
}

public class GcdComparison : IComparison
{
    private int _gcd;

    public GcdComparison(int n)
    {
        this._gcd = n;
    }

    public bool Compare(int a, int b)
    {
        return GCD(a, b) == this._gcd;
    }

    private int GCD(int m, int n)
    {
        int a = Math.Max(m, n);
        int b = Math.Min(m, n);
        while (a % b != 0)
        {
            int c = a % b;
            a = b;
            b = c;
        }

        return b;
    }
}