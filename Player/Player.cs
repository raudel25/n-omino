using Table;
using Rules;
using InfoGame;

namespace Player;

public class Player
{
    InfoPlayer info;
    public Player(InfoPlayer infoplayer)
    {
        this.info = infoplayer;
    }
    public (Token, INode) Play(IJugada<Token> jugada, TableGame table, IValidPlay validador)
    {
        List<Token> posibles = info.Mano.Clone();

        while (posibles.Count > 0)
        {
            Token token = jugada.Play(posibles);

            foreach (var node in table.FreeNode)
                if (validador.ValidPlay(node, token, table)) return (token, node);

            posibles.Remove(token);
        }
        return (null!, null!);
    }
}

public class InfoPlayer
{
    public List<Token> Mano { get; private set; }
    public int Score { get; private set; }
    public int Pasadas;
    public InfoPlayer(List<Token> tokens)
    {
        this.Mano = tokens;
    }

    void UpdatePlayerStatus(IAsignScorePlayer asignador)
    {

    }
}

public class RandomPlayer<T> : IJugada<T>
{
    public RandomPlayer() { }
    public T Play(List<T> tokens)
    {
        Random r = new Random();
        int index = r.Next(0, tokens.Count);
        return tokens[index];
    }
}


public class BotaGordaPlayer<T> : IJugada<T>
{
    IComparer<T> _comparer;
    public BotaGordaPlayer(IComparer<T> comparer)
    {
        this._comparer = comparer;
    }
    public T Play(List<T> tokens)
    {
        T max = tokens[0];
        int pos = 0;
        for (int i = 1; i < tokens.Count; i++)
        {
            if (_comparer.Compare(max, tokens[i]) < 0)
            {
                max = tokens[i];
                pos = i;
            }
        }
        return tokens[pos];
    }
}

static class ListExtension
{
    public static List<Token> Clone(this List<Token> list)
    {
        Token[] arr = new Token[list.Count];
        list.CopyTo(arr);
        return arr.ToList();
    }
}

public interface IJugada<T>
{
    public T Play(List<T> tokens);
}