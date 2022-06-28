using Table;
using InfoGame;

namespace Game;

public class InitializerGame<T>
{
    private ITokensMaker<T> _maker;
    private IDealer<T> _dealer;
    private TableGame<T> _table;
    private int _cantPlayers;
    private int _dimensionToken;
    private int _cantTokenToDeal;
    private T[] _generator;
    private List<int>[] _createTeams;

    public InitializerGame(ITokensMaker<T> maker, IDealer<T> dealer, List<int>[] team, TableGame<T> table,
        T[] generator, int cant, int cantDeal, int dimension)
    {
        this._dealer = dealer;
        this._maker = maker;
        this._table = table;
        this._generator = generator;
        this._cantPlayers = cant;
        this._dimensionToken = dimension;
        this._cantTokenToDeal = cantDeal;
        this._createTeams = team;
    }

    public GameStatus<T> StartGame()
    {
        List<Token<T>> tokens = this._maker.MakeTokens(this._generator, this._dimensionToken);
        InfoPlayer<T>[] playersInfo = new InfoPlayer<T>[this._cantPlayers];

        for (int i = 0; i < this._cantPlayers; i++)
        {
            var hand = this._dealer.Deal(tokens, this._cantTokenToDeal);

            playersInfo[i] = new InfoPlayer<T>(hand, new History<T>(), 0, i);
        }

        List<InfoPlayer<T>>[] team = new List<InfoPlayer<T>>[this._createTeams.Length];

        for (int i = 0; i < team.Length; i++)
        {
            team[i] = new List<InfoPlayer<T>>();
            for (int j = 0; j < this._createTeams[i].Count; j++)
            {
                team[i].Add(playersInfo[this._createTeams[i][j]]);
            }
        }

        return new GameStatus<T>(playersInfo, team, this._table, new[] { 0, 1, 2, 3 }, tokens);
    }
}