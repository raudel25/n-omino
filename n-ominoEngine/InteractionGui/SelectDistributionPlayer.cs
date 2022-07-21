using Rules;

namespace InteractionGui;

public class SelectDistributionPlayer<T> : IVariant<IDistributionPlayer, T>
{
    public string Description { get; } = "Determinar la distribución de los jugadores en el juego";

    public List<IVariant<IDistributionPlayer, T>.Select> Values { get; } =
        new List<IVariant<IDistributionPlayer, T>.Select>()
        {
            (comp) => new ClassicDistribution(),
            (comp) => new TeamDistribution()
        };

    public List<ParamSelect> Param { get; } = new List<ParamSelect>
    {
        new ParamSelect("Distribución clásica", "Los jugadores de cada equipo se sitúan alternadamente", 0),
        new ParamSelect("Distribución por equipos", "Los miembros de cada equipo se sitúan uno al lado del otro", 1)
    };
}