using Table;

namespace InfoGame;

public class GameStatus<T>
{
    public InfoPlayer<T>[] Players;
    public List<InfoPlayer<T>>[] Teams;
    public TableGame<T> Table;
    public int[] Turns { get; set; }

    //Lista de fichas fuera de la mesa
    public List<Token<T>>? TokensTable { get; set; }

    //Determinar si no se puede realizar una jugada valida por cada uno de los jugadores init false
    public bool NoValidPlay { get; set; }

    //Determinar cuando se hizo un pase inmediatamente init false
    public bool InmediatePass { get; set; }

    //Id Jugador que gano init -1
    public int PlayerWinner { get; set; }

    //Id Equipo que gano init -1 
    public int TeamWinner { get; set; }

    /// <summary>
    /// Indice del jugador que comienza el juego
    /// </summary>
    public int PlayerStart { get; set; }

    public Token<T>? TokenStart { get; set; }

    public GameStatus(InfoPlayer<T>[] players, List<InfoPlayer<T>>[] teams, TableGame<T> table, int[] turns,
        List<Token<T>> tokens)
    {
        this.Players = players;
        this.Teams = teams;
        this.Table = table;
        this.Turns = turns;
        this.TokensTable = tokens;
        this.PlayerWinner = -1;
        this.TeamWinner = -1;
        this.PlayerStart = -1;
    }

    //Determinar el equipo al que pertenece un jugador
    public int FindTeamPlayer(int id)
    {
        for (int i = 0; i < this.Teams.Length; i++)
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
        InfoPlayer<T>[] players = new InfoPlayer<T>[this.Players.Length];
        List<InfoPlayer<T>>[] teams = new List<InfoPlayer<T>>[this.Teams.Length];
        for (int i = 0; i < this.Players.Length; i++)
        {
            players[i] = this.Players[i].Clone();
        }

        for (int i = 0; i < this.Teams.Length; i++)
        {
            teams[i] = new List<InfoPlayer<T>>();
            for (int j = 0; j < this.Teams[i].Count; j++)
            {
                teams[i].Add(players[this.Teams[i][j].Id]);
            }
        }

        return new GameStatus<T>(players, teams, this.Table.Clone(), this.Turns.ToArray(), this.TokensTable!.ToList());
    }
}