using Player;

namespace InteractionGui;

public class SelectStrategies<T> : IVariant<IStrategy<T>, T>
{
    public List<IVariant<IStrategy<T>, T>.Select> Values {get;} = new(){
        (a) => new RandomPlayer<T>(),
        (a) => new GreedyPlayer<T>(),
        (a) => new PartnerPlayer<T>()
    };

    public string Description => "Estrategia del jugador";

    public List<ParamSelect> Param {get;} = new(){
        new ParamSelect("Random", "Realiza una jugada aleatoria", 0, 0, false, false),
        new ParamSelect("Bota Gorda", "Juega la ficha que más valor tiene", 1, 0, false, false),
        new ParamSelect("Para el equipo", "Pone las fichas que a puesto su equipo", 2, 0, false, false)
    };
}