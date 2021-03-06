using InfoGame;
using Table;

namespace Rules;

public interface ICondition<T>
{
    /// <summary>
    /// Determinar bajo que condiciones se ejecuta una regla
    /// </summary>
    /// <param name="tournament">Datos del torneo</param>
    /// <param name="game">Estado del juego</param>
    /// <param name="rules">Reglas del torneo</param>
    /// <param name="ind">Indice del jugador que le corresponde jugar</param>
    /// <returns>Si es valido que se ejecute la regla</returns>
    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind);
}

public class ClassicWin<T> : ICondition<T>
{
    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind)
    {
        return game.Players[game.Turns[ind]].Hand.Count == 0;
    }
}

public class ClassicTeamWin<T> : ICondition<T>
{
    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind)
    {
        bool win = true;
        for (int i = 0; i < game.Teams.Count; i++)
        {
            for (int j = 0; j < game.Teams[i].Count; j++)
            {
                if (game.Teams[game.Turns[i]][j].Hand.Count != 0) win = false;
            }
        }

        return win;
    }
}

public class CantToPass<T> : ICondition<T>
{
    public int Cant { get; private set; }

    public CantToPass(int cant)
    {
        this.Cant = cant;
    }

    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind)
    {
        return game.Players[game.Turns[ind]].Passes == this.Cant;
    }
}

public class CantToPassTeam<T> : ICondition<T>
{
    public int Cant { get; private set; }

    public CantToPassTeam(int cant)
    {
        this.Cant = cant;
    }

    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind)
    {
        bool condition = true;
        for (int i = 0; i < game.Teams.Count; i++)
        {
            for (int j = 0; j < game.Teams[i].Count; j++)
            {
                if (game.Teams[i][j].Passes == this.Cant) condition = false;
            }
        }

        return condition;
    }
}

public class ImmediatePass<T> : ICondition<T>
{
    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind)
    {
        return game.ImmediatePass;
    }
}

public class NoValidPlayFirstPlayerPass<T> : ICondition<T>
{
    private bool _noValid;
    private int _firstPlayerPass;

    public NoValidPlayFirstPlayerPass()
    {
        this._noValid = false;
        this._firstPlayerPass = -1;
    }

    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind)
    {
        if (!game.ImmediatePass) _noValid = false;
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
    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind)
    {
        for (int i = 0; i < game.Turns.Length; i++)
        {
            if (rules.IsValidPlay.ValidPlayPlayer(game.Players[game.Turns[i]].Hand, game, i)) return false;
        }

        return true;
    }
}

public class SumFreeNode : ICondition<int>
{
    public int Value { get; private set; }
    private IComparison<int> _comparison;

    public SumFreeNode(int value, IComparison<int> comparison)
    {
        this._comparison = comparison;
        this.Value = value;
    }

    public bool RunRule(TournamentStatus tournament, GameStatus<int> game, InfoRules<int> rules, int ind)
    {
        return this._comparison.Compare(AuxTable.SumConnectionFree(game.Table), this.Value);
    }
}

public class ConditionDefault<T> : ICondition<T>
{
    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind)
    {
        return true;
    }
}

public class SecondRoundTournament<T> : ICondition<T>
{
    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind)
    {
        return tournament.Index > 0;
    }
}

public class HigherThanScoreHandCondition<T> : ICondition<T>
{
    int MinScore { get; }

    public HigherThanScoreHandCondition(int n)
    {
        MinScore = n;
    }

    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind)
    {
        int sum = 0;
        int id = game.Turns[ind];
        
        foreach (var item in game.Players[id].Hand)
            sum += rules.ScoreToken.ScoreToken(item);

        return sum > MinScore;
    }
}

public class PostRoundCondition<T> : ICondition<T>
{
    int MinRound { get; }

    public PostRoundCondition(int n)
    {
        MinRound = n;
    }

    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind)
    {
        int id = game.Turns[ind];
        return game.Players[id].History.Turns > MinRound;
    }
}

public class MaxScoreTeamTournament<T> : ICondition<T>
{
    private int _socre;

    public MaxScoreTeamTournament(int score)
    {
        this._socre = score;
    }

    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind)
    {
        for (int i = 0; i < tournament.ScoreTeams.Length; i++)
        {
            if (tournament.ScoreTeams[i] >= this._socre)
            {
                return true;
            }
        }

        return false;
    }
}

public class CantGamesTournament<T> : ICondition<T>
{
    private int _cant;

    public CantGamesTournament(int n)
    {
        this._cant = n;
    }

    public bool RunRule(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind)
    {
        return tournament.Index == _cant - 1;
    }
}