using Rules;

namespace InteractionGui;

public class SelectComparison<T> : IVariant<IComparison<T>, T>
{
    private T? Value;

    public List<IVariant<IComparison<T>, T>.Select> Values { get; } = new()
    {
        (comp) => new ClassicComparison<T>()
    };

    public string Description { get; } = "Elija el tipo de comparador";

    public List<ParamSelect> Param { get; } = new List<ParamSelect>()
    {
        new ParamSelect("Comparador clásico", "Equals", 0)
    };

    public SelectComparison(T val)
    {
        this.Value = val;
        this.SelectComparisons();
    }

    public void SelectComparisons()
    {
        if (this.Value is int)
        {
            Values.Add(SelectCongruenceComparison);
            Param.Add(new ParamSelect("Comparador por congruencia módulo n", "Dos números de consideran iguales si dejan el mismo resto módulo n", 1, false, true));
            Values.Add(SelectHighNumberComparison);
            Param.Add(new ParamSelect("Comparador por mayor que el número n", "Dos números de consideran iguales si son mayores que n", 2, true));
            Values.Add(SelectSmallNumberComparison);
            Param.Add(new ParamSelect("Comparador por menor que el número n", "Dos números de consideran iguales si son menores que n", 3, true));
            Values.Add(SelectComodinComparison);
            Param.Add(new ParamSelect("Comparador por comodín", "Un comodín que machea con todos los números", 4, true));
            Values.Add(SelectDivisibleComparison);
            Param.Add(new ParamSelect("Comparador por divisivilidad con n", "Dos números de consideran iguales si son divisibles por n", 5, false, true));
            Values.Add(SelectGcdComparison);
            Param.Add(new ParamSelect("Máximo común divisor igual a n", "Dos números de consideran iguales si su mcd es igual a n", 6, true));
        }

        //chars
        if (this.Value is char)
        {
            Values.Add(SelectLetterComparisonVocalsComodin);
            Param.Add(new ParamSelect("Comparador de vocales", "", 1));
            Values.Add(SelectLetterComparisonAlphabet);
            Param.Add(new ParamSelect("Orden alfabetico", "", 2));
        }
    }

    private IComparison<T> SelectCongruenceComparison(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<int>;
        return (new CongruenceComparison(a!.ValueForParam) as IComparison<T>)!;
    }

    private IComparison<T> SelectHighNumberComparison(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<int>;
        return (new HighNumberComparison(a!.ValueForParam) as IComparison<T>)!;
    }

    private IComparison<T> SelectSmallNumberComparison(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<int>;
        return (new SmallNumberComparison(a!.ValueForParam) as IComparison<T>)!;
    }

    private IComparison<T> SelectComodinComparison(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<int>;
        return (new ComodinComparison(a!.ValueForParam) as IComparison<T>)!;
    }

    private IComparison<T> SelectDivisibleComparison(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<int>;
        return (new DivisibleComparison(a!.ValueForParam) as IComparison<T>)!;
    }

    private IComparison<T> SelectGcdComparison(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<int>;
        return (new GcdComparison(a!.ValueForParam) as IComparison<T>)!;
    }

    private IComparison<T> SelectLetterComparisonVocalsComodin(ParamSelectFunction<T> fun)
    {
        return (new LetterComparisonVocalsComodin() as IComparison<T>)!;
    }

    private IComparison<T> SelectLetterComparisonAlphabet(ParamSelectFunction<T> fun)
    {
        return (new LetterComparisonAlphabet() as IComparison<T>)!;
    }
}