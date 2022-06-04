using Table;
using Rules;
using InfoGame;

namespace Player;

public abstract class Player
{
    public int ID; 
    IPlay<Token>[] _plays; //estrategias de juego
    public Player (int id, IPlay<Token>[] plays)
    {
        this.ID = id;
        this._plays = plays;
    }
    public abstract (Token, Node) Play(GameStatus status, InfoRules rules);
}

public class RandomPlayer<T> : IPlay<T>
{
    public RandomPlayer() {}
    public T Play(List<T> tokens)
    {
        Random r = new Random();
        int index = r.Next(0,tokens.Count);
        return tokens[index];
    }
}


public class BotaGordaPlayer<T> : IPlay<T>
{
    IComparer<T> _comparer;
    public BotaGordaPlayer(IComparer<T> comparer)
    {
        this._comparer = comparer;
    }
    public T Play(List<T> tokens)
    {
        tokens.Sort(_comparer);
        return tokens[0];
    }
}

public interface IPlay<T>
{
    public T Play(List<T> tokens);
}