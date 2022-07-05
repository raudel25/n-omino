using Table;
using Rules;

namespace InteractionGui;

public class ParamSelect
{
    public string Name { get; private set; }
    
    public string Description { get; private set; }
    public int Index { get; private set; }
    public int Values { get; private set; }
    public bool Cant { get; private set; }
    public bool Comparison { get; private set; }

    public ParamSelect(string name,string description, int ind, int values = 0,bool cant=false, bool comparison = false)
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