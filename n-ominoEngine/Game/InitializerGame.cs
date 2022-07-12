using Table;
using InfoGame;
using Rules;

namespace Game;

public class InitializerGame<T>
{
    /// <summary>
    /// Forma de generar las fichas de fichas
    /// </summary>
    private ITokensMaker<T> _maker;

    /// <summary>
    /// Reaprtidor de fichas
    /// </summary>
    private IDealer<T> _dealer;

    /// <summary>
    /// Tablero a usar
    /// </summary>
    private TableGame<T> _table;

    /// <summary>
    /// Cantidad de fichas a repartir
    /// </summary>
    private int _cantTokenToDeal;

    /// <summary>
    /// Generador de fichas
    /// </summary>
    private T[] _generator;

    public InitializerGame(ITokensMaker<T> maker, IDealer<T> dealer,
        TableGame<T> table, T[] generator, int cantDeal)
    {
        this._dealer = dealer;
        this._maker = maker;
        this._table = table;
        this._generator = generator;
        this._cantTokenToDeal = cantDeal;
    }

    /// <summary>
    /// Determinar el estado del juego necesario para iniciar el juego
    /// </summary>
    /// <param name="playerTeams">Distribucion de los jugadores</param>
    /// <returns>Estado del juego</returns>
    public GameStatus<T> StartGame(List<(int, int)> playerTeams)
    {
        //Generar las fichas
        List<Token<T>> tokens = this._maker.MakeTokens(this._generator, this._table.DimensionToken);
        List<InfoPlayer<T>> playersInfo = new List<InfoPlayer<T>>();
        
        foreach (var item in playerTeams)
        {
            var hand = this._dealer.Deal(tokens, this._cantTokenToDeal);
            var aux = new InfoPlayer<T>(hand, new History<T>(), 0, item.Item2);

            playersInfo.Add(aux);
        }
        
        List<InfoTeams<InfoPlayer<T>>> teamGame = DeterminateTeams(playerTeams,playersInfo);

        var turns = new int[playersInfo.Count];

        for (int i = 0; i < turns.Length; i++) turns[i] = i;

        var game = new GameStatus<T>(playersInfo, teamGame, this._table.Clone(), turns, tokens);

        DeterminateLongana(game);
        
        return game;
    }

    private List<InfoTeams<InfoPlayer<T>>> DeterminateTeams(List<(int, int)> playerTeams,List<InfoPlayer<T>> playersInfo)
    {
        List<(int, int, int)> aux = new List<(int, int, int)>();

        //Preprocesamiento
        for (int i = 0; i < playerTeams.Count; i++)
        {
            aux.Add((playerTeams[i].Item1,playerTeams[i].Item2,i));
        }
        
        aux.Sort();

        //Asignar equipos
        List<InfoTeams<InfoPlayer<T>>> teamGame = new List<InfoTeams<InfoPlayer<T>>>();
        
        int value = -1;

        foreach (var item in aux)
        {
            if (value != item.Item1)
            {
                teamGame.Add(new InfoTeams<InfoPlayer<T>>(item.Item1));
                value = item.Item1;
            }

            teamGame[teamGame.Count - 1].Add(playersInfo[item.Item3]);
        }

        return teamGame;
    }

    private void DeterminateLongana(GameStatus<T> game)
    {
        var aux = game.Table as TableLongana<T>;
        if (aux != null)
        {
            aux.AssignCantPlayers(game.Players.Count);
        }
    }
}