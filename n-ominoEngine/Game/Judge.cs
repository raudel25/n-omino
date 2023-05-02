using InfoGame;
using Player;
using Rules;
using Table;

namespace Game;

public class Judge<T>
{
    /// <summary>
    ///     Estado del juego
    /// </summary>
    private readonly GameStatus<T> _infoGame;

    /// <summary>
    ///     Reglas del juego
    /// </summary>
    private readonly InfoRules<T> _judgeRules;

    /// <summary>
    ///     Jugadores
    /// </summary>
    private readonly List<Player<T>> _players;

    /// <summary>
    ///     Printiador del juego
    /// </summary>
    private readonly Printer _print;

    /// <summary>
    ///     Estado del torneo
    /// </summary>
    private readonly TournamentStatus _tournament;

    public Judge(TournamentStatus tournament, InfoRules<T> infoRules, GameStatus<T> infoGame, List<Player<T>> players,
        Printer print)
    {
        _judgeRules = infoRules;
        _infoGame = infoGame;
        _players = players;
        _print = print;
        _tournament = tournament;
    }

    public void Game()
    {
        var i = StartGame();

        while (!EndGame())
        {
            if (i == _infoGame.Turns.Length) i = 0;
            _infoGame.LastIndex = i;

            //Clonar el estado del juego
            var copy = _infoGame.Clone();
            var rulesCopy = _judgeRules.Clone();
            var tournamentCopy = _tournament.Clone();

            var play = PrePlay(copy, i);

            PlayPlayer(play, tournamentCopy, copy, rulesCopy, i);

            //Determinar si es posible pasarse con fichas
            //this._judgeRules.ToPassToken.RunRule(copy, this._infoGame, this._judgeRules, i);
            //bool toPass = !play || this._judgeRules.ToPassToken.PossibleToPass;
            //this._infoGame.InmediatePass = toPass;

            _infoGame.NoValidPlay = NoValid();

            PostPlay(i);

            i++;
        }

        Printer.ExecuteMessageEvent(_infoGame.Players[_infoGame.FindPLayerById(_infoGame.PlayerWinner)].Name +
                                    " ha ganado");
    }

    /// <summary>
    ///     Printiar el estado del juego
    /// </summary>
    /// <param name="play">Token para jugar</param>
    /// <param name="ind">Indice del jugador respecto al turno</param>
    private void GuiJudge(Token<T>? play, int ind)
    {
        Printer.ExecuteMessageEvent("");
        _print.LocationTable(_infoGame.Table);
        _print.LocationHand(_infoGame.Players[ind], play, _infoGame.Table);
    }

    /// <summary>
    ///     Determinar como se inicia el juego
    /// </summary>
    /// <returns>Indice relativo a la mesa</returns>
    private int StartGame()
    {
        _judgeRules.ReorganizeHands.RunRule(_tournament, _infoGame, _judgeRules.ScoreToken, 0);
        _judgeRules.Begin.RunRule(_tournament, _infoGame, _judgeRules.ScoreToken, 0);

        var id = _infoGame.PlayerStart;

        var ind = _infoGame.FindPLayerById(id);

        if (_infoGame.TokenStart != null)
        {
            _judgeRules.IsValidPlay.RunRule(_tournament, _infoGame, _judgeRules.ScoreToken, ind);

            var possible = _judgeRules.IsValidPlay.ValidPlays(_infoGame.Table.TableNode[0],
                _infoGame.TokenStart, _infoGame, ind);

            if (possible.Count != 0)
            {
                //Clonar el estado del juego
                var copy = _infoGame.Clone();

                PrePlay(copy, ind);

                PlayToken(possible[0], _infoGame.Table.TableNode[0],
                    _infoGame.TokenStart, ind, _players[ind].Id);

                PostPlay(ind);

                ind++;

                if (ind == _infoGame.Turns.Length) ind = 0;
            }
        }

        return ind;
    }

