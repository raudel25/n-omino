using Rules;

namespace InteractionGui;

public class SelectConditionInt : ISelectVariantGui<ICondition<int>>
{
    public List<ICondition<int>> ValueParam { get; } = new List<ICondition<int>>();

    public ICondition<int>[] Values { get; } = new ICondition<int>[]
    {
        new ClassicWin<int>(), new ClassicTeamWin<int>(),
        new CantToPass<int>(2), new CantToPassTeam<int>(2), new ImmediatePass<int>(), new NoValidPLay<int>(),
        new ConditionDefault<int>()
    };

    public ParamSelect[] Param { get; } = new ParamSelect[]
    {
        new ParamSelect("Condicion clasica para ganar un jugador", 0),
        new ParamSelect("Condicion clasica para ganar un equipo", 1),
        new ParamSelect("Cantidad de pases de un jugador", 2), new ParamSelect("Cantidad de pases de un equipo", 3),
        new ParamSelect("Pase inmediato de un jugador", 4), new ParamSelect("Ningun jugador tiene jugada valida", 5),
        new ParamSelect("Condicion activa por defecto", 6)
    };
}