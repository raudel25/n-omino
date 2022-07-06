using Rules;

namespace InteractionGui;

public class SelectAssignScoreToken<T> : IVariant<IAssignScoreToken<T>, T>
{
    public List<IVariant<IAssignScoreToken<T>, T>.Select> Values { get; } = new();

    private void AssignScore()
    {
        Values.Add(AssignScoreTokenClassic);
        Param.Add(new ParamSelect("Asignador clásico", "Suma los valores de cada cara de la ficha", 0, false));

    }

    public string Description => "Asignador de score de la ficha";

    public List<ParamSelect> Param {get;} = new();

    public SelectAssignScoreToken()
    {
        this.AssignScore();
    }

    public IAssignScoreToken<T> AssignScoreTokenClassic (ParamSelectFunction<T> fun)
    {
        var a = fun as ParamSelectFunction<int>;
        return(new AssignScoreTokenClassic() as IAssignScoreToken<T>)!;
    }
}