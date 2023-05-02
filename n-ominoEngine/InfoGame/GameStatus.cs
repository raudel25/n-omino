using System.Collections.ObjectModel;
using Table;

namespace InfoGame;

public class GameStatus<T>
{
    public GameStatus(List<InfoPlayer<T>> players, List<InfoTeams<InfoPlayer<T>>> teams, TableGame<T> table,
        int[] turns,
        List<Token<T>> tokens, ReadOnlyCollection<T> values, int playerStart = -1, bool immediatePass = false,
        bool noValid = false)
    {
        Players = players;
        Teams = teams;
        Table = table;
        Turns = turns;
        TokensTable = tokens;
        PlayerWinner = -1;
        TeamWinner = -1;
        Values = values;
        PlayerStart = playerStart;
        ImmediatePass = immediatePass;
        NoValidPlay = noValid;
        LastIndex = 0;
    }

    public List<InfoPlayer<T>> Players { get; set; }

    public List<InfoTeams<InfoPlayer<T>>> Teams { get; set; }

    public TableGame<T> Table { get; set; }

    public int[] Turns { get; set; }

    //Valores que tienen las fichas
    public ReadOnlyCollection<T> Values { get; set; }

    //Lista de fichas fuera de la mesa
    public List<Token<T>> TokensTable { get; set; }

    //Determinar cuando se hizo un pase inmediatamente init false
    public bool ImmediatePass { get; set; }

    //Id Jugador que gano init -1
    public int PlayerWinner { get; set; }

    //Id Equipo que gano init -1 
    public int TeamWinner { get; set; }

    /// <summary>
    ///     Indice del jugador que comienza el juego
    /// </summary>
    public int PlayerStart { get; set; }

    public bool NoValidPlay { get; set; }

    public int LastIndex { get; set; }

    public Token<T>? TokenStart { get; set; }

    /// <summary>
    ///     Determinar el indice de un jugador dado su Id
    /// </summary>
    /// <param name="id">Id del jugador</param>
    /// <returns>Indice del jugador</returns>
    public int FindPLayerById(int id)
    {
        for (var i = 0; i < Players.Count; i++)
            if (Players[i].Id == id)
                return i;

        return -1;
    }

    /// <summary>
    ///     Determinar el indice de un equipo dado su Id
    /// </summary>
    /// <param name="id">Id del Equipo</param>
    /// <returns>Indice del equipo</returns>
    public int FindTeamById(int id)
    {
        for (var i = 0; i < Teams.Count; i++)
            if (Teams[i].Id == id)
                return i;

        return -1;
    }

    //Determinar el equipo al que pertenece un jugador
    public int FindTeamPlayer(int id)
    {
        for (var i = 0; i < Teams.Count; i++)
        for (var j = 0; j < Teams[i].Count; j++)
            if (Teams[i][j].Id == id)
                return i;

        return -1;
    }

    public GameStatus<T> Clone()
    {
        var players = new List<InfoPlayer<T>>();
        var teams = new List<InfoTeams<InfoPlayer<T>>>();

        for (var i = 0; i < Players.Count; i++) players.Add(Players[i].Clone());

        for (var i = 0; i < Teams.Count; i++)
        {
            teams.Add(new InfoTeams<InfoPlayer<T>>(Teams[i].Id));
            for (var j = 0; j < Teams[i].Count; j++)
                teams[teams.Count - 1].Add(players[FindPLayerById(Teams[i][j].Id)]);
        }

        return new GameStatus<T>(players, teams, Table.Clone(), Turns.ToArray(), TokensTable.ToList(),
            Values,
            PlayerStart, ImmediatePass, NoValidPlay);
    }
}