using Table;

namespace InteractionGui;

public class SelectTableInt : ISelectVariantGui<TableGame<int>, int>
{
    public List<TableGame<int>> ValueParam { get; } = new List<TableGame<int>>();

    public ISelectVariantGui<TableGame<int>, int>.Select[] Values { get; } =
        new ISelectVariantGui<TableGame<int>, int>.Select[]
        {
            ((comparison, a, b) => (new TableTriangular<int>(new[] {(0, 0), (1, 1), (2, 0)}))),
            ((comparison, a, b) => (new TableSquare<int>(new[] {(0, 0), (0, 2), (2, 2), (2, 0)}))),
            ((comparison, a, b) => (new TableHexagonal<int>(new[] {(0, 0), (-1, 1), (0, 2), (2, 2), (3, 1), (2, 0)}))),
            ((comparison, a, b) => (new TableDimension<int>(2)))
        };

    public ParamSelect[] Param { get; } = new ParamSelect[]
    {
        new ParamSelect("Triangular", "Tablero triangular", 0), new ParamSelect("Cuadrado", "Tablero cuadrado", 1),
        new ParamSelect("Hexagonal", "Tablero hexagonal", 2),
        new ParamSelect("Domino", "Tablero de domino", 3)
    };
}