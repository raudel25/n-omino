namespace Table;

public class ValuesNode<T> where T : struct
{
    public List<T> Values { get; set; }
    public bool IsAssignValue { get; set; }

    public ValuesNode(bool assign = false)
    {
        this.Values = new List<T>();
        this.IsAssignValue = assign;
    }
}