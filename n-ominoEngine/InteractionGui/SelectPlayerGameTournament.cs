using Rules;

namespace InteractionGui;

public class SelectPlayerGameTournament<T> : IVariant<IPlayerGame, T>
{
    public string Description { get; } = "Determinar los jugadores que participan en el torneo";

    public List<IVariant<IPlayerGame, T>.Select> Values { get; } = new List<IVariant<IPlayerGame, T>.Select>()
    {
        (comp) => new ClassicPlayerGame(),
        (comp) => new EvenPlayerGame(),
        (comp) => new OddPlayerGame()
    };

    public List<ParamSelect> Param { get; } = new List<ParamSelect>
    {
        new ParamSelect("Asignacion clasica de los jugadores", "Todos los jugadores participan en el juego", 0),
        new ParamSelect("Asignacion de los jugadores pares", "Solo participan los jugadores con Id par", 1),
        new ParamSelect("Asignacion de los jugadores impares", "Solo participan los jugadores con Id impar", 2)
    };
}