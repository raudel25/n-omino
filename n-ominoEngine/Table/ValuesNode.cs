namespace Table;

public class ValuesNode<T>
{
    public ValuesNode(bool assign = false)
    {
        Values = new List<T>();
        IsAssignValue = assign;
    }

    public List<T> Values { get; internal set; }
    public bool IsAssignValue { get; internal set; }
}