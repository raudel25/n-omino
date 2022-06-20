using Table;

namespace Game;

public interface IDealer<T>
{
    /// <summary>Reparte una cantidad de T</summary>
    /// <param name="items">Elementos a repartir</param>
    /// <param name="cant">Cantidad de elementos que se quieren</param>
    /// <returns>Una lista con lo que repartió</returns>
    public List<T> Deal(List<T> items, int cant);
}

public class RandomDealer<T> : IDealer<Token<T>>
{
    public List<Token<T>> Deal(List<Token<T>> items, int cant)
    {
        List<Token<T>> res = new();
        Random r = new Random();
        int count = 0;
        while(count++ < cant)
            {
                int index = r.Next(items.Count);
                res.Add(items[index]);
                items.RemoveAt(index);
            }
        return res;
    }
}

public interface ITokensMaker<T>
{
    /// <summary>Genera las fichas</summary>
    /// <param name="values">Valores que tendrán las fichas</param>
    /// <param name="n">Cantidad de caras que tendrá una ficha</param>
    /// <returns>Una lista con las fichas creadas</returns>
    public List<Token<T>> MakeTokens (T[] values, int n);
}

public class TokensMaker<T>
{
    public List<Token<T>> MakeTokens (T[] array, int n)
    {
        var r = new List<Token<T>>();
        Comb(array, 0, 0, r, new T[n]);
        return r;
    }
    private void Comb (T[] array, int index, int current, List<Token<T>> tokens, T[] values)
    {
        if(index == values.Length)
        {
            T[] val = new T[values.Length];
            Array.Copy(values,val,values.Length);
            Token<T> token = new(val);
            tokens.Add(token);
            return;
        }
        for(int i = current; i < array.Length; i++)
        {
            values[index] = array[i];
            Comb(array, index+1, i,tokens,values);
        }
    }
}