using Table;
using InfoGame;

namespace Game;

public class InitializerGame<T>
{
    private ITokensMaker<T> _maker;
    private IDealer<T> _dealer;
    private TableGame<T> _table;
    private int _dimensionToken;
    private int _cantTokenToDeal;
    private T[] _generator;

    public InitializerGame(ITokensMaker<T> maker, IDealer<T> dealer,
        TableGame<T> table,
        T[] generator, int cantDeal, int dimension)
    {
        this._dealer = dealer;
        this._maker = maker;
        this._table = table;
        this._generator = generator;
        this._dimensionToken = dimension;
        this._cantTokenToDeal = cantDeal;
    }

    public GameStatus<T> StartGame(List<int> players,List<int>[] teams)
    {
        List<Token<T>> tokens = this._maker.MakeTokens(this._generator, this._dimensionToken);
        InfoPlayer<T>[] playersInfo = new InfoPlayer<T>[players.Count];

        for (int i = 0; i < players.Count; i++)
        {
            var hand = this._dealer.Deal(tokens, this._cantTokenToDeal);

            playersInfo[i] = new InfoPlayer<T>(hand, new History<T>(), 0, players[i]);
        }

        List<InfoPlayer<T>>[] teamGame = new List<InfoPlayer<T>>[teams.Length];

        for (int i = 0; i < teamGame.Length; i++)
        {
            teamGame[i] = new List<InfoPlayer<T>>();
            for (int j = 0; j < teams[i].Count; j++)
            {
                teamGame[i].Add(playersInfo[teams[i][j]]);
            }
        }

        return new GameStatus<T>(playersInfo, teamGame, this._table.Clone(), new[] {0, 1, 2, 3}, tokens);
    }
}