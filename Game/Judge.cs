using Table;
using Rules;

namespace Judge;

public class Judge
{
    public InfoRules JudgeRules { get; private set; }
    public Judge(InfoRules infoRules)
    {
        this.JudgeRules = infoRules;
    }
}