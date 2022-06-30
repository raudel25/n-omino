using Rules;

namespace InteractionGui;

public class SelectConditionInt : ISelectVariantGui<ICondition<int>, int>
{
    public List<ICondition<int>> ValueParam { get; } = new List<ICondition<int>>();

    public ISelectVariantGui<ICondition<int>, int>.Select[] Values { get; } =
        new ISelectVariantGui<ICondition<int>, int>.Select[]
        {
            ((comparison, a, b) => new ClassicWin<int>()), ((comparison, a, b) => new ClassicTeamWin<int>()),
            ((comparison, a, b) => (new CantToPass<int>(a))), ((comparison, a, b) => (new CantToPassTeam<int>(a))),
            ((comparison, a, b) => (new ImmediatePass<int>())), ((comparison, a, b) => (new NoValidPLay<int>())),
            ((comparison, a, b) => (new ConditionDefault<int>()))
        };

    public ParamSelect[] Param { get; } = new ParamSelect[]
    {
        new ParamSelect("Ganar el juego clasico", "Condicion clasica para ganar un jugador", 0),
        new ParamSelect("Ganar el juego clasico (team)", "Condicion clasica para ganar un equipo", 1),
        new ParamSelect("Cantidad de pases", "Cantidad de pases de un jugador", 2,1),
        new ParamSelect("Cantidad de pases (team)", "Cantidad de pases de un equipo", 3,1),
        new ParamSelect("Pase inmediato", "Pase inmediato de un jugador", 4),
        new ParamSelect("Tranque clasico", "Ningun jugador tiene jugada valida", 5),
        new ParamSelect("Defecto", "Condicion activa por defecto", 6)
    };
}