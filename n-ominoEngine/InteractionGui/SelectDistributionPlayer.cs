using Rules;

namespace InteractionGui;

public class SelectDistributionPlayer<T> : IVariant<IDistributionPlayer, T>
{
    public string Description { get; } = "Forma de determinar la distribucion de los juegadores en el juego";

    public List<IVariant<IDistributionPlayer, T>.Select> Values { get; } =
        new List<IVariant<IDistributionPlayer, T>.Select>()
        {
            (comp) => new ClassicDistribution(),
            (comp) => new TeamDistribution()
        };

    public List<ParamSelect> Param { get; } = new List<ParamSelect>
    {
        new ParamSelect("Distribucion clasica", "Los jugadores de cada equipo se situan alternadamente", 0),
        new ParamSelect("Distribucion por equipos", "Los miembros de cada equipo se situan uno al lado del otro", 1)
    };
}