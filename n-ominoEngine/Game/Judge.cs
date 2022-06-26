using InfoGame;
using Table;
using Rules;
using Player;

namespace Game;

public class Judge<T> where T : struct
{
    private Player<T>[] _players;
    private InfoRules<T> _judgeRules;
    private GameStatus<T> _infoGame;
    public Token<T> t;
    public PrinterGeometry g = new PrinterGeometry();
    Printer q12 = new PrinterDomino();

    public Judge(InfoRules<T> infoRules, GameStatus<T> infoGame, Player<T>[] players)
    {
        this._judgeRules = infoRules;
        this._infoGame = infoGame;
        this._players = players;
        //this.Game();
    }

    public void Game()
    {
        IBeginGame<T> a = new BeginGameToken<T>(t);
        a.Start(new TournamentStatus(), _infoGame, _judgeRules);

        int i = _infoGame.PlayerStart;
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
            Console.WriteLine(play);

            if (play)
            {
                this._infoGame.InmediatePass = false;
                Jugada<T> jugada = _players[ind].Play(_infoGame, _judgeRules);
                if (_judgeRules.IsValidPlay[jugada.ValidPlay].Item2 &&
                    _judgeRules.IsValidPlay[jugada.ValidPlay].Item1
                        .ValidPlay(jugada.Node, jugada.Token, _infoGame.Table))
                {
                    T[] aux = _judgeRules.IsValidPlay[jugada.ValidPlay].Item1
                        .AssignValues(jugada.Node, jugada.Token, _infoGame.Table);
                    _infoGame.Table.PlayTable(jugada.Node, jugada.Token, aux);
                    _infoGame.Players[ind].Hand!.Remove(jugada.Token);
                    Console.WriteLine("Jugador " + i + " jugo");
                    Console.WriteLine((jugada.Token[0], jugada.Token[1]));
                }

                GuiJudge(jugada.Token, ind);
            }
            else
            {
                this._infoGame.InmediatePass = true;
                GuiJudge(null, ind);
            }

            //Determinar si es posible pasarse con fichas
            //this._judgeRules.ToPassToken.RunRule(copy, this._infoGame, this._judgeRules, i);
            //bool toPass = !play || this._judgeRules.ToPassToken.PossibleToPass;
            //this._infoGame.InmediatePass = toPass;

            //Determinar la distribucion de los turnos
            this._judgeRules.TurnPlayer.RunRule(copy, this._infoGame, this._judgeRules, i);

            //Asignar Score a los jugadores
            this._judgeRules.AsignScorePlayer.RunRule(copy, this._infoGame, this._judgeRules, i);

            //Determinar el ganador del juego
            this._judgeRules.WinnerGame.RunRule(copy, this._infoGame, this._judgeRules, i);

            if (this._infoGame.PlayerWinner != -1 || this._infoGame.TeamWinner != -1) break;
            //Console.WriteLine(this._infoGame.Players[ind].Score);

            i++;
            if (i == this._infoGame.Turns.Length) i = 0;
        }
        Console.WriteLine("Termino");
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

    private void GuiJudge(Token<T>? play, int ind)
    {
        Thread.Sleep(1000);
        g.LocationTable(_infoGame.Table);
        g.LocationHand(_infoGame.Players[ind].Hand!, play, _infoGame.Table, _infoGame.Players[ind].Id + "");
        // q12.LocationTable(_infoGame.Table);
        // q12.LocationHand(_infoGame.Players[ind].Hand!, play, _infoGame.Table, _infoGame.Players[ind].Id + "");
        Thread.Sleep(1000);

    }
}