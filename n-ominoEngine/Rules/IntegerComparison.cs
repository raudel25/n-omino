namespace Rules;

public class CongruenceComparison : IComparison<int>
{
    private readonly int _congruence;

    public CongruenceComparison(int n)
    {
        _congruence = n;
    }

    public bool Compare(int a, int b)
    {
        return a % _congruence == b % _congruence;
    }
}

public class HighNumberComparison : IComparison<int>
{
    private readonly int _highNumber;

    public HighNumberComparison(int n)
    {
        _highNumber = n;
    }

    public bool Compare(int a, int b)
    {
        return a > _highNumber && b > _highNumber;
    }
}

public class SmallNumberComparison : IComparison<int>
{
    private readonly int _smallNumber;

    public SmallNumberComparison(int n)
    {
        _smallNumber = n;
    }

    public bool Compare(int a, int b)
    {
        return a < _smallNumber && b < _smallNumber;
    }
}

public class ComodinComparison : IComparison<int>
{
    private readonly int _comodin;

    public ComodinComparison(int n)
    {
        _comodin = n;
    }

    public bool Compare(int a, int b)
    {
        if (a == _comodin || b == _comodin) return true;
        return a == b;
    }
}

public class DivisibleComparison : IComparison<int>
{
    private readonly int _divisible;

    public DivisibleComparison(int n)
    {
        _divisible = n;
    }

    public bool Compare(int a, int b)
    {
        return a % _divisible == 0 && b % _divisible == 0;
    }
}

public class GcdComparison : IComparison<int>
{
    private readonly int _gcd;

    public GcdComparison(int n)
    {
        _gcd = n;
    }

    public bool Compare(int a, int b)
    {
        if (a == 0 || b == 0) return false;
        return GCD(a, b) == _gcd;
    }

    public static int GCD(int m, int n)
    {
        var a = Math.Max(m, n);
        var b = Math.Min(m, n);
        while (a % b != 0)
        {
            var c = a % b;
            a = b;
            b = c;
        }

        return b;
    }
}