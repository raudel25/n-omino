using Table;
using Rules;
using Game;

namespace InteractionGui;

public class SelectGamesCreated
{
    private TableGame<int>[] Table => new TableGame<int>[]
    {
        new TableDimension<int>(2),
        new TableLongana<int>(2),
        new TableTriangular<int>(new[] {(0, 0), (1, 1), (2, 0)}),
        new TableSquare<int>(new[] {(0, 0), (0, 2), (2, 2), (2, 0)}),
        new TableHexagonal<int>(new[] {(0, 0), (-1, 1), (0, 2), (2, 2), (3, 1), (2, 0)})
    };

    private ITokensMaker<int>[] Maker => new ITokensMaker<int>[]
    {
        new ClassicTokensMaker<int>(),
        new CircularTokensMaker<int>()
    };

    private ParamSelectFunction<int> Number(int n)
    {
        var aux = new ParamSelectFunction<int>();
        aux.Cant = n;

        return aux;
    }

    private int[][] Generator => new[]
    {
        new SelectGenerator<int>(1).Values[0](Number(7)),
        new SelectGenerator<int>(1).Values[0](Number(10))
    };

    private IsValidRule<int>[] IsValid => new[]
    {
        new IsValidRule<int>(Array.Empty<IValidPlay<int>>(), Array.Empty<ICondition<int>>(),
            new ValidPlayDimension<int>(new ClassicComparison<int>())),
        new IsValidRule<int>(new[] {new ValidPlayLonganaComplement<int>(new ClassicComparison<int>())},
            new[] {new ImmediatePass<int>()}, new ValidPlayLongana<int>(new ClassicComparison<int>())),
        new IsValidRule<int>(Array.Empty<IValidPlay<int>>(), Array.Empty<ICondition<int>>(),
            new ValidPlayGeometry<int>(new ClassicComparison<int>())),
    };

    private BeginGameRule<int>[] Begin => new[]
    {
        new BeginGameRule<int>(new[] {new BeginGameLastWinner<int>()}, new[] {new SecondRoundTournament<int>()},
            new BeginGameRandom<int>()),
        new BeginGameRule<int>(new[] {new BeginGameLastWinner<int>()}, new[] {new SecondRoundTournament<int>()},
            new BeginGameToken<int>(new Token<int>(new[] {6, 6})))
    };

    private StealTokenRule<int> Steal => new StealTokenRule<int>(Array.Empty<IStealToken<int>>(),
        Array.Empty<ICondition<int>>(), new NoStealToken<int>());

    private VisibilityPlayerRule<int> Visibility => new VisibilityPlayerRule<int>(
        Array.Empty<IVisibilityPlayer<int>>(),
        Array.Empty<ICondition<int>>(), new ClassicVisibilityPlayer<int>());

    private ToPassTokenRule<int> ToPass => new ToPassTokenRule<int>(Array.Empty<IToPassToken>(),
        Array.Empty<ICondition<int>>(), new NoToPassToken());

    private TurnPlayerRule<int> Turn => new TurnPlayerRule<int>(Array.Empty<ITurnPlayer>(),
        Array.Empty<ICondition<int>>(), new TurnPlayerClassic());

    private IAssignScoreToken<int> ScoreToken => new AssignScoreTokenClassic();

    private IAssignScorePlayer<int>[] ScorePlayer => new IAssignScorePlayer<int>[]
    {
        new AssignScoreClassic<int>(),
        new AssignScoreHands<int>()
    };

    private IWinnerGame<int>[] Winner => new IWinnerGame<int>[]
    {
        new WinnerGameHigh<int>(),
        new WinnerGameSmall<int>()
    };

    private ICondition<int>[] Conditions => new ICondition<int>[]
    {
        new ClassicWin<int>(),
        new NoValidPlay<int>()
    };

    private AssignScorePlayerRule<int> ScorePlayerRule =>
        new AssignScorePlayerRule<int>(ScorePlayer, Conditions);

    private WinnerGameRule<int> WinnerGameRule => new WinnerGameRule<int>(Winner, Conditions);

    private ReorganizeHandsRule<int> ReorganizeHands => new ReorganizeHandsRule<int>(
        new[] { new HandsTeamWin<int>() }, new[] { new SecondRoundTournament<int>() }, new ClassicReorganize<int>());

    private InfoRules<int>[] Rules => new[]
    {
        new InfoRules<int>(IsValid[0], Visibility, Turn, Steal, ToPass, ScorePlayerRule, WinnerGameRule,
            ScoreToken, Begin[1], ReorganizeHands),
        new InfoRules<int>(IsValid[0], Visibility, Turn, Steal, ToPass, ScorePlayerRule, WinnerGameRule,
            ScoreToken, Begin[0], ReorganizeHands),
        new InfoRules<int>(IsValid[1], Visibility, Turn, Steal, ToPass, ScorePlayerRule, WinnerGameRule,
            ScoreToken, Begin[1], ReorganizeHands),
        new InfoRules<int>(IsValid[1], Visibility, Turn, Steal, ToPass, ScorePlayerRule, WinnerGameRule,
            ScoreToken, Begin[0], ReorganizeHands),
        new InfoRules<int>(IsValid[2], Visibility, Turn, Steal, ToPass, ScorePlayerRule, WinnerGameRule,
            ScoreToken, Begin[0], ReorganizeHands)
    };

    private InitializerGame<int>[] Initializer => new[]
    {
        new InitializerGame<int>(Maker[0], new RandomDealer<int>(), Table[0], Generator[0], 7),
        new InitializerGame<int>(Maker[0], new RandomDealer<int>(), Table[0], Generator[1], 10),
        new InitializerGame<int>(Maker[0], new RandomDealer<int>(), Table[1], Generator[0], 7),
        new InitializerGame<int>(Maker[0], new RandomDealer<int>(), Table[1], Generator[1], 10),
        new InitializerGame<int>(Maker[1], new RandomDealer<int>(), Table[2], Generator[1], 50),
        new InitializerGame<int>(Maker[1], new RandomDealer<int>(), Table[3], Generator[1], 50),
        new InitializerGame<int>(Maker[1], new RandomDealer<int>(), Table[4], Generator[1], 50)
    };

    private Printer[] Printers => new Printer[]
    {
        new PrinterDomino(1000, true),
        new PrinterLongana(1000, true),
        new PrinterGeometry(1000)
    };

    public BuildGame<int>[] Build => new[]
    {
        new BuildGame<int>(Initializer[0], Rules[0], Printers[0]),
        new BuildGame<int>(Initializer[1], Rules[1], Printers[0]),
        new BuildGame<int>(Initializer[2], Rules[2], Printers[1]),
        new BuildGame<int>(Initializer[3], Rules[3], Printers[1]),
        new BuildGame<int>(Initializer[4], Rules[4], Printers[2]),
        new BuildGame<int>(Initializer[5], Rules[4], Printers[2]),
        new BuildGame<int>(Initializer[6], Rules[4], Printers[2])
    };

    public (string, int)[] NameGames => new[]
    {
        ("Juego clásico de dominó (6x6)", 0), ("Juego clásico de dominó (9x9)", 1),
        ("Longana (6x6)", 2), ("Longana (9x9)", 3), ("Tablero triangular", 4),
        ("Tablero cuadrado", 5), ("Tablero hexagonal", 6)
    };
}