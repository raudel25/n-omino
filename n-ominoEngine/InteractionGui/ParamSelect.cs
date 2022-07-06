using Table;
using Rules;
using Player;

namespace InteractionGui;

public class ParamSelect
{
    /// <summary>
    /// Nombre de la regla
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Descripcion de la regla
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Indice de la regla relativo al array de reglas
    /// </summary>
    public int Index { get; private set; }

    /// <summary>
    /// Cantidade valores genericos
    /// </summary>
    public bool ValueForParam { get; private set; }

    /// <summary>
    /// Determina si a la regla se le puede introducir una cantidad numerica
    /// </summary>
    public bool Cant { get; private set; }

    /// <summary>
    /// Determina si la regla necesita un comparador
    /// </summary>
    public bool Comparison { get; private set; }

    public bool Strategy { get; private set; }

    public ParamSelect(string name, string description, int ind, bool valueForParam = false, bool cant = false,
        bool comparison = false, bool strategy = false)
    {
        this.Name = name;
        this.Index = ind;
        this.Comparison = comparison;
        this.ValueForParam = valueForParam;
        this.Description = description;
        this.Cant = cant;
        this.Strategy = strategy;
    }
}

public class ParamSelectFunction<T>
{
    public IComparison<T>? Comp { get; set; }

    public int Cant { get; set; }

    public T? ValueForParam { get; set; }

    public Token<T>? Token { get; set; }

    public int n { get; set; }

    public IStrategy<T> Strategy { get; set; }
}