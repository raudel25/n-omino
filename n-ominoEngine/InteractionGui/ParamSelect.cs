using Table;
using Rules;

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
    public int Values { get; private set; }
    
    /// <summary>
    /// Determina si a la regla se le puede introducir una cantidad numerica
    /// </summary>
    public bool Cant { get; private set; }
    
    /// <summary>
    /// Determina si la regla necesita un comparador
    /// </summary>
    public bool Comparison { get; private set; }

    public ParamSelect(string name, string description, int ind, int values = 0, bool cant = false,
        bool comparison = false)
    {
        this.Name = name;
        this.Index = ind;
        this.Comparison = comparison;
        this.Values = values;
        this.Description = description;
        this.Cant = cant;
    }
}

public class ParamSelectFunction<T>
{
    public IComparison<T>? Comp { get; set; }

    public int Cant { get; set; }

    public T? Left { get; set; }

    public T? Right { get; set; }

    public Token<T>? Token { get; set; }

    public int n { get; set; }
}