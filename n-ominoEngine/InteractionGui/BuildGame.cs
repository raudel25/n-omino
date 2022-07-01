using Rules;
using Table;

namespace InteractionGui;

public class BuildGame<T>
{
    public ITokensMaker<T> _maker;
    public IDealer<T> _dealer;
    public TableGame<T> _table;
    public int _cantTokenToDeal;
    public T[] _generator;
}