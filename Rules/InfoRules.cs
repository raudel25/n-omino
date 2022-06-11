namespace Rules;

public class InfoRules
{
    /// <summary>Determinar si es valido jugar una ficha por un nodo</summary>
    public IsValidRule IsValidPlay { get; private set; }

    /// <summary>
    /// Determinar la visibilidad del juego de los jugadores
    /// </summary>
    public VisibilityPlayerRule VisibilityPlayer { get; private set; }

    /// <summary>
    /// Determinar la forma de robar de los jugadores
    /// </summary>
    public StealTokenRule StealTokens { get; private set; }

    /// <summary>
    /// Determinar si un jugador se puede pasar con fichas
    /// </summary>
    public ToPassTokenRule ToPassToken { get; private set; }

    /// <summary>Determinar la rotacion de los jugadores</summary>
    public TurnPlayerRule TurnPlayer { get; private set; }

    /// <summary>Asignar un score a cada jugador</summary>
    public AssignScorePlayerRule AsignScorePlayer { get; private set; }

    /// <summary>Determinar el ganador del juego</summary>
    public WinnerGameRule WinnerGame { get; private set; }

    /// <summary>
    /// Determinar el Score de una ficha
    /// </summary>
    public IAsignScoreToken ScoreToken { get; private set; }

    public InfoRules(IsValidRule validPlay, VisibilityPlayerRule visibility, TurnPlayerRule turn,
        StealTokenRule steal, ToPassTokenRule toPass,
        AssignScorePlayerRule assign, WinnerGameRule winnerGame, IAsignScoreToken scoreToken)
    {
        this.IsValidPlay = validPlay;
        this.AsignScorePlayer = assign;
        this.TurnPlayer = turn;
        this.ToPassToken = toPass;
        this.WinnerGame = winnerGame;
        this.VisibilityPlayer = visibility;
        this.StealTokens = steal;
        this.ScoreToken = scoreToken;
    }

    /// <summary>Clonar el objeto InfoRules</summary>
    /// <returns>Clon de InfoRules</returns>
    public InfoRules Clone()
    {
        return new InfoRules(this.IsValidPlay.Clone(), this.VisibilityPlayer.Clone(), this.TurnPlayer.Clone(),
            this.StealTokens.Clone(), this.ToPassToken.Clone(),
            this.AsignScorePlayer.Clone(), this.WinnerGame.Clone(), this.ScoreToken);
    }
}