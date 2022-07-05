using System.Collections;

namespace InfoGame;

public class InfoTeams<T>:IEnumerable<T>
{
    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in this._values)
        {
            yield return item;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private List<T> _values;

    public int Id { get; private set; }
    
    public InfoTeams(int id)
    {
        this.Id = id;
        _values = new List<T>();
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= this._values.Count) throw new IndexOutOfRangeException();
            return this._values[index];
        }
        set
        {
            if (index < 0 || index >= this._values.Count) throw new IndexOutOfRangeException();
            this._values[index] = value;
        }
    }

    public void Add(T value)
    {
        this._values.Add(value);
    }
    
    public int Count
    {
        get { return this._values.Count; }
    }
}