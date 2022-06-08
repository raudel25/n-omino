using Table;

namespace Rules;

public class InfoRules
{
    /// <summary>Determinar si es valido jugar una ficha por un nodo</summary>
    public List<IValidPlay> IsValidPlay { get; private set; }

    /// <summary>Asignar un score a cada jugador</summary>
    public List<IAsignScorePlayer> AsignScorePlayer { get; private set; }

    /// <summary>Determinar la rotacion de los jugadores</summary>
    public List<ITurnPlayer> TurnPlayer { get; private set; }

    /// <summary>Determinar si termino el juego</summary>
    public List<IEndGame> EndPlayer { get; private set; }

    /// <summary>Determinar el ganador del juego</summary>
    public List<IWinnerGame> WinnerGame { get; private set; }

    public List<IVisibilityPlayer> VisibilityPlayer { get; private set; }
    public List<IStealToken> StealTokens { get; private set; }

    public InfoRules()
    {
        this.IsValidPlay = new List<IValidPlay>();
        this.AsignScorePlayer = new List<IAsignScorePlayer>();
        this.TurnPlayer = new List<ITurnPlayer>();
        this.EndPlayer = new List<IEndGame>();
        this.WinnerGame = new List<IWinnerGame>();
        this.VisibilityPlayer = new List<IVisibilityPlayer>();
        this.StealTokens = new List<IStealToken>();
    }

    /// <summary>Clonar el objeto InfoRules</summary>
    /// <returns>Clon de InfoRules</returns>
    public InfoRules Clone()
    {
        InfoRules aux = new InfoRules();
        aux.IsValidPlay = this.IsValidPlay.ToList();
        aux.AsignScorePlayer = this.AsignScorePlayer.ToList();
        aux.TurnPlayer = this.TurnPlayer.ToList();
        aux.WinnerGame = this.WinnerGame.ToList();
        aux.EndPlayer = this.EndPlayer.ToList();
        aux.VisibilityPlayer = this.VisibilityPlayer.ToList();
        aux.StealTokens = this.StealTokens.ToList();

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