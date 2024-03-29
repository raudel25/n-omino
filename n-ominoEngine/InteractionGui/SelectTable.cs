using Table;

namespace InteractionGui;

public class SelectTable<T> : IVariant<TableGame<T>, T>
{
    public string Description { get; } = "Seleccionar un tablero";

    public List<IVariant<TableGame<T>, T>.Select> Values { get; } = new()
    {
        a => new TableTriangular<T>(new[] { (0, 0), (1, 1), (2, 0) }),
        a => new TableSquare<T>(new[] { (0, 0), (0, 2), (2, 2), (2, 0) }),
        a => new TableHexagonal<T>(new[] { (0, 0), (-1, 1), (0, 2), (2, 2), (3, 1), (2, 0) }),
        a => new TableDimension<T>(2),
        a => new TableLongana<T>(2),
        a => new TableDimension<T>(a.Cant),
        a => new TableLongana<T>(a.Cant)
    };

    public List<ParamSelect> Param { get; } = new()
    {
        new("Triangular", "Tablero triangular", 0),
        new("Cuadrado", "Tablero cuadrado", 1),
        new("Hexagonal", "Tablero hexagonal", 2),
        new("Dominó", "Tablero de dominó", 3),
        new("Longana", "Tablero de longana", 4),
        new("Nomino", "Tablero con fichas de varias caras", 5, false, true),
        new("NLongana", "Longana con fichas de varias caras", 6, false, true)
    };
}