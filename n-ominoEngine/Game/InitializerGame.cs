using InfoGame;
using Rules;
using Table;

namespace Game;

public class InitializerGame<T> : IReset<InitializerGame<T>>
{
    /// <summary>
    ///     Cantidad de fichas a repartir
    /// </summary>
    private readonly int _cantTokenToDeal;

    /// <summary>
    ///     Reaprtidor de fichas
    /// </summary>
    private readonly IDealer<T> _dealer;

    /// <summary>
    ///     Generador de fichas
    /// </summary>
    private readonly T[] _generator;

    /// <summary>
    ///     Forma de generar las fichas de fichas
    /// </summary>
    private readonly ITokensMaker<T> _maker;

    /// <summary>
    ///     Tablero a usar
    /// </summary>
    private readonly TableGame<T> _table;

    public InitializerGame(ITokensMaker<T> maker, IDealer<T> dealer,
        TableGame<T> table, T[] generator, int cantDeal)
    {
        _dealer = dealer;
        _maker = maker;
        _table = table;
        _generator = generator;
        _cantTokenToDeal = cantDeal;
    }

    public InitializerGame<T> Reset()
    {
        return new InitializerGame<T>(_maker, _dealer, _table.Reset(), _generator,
            _cantTokenToDeal);
    }

    /// <summary>
    ///     Determinar el estado del juego necesario para iniciar el juego
    /// </summary>
    /// <param name="playerTeams">Distribucion de los jugadores</param>
    /// <returns>Estado del juego</returns>
    public GameStatus<T> StartGame(List<(int, int, string)> playerTeams)
    {
        //Generar las fichas
        var tokens = _maker.MakeTokens(_generator, _table.DimensionToken);
        var playersInfo = new List<InfoPlayer<T>>();

        foreach (var item in playerTeams)
        {
            var hand = _dealer.Deal(tokens, _cantTokenToDeal);
            var aux = new InfoPlayer<T>(hand, new History<T>(), 0, item.Item2, item.Item3);

            playersInfo.Add(aux);
        }

        var teamGame = DeterminateTeams(playerTeams, playersInfo);

        var turns = new int[playersInfo.Count];

        for (var i = 0; i < turns.Length; i++) turns[i] = i;

        var game = new GameStatus<T>(playersInfo, teamGame, _table.Clone(), turns, tokens,
            Array.AsReadOnly(_generator));

        DeterminateLongana(game);

        return game;
    }

    private List<InfoTeams<InfoPlayer<T>>> DeterminateTeams(List<(int, int, string)> playerTeams,
        List<InfoPlayer<T>> playersInfo)
    {
        List<(int team, int Id, int ind)> aux = new();

        //Preprocesamiento
        for (var i = 0; i < playerTeams.Count; i++) aux.Add((playerTeams[i].Item1, playerTeams[i].Item2, i));

        aux.Sort();

        //Asignar equipos
        var teamGame = new List<InfoTeams<InfoPlayer<T>>>();

        var value = -1;

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
        if (aux != null) aux.AssignCantPlayers(game.Players.Count);
    }
}