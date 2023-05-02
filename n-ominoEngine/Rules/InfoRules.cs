using Table;

namespace Rules;

public class InfoRules<T> : ICloneable<InfoRules<T>>
{
    public InfoRules(IsValidRule<T> validPlay, VisibilityPlayerRule<T> visibility, TurnPlayerRule<T> turn,
        StealTokenRule<T> steal, ToPassTokenRule<T> toPass,
        AssignScorePlayerRule<T> assign, WinnerGameRule<T> winnerGame, IAssignScoreToken<T> scoreToken,
        BeginGameRule<T> begin, ReorganizeHandsRule<T> reorganize)
    {
        IsValidPlay = validPlay;
        AssignScorePlayer = assign;
        TurnPlayer = turn;
        ToPassToken = toPass;
        WinnerGame = winnerGame;
        VisibilityPlayer = visibility;
        StealTokens = steal;
        ScoreToken = scoreToken;
        Begin = begin;
        ReorganizeHands = reorganize;
    }

    /// <summary>Determinar si es valido jugar una ficha por un nodo</summary>
    public IsValidRule<T> IsValidPlay { get; }

    /// <summary>
    ///     Determinar la visibilidad del juego de los jugadores
    /// </summary>
    public VisibilityPlayerRule<T> VisibilityPlayer { get; }

    /// <summary>
    ///     Determinar la forma de robar de los jugadores
    /// </summary>
    public StealTokenRule<T> StealTokens { get; }

    /// <summary>
    ///     Determinar si un jugador se puede pasar con fichas
    /// </summary>
    public ToPassTokenRule<T> ToPassToken { get; }

    /// <summary>Determinar la rotacion de los jugadores</summary>
    public TurnPlayerRule<T> TurnPlayer { get; }

    /// <summary>Asignar un score a cada jugador</summary>
    public AssignScorePlayerRule<T> AssignScorePlayer { get; }

    /// <summary>Determinar el ganador del juego</summary>
    public WinnerGameRule<T> WinnerGame { get; }

    /// <summary>
    ///     Determinar como se inicia el juego
    /// </summary>
    public BeginGameRule<T> Begin { get; }

    /// <summary>
    ///     Determinar el Score de una ficha
    /// </summary>
    public IAssignScoreToken<T> ScoreToken { get; }

    /// <summary>
    ///     Reorganizar las manos de los jugadores
    /// </summary>
    public ReorganizeHandsRule<T> ReorganizeHands { get; }

    /// <summary>Clonar el objeto InfoRules</summary>
    /// <returns>Clon de InfoRules</returns>
    public InfoRules<T> Clone()
    {
        return new InfoRules<T>(IsValidPlay.Clone(), VisibilityPlayer.Clone(), TurnPlayer.Clone(),
            StealTokens.Clone(), ToPassToken.Clone(),
            AssignScorePlayer.Clone(), WinnerGame.Clone(), ScoreToken, Begin.Clone(),
            ReorganizeHands.Clone());
    }
}