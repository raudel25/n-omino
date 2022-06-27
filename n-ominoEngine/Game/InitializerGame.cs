using Table;
using InfoGame;

namespace Game;

public class InitializerGame<T> where T : struct
{
    public ITokensMaker<T> Maker { get; private set; }
    public IDealer<T> Dealer { get; private set; }
    public TableGame<T> Table { get; private set; }
    public int CantPlayers { get; private set; }
    public int DimensionToken { get; private set; }
    public int CantTokenToDeal { get; private set; }
    public T[] Generator { get; private set; }
    public List<int>[] CreateTeams { get; private set; }

    public InitializerGame(ITokensMaker<T> maker, IDealer<T> dealer, List<int>[] team, TableGame<T> table,
        T[] generator, int cant, int cantDeal, int dimension)
    {
        this.Dealer = dealer;
        this.Maker = maker;
        this.Table = table;
        this.Generator = generator;
        this.CantPlayers = cant;
        this.DimensionToken = dimension;
        this.CantTokenToDeal = cantDeal;
        this.CreateTeams = team;
    }

    public GameStatus<T> StartGame()
    {
        List<Token<T>> tokens = this.Maker.MakeTokens(this.Generator, this.DimensionToken);
        InfoPlayer<T>[] playersInfo = new InfoPlayer<T>[this.CantPlayers];

        for (int i = 0; i < this.CantPlayers; i++)
        {
            var hand = this.Dealer.Deal(tokens, this.CantTokenToDeal);

            playersInfo[i] = new InfoPlayer<T>(hand, new History<T>(), 0, i);
        }

        List<InfoPlayer<T>>[] team = new List<InfoPlayer<T>>[this.CreateTeams.Length];

        for (int i = 0; i < team.Length; i++)
        {
            team[i] = new List<InfoPlayer<T>>();
            for (int j = 0; j < this.CreateTeams[i].Count; j++)
            {
                team[i].Add(playersInfo[this.CreateTeams[i][j]]);
            }
        }

        return new GameStatus<T>(playersInfo, team, this.Table, new[] {0, 1, 2, 3}, tokens);
    }
}