using Table;

namespace InteractionGui;

public class SelectTable<T> : IVariant<TableGame<T>,T>
{
    public string Description { get; } = "Seleccionar un tablero";

    public List<IVariant<TableGame<T>,T>.Select> Values { get; }= new List<IVariant<TableGame<T>, T>.Select>()
    {
        (a) => new TableTriangular<T>(new[] {(0, 0), (1, 1), (2, 0)}),
        (a) => new TableSquare<T>(new[] {(0, 0), (0, 2), (2, 2), (2, 0)}),
        (a) => new TableHexagonal<T>(new[] {(0, 0), (-1, 1), (0, 2), (2, 2), (3, 1), (2, 0)}),
        (a) => new TableDimension<T>(2)
    };

    public List<ParamSelect> Param { get; } = new List<ParamSelect>()
    {
        new ParamSelect("Triangular", "Tablero triangular", 0), new ParamSelect("Cuadrado", "Tablero cuadrado", 1),
        new ParamSelect("Hexagonal", "Tablero hexagonal", 2),
        new ParamSelect("Domino", "Tablero de domino", 3)
    };
}