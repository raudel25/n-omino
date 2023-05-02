using Player;

namespace InteractionGui;

public class SelectStrategies<T> : IVariant<IStrategy<T>, T>
{
    public List<IVariant<IStrategy<T>, T>.Select> Values { get; } = new()
    {
        a => new RandomPlayer<T>(),
        a => new GreedyPlayer<T>(),
        a => new EnemyPlayer<T>(),
        a => new TeamPlayer<T>(),
        a => new SinglePlayer<T>(),
        a => new NoQuedarseAlFallo<T>()
    };

    public string Description => "Estrategia del jugador";

    public List<ParamSelect> Param { get; } = new()
    {
        new ParamSelect("Random", "Realiza una jugada aleatoria", 0),
        new ParamSelect("Bota Gorda", "Juega la ficha que más valor tiene", 1),
        new ParamSelect("Contra los oponentes", "Juega la el valor que más han matado los contrarios", 2),
        new ParamSelect("Para el equipo",
            "No mata las fichas que pusieron los de su equipo, pone las fichas que ha puesto el equipo", 3),
        new ParamSelect("Más cantidad", "Juaga el valor que más tiene", 4),
        new ParamSelect("No fallo", "Juega para no quedarse al fallo", 5)
    };
}

public class SelectDefaultStrategy<T> : IVariant<IStrategy<T>, T>
{
    public List<IVariant<IStrategy<T>, T>.Select> Values { get; } = new()
    {
        a => new RandomPlayer<T>(),
        a => new GreedyPlayer<T>(),
        a => new SinglePlayer<T>()
    };

    public string Description => "Estrategia por defecto del jugador";

    public List<ParamSelect> Param { get; } = new()
    {
        new ParamSelect("Random", "Realiza una jugada aleatoria", 0),
        new ParamSelect("Bota Gorda", "Juega la ficha que más valor tiene", 1),
        new ParamSelect("Más cantidad", "Juaga el valor que más tiene", 2)
    };
}