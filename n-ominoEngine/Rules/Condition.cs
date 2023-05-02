using InfoGame;
using Table;

namespace Rules;

public interface ICondition<T>
{
    /// <summary>
    ///     Determinar bajo que condiciones se ejecuta una regla
    /// </summary>
    /// <param name="tournament">Datos del torneo</param>
    /// <param name="game">Estado del juego</param>
    /// <param name="rules">Reglas del torneo</param>
    /// <param name="ind">Indice del jugador que le corresponde jugar</param>
    /// <returns>Si es valido que se ejecute la regla</returns>
    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules, int ind);
}

public class ClassicWin<T> : ICondition<T>
{
    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules, int ind)
    {
        if (game.Players.Count == 0) return false;
        return game.Players[game.Turns[ind]].Hand.Count == 0;
    }
}

public class ClassicTeamWin<T> : ICondition<T>
{
    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules, int ind)
    {
        var win = true;
        for (var i = 0; i < game.Teams.Count; i++)
        for (var j = 0; j < game.Teams[i].Count; j++)
            if (game.Teams[i][j].Hand.Count != 0)
                win = false;

        return win;
    }
}

public class CantToPass<T> : ICondition<T>
{
    public CantToPass(int cant)
    {
        Cant = cant;
    }

    public int Cant { get; }

    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules, int ind)
    {
        if (game.Players.Count == 0) return false;
        return game.Players[game.Turns[ind]].Passes == Cant;
    }
}

public class CantToPassTeam<T> : ICondition<T>
{
    public CantToPassTeam(int cant)
    {
        Cant = cant;
    }

    public int Cant { get; }

    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules, int ind)
    {
        var condition = true;
        for (var i = 0; i < game.Teams.Count; i++)
        for (var j = 0; j < game.Teams[i].Count; j++)
            if (game.Teams[i][j].Passes == Cant)
                condition = false;

        return condition;
    }
}

public class ImmediatePass<T> : ICondition<T>
{
    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules, int ind)
    {
        return game.ImmediatePass;
    }
}

public class NoValidPlayFirstPlayerPass<T> : ICondition<T>
{
    private int _firstPlayerPass;
    private bool _noValid;

    public NoValidPlayFirstPlayerPass()
    {
        _noValid = false;
        _firstPlayerPass = -1;
    }

    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules, int ind)
    {
        if (!game.ImmediatePass)
        {
            _noValid = false;
        }
        else
        {
            if (!_noValid)
            {
                _firstPlayerPass = game.Turns[ind];
            }
            else
            {
                if (_firstPlayerPass == game.Turns[ind]) return true;
            }

            _noValid = true;
        }

        return false;
    }
}

public class NoValidPlay<T> : ICondition<T>
{
    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules, int ind)
    {
        return game.NoValidPlay;
    }
}

public class SumFreeNode : ICondition<int>
{
    private readonly IComparison<int> _comparison;

    public SumFreeNode(int value, IComparison<int> comparison)
    {
        _comparison = comparison;
        Value = value;
    }

    public int Value { get; }

    public bool RunRule(TournamentStatus tournament, GameStatus<int> game, IAssignScoreToken<int> rules, int ind)
    {
        return _comparison.Compare(AuxTable.SumConnectionFree(game.Table), Value);
    }
}

public class ConditionDefault<T> : ICondition<T>
{
    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules, int ind)
    {
        return true;
    }
}

public class SecondRoundTournament<T> : ICondition<T>
{
    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules, int ind)
    {
        return tournament.Index > 0;
    }
}

public class HigherThanScoreHandCondition<T> : ICondition<T>
{
    public HigherThanScoreHandCondition(int n)
    {
        MinScore = n;
    }

    private int MinScore { get; }

    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules, int ind)
    {
        var sum = 0;
        var id = game.Turns[ind];

        foreach (var item in game.Players[id].Hand)
            sum += rules.ScoreToken(item);

        return sum > MinScore;
    }
}

public class PostRoundCondition<T> : ICondition<T>
{
    public PostRoundCondition(int n)
    {
        MinRound = n;
    }

    private int MinRound { get; }

    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules, int ind)
    {
        if (game.Turns.Length == 0) return false;
        var id = game.Turns[ind];
        return game.Players[id].History.Turns > MinRound;
    }
}

public class MaxScoreTeamTournament<T> : ICondition<T>
{
    private readonly int _socre;

    public MaxScoreTeamTournament(int score)
    {
        _socre = score;
    }

    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules, int ind)
    {
        for (var i = 0; i < tournament.ScoreTeams.Length; i++)
            if (tournament.ScoreTeams[i] >= _socre)
                return true;

        return false;
    }
}

public class CantGamesTournament<T> : ICondition<T>
{
    private readonly int _cant;

    public CantGamesTournament(int n)
    {
        _cant = n;
    }

    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules, int ind)
    {
        return tournament.Index == _cant - 1;
    }
}