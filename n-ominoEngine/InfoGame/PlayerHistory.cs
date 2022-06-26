using System.Collections;
using Table;

namespace InfoGame;
public class History<T> : ICloneable<History<T>> where T : struct
{
    private List<Jugada<T>> _history;
    public History()
    {
        _history = new();
    }
    public Jugada<T> this[int index] { get => _history[index]; set => _history[index] = value; }

    //turnos que han pasado
    public int Turns => _history.Count;

    //cantidad de pases totales
    public int Passes => _history.Where(x => x is null).Count();

    //cantidad de pases consecutivos 
    public int ConsecutivePasses
    {
        get
        {
            int count = 0;
            for (int i = this.Turns - 1; i > 0; i--)
            {
                if (this[i] is not null) break;
                else count++;
            }
            return count;
        }
    }

    //añadir una jugada al historial
    public void Add(Jugada<T> item)
    {
        _history.Add(item);
    }

    //ver si el jugador ha puesto una ficha
    public bool Contains(Jugada<T> item)
    {
        return _history.Contains(item);
    }

    public IEnumerator<Jugada<T>> GetEnumerator()
    {
        return _history.GetEnumerator();
    }

    //ver en qué turno hizo una jugada
    public int TurnOf(Jugada<T> item)
    {
        return _history.IndexOf(item);
    }

    //elimina la última jugada del historial
    public void RemoveLast()
    {
        int index = _history.Count - 1;
        _history.RemoveAt(index);
    }

    public History<T> Clone()
    {
        History<T> Copy = new();
        foreach (var item in _history)
            Copy.Add(item.Clone());
        return Copy;
    }

    object ICloneable.Clone()
    {
        return this.Clone();
    }
}
