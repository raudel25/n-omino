using Table;

namespace Rules;

public interface IAsignScoreToken
{
    /// <summary>
    /// Determinar el score de una ficha
    /// </summary>
    /// <param name="token">Ficha</param>
    /// <returns>Score de la ficha</returns>
    public int ScoreToken(Token token);
}

public class AsignScoreTokenClasic : IAsignScoreToken
{
    public int ScoreToken(Token token)
    {
        return token.Values.Sum();
    }
}