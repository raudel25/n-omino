namespace InteractionGui;

public class ParamSelect
{
    public string Name { get; private set; }
    
    public string Description { get; private set; }
    public int Index { get; private set; }
    public int Values { get; private set; }
    public bool Comparison { get; private set; }

    public ParamSelect(string name,string description, int ind, int values = 0, bool comparison = false)
    {
        this.Name = name;
        this.Index = ind;
        this.Comparison = comparison;
        this.Values = values;
        this.Description = description;
    }
}