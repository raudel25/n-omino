using Rules;

namespace InteractionGui;

public class SelectPlayerGameTournament<T> : IVariant<IPlayerGame, T>
{
    public string Description { get; } = "Determinar los jugadores que participan en el torneo";

    public List<IVariant<IPlayerGame, T>.Select> Values { get; } = new()
    {
        comp => new ClassicPlayerGame(),
        comp => new EvenPlayerGame(),
        comp => new OddPlayerGame()
    };

    public List<ParamSelect> Param { get; } = new()
    {
        new("Asignación clásica de los jugadores", "Todos los jugadores participan en el juego", 0),
        new("Asignación de los jugadores pares", "Solo participan los jugadores con Id par", 1),
        new("Asignación de los jugadores impares", "Solo participan los jugadores con Id impar", 2)
    };
}