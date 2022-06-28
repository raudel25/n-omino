namespace Rules;

public class InfoRules<T>
{
    /// <summary>Determinar si es valido jugar una ficha por un nodo</summary>
    public IsValidRule<T> IsValidPlay { get; private set; }

    /// <summary>
    /// Determinar la visibilidad del juego de los jugadores
    /// </summary>
    public VisibilityPlayerRule<T> VisibilityPlayer { get; private set; }

    /// <summary>
    /// Determinar la forma de robar de los jugadores
    /// </summary>
    public StealTokenRule<T> StealTokens { get; private set; }

    /// <summary>
    /// Determinar si un jugador se puede pasar con fichas
    /// </summary>
    public ToPassTokenRule<T> ToPassToken { get; private set; }

    /// <summary>Determinar la rotacion de los jugadores</summary>
    public TurnPlayerRule<T> TurnPlayer { get; private set; }

    /// <summary>Asignar un score a cada jugador</summary>
    public AssignScorePlayerRule<T> AsignScorePlayer { get; private set; }

    /// <summary>Determinar el ganador del juego</summary>
    public WinnerGameRule<T> WinnerGame { get; private set; }

    /// <summary>
    /// Determinar como se inicia el juego
    /// </summary>
    public BeginGameRule<T> Begin { get; private set; }

    /// <summary>
    /// Determinar el Score de una ficha
    /// </summary>
    public IAssignScoreToken<T> ScoreToken { get; private set; }

    public InfoRules(IsValidRule<T> validPlay, VisibilityPlayerRule<T> visibility, TurnPlayerRule<T> turn,
        StealTokenRule<T> steal, ToPassTokenRule<T> toPass,
        AssignScorePlayerRule<T> assign, WinnerGameRule<T> winnerGame, IAssignScoreToken<T> scoreToken,
        BeginGameRule<T> begin)
    {
        this.IsValidPlay = validPlay;
        this.AsignScorePlayer = assign;
        this.TurnPlayer = turn;
        this.ToPassToken = toPass;
        this.WinnerGame = winnerGame;
        this.VisibilityPlayer = visibility;
        this.StealTokens = steal;
        this.ScoreToken = scoreToken;
        this.Begin = begin;
    }

    /// <summary>Clonar el objeto InfoRules</summary>
    /// <returns>Clon de InfoRules</returns>
    public InfoRules<T> Clone()
    {
        return new InfoRules<T>(this.IsValidPlay.Clone(), this.VisibilityPlayer.Clone(), this.TurnPlayer.Clone(),
            this.StealTokens.Clone(), this.ToPassToken.Clone(),
            this.AsignScorePlayer.Clone(), this.WinnerGame.Clone(), this.ScoreToken, this.Begin.Clone());
    }
}