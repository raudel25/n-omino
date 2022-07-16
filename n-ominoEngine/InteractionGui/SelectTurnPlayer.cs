using Rules;

namespace InteractionGui;

public class SelectTurnPlayer<T> : IVariant<ITurnPlayer, T>
{
    public List<IVariant<ITurnPlayer, T>.Select> Values { get; } = new List<IVariant<ITurnPlayer, T>.Select>()
    {
        (comp) => new TurnPlayerClassic(),
        (comp) => new TurnPlayerInvert(),
        (com) => new TurnPlayerRepeatPlay()
    };

    public string Description { get; } = "Seleccionar la visibilidad de los jugadores";

    public List<ParamSelect> Param { get; } = new List<ParamSelect>()
    {
        new ParamSelect("Turneador clásico", "Se reparten los turnos de forma clásica", 0),
        new ParamSelect("Turneador inverso", "Se invierte el orden de los turnos", 1),
        new ParamSelect("Repetir jugada", "Se repite el turno del jugador que acaba de jugar", 2)
    };
}