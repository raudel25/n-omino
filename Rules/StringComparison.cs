namespace Rules;

public class StringComparisonLevenshtein:IComparison<string>
{
    private int _comparison;

    public StringComparisonLevenshtein(int comparison)
    {
        this._comparison = comparison;
    }

    public bool Compare(string a, string b)
    {
        return LevenshteinDistance(a, b) <= _comparison;
    }
    private int LevenshteinDistance(string s, string t)
    {

        // d es una tabla con m+1 renglones y n+1 columnas
        int cost = 0;
        int m = s.Length;
        int n = t.Length;
        int[,] d = new int[m + 1, n + 1]; 

        // Verifica que exista algo que comparar
        if (n == 0) return m;
        if (m == 0) return n;

        // Llena la primera columna y la primera fila.
        for (int i = 0; i <= m; i++) d[i, 0] = i;
        for (int j = 0; j <= n; j++) d[0, j] = j;
   
   
        // recorre la matriz llenando cada unos de los pesos.
        // i columnas, j renglones`
        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                cost = (s[i - 1] == t[j - 1]) ? 0 : 1;  
                d[i, j] = System.Math.Min(System.Math.Min(d[i - 1, j] + 1,  //Eliminacion
                        d[i, j - 1] + 1),                             //Insercion 
                    d[i - 1, j - 1] + cost);                     //Sustitucion
            }
        }

        return d[m, n]; 
    }
}