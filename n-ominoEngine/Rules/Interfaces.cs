using InfoGame;
using Table;

namespace Rules;

#region StartGame

public interface IDealer<T>
{
    /// <summary>Reparte una cantidad de T</summary>
    /// <param name="items">Elementos a repartir</param>
    /// <param name="tokensPerPlayer">Cantidad de elementos que se quieren</param>
    /// <returns>Una lista con lo que repartió</returns>
    public IEnumerable<Hand<T>> Deal(List<Token<T>> items, int[] tokensPerPlayer);
    public Hand<T> Deal(List<Token<T>> items, int cant);
}

public interface ITokensMaker<T>
{
    /// <summary>Genera las fichas</summary>
    /// <param name="values">Valores que tendrán las fichas</param>
    /// <param name="n">Cantidad de caras que tendrá una ficha</param>
    /// <returns>Una lista con las fichas creadas</returns>
    public List<Token<T>> MakeTokens(T[] values, int n);
}

#endregion

#region Game

public interface IAssignScorePlayer<T>
{
    /// <summary>
    /// Determinar la forma de asignar puntos a un jugador
    /// </summary>
    /// <param name="game">Estado del juego</param>
    /// <param name="ind">Indice del jugador</param>
    /// <param name="rules">Reglas del juego</param>
    public void AssignScore(GameStatus<T> game, IAssignScoreToken<T> rules, int ind);
}

public interface IAssignScoreToken<T>
{
    /// <summary>
    /// Determinar el score de una ficha
    /// </summary>
    /// <param name="token">Ficha</param>
    /// <returns>Score de la ficha</returns>
    public int ScoreToken(Token<T> token);
}

public interface IBeginGame<T>
{
    /// <summary>
    /// Determinar como se inicia el juego
    /// </summary>
    /// <param name="tournament">Datos del torneo</param>
    /// <param name="game">Datos del juego</param>
    public void Start(TournamentStatus tournament, GameStatus<T> game);
}

public interface IComparison<T>
{
    /// <summary>Criterio de comparacion</summary>
    /// <param name="a">Entero a comparar</param>
    /// <param name="b">Entero a comparar</param>
    /// <returns>Si el criterio es valido</returns>
    public bool Compare(T a, T b);
}

public interface IReorganizeHands<T>
{
    public void Reorganize(TournamentStatus tournament, GameStatus<T> game);
}

public interface IStealToken<T>
{
    /// <summary>
    /// Cantidad de fichas maximas que puede robar el jugador
    /// </summary>
    int CantMax { get; }

    /// <summary>
    /// Determinar las condiciones bajo las cuales se puede robar en el juego
    /// </summary>
    /// <param name="game">Estado del juego</param>
    /// <param name="ind">Indice del jugador que le corresponde jugar</param>
    /// <param name="rules">Reglas del juego</param>
    /// <param name="original">Estado Original del juego</param>
    /// <param name="play">Determinar si es posible jugar</param>
    public void Steal(GameStatus<T> game, GameStatus<T> original, IsValidRule<T> rules, int ind, ref bool play);
}

public interface IToPassToken
{
    /// <summary>
    /// Determinar si el jugador se puede pasar con fichas
    /// </summary>
    /// <returns></returns>
    public bool ToPass();
}

public interface ITurnPlayer
{
    /// <summary>Determina la distibucion de los turnos de los jugadores</summary>
    /// <param name="turns">Distribucion de los turnos de los jugadores</param>
    /// <param name="ind">Indice del jugador que le corresponde jugar</param>
    public void Turn(int[] turns, int ind);
}

public interface IValidPlay<T>
{
    /// <summary>Determinar si es valido jugar una ficha por un nodo</summary>
    /// <param name="node">Nodo por el que se va a jugar</param>
    /// <param name="token">Ficha que se va a jugar</param>
    /// <param name="game">Estado del juego</param>
    /// <param name="ind">Indice del jugador relativo a la mesa</param>
    /// <returns>Si el criterio es valido</returns>
    public bool ValidPlay(INode<T> node, Token<T> token, GameStatus<T> game, int ind);

    /// <summary>Determinar los valores para asignar al nodo</summary>
    /// <param name="node">Nodo por el que se va a jugar</param>
    /// <param name="token">Ficha que se va a jugar</param>
    /// <param name="table">Mesa para jugar</param>
    /// <returns>Valores a asignar al nodo, retorna una array cuyo primer
    /// elemento es -1 si el criterio no es valido</returns>
    public T[] AssignValues(INode<T> node, Token<T> token, TableGame<T> table);
}

public interface IVisibilityPlayer<T>
{
    /// <summary>
    /// Determinar la visibilidad de los jugadores sobre las fichas del juego
    /// </summary>
    /// <param name="game">Estado del juego</param>
    /// <param name="ind">Indice del jugador que le corresponde jugar</param>
    public void Visibility(GameStatus<T> game, int ind);
}

public interface IWinnerGame<T>
{
    /// <summary>
    /// Determinar el ganador del juego
    /// </summary>
    /// <param name="game">Estado del juego</param>
    /// <param name="ind">Indice del jugador que le corresponde jugar</param>
    public void Winner(GameStatus<T> game, int ind);
}

#endregion

#region Tournamnet

public interface IDistributionPlayer
{
    /// <summary>
    /// Determinar la distribucion de los players en el juego
    /// </summary>
    /// <param name="tournament">Estado del torneo</param>
    public void DeterminateDistribution(TournamentStatus tournament);
}

public interface IPlayerGame
{
    public void DeterminatePlayers(TournamentStatus tournament);
}

public interface IScorePlayerTournament<T>
{
    /// <summary>
    /// Asignar score a los jugadores en el torneo
    /// </summary>
    /// <param name="tournament">Estado del torneo</param>
    /// <param name="game">Estado del actual juego</param>
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game);
}

public interface IScoreTeamTournament<T>
{
    /// <summary>
    /// Asignar score a los equipos durante el torneo
    /// </summary>
    /// <param name="tournament">Estado del torneo</param>
    /// <param name="game">Estado del juego actual</param>
    /// <param name="rules">Reglas del actual juego</param>
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules);
}

public interface ITeamsGame
{
    public void DeterminateTeams(TournamentStatus tournament);
}

public interface IWinnerTournament
{
    /// <summary>
    /// Determinar el ganador de un torneo
    /// </summary>
    /// <param name="tournament">Estado del torneo</param>
    public void DeterminateWinner(TournamentStatus tournament);
}

#endregion


