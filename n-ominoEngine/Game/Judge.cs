using InfoGame;
using Table;
using Rules;
using Player;
using InteractionGui;

namespace Game;

public class Judge<T>
{
    /// <summary>
    /// Jugadores
    /// </summary>
    private Player<T>[] _players;
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
        Printer.ExecuteWinnerEvent("");

        int i = StartGame();

        while (!EndGame())
        {
            if (i == this._infoGame.Turns.Length) i = 0;
            
            bool play = PrePlay(i);

           // DeterminateNoValidPlay(play, ref noValid, ref lastPlayerPass, i);

            PlayPlayer(play, this._infoGame.Turns[i]);

            //Determinar si es posible pasarse con fichas
            //this._judgeRules.ToPassToken.RunRule(copy, this._infoGame, this._judgeRules, i);
            //bool toPass = !play || this._judgeRules.ToPassToken.PossibleToPass;
            //this._infoGame.InmediatePass = toPass;

            PostPlay(i);

            i++;
        }
        
        Printer.ExecuteWinnerEvent("El jugador "+this._infoGame.PlayerWinner+" ha ganado");
    }
    
    /// <summary>
    /// Printiar el estado del juego
    /// </summary>
    /// <param name="play">Token para jugar</param>
    /// <param name="ind">Indice del jugador respecto al turno</param>
    private void GuiJudge(Token<T>? play, int ind)
    {
        this._print.LocationTable(_infoGame.Table);
        this._print.LocationHand(_infoGame.Players[ind], play, _infoGame.Table);
    }

    /// <summary>
    /// Determinar como se inicia el juego
    /// </summary>
    /// <returns>Indice relativo a la mesa</returns>
    private int StartGame()
    {
        this._judgeRules.Begin.RunRule(this._tournament, this._infoGame, this._infoGame, this._judgeRules, -1);

        int id = this._infoGame.PlayerStart;

        int ind = this._infoGame.FindPLayerById(id);

        if (this._infoGame.TokenStart != null)
        {
            PrePlay(ind);
            
            PlayToken(this._judgeRules.IsValidPlay.CantValid - 1, this._infoGame.Table.TableNode[0],
                this._infoGame.TokenStart, ind);
            
            PostPlay(ind);

            ind++;
        }

        return ind;
    }

    /// <summary>
    /// Determinar la Move del jugador
    /// </summary>
    /// <param name="play">Si es posible jugar</param>
    /// <param name="ind">Inidice del jugador relativo a la mesa</param>
    private void PlayPlayer(bool play, int ind)
    {
        if (play)
        {
            this._infoGame.ImmediatePass = false;
            Move<T> Move = _players[ind].Play(_infoGame, _judgeRules);
            if (_judgeRules.IsValidPlay[Move.ValidPlay].Item2 &&
                _judgeRules.IsValidPlay[Move.ValidPlay].Item1
                    .ValidPlay(Move.Node!, Move.Token!, _infoGame.Table))
            {
                PlayToken(Move.ValidPlay, Move.Node!, Move.Token!, ind);
                Console.WriteLine($"jugador {ind} jugó {Move.Token}");
                HistoryPlayer(Move,ind);
            }
        }
        else
        {
            HistoryPlayer(new Move<T>(null,null,-1),ind);
            GuiJudge(null, ind);
        }
    }

    private void HistoryPlayer(Move<T> play,int ind)
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
    private void PlayToken(int valid, INode<T> node, Token<T> token, int ind)
    {
        T[] aux = _judgeRules.IsValidPlay[valid].Item1
            .AssignValues(node, token, _infoGame.Table);

        _infoGame.Table.PlayTable(node, token, aux);
        _infoGame.Players[ind].Hand!.Remove(token);

        GuiJudge(token, ind);
    }
    
    /// <summary>
    /// Determinar las reglas antes de ejecutar la Move
    /// </summary>
    /// <param name="indTable">Indice del jugador relativo a la mesa</param>
    /// <returns>Si es posible jugar</returns>
    private bool PrePlay(int indTable)
    {
        //Clonar el estado del juego
        GameStatus<T> copy = this._infoGame.Clone();
        
        InfoPlayer<T> player = this._infoGame.Players[this._infoGame.Turns[indTable]];

        //Determinar si es posible jugar
        this._judgeRules.IsValidPlay.RunRule(this._tournament, copy, this._infoGame, this._judgeRules, indTable);
        bool play = this._judgeRules.IsValidPlay.ValidPlayPlayer(player.Hand!, _infoGame.Table);

        this._infoGame.ImmediatePass = !play;

        //Determinar la visibilidad y posibilidades de robar
        this._judgeRules.VisibilityPlayer.RunRule(this._tournament, copy, this._infoGame, this._judgeRules, indTable);
        this._judgeRules.StealTokens.RunRule(this._tournament, copy, this._infoGame, this._judgeRules, indTable);

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
        this._judgeRules.TurnPlayer.RunRule(this._tournament, this._infoGame, this._infoGame, this._judgeRules,
            indTable);

        //Asignar Score a los jugadores
        this._judgeRules.AssignScorePlayer.RunRule(this._tournament, this._infoGame, this._infoGame, this._judgeRules,
            indTable);

        //Determinar el ganador del juego
        this._judgeRules.WinnerGame.RunRule(this._tournament, this._infoGame, this._infoGame, this._judgeRules,
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
}