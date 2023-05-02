using Rules;
using Table;

namespace InteractionGui;

public class ParamSelect
{
    public ParamSelect(string name, string description, int ind, bool valueForParam = false, bool cant = false,
        bool comparison = false, bool strategy = false, bool token = false)
    {
        Name = name;
        Index = ind;
        Comparison = comparison;
        ValueForParam = valueForParam;
        Description = description;
        Cant = cant;
        Token = token;
    }

    /// <summary>
    ///     Nombre de la regla
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Descripcion de la regla
    /// </summary>
    public string Description { get; }

    /// <summary>
    ///     Indice de la regla relativo al array de reglas
    /// </summary>
    public int Index { get; }

    /// <summary>
    ///     Cantidade valores genericos
    /// </summary>
    public bool ValueForParam { get; }

    /// <summary>
    ///     Determina si a la regla se le puede introducir una cantidad numerica
    /// </summary>
    public bool Cant { get; }

    /// <summary>
    ///     Determina si la regla necesita un comparador
    /// </summary>
    public bool Comparison { get; }

    public bool Token { get; }
}

public class ParamSelectFunction<T>
{
    public IComparison<T>? Comp { get; set; }

    public int Cant { get; set; }

    public T? ValueForParam { get; set; }

    public Token<T>? Token { get; set; }
}