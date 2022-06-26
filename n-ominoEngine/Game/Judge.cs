using InfoGame;
using Table;
using Rules;
using Player;

namespace Game;

public class Judge<T> where T : ICloneable<T> 
{
    private InfoRules<T> _judgeRules;
    private GameStatus<T> _infoGame;

    public Judge(InfoRules<T> infoRules, GameStatus<T> infoGame)
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
            InfoPlayer<T> player = this._infoGame.Players[_infoGame.Turns[ind]];

            //Clonar el estado del juego
            GameStatus<T> copy = this._infoGame.Clone();
            
            //Determinar si es posible jugar
            this._judgeRules.IsValidPlay.RunRule(copy, this._infoGame, this._judgeRules, i);
            bool play = this.ValidPlayPlayer(player.Hand!, _infoGame.Table);

            //Determinar la visibilidad y posibilidades de robar
            this._judgeRules.VisibilityPlayer.RunRule(copy, this._infoGame, this._judgeRules, i);
            this._judgeRules.StealTokens.RunRule(copy, this._infoGame, this._judgeRules, i);

            //Determinar si es posible jugar con el criterio de robar
            play = play || this._judgeRules.StealTokens.Play;

            if (play) noValid = false;
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

            //Determinar si es posible pasarse con fichas
            this._judgeRules.ToPassToken.RunRule(copy, this._infoGame, this._judgeRules, i);
            bool toPass = !play || this._judgeRules.ToPassToken.PossibleToPass;
            //this._infoGame.ImmediatePass = toPass;

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
    private bool ValidPlayPlayer(Hand<T> tokens, TableGame<T> table)
    {
        foreach (var item in table.FreeNode)
        {
            foreach (var token in tokens)
            {
                if (this._judgeRules.IsValidPlay.ValidPlays(item, token, table).Count != 0) return true;
            }
        }

        return false;
    }
}