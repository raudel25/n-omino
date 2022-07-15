using Player;

namespace InteractionGui;

public class SelectMoveScorer<T> : IVariant<Scorer<T>.MoveScorer, T>
{
    public List<IVariant<Scorer<T>.MoveScorer, T>.Select> Values {get;} = new(){
        (a) => Scorer<T>.RandomScorer
    };

    public string Description => "Seleccione un scorer para las jugadas";

    public List<ParamSelect> Param {get;} = new(){
        new ParamSelect("Scorer random", "Otorga un score random a la jugada", 0)
    };
}