using InfoGame;
using Table;
using Rules;
using Player;

namespace Game;

public class Judge<T> 
{
    private Player<T>[] _players;
    private InfoRules<T> _judgeRules;
    private GameStatus<T> _infoGame;
    private Printer _print;
    private TournamentStatus _tournament;

    public Judge(TournamentStatus tournament, InfoRules<T> infoRules, GameStatus<T> infoGame, Player<T>[] players,
        Printer print)
    {
        this._judgeRules = infoRules;
        this._infoGame = infoGame;
        this._players = players;
        this._print = print;
        this._tournament = tournament;
        //this.Game();
    }

    public void Game()
    {
        int i = StartGame();
        bool noValid = false;
        int lastPlayerPass = -1;

        while (!EndGame())
        {
            if (i == this._infoGame.Turns.Length) i = 0;

            int ind = this._infoGame.Turns[i];
            InfoPlayer<T> player = this._infoGame.Players[ind];

            //Clonar el estado del juego
            GameStatus<T> copy = this._infoGame.Clone();

            bool play = PrePlay(copy, player, i);

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
                this._infoGame.ImmediatePass = false;
                Jugada<T> jugada = _players[ind].Play(_infoGame, _judgeRules);
                if (_judgeRules.IsValidPlay[jugada.ValidPlay].Item2 &&
                    _judgeRules.IsValidPlay[jugada.ValidPlay].Item1
                        .ValidPlay(jugada.Node, jugada.Token, _infoGame.Table))
                {
                    PlayToken(jugada.ValidPlay, jugada.Node, jugada.Token, ind);
                    Console.WriteLine("Jugador " + i + " jugo");
                    Console.WriteLine((jugada.Token[0], jugada.Token[1]));
                }
            }
            else
            {
                this._infoGame.ImmediatePass = true;
                GuiJudge(null, ind);
            }

            //Determinar si es posible pasarse con fichas
            //this._judgeRules.ToPassToken.RunRule(copy, this._infoGame, this._judgeRules, i);
            //bool toPass = !play || this._judgeRules.ToPassToken.PossibleToPass;
            //this._infoGame.InmediatePass = toPass;

            PostPlay(copy, i);

            i++;
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
        this._print.LocationTable(_infoGame.Table);
        this._print.LocationHand(_infoGame.Players[ind].Hand!, play, _infoGame.Table, _infoGame.Players[ind].Id + "");
    }

    private int StartGame()
    {
        this._judgeRules.Begin.RunRule(this._tournament, this._infoGame, this._infoGame, this._judgeRules, -1);

        int ind = this._infoGame.PlayerStart;

        if (this._infoGame.TokenStart != null)
        {
            PlayToken(this._judgeRules.IsValidPlay.CantValid - 1, this._infoGame.Table.TableNode[0],
                this._infoGame.TokenStart, this._infoGame.PlayerStart);

            PostPlay(this._infoGame, ind);

            ind++;
        }

        return ind;
    }

    private void PlayToken(int valid, INode<T> node, Token<T> token, int ind)
    {
        T[] aux = _judgeRules.IsValidPlay[valid].Item1
            .AssignValues(node, token, _infoGame.Table);

        _infoGame.Table.PlayTable(node, token, aux);
        _infoGame.Players[ind].Hand!.Remove(token);

        GuiJudge(token, ind);
    }

    private bool PrePlay(GameStatus<T> copy, InfoPlayer<T> player, int ind)
    {
        //Determinar si es posible jugar
        this._judgeRules.IsValidPlay.RunRule(this._tournament, copy, this._infoGame, this._judgeRules, ind);
        bool play = this.ValidPlayPlayer(player.Hand!, _infoGame.Table);

        //Determinar la visibilidad y posibilidades de robar
        this._judgeRules.VisibilityPlayer.RunRule(this._tournament, copy, this._infoGame, this._judgeRules, ind);
        this._judgeRules.StealTokens.RunRule(this._tournament, copy, this._infoGame, this._judgeRules, ind);

        //Determinar si es posible jugar con el criterio de robar
        play = play || this._judgeRules.StealTokens.Play;

        return play;
    }

    private void PostPlay(GameStatus<T> copy, int ind)
    {
        //Determinar la distribucion de los turnos
        this._judgeRules.TurnPlayer.RunRule(this._tournament, copy, this._infoGame, this._judgeRules, ind);

        //Asignar Score a los jugadores
        this._judgeRules.AssignScorePlayer.RunRule(this._tournament, copy, this._infoGame, this._judgeRules, ind);

        //Determinar el ganador del juego
        this._judgeRules.WinnerGame.RunRule(this._tournament, copy, this._infoGame, this._judgeRules, ind);
    }

    private bool EndGame()
    {
        return (this._infoGame.PlayerWinner != -1 || this._infoGame.TeamWinner != -1);
    }
}