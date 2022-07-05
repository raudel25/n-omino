using Player;

namespace InteractionGui;

public class SelectPlayer<T> : IVariant<Player<T>, T>
{
    public List<IVariant<Player<T>, T>.Select> Values {get; } = new(){
        (a) => new PurePlayer<T>(a.Cant, a.Strategy)
    };

    public string Description => throw new NotImplementedException();

    public List<ParamSelect> Param {get; } = new(){
        new ParamSelect("Jugador Puro", "Juega con una sola estrategia", 0, 0, true, false, true)
    };
}