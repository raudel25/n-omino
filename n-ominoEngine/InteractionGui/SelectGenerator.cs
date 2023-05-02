namespace InteractionGui;

public class SelectGenerator<T> : IVariant<T[], T>
{
    public SelectGenerator(T value)
    {
        if (value is int)
        {
            Values.Add(SelectInt0_n);
            Values.Add(SelectIntEven);
            Values.Add(SelectIntOdd);
            Values.Add(PrimeNumber);
            Param.Add(new ParamSelect("Del 0 a n", "Genera numeros del 0 a n", 0, false, true));
            Param.Add(new ParamSelect("Pares", "Genera numeros pares hasta n", 1, false, true));
            Param.Add(new ParamSelect("Impares", "Genera numeros impares hasta n", 2, false, true));
            Param.Add(new ParamSelect("Numeros primos", "Genera numeros primos hasta n", 3, false, true));
        }

        if (value is char)
        {
            Values.Add(SelectLetter);
            Param.Add(new ParamSelect("Letras del alfabeto", "Genera las letras del alfabeto", 0));
        }
    }

    public List<IVariant<T[], T>.Select> Values { get; } = new();
    public string Description { get; } = "Seleccione un generador para las fichas";
    public List<ParamSelect> Param { get; } = new();

    private T[] SelectInt0_n(ParamSelectFunction<T> comp)
    {
        var aux = new int[comp.Cant];
        for (var i = 0; i < comp.Cant; i++) aux[i] = i;

        return (aux as T[])!;
    }

    private T[] SelectIntEven(ParamSelectFunction<T> comp)
    {
        var aux = new int[comp.Cant / 2];
        for (var i = 0; i < comp.Cant / 2; i++) aux[i] = 2 * i;

        return (aux as T[])!;
    }

    private T[] SelectIntOdd(ParamSelectFunction<T> comp)
    {
        var aux = new int[comp.Cant / 2];
        for (var i = 1; i < comp.Cant / 2; i++) aux[i] = 2 * i + 1;

        return (aux as T[])!;
    }

    private T[] PrimeNumber(ParamSelectFunction<T> comp)
    {
        var aux = new bool[comp.Cant];
        for (var i = 2; i <= comp.Cant; i++)
        for (var j = i * i; j <= comp.Cant; j += i)
            aux[j - 1] = true;

        var generator = new List<int>();

        for (var i = 1; i < comp.Cant; i++)
            if (!aux[i])
                generator.Add(i + 1);

        return (generator.ToArray() as T[])!;
    }

    private T[] SelectLetter(ParamSelectFunction<T> comp)
    {
        var aux = new char[26];
        for (var i = 0; i < 26; i++) aux[i] = (char)(97 + i);

        return (aux as T[])!;
    }
}