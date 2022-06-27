using InfoGame;
using Player;
using Rules;

namespace Game;

public class JudgeTournament<T> where T : struct
{
    private InitializerGame<T>[] _games;
    private InfoRules<T>[] _rules;
    private Player<T>[][] _players;
    private Printer[] _print;
    private TournamentStatus _tournament;

    public JudgeTournament(InitializerGame<T>[] games, InfoRules<T>[] rules, Player<T>[][] players, Printer[] print)
    {
        this._games = games;
        this._rules = rules;
        this._players = players;
        this._print = print;
        this._tournament = new TournamentStatus();
    }

    public void TournamentGame()
    {
        for (int i = 0; i < this._games.Length; i++)
        {
            GameStatus<T> init = this._games[i].StartGame();

            Judge<T> judge = new Judge<T>(this._tournament, this._rules[i], init, this._players[i], this._print[i]);
            judge.Game();
        }
    }
}