    /// <summary>
    ///     Determinar la Move del jugador
    /// </summary>
    /// <param name="copy">Copia del juego</param>
    /// <param name="copyRules">Copia de las reglas</param>
    /// <param name="play">Si es posible jugar</param>
    /// <param name="tournamentCopy">Copia del estado del torneo</param>
    /// <param name="indTable">Inidice del jugador relativo a la mesa</param>
    private void PlayPlayer(bool play, TournamentStatus tournamentCopy, GameStatus<T> copy, InfoRules<T> copyRules,
        int indTable)
    {
        var ind = _infoGame.Turns[indTable];

        if (play)
        {
            _infoGame.ImmediatePass = false;

            var move = _players[ind].Play(tournamentCopy, copy, copyRules, indTable);

            var aux = _infoGame.Table.TableNode[move.Node!.Id];

            //Determinar si el player juega correctamente
            if (_judgeRules.IsValidPlay[move.ValidPlay].Item2 &&
                _judgeRules.IsValidPlay[move.ValidPlay].Item1
                    .ValidPlay(aux, move.Token!, _infoGame, indTable) &&
                _infoGame.Players[ind].Hand.Contains(move.Token!))
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
        _infoGame.Players[ind].History.Add(play);
    }

    /// <summary>
    ///     Jugar una ficha
    /// </summary>
    /// <param name="valid">Criterio por el cual se juega</param>
    /// <param name="node">Nodo por el cual se juega</param>
    /// <param name="token">Ficha a jugar</param>
    /// <param name="ind">Indice del jugador relativo al turno</param>
    /// <param name="id">Id del jugador</param>
    private void PlayToken(int valid, INode<T> node, Token<T> token, int ind, int id)
    {
        var aux = _judgeRules.IsValidPlay[valid].Item1
            .AssignValues(node, token, _infoGame.Table);

        _infoGame.Table.PlayTable(node, token, aux, id);
        _infoGame.Players[ind].Hand.Remove(token);

        GuiJudge(token, ind);
    }

    /// <summary>
    ///     Determinar las reglas antes de ejecutar la Move
    /// </summary>
    /// <param name="copy">Estado conado del juego</param>
    /// <param name="indTable">Indice del jugador relativo a la mesa</param>
    /// <returns>Si es posible jugar</returns>
    private bool PrePlay(GameStatus<T> copy, int indTable)
    {
        var player = _infoGame.Players[_infoGame.Turns[indTable]];

        //Determinar si es posible jugar
        _judgeRules.IsValidPlay.RunRule(_tournament, _infoGame, _judgeRules.ScoreToken, indTable);
        var play = _judgeRules.IsValidPlay.ValidPlayPlayer(player.Hand, _infoGame, indTable);

        _infoGame.ImmediatePass = !play;

        //Determinar la visibilidad y posibilidades de robar
        _judgeRules.VisibilityPlayer.RunRule(_tournament, copy, _infoGame, _judgeRules.ScoreToken,
            indTable);
        _judgeRules.StealTokens.RunRule(_tournament, copy, _infoGame, _judgeRules.IsValidPlay,
            _judgeRules.ScoreToken, indTable);

        //Determinar si es posible jugar con el criterio de robar
        play = play || _judgeRules.StealTokens.Play;

        return play;
    }

    /// <summary>
    ///     Determinar las reglas despues de ejecutar la Move
    /// </summary>
    /// <param name="indTable">Indice del jugador relativo a la mesa</param>
    private void PostPlay(int indTable)
    {
        //Determinar la distribucion de los turnos
        _judgeRules.TurnPlayer.RunRule(_tournament, _infoGame, _judgeRules.ScoreToken,
            indTable);

        //Asignar Score a los jugadores
        _judgeRules.AssignScorePlayer.RunRule(_tournament, _infoGame, _judgeRules.ScoreToken,
            indTable);

        //Determinar el ganador del juego
        _judgeRules.WinnerGame.RunRule(_tournament, _infoGame, _judgeRules.ScoreToken,
            indTable);
    }

    /// <summary>
    ///     Determinar cuando finaliza el juego
    /// </summary>
    /// <returns>Fin del juego</returns>
    private bool EndGame()
    {
        return _infoGame.PlayerWinner != -1 && _infoGame.TeamWinner != -1;
    }

    /// <summary>
    ///     Determinar si algun jugador tiene una jugada valida
    /// </summary>
    /// <returns>Alguna jugada valida</returns>
    private bool NoValid()
    {
        for (var i = 0; i < _infoGame.Turns.Length; i++)
            if (_judgeRules.IsValidPlay.ValidPlayPlayer(_infoGame.Players[_infoGame.Turns[i]].Hand,
                    _infoGame, i))
                return false;

        return true;
    }
}