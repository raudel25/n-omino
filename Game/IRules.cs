using Table;

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
    /// <summary>Determinar si es valido jugar una ficah por un nodo</summary>
    /// <param name="node">Nodo por el que se va a jugar</param>
    /// <param name="token">Ficha que se va a jugar</param>
    /// <param name="table">Mesa para jugar</param>
    public bool ValidPlay(Node node, Token token, TableGame table);
}
public interface IAsignScorePlayer
{

}
public interface ITurnPlayer
{

}
#endregion

#region EndGame
public interface IEndPlayer
{

}
public interface IWinnerGame
{

}
#endregion