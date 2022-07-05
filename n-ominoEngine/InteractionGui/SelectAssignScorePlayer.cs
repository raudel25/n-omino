using Rules;

namespace InteractionGui;

public class SelectAssignScorePlayer<T> : IVariant<IAssignScorePlayer<T>,T>
{
    public string Description { get; } = "Asignar score a los jugadores";

    public List<IVariant<IAssignScorePlayer<T>,T>.Select> Values { get; }= new List<IVariant<IAssignScorePlayer<T>, T>.Select>()
    {
        (comp)=>new AssignScoreClassic<T>(),
        (comp)=>new AssignScoreHands<T>(),
        (comp)=>new AssignScoreHandsSmallCant<T>(),
        (comp)=>new AssignScoreHandsHighCant<T>(),
    };

    public List<ParamSelect> Param { get; } = new List<ParamSelect>()
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
            3)
    };
    
    public SelectAssignScorePlayer()
    {
        T? a = default;
        if (a is int)
        {
            Values.Add(SelectInt);
            Param.Add(new ParamSelect("Score por los nodos libres",
                "Asignar el Score por la suma de los valores de las posiciones libres", 4));
        }
    }

    private IAssignScorePlayer<T> SelectInt(ParamSelectFunction<T> comp)
    {
        return (new AssignScoreSumFreeNode() as IAssignScorePlayer<T>)!;
    }
}