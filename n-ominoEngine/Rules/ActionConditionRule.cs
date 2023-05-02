namespace Rules;

public abstract class ActionConditionRule<T1, T2>
{
    public ActionConditionRule(IEnumerable<T1> rules, IEnumerable<ICondition<T2>> condition, T1? rule)
    {
        Actions = rules.ToArray();
        Condition = condition.ToArray();
        Default = rule;
    }

    /// <summary>
    ///     Acciones que determinan las reglas
    /// </summary>
    public T1[] Actions { get; protected set; }

    /// <summary>
    ///     Criterios bajo los cuales se ejecutan las reglas
    /// </summary>
    public ICondition<T2>[] Condition { get; protected set; }

    /// <summary>
    ///     Regla que se ejecuta por defecto
    /// </summary>
    public T1? Default { get; }
}