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
        (comp) => new ConditionDefault<T>(),
        (comp) => new HigherThanScoreHandCondition<T>(comp.Cant),
        (comp) => new PostRoundCondition<T>(comp.Cant),
        (comp) => new MaxScoreTeamTournament<T>(comp.Cant),
        (comp) => new CantGamesTournament<T>(comp.Cant)
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
        new ParamSelect("", "", 8, false, true),
        new ParamSelect("", "", 9, false, true),
        new ParamSelect("Maximo score para un torneo",
            "Determina si un equipo alcanzo una cantidad determinada de puntos", 10, false, true),
        new ParamSelect("Cantidad de juegos", "Determina la cantidad de juegos a efectuarse", 11, false, true)
    };

    public SelectCondition(T value)
    {
        if (value is int)
        {
            Values.Add(SelectInt);
            Param.Add(new ParamSelect("Suma de los nodos libre",
                "Se activa cuando la suma de los nodos tiene un valor especifico",
                12, true, false, true));
        }
    }

    private ICondition<T> SelectInt(ParamSelectFunction<T> comp)
    {
        var aux = (comp as ParamSelectFunction<int>)!;
        return (new SumFreeNode(aux.ValueForParam, aux.Comp!) as ICondition<T>)!;
    }
}