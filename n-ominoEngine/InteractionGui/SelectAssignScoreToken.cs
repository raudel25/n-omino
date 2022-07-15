using Rules;

namespace InteractionGui;

public class SelectAssignScoreToken<T> : IVariant<IAssignScoreToken<T>, T>
{
    public List<IVariant<IAssignScoreToken<T>, T>.Select> Values { get; } = new();

    private void AssignScore(T value)
    {
        if (value is int)
        {
            Values.Add(AssignScoreTokenClassic);
            Param.Add(new ParamSelect("Asignador clásico", "Suma los valores de cada cara de la ficha", 0, false));
            Values.Add(AssignScoreTokenMax);
            Param.Add(new ParamSelect("Maximo valor",
                "Asigna el score a la ficha por el maximo valor que esta contiene", 1));
            Values.Add(AssignScoreTokenMin);
            Param.Add(new ParamSelect("Minimo valor",
                "Asigna el score a la ficha por el minimo valor que esta contiene", 2));
            Values.Add(AssignScoreTokenGcd);
            Param.Add(new ParamSelect("MCD de sus valores", "Asigna el mcd de los valores de la ficha", 3));
        }

        if (value is char)
        {
            Values.Add(AssignScoreTokenLetter);
            Param.Add(new ParamSelect("Orden alfabetico", "Asigan el score por el orden alfabetico", 0));
        }
    }

    public string Description => "Asignador de score de la ficha";

    public List<ParamSelect> Param { get; } = new();

    public SelectAssignScoreToken(T value)
    {
        this.AssignScore(value);
    }

    public IAssignScoreToken<T> AssignScoreTokenClassic(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<int>;
        return (new AssignScoreTokenClassic() as IAssignScoreToken<T>)!;
    }

    public IAssignScoreToken<T> AssignScoreTokenMax(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<int>;
        return (new AssignScoreTokenMax() as IAssignScoreToken<T>)!;
    }

    public IAssignScoreToken<T> AssignScoreTokenMin(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<int>;
        return (new AssignScoreTokenMin() as IAssignScoreToken<T>)!;
    }

    public IAssignScoreToken<T> AssignScoreTokenGcd(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<int>;
        return (new AssignScoreTokenGcd() as IAssignScoreToken<T>)!;
    }

    public IAssignScoreToken<T> AssignScoreTokenLetter(ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<char>;
        return (new AssignScoreTokenLetter() as IAssignScoreToken<T>)!;
    }
}