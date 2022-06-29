using Rules;

namespace InteractionGui;

public class SelectAssignScorePlayerInt : ISelectVariantGui<IAssignScorePlayer<int>>
{
    public List<IAssignScorePlayer<int>> ValueParam { get; } = new List<IAssignScorePlayer<int>>();

    public IAssignScorePlayer<int>[] Values { get; } = new IAssignScorePlayer<int>[]
    {
        new AssignScoreClassic<int>(), new AssignScoreHands<int>(),
        new AssignScoreHandsSmallCant<int>(), new AssignScoreHandsHighCant<int>(), new AssignScoreSumFreeNode()
    };

    public ParamSelect[] Param { get; } = new ParamSelect[]
    {
        new ParamSelect("Asignar el Score cuando un jugador se pega", 0),
        new ParamSelect("Asignar el Score por el valor de las fichas en la mano del jugador", 1),
        new ParamSelect(
            "Asignar el Score por el valor de las fichas en la mano del jugador, dandole prioridad al que menos fichas tenga",
            2),
        new ParamSelect(
            "Asignar el Score por el valor de las fichas en la mano del jugador, dandole prioridad al que mas fichas tenga",
            3),
        new ParamSelect("Asignar el Score por la suma de los valores de las posiciones libres", 4)
    };
}