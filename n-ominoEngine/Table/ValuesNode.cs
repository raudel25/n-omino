namespace Table;

public class ValuesNode<T>
{
    public List<T> Values { get; internal set; }
    public bool IsAssignValue { get; internal set; }

    public ValuesNode(bool assign = false)
    {
        this.Values = new List<T>();
        this.IsAssignValue = assign;
    }
}