using Table;
using InfoGame;

namespace Game;

public class PrinterDimension : Printer
{
    public PrinterDimension(int speed) : base(speed)
    {
    }

    public override void LocationTable<T>(TableGame<T> table)
    {
        Thread.Sleep(this.Speed);

        Printer.ExecuteTableEvent(
            AssignValues(TokensPlayNode(table), 1, table.DimensionToken + 1, TypeToken.NDimension));
    }

    public override void LocationHand<T>(InfoPlayer<T> player, Token<T>? play, TableGame<T> table)
    {
        DeterminateLocationHand(play, table, player, 1, table.DimensionToken + 1, TypeToken.NDimension);

        Thread.Sleep(this.Speed);
    }

    private IEnumerable<Token<T>> TokensPlayNode<T>(TableGame<T> table)
    {
        foreach (var item in table.PlayNode)
        {
            yield return item.ValueToken;
        }
    }
}