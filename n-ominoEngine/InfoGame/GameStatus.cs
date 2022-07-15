using Table;
using System.Collections.ObjectModel;

namespace InfoGame;

public class GameStatus<T>
{
    public List<InfoPlayer<T>> Players { get; set; }
    public List<InfoTeams<InfoPlayer<T>>> Teams { get; set; }
    public TableGame<T> Table { get; set; }
    public int[] Turns { get; set; }
    public ReadOnlyCollection<T> Values { get; set; }

    //cantidad de rondas que lleva el juego
    public int Round { get; set; }

    //Lista de fichas fuera de la mesa
    public List<Token<T>>? TokensTable { get; set; }

    //Determinar cuando se hizo un pase inmediatamente init false
    public bool ImmediatePass { get; set; }

    //Id Jugador que gano init -1
    public int PlayerWinner { get; set; }

    //Id Equipo que gano init -1 
    public int TeamWinner { get; set; }

    /// <summary>
    /// Indice del jugador que comienza el juego
    /// </summary>
    public int PlayerStart { get; set; }

    public Token<T>? TokenStart { get; set; }

    public GameStatus(List<InfoPlayer<T>> players, List<InfoTeams<InfoPlayer<T>>> teams, TableGame<T> table,
        int[] turns,
        List<Token<T>> tokens, ReadOnlyCollection<T> values, int playerStart = -1, bool immediatePass = false)
    {
        this.Players = players;
        this.Teams = teams;
        this.Table = table;
        this.Turns = turns;
        this.TokensTable = tokens;
        this.PlayerWinner = -1;
        this.TeamWinner = -1;
        this.Values = values;
        this.PlayerStart = playerStart;
        this.ImmediatePass = immediatePass;
    }

    public int FindPLayerById(int id)
    {
        for (int i = 0; i < this.Players.Count; i++)
        {
            if (this.Players[i].Id == id) return i;
        }

        return -1;
    }

    //Determinar el equipo al que pertenece un jugador
    public int FindTeamPlayer(int id)
    {
        for (int i = 0; i < this.Teams.Count; i++)
        {
            for (int j = 0; j < this.Teams[i].Count; j++)
            {
                if (this.Teams[i][j].Id == id) return i;
            }
        }

        return -1;
    }

    public GameStatus<T> Clone()
    {
        List<InfoPlayer<T>> players = new List<InfoPlayer<T>>();
        List<InfoTeams<InfoPlayer<T>>> teams = new List<InfoTeams<InfoPlayer<T>>>();

        for (int i = 0; i < this.Players.Count; i++)
        {
            players.Add(this.Players[i].Clone());
        }

        for (int i = 0; i < this.Teams.Count; i++)
        {
            teams.Add(new InfoTeams<InfoPlayer<T>>(this.Teams[i].Id));
            for (int j = 0; j < this.Teams[i].Count; j++)
            {
                teams[teams.Count - 1].Add(players[this.Teams[i][j].Id]);
            }
        }

        return new GameStatus<T>(players, teams, this.Table.Clone(), this.Turns.ToArray(), this.TokensTable!.ToList(), this.Values, 
            this.PlayerStart, this.ImmediatePass);
    }
}