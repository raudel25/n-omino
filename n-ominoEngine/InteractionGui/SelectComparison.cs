using Rules;

namespace InteractionGui;

public class SelectComparison<T> : IVariant<IComparison<T>, T>
{
    private T? Value;

    public List<IVariant<IComparison<T>, T>.Select> Values {get; } = new(){
        (comp) => new ClassicComparison<T>()
    };

    public string Description { get; } = "Elija el tipo de comparador";

    public List<ParamSelect> Param {get; } = new List<ParamSelect>(){
        new ParamSelect("Comparador clásico", "Equals", 0)
    };

    public void SelectComparisons()
    {
        if(this.Value is int)
        {
            Values.Add(SelectCongruenceComparison);
            Param.Add(new ParamSelect("Comparador por congruencia módulo ?", "", 1, 1, false));
            Values.Add(SelectHighNumberComparison);
            Param.Add(new ParamSelect("Comparador por mayor que número ?", "", 2, 1, false));
            Values.Add(SelectSmallNumberComparison);
            Param.Add(new ParamSelect("Comparador por menor que número ?", "", 3, 1, false));
            Values.Add(SelectComodinComparison);
            Param.Add(new ParamSelect("Comparador por comodín", "", 3, 1, false));
            Values.Add(SelectDivisibleComparison);
            Param.Add(new ParamSelect("Comparador por divisivilidad con ?", "", 3, 1, false));
            Values.Add(SelectGcdComparison);
            Param.Add(new ParamSelect("Comparador por máximo común divisor con ?", "", 3, 1, false));
        }
        if(this.Value is string)
        {
            Values.Add(SelectStringComparisonLevenshtein);
            Param.Add(new ParamSelect("Comparador por distancia de Levenshtein", "", 1, 0, false,false));
        }
        //chars
    }
    private IComparison<T> SelectCongruenceComparison(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<int>;
        return (new CongruenceComparison(a!.Left) as IComparison<T>)!;
    }

    private IComparison<T> SelectHighNumberComparison(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<int>;
        return (new HighNumberComparison(a!.Left) as IComparison<T>)!;
    }

    private IComparison<T> SelectSmallNumberComparison(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<int>;
        return (new SmallNumberComparison(a!.Left) as IComparison<T>)!;
    }

    private IComparison<T> SelectComodinComparison(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<int>;
        return (new ComodinComparison(a!.Left) as IComparison<T>)!;
    }

    private IComparison<T> SelectDivisibleComparison(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<int>;
        return (new DivisibleComparison(a!.Left) as IComparison<T>)!;
    }

    private IComparison<T> SelectGcdComparison(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<int>;
        return (new GcdComparison(a!.Left) as IComparison<T>)!;
    }

    private IComparison<T> SelectStringComparisonLevenshtein(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<string>;
        return (new StringComparisonLevenshtein(a!.n) as IComparison<T>)!;
    }
}