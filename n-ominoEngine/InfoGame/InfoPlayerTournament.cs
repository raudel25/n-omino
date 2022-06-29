namespace InfoGame;

public class InfoPlayerTournament
{
    public int GamesToWin { get; set; }
    public int ScoreTournament { get; set; }
    public int Id { get; private set; }

    public InfoPlayerTournament(int id)
    {
        this.Id = id;
    }
}