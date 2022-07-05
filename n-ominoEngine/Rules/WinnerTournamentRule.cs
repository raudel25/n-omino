using InfoGame;
using Table;

namespace Rules;

public class WinnerTournamentRule<T> : ActionConditionRule<IWinnerTournament, T>, ICloneable<WinnerTournamentRule<T>>
{
    public WinnerTournamentRule(IEnumerable<IWinnerTournament> rules, IEnumerable<ICondition<T>> condition,
        IWinnerTournament rule) : base(rules, condition, rule)
    {
    }

    public override void RunRule(TournamentStatus tournament, GameStatus<T> game, GameStatus<T> original,
        InfoRules<T> rules, int ind)
    {
        bool activate = false;
        for (int i = 0; i < this.Condition.Length; i++)
        {
            if (this.Condition[i].RunRule(tournament, original, rules, ind))
            {
                this.Actions[i].DeterminateWinner(tournament, ind);
                activate = true;
            }
        }

        if (!activate) this.Default!.DeterminateWinner(tournament, ind);
    }

    public WinnerTournamentRule<T> Clone()
    {
        return new WinnerTournamentRule<T>(this.Actions, this.Condition, this.Default!);
    }

    object ICloneable.Clone()
    {
        return this.Clone();
    }
}