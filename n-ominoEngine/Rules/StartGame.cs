using InfoGame;
using Table;

namespace Rules;

public class RandomDealer<T> : IDealer<T>
{
    private readonly Random r = new();

    public Hand<T> Deal(List<Token<T>> items, int cant)
    {
        Hand<T> res = new();

        if (items is null || items.Count == 0) return res;

        if (items.Count <= cant)
        {
            foreach (var item in items) res.Add(item);
            items.Clear();
            return res;
        }

        var count = 0;
        while (count++ < cant)
        {
            var index = r.Next(items.Count);
            res.Add(items[index]);
            items.RemoveAt(index);
        }

        return res;
    }

    public IEnumerable<Hand<T>> Deal(List<Token<T>> items, int[] tokensperplayer)
    {
        foreach (var canttokens in tokensperplayer) yield return Deal(items, canttokens);
    }
}

public class ClassicTokensMaker<T> : ITokensMaker<T>
{
    public List<Token<T>> MakeTokens(T[] array, int n)
    {
        var r = new List<Token<T>>();
        Comb(array, 0, 0, r, new T[n]);
        return r;
    }

    private void Comb(T[] array, int index, int current, List<Token<T>> tokens, T[] values)
    {
        if (index == values.Length)
        {
            var val = new T[values.Length];
            Array.Copy(values, val, values.Length);
            Token<T> token = new(val);
            tokens.Add(token);
            return;
        }

        for (var i = current; i < array.Length; i++)
        {
            values[index] = array[i];
            Comb(array, index + 1, i, tokens, values);
        }
    }
}

public class CircularTokensMaker<T> : ITokensMaker<T>
{
    public List<Token<T>> MakeTokens(T[] array, int n)
    {
        var r = new ClassicTokensMaker<T>();
        var tokens = r.MakeTokens(array, n);
        var permutation = new List<Token<T>>();

        foreach (var item in tokens)
        {
            var values = new T[item.CantValues];
            values[0] = item[0];
            CircularComb(values, item, new bool[item.CantValues], permutation, 1);
        }

        return permutation;
    }

    public void CircularComb(T[] values, Token<T> token, bool[] visited, List<Token<T>> permutation, int index)
    {
        if (index == values.Length)
        {
            var val = new T[values.Length];
            Array.Copy(values, val, values.Length);
            Token<T> tokenPer = new(val);
            permutation.Add(tokenPer);
            return;
        }

        for (var i = 1; i < values.Length; i++)
        {
            if (visited[i]) continue;
            values[index] = token[i];
            visited[i] = true;
            CircularComb(values, token, visited, permutation, index + 1);
            visited[i] = false;
        }
    }
}