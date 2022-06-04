using Table;
namespace InfoGame;
public class Class1
{

}

public class InfoPlayer
{
    public Hand Hand {get; set;}
    public int Pasadas {get; set;}
    public Actions Actionsjugadas {get; set;}
    public int Score {get; set;}
    public int ID {get; set;}
    public InfoPlayer( Hand hand, int pasadas, Actions Actions, int score, int id)
    {
        this.Hand = hand;
        this.Pasadas = pasadas;
        this.Actionsjugadas = Actions;
        this.Score = score;
        this.ID = id;
    }
    public InfoPlayer Clone()
    {
        return new InfoPlayer(new Hand(), Pasadas, new Actions(), Score, ID);
    }
}

public class GameStatus
{
    public InfoPlayer[] Players;
    public List<InfoPlayer>[] Teams;
    public TableGame Table;
    public GameStatus(InfoPlayer[] players, List<InfoPlayer>[] teams, TableGame table)
    {
        this.Players = players;
        this.Teams = teams;
        this.Table = table;
    }
}

public class Actions
{
    public Actions()
    {
        
    }
}

public class Hand
{
    public Hand()
    {
        
    }
}

