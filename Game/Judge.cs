using InfoGame;
using Table;
using Rules;
using Player;

namespace Game;

public class Judge
{
    private InfoRules _judgeRules;
    private GameStatus _infoGame;

    public Judge(InfoRules infoRules, GameStatus infoGame)
    {
        this._judgeRules = infoRules;
        this._infoGame = infoGame;
        this.Game();
    }

    private void Game()
    {
        int i = 0;
        bool noValid = false;
        int lastPlayerPass = -1;
        while (true)
        {
            int ind = this._infoGame.Turns[i];
            InfoPlayer player = this._infoGame.Players[_infoGame.Turns[ind]];
            bool play = this.ValidPlayPlayer(player.Hand!, _infoGame.Table);

            //Clonar el estado del juego
            GameStatus copy = this._infoGame.Clone();

            //Determinar la visibilidad y posibilidades de robar
            this._judgeRules.VisibilityPlayer.RunRule(copy, this._infoGame, this._judgeRules, i);
            this._judgeRules.StealTokens.RunRule(copy, this._infoGame, this._judgeRules, i);

            //Determinar si es posible jugar
            play = play || this._judgeRules.StealTokens.Play;
            if (play)
            {
                noValid = false;
            }
            else
            {
                if (!noValid)
                {
                    lastPlayerPass = this._infoGame.Turns[i];
                }
                else
                {
                    if (lastPlayerPass == this._infoGame.Turns[i]) this._infoGame.NoValidPlay = true;
                }

                noValid = true;
            }

            this._infoGame.InmediatePass = !play;

            //Determinar la distribucion de los turnos
            this._judgeRules.TurnPlayer.RunRule(copy, this._infoGame, this._judgeRules, i);

            //Asignar Score a los jugadores
            this._judgeRules.AsignScorePlayer.RunRule(copy, this._infoGame, this._judgeRules, i);

            //Determinar el ganador del juego
            this._judgeRules.WinnerGame.RunRule(copy, this._infoGame, this._judgeRules, i);

            if (this._infoGame.PlayerWinner != -1 || this._infoGame.TeamWinner != -1) break;

            i++;
            if (i == this._infoGame.Turns.Length) i = 0;
        }
    }

    /// <summary>Determina si el jugador tiene opciones para jugar</summary>
    /// <param name="tokens">Fichas de las que dispone el jugador</param>
    /// <param name="table">Mesa para jugar</param>
    /// <returns>El jugador tiene opciones para jugar</returns>
    private bool ValidPlayPlayer(HashSet<Token> tokens, TableGame table)
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