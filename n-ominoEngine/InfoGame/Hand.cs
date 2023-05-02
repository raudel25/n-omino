using System.Collections;
using Table;

namespace InfoGame;

public class Hand<T> : ICollection<Token<T>>, ICloneable<Hand<T>>
{
    private readonly HashSet<Token<T>> _hand;

    public Hand()
    {
        _hand = new HashSet<Token<T>>();
    }

    public Hand<T> Clone()
    {
        Hand<T> copy = new();
        foreach (var item in _hand)
            copy.Add(item.Clone());
        return copy;
    }

    //Cuántas fichas tiene la mano
    public int Count => _hand.Count;

    public bool IsReadOnly => false;

    //añadir ficha a la mano
    public void Add(Token<T> item)
    {
        _hand.Add(item);
    }

    public void Clear()
    {
        _hand.Clear();
    }

    //ver si tienes una ficha
    public bool Contains(Token<T> item)
    {
        return _hand.Contains(item);
    }

    public void CopyTo(Token<T>[] array, int arrayIndex)
    {
        _hand.CopyTo(array, arrayIndex);
    }

    //quitar una ficha de la mano
    public bool Remove(Token<T> item)
    {
        return _hand.Remove(item);
    }

    public IEnumerator<Token<T>> GetEnumerator()
    {
        return _hand.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    //Determinar cuantas fichas de la mano contienen el valor
    public int HowManyTokensContains(T value)
    {
        var cont = 0;
        foreach (var token in this)
            if (token.Contains(value))
                cont++;

        return cont;
    }
}