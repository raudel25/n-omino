using InfoGame;
using Table;
using Rules;
using Player;

namespace Game;

public class Judge
{
    private InfoRules _judgeRules;
    private GameStatus _infoGame;
    private Player.Player[] _players;
    public Judge(InfoRules infoRules,GameStatus infoGame)
    {
        this._judgeRules = infoRules;
        this._infoGame = infoGame;
    }
    private void Game()
    {
        int i = 0;
        while (true)
        {
            int ind = this._infoGame.Turns[i];
            InfoPlayer player = this._infoGame.Players[ind];
            if (this.ValidPlayPlayer(player.Hand!,_infoGame.Table))
            {
                GameStatus aux = _infoGame.Clone();
                //this._judgeRules.VisibilityPlayer!.Visibility(aux, ind);
                
            }
            i++;
            if (i == this._infoGame.Turns.Length) i = 0;
        }
    }
    /// <summary>Determina si el jugador tiene opciones para jugar</summary>
    /// <param name="tokens">Fichas de las que dispone el jugador</param>
    /// <param name="table">Mesa para jugar</param>
    /// <returns>El jugador tiene opciones para jugar</returns>
    private bool ValidPlayPlayer(IEnumerable<Token> tokens, TableGame table)
    {
        foreach (var item in table.FreeNode)
        {
            foreach (var token in tokens)
            {
                if (this._judgeRules.ValidPlays(item, token, table).Count != 0) return true;
            }
        }
        return false;
    }
}