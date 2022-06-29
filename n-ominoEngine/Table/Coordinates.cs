namespace Table;

public class Coordinates
{
    /// <summary>Lista de coordenadas</summary>
    public (int, int)[] Coord { get; private set; }

    public int BorderLeft { get; private set; }

    public int BorderTop { get; private set; }

    /// <summary>Lista de coordenadas ordenadas</summary>
    private readonly (int, int)[] _listCoord;

    public Coordinates((int, int)[] list)
    {
        (int, int)[] listCopy = new (int, int)[list.Length];
        (int, int)[] listCopy1 = new (int, int)[list.Length];
        Array.Copy(list, listCopy, list.Length);
        Array.Copy(list, listCopy1, list.Length);
        Array.Sort(listCopy);
        this._listCoord = listCopy;
        this.Coord = listCopy1;
        this.BorderLeft = listCopy[0].Item1;
        int max = int.MinValue;
        for (int i = 0; i < listCopy.Length; i++)
        {
            max = Math.Max(max, listCopy[i].Item2);
        }

        this.BorderTop = max;
    }

    public override bool Equals(object? obj)
    {
        Coordinates? aux = (obj as Coordinates);
        if (aux == null) return false;
        bool equal = true;
        for (int i = 0; i < this._listCoord.Length; i++)
        {
            equal = equal && this._listCoord[i] == aux._listCoord[i];
        }

        return equal;
    }

    public override int GetHashCode()
    {
        return this._listCoord[0].GetHashCode();
    }
}