using Table;
namespace InfoGame;
public class InfoPlayer
{
    public HashSet<Token>? Hand { get; set; }
    public int HandCount{get{return Hand!.Count();}}
    public int Passes {get; set;}
    public Actions Actions {get; set;}
    public double Score { get; set; }
    public int ID {get; set;}

    public InfoPlayer(HashSet<Token> hand, int passes, Actions actions, double score, int id)
    {
        this.Hand = hand;
        this.Passes = passes;
        this.Actions = actions;
        this.Score = score;
        this.ID = id;
    }
    public InfoPlayer Clone()
    {
        return new InfoPlayer(Clone(Hand!), Passes, Clone(Actions), Score, ID);
    }

    private HashSet<Token> Clone(HashSet<Token> collection)
    {
        HashSet<Token> aux = new HashSet<Token>();
        foreach (var item in collection)
            aux.Add(item.Clone());
        return aux;
    }
    private Actions Clone(Actions x)
    {
        return (Actions)x.Clone();
    }
}


// public static class Extensions
// {
//     public static IEnumerable<T> Clone<T>(this IEnumerable<T> items)
//     {
//         foreach (var item in items) yield return item.Clone();
//     }
// }

public class GameStatus
{
    public InfoPlayer[] Players;
    public List<InfoPlayer>[] Teams;
    public TableGame Table;
    public int[] Turns { get; set; }
    public List<Token>? TokensTable { get; set; }

    public GameStatus(InfoPlayer[] players, List<InfoPlayer>[] teams, TableGame table, int[] turns, List<Token> tokens)
    {
        this.Players = players;
        this.Teams = teams;
        this.Table = table;
        this.Turns = turns;
        this.TokensTable = tokens;
    }

    public GameStatus Clone()
    {
        InfoPlayer[] players = new InfoPlayer[this.Players.Length];
        List<InfoPlayer>[] teams = new List<InfoPlayer>[this.Teams.Length];
        for (int i = 0; i < this.Players.Length; i++)
        {
            players[i] = this.Players[i].Clone();
        }

        for (int i = 0; i < this.Teams.Length; i++)
        {
            teams[i] = new List<InfoPlayer>();
            for (int j = 0; j < this.Teams[i].Count; j++)
            {
                teams[i].Add(players[this.Teams[i][j].ID]);
            }
        }

        return new GameStatus(players, teams, this.Table.Clone(), this.Turns.ToArray(), this.TokensTable.ToList());
    }
}

public class Actions : ICloneable
{
    public Actions()
    {
        
    }

    public object Clone()
    {
        throw new NotImplementedException();
    }
}

