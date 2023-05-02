using Rules;

namespace InteractionGui;

public class SelectCondition<T> : IVariant<ICondition<T>, T>
{
    public SelectCondition(T value)
    {
        if (value is int)
        {
            Values.Add(SelectInt);
            Param.Add(new ParamSelect("Suma de los nodos libre",
                "Se activa cuando la suma de los nodos tiene un valor especifico",
                13, true, false, true));
        }
    }

    public string Description { get; } = "Seleccionar una condición";

    public List<IVariant<ICondition<T>, T>.Select> Values { get; } = new()
    {
        comp => new ClassicWin<T>(),
        comp => new ClassicTeamWin<T>(),
        comp => new CantToPass<T>(comp.Cant),
        comp => new CantToPassTeam<T>(comp.Cant),
        comp => new ImmediatePass<T>(),
        comp => new NoValidPlayFirstPlayerPass<T>(),
        comp => new NoValidPlay<T>(),
        comp => new ConditionDefault<T>(),
        comp => new HigherThanScoreHandCondition<T>(comp.Cant),
        comp => new PostRoundCondition<T>(comp.Cant),
        comp => new MaxScoreTeamTournament<T>(comp.Cant),
        comp => new CantGamesTournament<T>(comp.Cant),
        comp => new SecondRoundTournament<T>()
    };

    public List<ParamSelect> Param { get; } = new()
    {
        new("Ganar el juego clásico", "Condición clásica para ganar un jugador", 0),
        new("Ganar el juego clásico (team)", "Condición clásica para ganar un equipo", 1),
        new("Cantidad de pases", "Cantidad de pases de un jugador", 2, false, true),
        new("Cantidad de pases (team)", "Cantidad de pases de un equipo", 3, false, true),
        new("Pase inmediato", "Pase inmediato de un jugador", 4),
        new("Tranque clásico (primero en pasarse)", "Ningun jugador tiene jugada válida", 5),
        new("Tranque clásico", "Ningun jugador tiene jugada válida", 6),
        new("Defecto", "Condición activa por defecto", 7),
        new("Puntos en mano", "El jugador tiene más de n puntos en la mano", 8, false, true),
        new("Después de n rondas", "El jugador ha jugado más de n veces", 9, false, true),
        new("Máximo score para un torneo",
            "Determina si un equipo alcanzó una cantidad determinada de puntos", 10, false, true),
        new("Cantidad de juegos", "Determina la cantidad de juegos a efectuarse", 11, false, true),
        new("Segunda ronda del torneo", "Se activa luego de la primera ronda del torneo", 12)
    };

    private ICondition<T> SelectInt(ParamSelectFunction<T> comp)
    {
        var aux = (comp as ParamSelectFunction<int>)!;
        return (new SumFreeNode(aux.ValueForParam, aux.Comp!) as ICondition<T>)!;
    }
}