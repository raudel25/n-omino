using Table;
using Rules;
using Game;

namespace InteractionGui;

public class SelectGamesCreated
{
    private static TableGame<int>[] _table = new TableGame<int>[]
    {
        new TableDimension<int>(2),
        new TableLongana<int>(2),
        new TableTriangular<int>(new[] {(0, 0), (1, 1), (2, 0)}),
        new TableSquare<int>(new[] {(0, 0), (0, 2), (2, 2), (2, 0)}),
        new TableHexagonal<int>(new[] {(0, 0), (-1, 1), (0, 2), (2, 2), (3, 1), (2, 0)})
    };

    private static ITokensMaker<int>[] _maker = new ITokensMaker<int>[]
    {
        new ClassicTokensMaker<int>(),
        new CircularTokensMaker<int>()
    };

    private static ParamSelectFunction<int> Number(int n)
    {
        var aux = new ParamSelectFunction<int>();
        aux.Cant = n;
        
        return aux;
    }

    private static int[][] _generator = new int[][]
    {
        new SelectGenerator<int>(1).Values[0](Number(7)),
        new SelectGenerator<int>(1).Values[0](Number(10))
    };

    private static IsValidRule<int>[] _isValid = new IsValidRule<int>[]
    {
        new IsValidRule<int>(Array.Empty<IValidPlay<int>>(), Array.Empty<ICondition<int>>(),
            new ValidPlayDimension<int>(new ClassicComparison<int>())),
        new IsValidRule<int>(Array.Empty<IValidPlay<int>>(), Array.Empty<ICondition<int>>(),
            new ValidPlayLongana<int>(new ClassicComparison<int>())),
        new IsValidRule<int>(Array.Empty<IValidPlay<int>>(), Array.Empty<ICondition<int>>(),
            new ValidPlayGeometry<int>(new ClassicComparison<int>())),
    };

    private static BeginGameRule<int>[] _begin = new BeginGameRule<int>[]
    {
        new BeginGameRule<int>(Array.Empty<IBeginGame<int>>(), Array.Empty<ICondition<int>>(),
            new BeginGameRandom<int>()),
        new BeginGameRule<int>(Array.Empty<IBeginGame<int>>(), Array.Empty<ICondition<int>>(),
            new BeginGameToken<int>(new Token<int>(new[] {6, 6})))
    };

    private static StealTokenRule<int> _steal = new StealTokenRule<int>(Array.Empty<IStealToken<int>>(),
        Array.Empty<ICondition<int>>(), new NoStealToken<int>());
    
    private static VisibilityPlayerRule<int> _visibility = new VisibilityPlayerRule<int>(Array.Empty<IVisibilityPlayer<int>>(),
        Array.Empty<ICondition<int>>(), new ClassicVisibilityPlayer<int>());
    
    private static ToPassTokenRule<int> _toPass = new ToPassTokenRule<int>(Array.Empty<IToPassToken>(),
        Array.Empty<ICondition<int>>(), new NoToPassToken());
    
    private static TurnPlayerRule<int> _turn = new TurnPlayerRule<int>(Array.Empty<ITurnPlayer>(),
        Array.Empty<ICondition<int>>(), new TurnPlayerClassic());

    private static IAssignScoreToken<int> _scoreToken = new AssignScoreTokenClassic();

    private static IAssignScorePlayer<int>[] _scorePlayer = new IAssignScorePlayer<int>[]
    {
        new AssignScoreClassic<int>(),
        new AssignScoreHands<int>()
    };

    private static IWinnerGame<int>[] _winner = new IWinnerGame<int>[]
    {
        new WinnerGameHigh<int>(),
        new WinnerGameSmall<int>()
    };

    private static ICondition<int>[] _conditions = new ICondition<int>[]
    {
        new ClassicWin<int>(),
        new NoValidPlay<int>()
    };

    private static AssignScorePlayerRule<int> _scorePlayerRule = new AssignScorePlayerRule<int>(_scorePlayer, _conditions);

    private static WinnerGameRule<int> _winnerGameRule = new WinnerGameRule<int>(_winner, _conditions);

    private static InfoRules<int>[] _rules = new InfoRules<int>[]
    {
        new InfoRules<int>(_isValid[0], _visibility, _turn, _steal, _toPass, _scorePlayerRule, _winnerGameRule,
            _scoreToken, _begin[1]),
        new InfoRules<int>(_isValid[0], _visibility, _turn, _steal, _toPass, _scorePlayerRule, _winnerGameRule,
            _scoreToken, _begin[0]),
        new InfoRules<int>(_isValid[1], _visibility, _turn, _steal, _toPass, _scorePlayerRule, _winnerGameRule,
            _scoreToken, _begin[1]),
        new InfoRules<int>(_isValid[1], _visibility, _turn, _steal, _toPass, _scorePlayerRule, _winnerGameRule,
            _scoreToken, _begin[0]),
        new InfoRules<int>(_isValid[2], _visibility, _turn, _steal, _toPass, _scorePlayerRule, _winnerGameRule,
            _scoreToken, _begin[0]),
    };

    private static InitializerGame<int>[] _initializer = new InitializerGame<int>[]
    {
        new InitializerGame<int>(_maker[0], new RandomDealer<int>(), _table[0], _generator[0], 7),
        new InitializerGame<int>(_maker[0], new RandomDealer<int>(), _table[0], _generator[1], 10),
        new InitializerGame<int>(_maker[0], new RandomDealer<int>(), _table[1], _generator[0], 7),
        new InitializerGame<int>(_maker[0], new RandomDealer<int>(), _table[1], _generator[1], 10),
        new InitializerGame<int>(_maker[1], new RandomDealer<int>(), _table[2], _generator[1], 50),
        new InitializerGame<int>(_maker[1], new RandomDealer<int>(), _table[3], _generator[1], 50),
        new InitializerGame<int>(_maker[1], new RandomDealer<int>(), _table[4], _generator[1], 50)
    };

    private static Printer[] _printers = new Printer[]
    {
        new PrinterDomino(1000, true),
        new PrinterLongana(1000, true),
        new PrinterGeometry(1000)
    };

    public readonly BuildGame<int>[] Build = new BuildGame<int>[]
    {
        new BuildGame<int>(_initializer[0], _rules[0], _printers[0]),
        new BuildGame<int>(_initializer[1], _rules[1], _printers[0]),
        new BuildGame<int>(_initializer[2], _rules[2], _printers[1]),
        new BuildGame<int>(_initializer[3], _rules[3], _printers[1]),
        new BuildGame<int>(_initializer[4], _rules[4], _printers[2]),
        new BuildGame<int>(_initializer[5], _rules[4], _printers[2]),
        new BuildGame<int>(_initializer[6], _rules[4], _printers[2])
    };

    public readonly (string, int)[] NameGames = new[]
    {
        ("Juego clasico de domino (6x6)", 0), ("Juego clasico de domino (9x9)", 1),
        ("Longana (6x6)", 2), ("Longana (9x9)", 3), ("Tablero triangular", 4),
        ("Tablero cuadrado", 5), ("Tablero hexagonal", 6)
    };
}