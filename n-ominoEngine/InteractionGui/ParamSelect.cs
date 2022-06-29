namespace InteractionGui;

public class ParamSelect
{
    public string Name { get; private set; }
    public int Index { get; private set; }
    public bool Values { get; private set; }
    public bool Comparison { get; private set; }

    public ParamSelect(string name, int ind, bool values = false, bool comparison = false)
    {
        this.Name = name;
        this.Index = ind;
        this.Comparison = comparison;
        this.Values = values;
    }
}