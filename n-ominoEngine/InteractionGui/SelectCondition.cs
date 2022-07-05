using Rules;

namespace InteractionGui;

public class SelectCondition<T> : IVariant<ICondition<T>, T>
{
    public string Description { get; } = "Seleccionar una condicion";

    public List<IVariant<ICondition<T>, T>.Select> Values { get; } = new List<IVariant<ICondition<T>, T>.Select>()
    {
        (comp) => new ClassicWin<T>(),
        (comp) => new ClassicTeamWin<T>(),
        (comp) => new CantToPass<T>(comp.Cant),
        (comp) => new CantToPassTeam<T>(comp.Cant),
        (comp) => new ImmediatePass<T>(),
        (comp) => new NoValidPlayFirstPlayerPass<T>(),
        (comp) => new NoValidPlay<T>(),
        (comp) => new ConditionDefault<T>()
    };

    public List<ParamSelect> Param { get; } = new List<ParamSelect>()
    {
        new ParamSelect("Ganar el juego clasico", "Condicion clasica para ganar un jugador", 0),
        new ParamSelect("Ganar el juego clasico (team)", "Condicion clasica para ganar un equipo", 1),
        new ParamSelect("Cantidad de pases", "Cantidad de pases de un jugador", 2, false, true),
        new ParamSelect("Cantidad de pases (team)", "Cantidad de pases de un equipo", 3, false, true),
        new ParamSelect("Pase inmediato", "Pase inmediato de un jugador", 4),
        new ParamSelect("Tranque clasico (primero en pasarse)", "Ningun jugador tiene jugada valida", 5),
        new ParamSelect("Tranque clasico", "Ningun jugador tiene jugada valida", 6),
        new ParamSelect("Defecto", "Condicion activa por defecto", 7),
    };

    public SelectCondition(T value)
    {
        if (value is int)
        {
            Values.Add(SelectInt);
            Param.Add(new ParamSelect("Suma de los nodos libre",
                "Se activa cuando la suma de los nodos tiene un valor especifico",
                7));
        }
    }

    private ICondition<T> SelectInt(ParamSelectFunction<T> comp)
    {
        IComparison<int> aux = (comp.Comp as IComparison<int>)!;
        return (new SumFreeNode(comp.Cant, aux) as ICondition<T>)!;
    }
}