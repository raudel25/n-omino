using Rules;

namespace InteractionGui;

public class SelectCondition<T> : IVariant<ICondition<T>,T>
{
    public string Description { get; } = "Seleccionar una condicion";

    //public delegate ICondition<T> Select(IComparison<T> comp, int a);


    public List<IVariant<ICondition<T>,T>.Select> Values { get; }= new List<IVariant<ICondition<T>,T>.Select>()
    {
        (comp) => new ClassicWin<T>(),
        (comp) => new ClassicTeamWin<T>(),
        (comp) => new CantToPass<T>(comp.Cant),
        (comp) => new CantToPassTeam<T>(comp.Cant),
        (comp) => new ImmediatePass<T>(),
        (comp) => new NoValidPLay<T>(),
        (comp) => new ConditionDefault<T>()
    };

    public List<ParamSelect> Param { get; } = new List<ParamSelect>()
    {
        new ParamSelect("Ganar el juego clasico", "Condicion clasica para ganar un jugador", 0),
        new ParamSelect("Ganar el juego clasico (team)", "Condicion clasica para ganar un equipo", 1),
        new ParamSelect("Cantidad de pases", "Cantidad de pases de un jugador", 2, 0,true),
        new ParamSelect("Cantidad de pases (team)", "Cantidad de pases de un equipo", 3, 0,true),
        new ParamSelect("Pase inmediato", "Pase inmediato de un jugador", 4),
        new ParamSelect("Tranque clasico", "Ningun jugador tiene jugada valida", 5),
        new ParamSelect("Defecto", "Condicion activa por defecto", 6),
    };

    public SelectCondition()
    {
        T? a = default;
        if (a is int)
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