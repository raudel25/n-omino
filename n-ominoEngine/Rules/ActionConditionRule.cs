using InfoGame;

namespace Rules;

public abstract class ActionConditionRule<T1, T2> where T2 : struct
{
    /// <summary>
    /// Acciones que determinan las reglas
    /// </summary>
    public T1[] Actions { get; protected set; }

    /// <summary>
    /// Criterios bajo los cuales se ejecutan las reglas
    /// </summary>
    public ICondition<T2>[] Condition { get; protected set; }

    /// <summary>
    /// Regla que se ejecuta por defecto
    /// </summary>
    public T1? Default { get; }

    public ActionConditionRule(IEnumerable<T1> rules, IEnumerable<ICondition<T2>> condition, T1? rule)
    {
        this.Actions = rules.ToArray();
        this.Condition = condition.ToArray();
        this.Default = rule;
    }

    /// <summary>
    /// Determinar la regla a utilizar
    /// </summary>
    /// <param name="tournament">Datos del torneo</param>
    /// <param name="game">Estado del juego clonado</param>
    /// <param name="original">Estado del juego original</param>
    /// <param name="rules">Reglas</param>
    /// <param name="ind">Indice del jugador que le corresponde jugar</param>
    public abstract void RunRule(TournamentStatus tournament,GameStatus<T2> game, GameStatus<T2> original, InfoRules<T2> rules, int ind);
}