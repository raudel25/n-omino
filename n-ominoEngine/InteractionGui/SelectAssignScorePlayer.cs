using Rules;

namespace InteractionGui;

public class SelectAssignScorePlayerInt : ISelectVariantGui<IAssignScorePlayer<int>, int>
{
    public List<IAssignScorePlayer<int>> ValueParam { get; } = new List<IAssignScorePlayer<int>>();

    public ISelectVariantGui<IAssignScorePlayer<int>, int>.Select[] Values { get; } =
        new ISelectVariantGui<IAssignScorePlayer<int>, int>.Select[]
        {
            ((comparison, a, b) => (new AssignScoreClassic<int>())),
            ((comparison, a, b) => (new AssignScoreHands<int>())),
            ((comparison, a, b) => (new AssignScoreHandsSmallCant<int>())),
            ((comparison, a, b) => (new AssignScoreHandsHighCant<int>())),
            ((comparison, a, b) => (new AssignScoreSumFreeNode()))
        };

    public ParamSelect[] Param { get; } = new ParamSelect[]
    {
        new ParamSelect("Score clasico", "Asignar el Score cuando un jugador se pega", 0),
        new ParamSelect("Score relativo al tranque",
            "Asignar el Score por el valor de las fichas en la mano del jugador", 1),
        new ParamSelect(
            "Mano del jugador por la menor cantidad",
            "Asignar el Score por el valor de las fichas en la mano del jugador, dandole prioridad al que menos fichas tenga",
            2),
        new ParamSelect(
            "Mano del jugador por la mayor cantidad",
            "Asignar el Score por el valor de las fichas en la mano del jugador, dandole prioridad al que mas fichas tenga",
            3),
        new ParamSelect("Score por los nodos libres",
            "Asignar el Score por la suma de los valores de las posiciones libres", 4)
    };
}