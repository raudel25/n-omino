using InfoGame;

namespace Rules;

public abstract class ActionConditionRule<T>
{
    /// <summary>
    /// Acciones que determinan las reglas
    /// </summary>
    public T[] Actions { get; protected set; }

    /// <summary>
    /// Criterios bajo los cuales se ejecutan las reglas
    /// </summary>
    public ICondition[] Critery { get; protected set; }

    /// <summary>
    /// Regla que se ejecuta por defecto
    /// </summary>
    public T? Default { get; }

    public ActionConditionRule(IEnumerable<T> rules, IEnumerable<ICondition> condition, T rule)
    {
        this.Actions = new T[rules.Count()];
        this.Critery = new ICondition[condition.Count()];
        int i = 0;
        foreach (var item in rules)
        {
            this.Actions[i] = item;
        }

        i = 0;
        foreach (var item in condition)
        {
            this.Critery[i] = item;
        }

        this.Default = rule;
    }

    /// <summary>
    /// Determinar la regla a utilizar
    /// </summary>
    /// <param name="game">Estado del juego clonado</param>
    /// <param name="original">Estado del juego original</param>
    /// <param name="rules">Reglas</param>
    /// <param name="ind">Indice del jugador que le corresponde jugar</param>
    public abstract void RunRule(GameStatus game, GameStatus original, InfoRules rules, int ind);
}