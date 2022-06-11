using Table;
using InfoGame;

namespace Rules;

#region StartGame

public interface ICreateToken
{
    public int[,] Create(int n);
}

public interface IAssignTokenPlayer
{
}

#endregion