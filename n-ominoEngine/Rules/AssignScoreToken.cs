using Table;

namespace Rules;

public interface IAssignScoreToken<T>
{
    /// <summary>
    /// Determinar el score de una ficha
    /// </summary>
    /// <param name="token">Ficha</param>
    /// <returns>Score de la ficha</returns>
    public int ScoreToken(Token<T> token);
}

public class AssignScoreTokenClassic : IAssignScoreToken<int>
{
    public int ScoreToken(Token<int> token)
    {
        return token.Sum();
    }
}

public class AssignScoreTokenMax : IAssignScoreToken<int>
{
    public int ScoreToken(Token<int> token)
    {
        int[] aux = token.ToArray();
        Array.Sort(aux);

        return aux[aux.Length - 1];
    }
}

public class AssignScoreTokenMin : IAssignScoreToken<int>
{
    public int ScoreToken(Token<int> token)
    {
        int[] aux = token.ToArray();
        Array.Sort(aux);

        return aux[0];
    }
}

public class AssignScoreTokenGcd : IAssignScoreToken<int>
{
    public int ScoreToken(Token<int> token)
    {
        int aux = token[0];

        if (aux == 0) return 0;

        foreach (var item in token)
        {
            if (item == 0) return 0;
            aux = GcdComparison.GCD(aux, item);
        }

        return aux;
    }
}

public class AssignScoreTokenLetter : IAssignScoreToken<char>
{
    public int ScoreToken(Token<char> token)
    {
        int sum = 0;
        foreach (var item in token)
        {
            sum += (int) item;
        }

        return sum;
    }
}