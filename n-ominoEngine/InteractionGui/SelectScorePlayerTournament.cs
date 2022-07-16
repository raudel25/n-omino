using Rules;

namespace InteractionGui;

public class SelectScorePlayerTournament<T> : IVariant<IScorePlayerTournament<T>, T>
{
    public string Description { get; } = "Forma de asignar puntos a los jugadores en el torneo";

    public List<IVariant<IScorePlayerTournament<T>, T>.Select> Values { get; } =
        new List<IVariant<IScorePlayerTournament<T>, T>.Select>()
        {
            (comp) => new ClassicScorePlayerTournament<T>(),
            (comp) => new GameScorePlayerTournament<T>()
        };

    public List<ParamSelect> Param { get; } = new List<ParamSelect>
    {
        new ParamSelect("Score clásico para los jugadores", "Se le asigna 100 puntos al jugador que gana el juego", 0),
        new ParamSelect("Score del juego actual",
            "Se le asigna a cada jugador la puntuación que acumuló dureante el juego", 1)
    };
}