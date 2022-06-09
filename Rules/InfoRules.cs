using Table;

namespace Rules;

public class InfoRules
{
    /// <summary>Determinar si es valido jugar una ficha por un nodo</summary>
    public List<IValidPlay> IsValidPlay { get; private set; }

    /// <summary>Asignar un score a cada jugador</summary>
    public AsignScorePlayerRule AsignScorePlayer { get; private set; }

    /// <summary>Determinar la rotacion de los jugadores</summary>
    public TurnPlayerRule TurnPlayer { get; private set; }

    /// <summary>Determinar si termino el juego</summary>
    public List<IEndGame> EndGame { get; private set; }

    /// <summary>Determinar el ganador del juego</summary>
    public List<IWinnerGame> WinnerGame { get; private set; }

    public VisibilityPlayerRule VisibilityPlayer { get; private set; }
    public StealTokenRule StealTokens { get; private set; }
    public IAsignScoreToken ScoreToken { get; }

    public InfoRules(VisibilityPlayerRule visibility, TurnPlayerRule turn, StealTokenRule steal,
        AsignScorePlayerRule asign)
    {
        this.IsValidPlay = new List<IValidPlay>();
        this.AsignScorePlayer = asign;
        this.TurnPlayer = turn;
        this.EndGame = new List<IEndGame>();
        this.WinnerGame = new List<IWinnerGame>();
        this.VisibilityPlayer = visibility;
        this.StealTokens = steal;
    }

    /// <summary>Clonar el objeto InfoRules</summary>
    /// <returns>Clon de InfoRules</returns>
    public InfoRules Clone()
    {
        InfoRules aux = new InfoRules(this.VisibilityPlayer.Clone(), this.TurnPlayer.Clone(), this.StealTokens.Clone(),
            this.AsignScorePlayer.Clone());
        aux.IsValidPlay = this.IsValidPlay.ToList();
        //aux.AsignScorePlayer = this.AsignScorePlayer.ToList();
        //aux.TurnPlayer = this.TurnPlayer.ToList();
        aux.WinnerGame = this.WinnerGame.ToList();
        aux.EndGame = this.EndGame.ToList();
        //aux.VisibilityPlayer = this.VisibilityPlayer.ToList();
        // aux.StealTokens = this.StealTokens.ToList();

        return aux;
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