using Table;
using InfoGame;

namespace Rules;

#region StartGame

public interface ICreateToken
{
    public int[,] Create(int n);
}

public interface IAsignTokenPlayer
{
}

#endregion

#region Game

public interface IValidPlay
{
    /// <summary>Determinar si es valido jugar una ficha por un nodo</summary>
    /// <param name="node">Nodo por el que se va a jugar</param>
    /// <param name="token">Ficha que se va a jugar</param>
    /// <param name="table">Mesa para jugar</param>
    /// <returns>Si el criterio es valido</returns>
    public bool ValidPlay(INode node, Token token, TableGame table);

    /// <summary>Determinar los valores para asignar al nodo</summary>
    /// <param name="node">Nodo por el que se va a jugar</param>
    /// <param name="token">Ficha que se va a jugar</param>
    /// <param name="table">Mesa para jugar</param>
    /// <returns>Valores a asignar al nodo, retorna una array cuyo primer
    /// elemento es -1 si el criterio no es valido</returns>
    public int[] AsignValues(INode node, Token token, TableGame table);
}

public interface IAsignScorePlayer
{
}

public interface ITurnPlayer
{
    /// <summary>Determina la distibucion de los nodos de los jugadores</summary>
    /// <param name="turns">Distribucion de los turnos de los jugadores</param>
    /// <param name="ind">Indice actual</param>
    /// <returns>Nueva distribucion de los jugadores</returns>
    public int[] Turn(int[] turns, int ind);
}

public interface IVisibilityPlayer
{
    /// <summary>
    /// Determinar la visibilidad de los jugadores sobre las fichas del juego
    /// </summary>
    /// <param name="game">Estado del juego</param>
    /// <param name="player">ID del jugador que le corresponde jugar</param>
    public void Visibility(GameStatus game, int player);
}

public interface IStealToken
{
    /// <summary>
    /// Cantidad de fichas minimas que puede robar el jugador
    /// </summary>
    int CantMin { get; }

    /// <summary>
    /// Cantidad de fichas maximas que puede robar el jugador
    /// </summary>
    int CantMax { get; }

    /// <summary>
    /// Determinar las condiciones bajo las cuales se puede robar en el juego
    /// </summary>
    /// <param name="game">Estado del juego</param>
    /// <param name="player">ID del jugador que le corresponde jugar</param>
    /// <param name="rules">Reglas del juego</param>
    /// /// <param name="original">Estado Original del juego</param>
    public void Steal(GameStatus game, GameStatus original, InfoRules rules, int player);
}

#endregion

#region EndGame

public interface IEndGame
{
}

public interface IWinnerGame
{
}

#endregion