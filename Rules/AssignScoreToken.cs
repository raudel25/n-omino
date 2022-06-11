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

public class AssignScoreTokenClasic : IAssignScoreToken<int>
{
    public int ScoreToken(Token<int> token)
    {
        return token.Values.Sum();
    }
}