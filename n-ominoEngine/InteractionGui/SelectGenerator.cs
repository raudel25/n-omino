namespace InteractionGui;

public class SelectGenerator<T> : IVariant<T[], T>
{
    public List<IVariant<T[], T>.Select> Values { get; } = new List<IVariant<T[], T>.Select>();
    public string Description { get; } = "Seleccione un generador para las fichas";
    public List<ParamSelect> Param { get; } = new List<ParamSelect>();

    public SelectGenerator(T value)
    {
        if (value is int)
        {
            Values.Add(SelectInt0_n);
            Values.Add(SelectIntEven);
            Values.Add(SelectIntOdd);
            Param.Add(new ParamSelect("Del 0 a n", "Genera numeros del 0 a n", 0, 0, true));
            Param.Add(new ParamSelect("Pares", "Genera numeros pares hasta n", 0, 0, true));
            Param.Add(new ParamSelect("Impares", "Genera numeros impares hasta n", 0, 0, true));
        }

        if (value is char)
        {
            Values.Add(SelectLetter);
            Param.Add(new ParamSelect("Letras del alfabeto", "Genera las letras del alfabeto", 0));
        }
    }

    private T[] SelectInt0_n(ParamSelectFunction<T> comp)
    {
        int[] aux = new int[comp.Cant];
        for (int i = 0; i < comp.Cant; i++)
        {
            aux[i] = i;
        }

        return (aux as T[])!;
    }

    private T[] SelectIntEven(ParamSelectFunction<T> comp)
    {
        int[] aux = new int[comp.Cant];
        for (int i = 0; i < comp.Cant * 2; i += 2)
        {
            aux[i] = i;
        }

        return (aux as T[])!;
    }

    private T[] SelectIntOdd(ParamSelectFunction<T> comp)
    {
        int[] aux = new int[comp.Cant];
        for (int i = 1; i < comp.Cant * 2; i += 2)
        {
            aux[i] = i;
        }

        return (aux as T[])!;
    }

    private T[] SelectLetter(ParamSelectFunction<T> comp)
    {
        char[] aux = new char[26];
        for (int i = 0; i < 26; i++)
        {
            aux[i] = (char) (97 + i);
        }

        return (aux as T[])!;
    }
}