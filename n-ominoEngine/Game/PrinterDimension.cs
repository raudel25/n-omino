using InfoGame;
using Table;

namespace Game;

public class PrinterDimension : Printer
{
    public PrinterDimension(int speed) : base(speed)
    {
    }

    public override void LocationTable<T>(TableGame<T> table)
    {
        Thread.Sleep(Speed);

        ExecuteTableEvent(
            AssignValues(TokensPlayNode(table), 1, table.DimensionToken + 1, TypeToken.NDimension));
    }

    public override void LocationHand<T>(InfoPlayer<T> player, Token<T>? play, TableGame<T> table)
    {
        DeterminateLocationHand(play, table, player, 1, table.DimensionToken + 1, TypeToken.NDimension);

        Thread.Sleep(Speed);
    }

    private IEnumerable<Token<T>> TokensPlayNode<T>(TableGame<T> table)
    {
        foreach (var item in table.PlayNode) yield return item.ValueToken;
    }

    public override Printer Reset()
    {
        return new PrinterDimension(Speed);
    }
}