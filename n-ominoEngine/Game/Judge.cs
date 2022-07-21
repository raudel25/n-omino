using InfoGame;
using Table;
using Rules;
using Player;

namespace Game;

public class Judge<T>
{
    /// <summary>
    /// Jugadores
    /// </summary>
    private List<Player<T>> _players;

    /// <summary>
    /// Reglas del juego
    /// </summary>
    private InfoRules<T> _judgeRules;

    /// <summary>
    /// Estado del juego
    /// </summary>
    private GameStatus<T> _infoGame;

    /// <summary>
    /// Printiador del juego
    /// </summary>
    private Printer _print;

    /// <summary>
    /// Estado del torneo
    /// </summary>
    private TournamentStatus _tournament;

    public Judge(TournamentStatus tournament, InfoRules<T> infoRules, GameStatus<T> infoGame, List<Player<T>> players,
        Printer print)
    {
        this._judgeRules = infoRules;
        this._infoGame = infoGame;
        this._players = players;
        this._print = print;
        this._tournament = tournament;
    }

    public void Game()
    {
        int i = StartGame();

        while (!EndGame())
        {
            if (i == this._infoGame.Turns.Length) i = 0;

            //Clonar el estado del juego
            GameStatus<T> copy = this._infoGame.Clone();
            InfoRules<T> rulesCopy = this._judgeRules.Clone();
            TournamentStatus tournamentCopy = this._tournament.Clone();

            bool play = PrePlay(copy, i);

            PlayPlayer(play, tournamentCopy, copy, rulesCopy, i);

            //Determinar si es posible pasarse con fichas
            //this._judgeRules.ToPassToken.RunRule(copy, this._infoGame, this._judgeRules, i);
            //bool toPass = !play || this._judgeRules.ToPassToken.PossibleToPass;
            //this._infoGame.InmediatePass = toPass;

            this._infoGame.NoValidPlay = NoValid();

            PostPlay(i);

            i++;
        }

        Printer.ExecuteMessageEvent(_infoGame.Players[_infoGame.FindPLayerById(this._infoGame.PlayerWinner)].Name +
                                    " ha ganado");
    }

    /// <summary>
    /// Printiar el estado del juego
    /// </summary>
    /// <param name="play">Token para jugar</param>
    /// <param name="ind">Indice del jugador respecto al turno</param>
    private void GuiJudge(Token<T>? play, int ind)
    {
        Printer.ExecuteMessageEvent("");
        this._print.LocationTable(_infoGame.Table);
        this._print.LocationHand(_infoGame.Players[ind], play, _infoGame.Table);
    }

    /// <summary>
    /// Determinar como se inicia el juego
    /// </summary>
    /// <returns>Indice relativo a la mesa</returns>
    private int StartGame()
    {
        this._judgeRules.ReorganizeHands.RunRule(this._tournament, this._infoGame, this._judgeRules.ScoreToken, -1);
        this._judgeRules.Begin.RunRule(this._tournament, this._infoGame, this._judgeRules.ScoreToken, -1);

        int id = this._infoGame.PlayerStart;

        int ind = this._infoGame.FindPLayerById(id);

        if (this._infoGame.TokenStart != null)
        {
            this._judgeRules.IsValidPlay.RunRule(this._tournament, this._infoGame, this._judgeRules.ScoreToken, ind);

            List<int> possible = this._judgeRules.IsValidPlay.ValidPlays(this._infoGame.Table.TableNode[0],
                this._infoGame.TokenStart, this._infoGame, ind);

            if (possible.Count != 0)
            {
                //Clonar el estado del juego
                GameStatus<T> copy = this._infoGame.Clone();

                PrePlay(copy, ind);

                PlayToken(possible[0], this._infoGame.Table.TableNode[0],
                    this._infoGame.TokenStart, ind, _players[ind].Id);

                PostPlay(ind);

                ind++;
            }
        }

        return ind;
    }

