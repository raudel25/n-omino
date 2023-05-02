namespace Table;

public class Coordinates
{
    /// <summary>Lista de coordenadas ordenadas</summary>
    private readonly (int, int)[] _listCoord;

    public Coordinates((int, int)[] list)
    {
        var listCopy = new (int, int)[list.Length];
        var listCopy1 = new (int, int)[list.Length];
        Array.Copy(list, listCopy, list.Length);
        Array.Copy(list, listCopy1, list.Length);
        Array.Sort(listCopy);
        _listCoord = listCopy;
        Coord = listCopy1;
        BorderLeft = listCopy[0].Item1;
        var max = int.MinValue;
        for (var i = 0; i < listCopy.Length; i++) max = Math.Max(max, listCopy[i].Item2);

        BorderTop = max;
    }

    /// <summary>Lista de coordenadas</summary>
    public (int, int)[] Coord { get; }

    public int BorderLeft { get; }

    public int BorderTop { get; }

    public override bool Equals(object? obj)
    {
        var aux = obj as Coordinates;
        if (aux == null) return false;
        var equal = true;
        for (var i = 0; i < _listCoord.Length; i++) equal = equal && _listCoord[i] == aux._listCoord[i];

        return equal;
    }

    public override int GetHashCode()
    {
        return _listCoord[0].GetHashCode();
    }
}