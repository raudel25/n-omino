using Game;
using InfoGame;
using Table;
using Player;
using Rules;
using InteractionGui;

namespace Game;

public static class Test
{
    public static Judge<int> Game()
    {
        // TableGeometry<int> table = new TableSquare<int>(new[] { (0, 0), (0, 2), (2, 2), (2, 0) });
        TableGeometry<int> table = new TableHexagonal<int>(new[] { (0, 0), (-1, 1), (0, 2), (2, 2), (3, 1), (2, 0) });
        // TableGame<int> table = new TableTriangular<int>(new[] { (0, 0), (1, 1), (2, 0) });
        // TableDimension<int> table = new TableDimension<int>(2);
        int[] array = new int[10];
        for (int i = 0; i < 10; i++)
        {
            array[i] = i;
        }

        //Jugadores
        ITokensMaker<int> maker = new CircularTokensMaker<int>();

        // List<Token<int>> tokens = maker.MakeTokens(array, 4);

        IDealer<int> dealer = new RandomDealer<int>();

        InfoPlayer<int>[] playersInfo = new InfoPlayer<int>[4];

        // for (int i = 0; i < 4; i++)
        // {
        //     var anabel = dealer.Deal(tokens, 50);

        //     playersInfo[i] = new InfoPlayer<int>(anabel, new History<int>(), 0, i);
        // }

        Player.Player<int>[] players = new Player<int>[4];

        IStrategy<int> strategy = new RandomPlayer<int>();

        for (int i = 0; i < 4; i++)
        {
            players[i] = new PurePlayer<int>(i, strategy, strategy);
        }

        // List<InfoPlayer<int>>[] team = new List<InfoPlayer<int>>[4];

        // for (int i = 0; i < 4; i++)
        // {
        //     team[i] = new List<InfoPlayer<int>>();
        //     team[i].Add(playersInfo[i]);
        // }

        List<int>[] op = new[] { new List<int>() { 0 }, new List<int>() { 1 }, new List<int>() { 2 }, new List<int>() { 3 } };
        InitializerGame<int> init = new InitializerGame<int>(maker, dealer, table, array, 50);

        //GameStatus<int> game = new GameStatus<int>(playersInfo, team, table, new[] { 0, 1, 2, 3 }, tokens);
        GameStatus<int> game = init.StartGame(new List<(int, int)>() { (0, 0), (1, 1), (2, 2), (3, 3) });

        IValidPlay<int> valid = new ValidPlayGeometry<int>(new GcdComparison(1));
        IValidPlay<int> valid3 = new ValidPlayGeometry<int>(new ClassicComparison<int>());
        IValidPlay<int> valid2 = new ValidPlayGeometry<int>(new ComodinComparison(0));
        IValidPlay<int> valid4 = new ValidPlayGeometry<int>(new HighNumberComparison(4));
        IValidPlay<int> valid5 = new ValidPlayDimension<int>(new ClassicComparison<int>());
        ITurnPlayer turn = new TurnPlayerClassic();
        IAssignScorePlayer<int> scorePlayer = new AssignScoreClassic<int>();
        IAssignScorePlayer<int> scorePlayerNo = new AssignScoreHands<int>();
        IWinnerGame<int> winnerGame = new WinnerGameHigh<int>();
        IWinnerGame<int> winnerGameTranque = new WinnerGameSmall<int>();
        IVisibilityPlayer<int> visibilityPlayer = new ClassicVisibilityPlayer<int>();
        IStealToken<int> steal = new NoStealToken<int>();
        IStealToken<int> stealToken = new ClassicStealToken<int>();
        IAssignScoreToken<int> scoreToken = new AssignScoreTokenClassic();

        ICondition<int> conditionWin = new ClassicWin<int>();
        ICondition<int> condition = new ConditionDefault<int>();
        ICondition<int> conditionTranque = new NoValidPlayFirstPlayerPass<int>();
        ICondition<int> conditionNoValid = new NoValidPlay<int>();
        ICondition<int> conditionToPass = new ImmediatePass<int>();
        ICondition<int> conditionSum = new SumFreeNode(5, new DivisibleComparison(5));
        ITurnPlayer turnPass = new TurnPlayerInvert();

        IsValidRule<int> isValidRule = new IsValidRule<int>(new[] { valid3 }, new[] { condition }, valid3);

        TurnPlayerRule<int> turnPlayerRule = new TurnPlayerRule<int>(new[] { turn }, new[] { conditionToPass }, turn);

        VisibilityPlayerRule<int> visibilityPlayerRule =
            new VisibilityPlayerRule<int>(new[] { visibilityPlayer }, new[] { condition }, visibilityPlayer);

        IAssignScorePlayer<int> sum = new AssignScoreSumFreeNode();

        StealTokenRule<int> stealTokenRule = new StealTokenRule<int>(new[] { steal }, new[] { conditionToPass }, steal);

        AssignScorePlayerRule<int> assignScorePlayerRule =
            new AssignScorePlayerRule<int>(new[] { sum }, new[] { conditionSum });

        WinnerGameRule<int> winnerGameRule = new WinnerGameRule<int>(new[] { winnerGame, winnerGameTranque },
            new[] { conditionWin, conditionNoValid });

        IBeginGame<int> beginGame = new BeginGameToken<int>(new Token<int>(new[] { 6, 6,6,6,6,6 }));

        BeginGameRule<int> beginGameRule = new BeginGameRule<int>(new[] { beginGame }, new[] { condition }, beginGame);

        Printer print = new PrinterGeometry(1000);
        // Printer print = new PrinterDomino(1000, true);
        
        ToPassTokenRule<int> qwe=new ToPassTokenRule<int>(new[] {new NoToPassToken()}, new[] {new ConditionDefault<int>()}, new NoToPassToken());

        InfoRules<int> rules = new InfoRules<int>(isValidRule, visibilityPlayerRule, turnPlayerRule, stealTokenRule,
            qwe,
            assignScorePlayerRule, winnerGameRule, scoreToken, beginGameRule);

        Judge<int> judge = new Judge<int>(new TournamentStatus(new List<InfoPlayerTournament>(), new List<InfoTeams<InfoPlayerTournament>>()), rules, game, players, print);


        return judge;
    }
}