namespace Rules;

public class ClasicComparation : IComparation
{
    public bool Compare(int a, int b)
    {
        return a == b;
    }
}
public class CongruenceComparation : IComparation
{
    public int _congruence { get; set; }
    public CongruenceComparation(int n)
    {
        this._congruence = n;
    }
    public bool Compare(int a, int b)
    {
        return a % this._congruence == b % this._congruence;
    }
}
public class MayorNumberComparation : IComparation
{
    public int _mayorNumber { get; set; }
    public MayorNumberComparation(int n)
    {
        this._mayorNumber = n;
    }
    public bool Compare(int a, int b)
    {
        return a > this._mayorNumber && b > this._mayorNumber;
    }
}
public class MenorNumberComparation : IComparation
{
    public int _menorNumber { get; set; }
    public MenorNumberComparation(int n)
    {
        this._menorNumber = n;
    }
    public bool Compare(int a, int b)
    {
        return a > this._menorNumber && b > this._menorNumber;
    }
}
public class ComodinComparation : IComparation
{
    private int _comodin { get; set; }
    public ComodinComparation(int n)
    {
        this._comodin = n;
    }
    public bool Compare(int a, int b)
    {
        if (a == this._comodin || b == this._comodin) return true;
        return a == b;
    }
}
public class DivisibleComparation : IComparation
{
    private int _divisible { get; set; }
    public DivisibleComparation(int n)
    {
        this._divisible = n;
    }
    public bool Compare(int a, int b)
    {
        return a % this._divisible == 0 && b % this._divisible == 0;
    }
}
public class GCDComparation : IComparation
{
    private int _gcd { get; set; }
    public GCDComparation(int n)
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