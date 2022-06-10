using Table;

namespace Rules;

public class InfoRules
{
    /// <summary>Determinar si es valido jugar una ficha por un nodo</summary>
    public List<IValidPlay> IsValidPlay { get; private set; }

    /// <summary>
    /// Determinar la visibilidad del juego de los jugadores
    /// </summary>
    public VisibilityPlayerRule VisibilityPlayer { get; private set; }

    /// <summary>
    /// Determinar la forma de robar de los jugadores
    /// </summary>
    public StealTokenRule StealTokens { get; private set; }

    /// <summary>Determinar la rotacion de los jugadores</summary>
    public TurnPlayerRule TurnPlayer { get; private set; }

    /// <summary>Asignar un score a cada jugador</summary>
    public AsignScorePlayerRule AsignScorePlayer { get; private set; }

    /// <summary>Determinar el ganador del juego</summary>
    public WinnerGameRule WinnerGame { get; private set; }

    public IAsignScoreToken ScoreToken { get; private set; }

    public InfoRules(List<IValidPlay> validPlay, VisibilityPlayerRule visibility, TurnPlayerRule turn,
        StealTokenRule steal,
        AsignScorePlayerRule asign, WinnerGameRule winnerGame, IAsignScoreToken scoreToken)
    {
        this.IsValidPlay = validPlay;
        this.AsignScorePlayer = asign;
        this.TurnPlayer = turn;
        this.WinnerGame = winnerGame;
        this.VisibilityPlayer = visibility;
        this.StealTokens = steal;
        this.ScoreToken = scoreToken;
    }

    /// <summary>Clonar el objeto InfoRules</summary>
    /// <returns>Clon de InfoRules</returns>
    public InfoRules Clone()
    {
        return new InfoRules(this.IsValidPlay.ToList(), this.VisibilityPlayer.Clone(), this.TurnPlayer.Clone(),
            this.StealTokens.Clone(),
            this.AsignScorePlayer.Clone(), this.WinnerGame.Clone(), this.ScoreToken);
    }

    /// <summary>Determinar si una jugada es correcta segun las reglas existentes</summary>
    /// <param name="node">Nodo por el que se quiere jugar</param>
    /// <param name="token">Ficha para jugar</param>
    /// <param name="table">Mesa para jugar</param>
    /// <returns>Criterios de jugada valida correspomdientes a la ficha y el nodo</returns>
    public List<int> ValidPlays(INode node, Token token, TableGame table)
    {
        List<int> valid = new List<int>();
        for (int j = 0; j < this.IsValidPlay.Count; j++)
        {
            if (this.IsValidPlay[j].ValidPlay(node, token, table)) valid.Add(j);
        }

        return valid;
    }
}