using System.Collections;
using Table;

namespace InfoGame;

public class Hand<T> : ICollection<Token<T>>, ICloneable<Hand<T>>
{
    private HashSet<Token<T>> _hand;

    public Hand()
    {
        this._hand = new();
    }

    //cuántas fichas tienes en la mano
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

    public IEnumerator<Token<T>> GetEnumerator()
    {
        return _hand.GetEnumerator();
    }

    //quitar una ficha de la mano
    public bool Remove(Token<T> item)
    {
        return _hand.Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public Hand<T> Clone()
    {
        Hand<T> copy = new();
        foreach (var item in this._hand)
            copy.Add(item.Clone());
        return copy;
    }

    object ICloneable.Clone()
    {
        return this.Clone();
    }
}