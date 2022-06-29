namespace Rules;

public class CongruenceComparison : IComparison<int>
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

public class HighNumberComparison : IComparison<int>
{
    private int _highNumber;

    public HighNumberComparison(int n)
    {
        this._highNumber = n;
    }

    public bool Compare(int a, int b)
    {
        return a > this._highNumber && b > this._highNumber;
    }
}

public class SmallNumberComparison : IComparison<int>
{
    private int _smallNumber;

    public SmallNumberComparison(int n)
    {
        this._smallNumber = n;
    }

    public bool Compare(int a, int b)
    {
        return a < this._smallNumber && b < this._smallNumber;
    }
}

public class ComodinComparison : IComparison<int>
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

public class DivisibleComparison : IComparison<int>
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

public class GcdComparison : IComparison<int>
{
    private int _gcd;

    public GcdComparison(int n)
    {
        this._gcd = n;
    }

    public bool Compare(int a, int b)
    {
        if (a == 0 || b == 0) return false;
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