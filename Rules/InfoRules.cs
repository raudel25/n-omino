namespace Rules;

public class InfoRules
{
    /// <summary>Determinar si es valido jugar una ficha por un nodo</summary>
    public IValidPlay ValidPlay { get; set; }
    /// <summary>Asignar un score a cada jugador</summary>
    public IAsignScorePlayer AsignScorePlayer { get; set; }
    /// <summary>Determinar la rotacion de los jugadores</summary>
    public ITurnPlayer TurnPlayer { get; set; }
    /// <summary>Determinar si termino el juego</summary>
    public IEndPlayer EndPlayer { get; set; }
    /// <summary>Determinar el ganador del juego</summary>
    public IWinnerGame WinnerGame { get; set; }
    public InfoRules(IValidPlay valid, IAsignScorePlayer score, ITurnPlayer turn, IEndPlayer end, IWinnerGame winner)
    {
        this.ValidPlay = valid;
        this.AsignScorePlayer = score;
        this.TurnPlayer = turn;
        this.EndPlayer = end;
        this.WinnerGame = winner;
    }
    /// <summary>Clonar el objeto InfoRules</summary>
    /// <returns>Clon de InfoRules</returns>
    public InfoRules Clone()
    {
        return new InfoRules(this.ValidPlay, this.AsignScorePlayer, this.TurnPlayer, this.EndPlayer, this.WinnerGame);
    }
}