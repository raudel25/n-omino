using Table;

namespace InfoGame;

public class History<T> : ICloneable<History<T>>
{
    private List<Move<T>> _history;

    public History()
    {
        _history = new();
    }

    public Move<T> this[int index]
    {
        get => _history[index];
        set => _history[index] = value;
    }

    //turnos que han pasado
    public int Turns => _history.Count;

    //cantidad de pases totales
    public int Passes => _history.Where(x => x.IsAPass()).Count();

    //cantidad de pases consecutivos 
    public int ConsecutivePasses
    {
        get
        {
            int count = 0;
            for (int i = this.Turns - 1; i > 0; i--)
            {
                if (!this[i].IsAPass()) break;
                count++;
            }

            return count;
        }
    }

    //añadir una Move al historial
    public void Add(Move<T> item)
    {
        _history.Add(item);
    }

    //ver si el jugador ha puesto una ficha
    public bool Contains(Move<T> item)
    {
        return _history.Contains(item);
    }

    public IEnumerator<Move<T>> GetEnumerator()
    {
        return _history.GetEnumerator();
    }

    //ver en qué turno hizo una jugada
    public int TurnOf(Move<T> item)
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
        History<T> copy = new();
        foreach (var move in _history)
            copy.Add(move.Clone());
        return copy;
    }

    public int HowManyPuso(T value) => _history.Where(x => !x.IsAPass() && !x.Mata(value)).Count();
    // {
    //     int cont = 0;
    //     foreach (var move in _history)
    //     {
    //         if(move.IsAPass()) continue;
    //         for (int i = 0; i < move.Token!.CantValues; i++)
    //         {
    //             //Si el valor no es el que busco continúo
    //             if(!move.Token[i]!.Equals(value)) continue;
    //             //Si este valor lo había matado continúo
    //             if (move.Node!.Fathers.Contains(move.Node.Connections[i]!)) continue;
    //             //Si el valor lo puse, lo cuento
    //             cont++;
    //         }
    //     }
    //     return cont;
    // }

    public int HowManyMato(T value) => _history.Where(x => !x.IsAPass() && x.Mata(value)).Count();
    // {
    //     int cont = 0;

    //     foreach (var move in _history)
    //     {
    //         if(move.IsAPass()) continue;
    //         for (int i = 0; i < move.Token!.CantValues; i++)
    //         {
    //             //Si el valor no es el que busco continúo
    //             if(!move.Token[i]!.Equals(value)) continue;
    //             //Si este valor lo había matado lo cuento
    //             if (move.Node!.Fathers.Contains(move.Node.Connections[i]!)) cont++;
    //         }
    //     }
    //     return cont;
    // }
    public IEnumerable<(T value, int cant)> Puestas(GameStatus<T> game)
    {
        foreach (var item in game.Values)
            yield return (item, HowManyPuso(item))!;
    }

    public IEnumerable<(T value, int cant)> Matadas(GameStatus<T> game)
    {
        foreach (var item in game.Values)
            yield return (item, HowManyMato(item))!;
    }
}