    /// <summary>
    /// Determinar la Move del jugador
    /// </summary>
    /// <param name="copy">Copia del juego</param>
    /// <param name="copyRules">Copia de las reglas</param>
    /// <param name="play">Si es posible jugar</param>
    /// <param name="tournamentCopy">Copia del estado del torneo</param>
    /// <param name="indTable">Inidice del jugador relativo a la mesa</param>
    private void PlayPlayer(bool play, TournamentStatus tournamentCopy, GameStatus<T> copy, InfoRules<T> copyRules,
        int indTable)
    {
        int ind = this._infoGame.Turns[indTable];

        if (play)
        {
            this._infoGame.ImmediatePass = false;

            Move<T> move = _players[ind].Play(tournamentCopy, copy, copyRules, indTable);

            INode<T> aux = this._infoGame.Table.TableNode[move.Node!.Id];

            //Determinar si el player juega correctamente
            if (_judgeRules.IsValidPlay[move.ValidPlay].Item2 &&
                _judgeRules.IsValidPlay[move.ValidPlay].Item1
                    .ValidPlay(aux, move.Token!, _infoGame, indTable) &&
                this._infoGame.Players[ind].Hand.Contains(move.Token!))
            {
                PlayToken(move.ValidPlay, aux, move.Token!, ind, _players[ind].Id);
                HistoryPlayer(move, ind);
            }
        }
        else
        {
            HistoryPlayer(new Move<T>(null, null, -1), ind);

            GuiJudge(null, ind);
        }
    }

    private void HistoryPlayer(Move<T> play, int ind)
    {
        this._infoGame.Players[ind].History.Add(play);
    }

    /// <summary>
    /// Jugar una ficha
    /// </summary>
    /// <param name="valid">Criterio por el cual se juega</param>
    /// <param name="node">Nodo por el cual se juega</param>
    /// <param name="token">Ficha a jugar</param>
    /// <param name="ind">Indice del jugador relativo al turno</param>
    /// <param name="id">Id del jugador</param>
    private void PlayToken(int valid, INode<T> node, Token<T> token, int ind, int id)
    {
        T[] aux = _judgeRules.IsValidPlay[valid].Item1
            .AssignValues(node, token, _infoGame.Table);

        _infoGame.Table.PlayTable(node, token, aux, id);
        _infoGame.Players[ind].Hand.Remove(token);

        GuiJudge(token, ind);
    }

    /// <summary>
    /// Determinar las reglas antes de ejecutar la Move
    /// </summary>
    /// <param name="copy">Estado conado del juego</param>
    /// <param name="indTable">Indice del jugador relativo a la mesa</param>
    /// <returns>Si es posible jugar</returns>
    private bool PrePlay(GameStatus<T> copy, int indTable)
    {
        InfoPlayer<T> player = this._infoGame.Players[this._infoGame.Turns[indTable]];

        //Determinar si es posible jugar
        this._judgeRules.IsValidPlay.RunRule(this._tournament, this._infoGame, this._judgeRules.ScoreToken, indTable);
        bool play = this._judgeRules.IsValidPlay.ValidPlayPlayer(player.Hand, _infoGame, indTable);

        this._infoGame.ImmediatePass = !play;

        //Determinar la visibilidad y posibilidades de robar
        this._judgeRules.VisibilityPlayer.RunRule(this._tournament, copy, this._infoGame, this._judgeRules.ScoreToken,
            indTable);
        this._judgeRules.StealTokens.RunRule(this._tournament, copy, this._infoGame, this._judgeRules.IsValidPlay,
            this._judgeRules.ScoreToken, indTable);

        //Determinar si es posible jugar con el criterio de robar
        play = play || this._judgeRules.StealTokens.Play;

        return play;
    }

    /// <summary>
    /// Determinar las reglas despues de ejecutar la Move
    /// </summary>
    /// <param name="indTable">Indice del jugador relativo a la mesa</param>
    private void PostPlay(int indTable)
    {
        //Determinar la distribucion de los turnos
        this._judgeRules.TurnPlayer.RunRule(this._tournament, this._infoGame, this._judgeRules.ScoreToken,
            indTable);

        //Asignar Score a los jugadores
        this._judgeRules.AssignScorePlayer.RunRule(this._tournament, this._infoGame, this._judgeRules.ScoreToken,
            indTable);

        //Determinar el ganador del juego
        this._judgeRules.WinnerGame.RunRule(this._tournament, this._infoGame, this._judgeRules.ScoreToken,
            indTable);
    }

    /// <summary>
    /// Determinar cuando finaliza el juego
    /// </summary>
    /// <returns>Fin del juego</returns>
    private bool EndGame()
    {
        return (this._infoGame.PlayerWinner != -1 || this._infoGame.TeamWinner != -1);
    }

    /// <summary>
    /// Determinar si algun jugador tiene una jugada valida
    /// </summary>
    /// <returns>Alguna jugada valida</returns>
    private bool NoValid()
    {
        for (int i = 0; i < this._infoGame.Turns.Length; i++)
        {
            if (this._judgeRules.IsValidPlay.ValidPlayPlayer(this._infoGame.Players[this._infoGame.Turns[i]].Hand,
                    this._infoGame, i)) return false;
        }

        return true;
    }
}