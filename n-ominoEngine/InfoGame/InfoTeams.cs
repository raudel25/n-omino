using System.Collections;

namespace InfoGame;

public class InfoTeams<T> : IEnumerable<T>
{
    private readonly List<T> _values;

    public InfoTeams(int id)
    {
        Id = id;
        _values = new List<T>();
    }

    public int Id { get; }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _values.Count) throw new IndexOutOfRangeException();
            return _values[index];
        }
        set
        {
            if (index < 0 || index >= _values.Count) throw new IndexOutOfRangeException();
            _values[index] = value;
        }
    }

    public int Count => _values.Count;

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in _values) yield return item;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T value)
    {
        _values.Add(value);
    }
}