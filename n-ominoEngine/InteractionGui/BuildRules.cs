using Rules;
using Table;

namespace InteractionGui;

public class BuildRules<T>
{
    /// <summary>Determinar si es valido jugar una ficha por un nodo</summary>
    public IsValidRule<T>? IsValidPlay { get; set; }

    /// <summary>
    /// Determinar la visibilidad del juego de los jugadores
    /// </summary>
    public VisibilityPlayerRule<T>? VisibilityPlayer { get; set; }

    /// <summary>
    /// Determinar la forma de robar de los jugadores
    /// </summary>
    public StealTokenRule<T>? StealTokens { get; set; }

    /// <summary>
    /// Determinar si un jugador se puede pasar con fichas
    /// </summary>
    public ToPassTokenRule<T>? ToPassToken { get; set; }

    /// <summary>Determinar la rotacion de los jugadores</summary>
    public TurnPlayerRule<T>? TurnPlayer { get; set; }

    /// <summary>Asignar un score a cada jugador</summary>
    public AssignScorePlayerRule<T>? AssignScorePlayer { get; set; }

    /// <summary>Determinar el ganador del juego</summary>
    public WinnerGameRule<T>? WinnerGame { get; set; }

    /// <summary>
    /// Determinar como se inicia el juego
    /// </summary>
    public BeginGameRule<T>? Begin { get; set; }

    /// <summary>
    /// Determinar el Score de una ficha
    /// </summary>
    public IAssignScoreToken<T>? ScoreToken { get; set; }

    /// <summary>
    /// Reorganizar las manos de los jugadores
    /// </summary>
    public ReorganizeHandsRule<T>? ReorganizeHands { get; set; }

    public TableGame<T>? Table { get; set; }

    public bool IsReady { get; set; }

    public InfoRules<T> Build()
    {
        return new InfoRules<T>(this.IsValidPlay!, this.VisibilityPlayer!, this.TurnPlayer!,
            this.StealTokens!, this.ToPassToken!,
            this.AssignScorePlayer!, this.WinnerGame!, this.ScoreToken!, this.Begin!, this.ReorganizeHands!);
    }
}