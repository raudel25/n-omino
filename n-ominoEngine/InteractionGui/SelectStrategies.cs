using Player;

namespace InteractionGui;

public class SelectStrategies<T> : IVariant<IStrategy<T>, T>
{
    public List<IVariant<IStrategy<T>, T>.Select> Values {get;} = new(){
        (a) => new RandomPlayer<T>(),
        (a) => new GreedyPlayer<T>(),
    };

    public string Description => "Estrategia del jugador";

    public List<ParamSelect> Param {get;} = new(){
        new ParamSelect("Random", "Realiza una jugada aleatoria", 0),
        new ParamSelect("Bota Gorda", "Juega la ficha que más valor tiene", 1)
    };
}