using Game;
using InfoGame;
using Table;
using Player;
using Rules;

namespace Game;

public static class Test
{
    public static Judge<int> Game()
    {
        // TableGeometry<int> table = new TableSquare<int>(new[] { (0, 0), (0, 2), (2, 2), (2, 0) });
        // TableGeometry<int> table = new TableHexagonal<int>(new[] { (0, 0), (-1, 1), (0, 2), (2, 2), (3, 1), (2, 0) });
        // TableGame<int> table = new TableTriangular<int>(new[] { (0, 0), (1, 1), (2, 0) });
        TableDimension<int> table = new TableDimension<int>(2);
        // TableLongana<int> table = new TableLongana<int>(2);
        int[] array = new int[7];
        for (int i = 0; i < 7; i++)
        {
            array[i] = i;
        }

        //Jugadores
        ITokensMaker<int> maker = new ClassicTokensMaker<int>();

        // List<Token<int>> tokens = maker.MakeTokens(array, 2);

        IDealer<int> dealer = new RandomDealer<int>();

        // InfoPlayer<int>[] playersInfo = new InfoPlayer<int>[4];

        // for (int i = 0; i < 4; i++)
        // {
        //     var anabel = dealer.Deal(tokens, 7);

        //     playersInfo[i] = new InfoPlayer<int>(anabel, new History<int>(), 0, i);
        // }

        ICondition<int> cond = new PostRoundCondition<int>(5);
        ICondition<int> cond1 = new HigherThanScoreHandCondition<int>(10);
        IStrategy<int> random = new RandomPlayer<int>();
        IStrategy<int> botagorda = new GreedyPlayer<int>();

        List<Player.Player<int>> players = new List<Player<int>>();

        for (int i = 0; i < 4; i++)
        {
            players.Add(new RandomStrategyPlayer<int>(new IStrategy<int>[] { }, new ICondition<int>[] { }, random, i));
        }

        // List<InfoPlayer<int>>[] team = new List<InfoPlayer<int>>[4];

        // for (int i = 0; i < 4; i++)
        // {
        //     team[i] = new List<InfoPlayer<int>>();
        //     team[i].Add(playersInfo[i]);
        // }

        List<int>[] op = new[] { new List<int>() { 0 }, new List<int>() { 1 }, new List<int>() { 2 }, new List<int>() { 3 } };
        InitializerGame<int> init = new InitializerGame<int>(maker, dealer, table, array, 7);

        //GameStatus<int> game = new GameStatus<int>(playersInfo, team, table, new[] { 0, 1, 2, 3 }, tokens);
        GameStatus<int> game = init.StartGame(new List<(int, int)> { (0, 0), (1, 1), (0, 2), (1, 3) });

        IValidPlay<int> valid5 = new ValidPlayDimension<int>(new ClassicComparison<int>());
        ITurnPlayer turn = new TurnPlayerClassic();
        IAssignScorePlayer<int> scorePlayer = new AssignScoreClassic<int>();
        IAssignScorePlayer<int> scorePlayerNo = new AssignScoreHands<int>();
        IWinnerGame<int> winnerGame = new WinnerGameHigh<int>();
        IWinnerGame<int> winnerGameTranque = new WinnerGameSmall<int>();
        IVisibilityPlayer<int> visibilityPlayer = new ClassicVisibilityPlayer<int>();
        IStealToken<int> steal = new NoStealToken<int>();
        IAssignScoreToken<int> scoreToken = new AssignScoreTokenClassic();

        ICondition<int> conditionWin = new ClassicWin<int>();
        ICondition<int> condition = new ConditionDefault<int>();
        ICondition<int> conditionTranque = new NoValidPlayFirstPlayerPass<int>();
        ICondition<int> conditionNoValid = new NoValidPlay<int>();
        ICondition<int> conditionToPass = new ImmediatePass<int>();
        ICondition<int> conditionSum = new SumFreeNode(5, new DivisibleComparison(5));
        ITurnPlayer turnPass = new TurnPlayerInvert();

        IsValidRule<int> isValidRule = new IsValidRule<int>(new[] { valid5 }, new[] { condition }, valid5);

        TurnPlayerRule<int> turnPlayerRule = new TurnPlayerRule<int>(new[] { turn }, new[] { conditionToPass }, turn);

        VisibilityPlayerRule<int> visibilityPlayerRule =
            new VisibilityPlayerRule<int>(new[] { visibilityPlayer }, new[] { condition }, visibilityPlayer);

        IAssignScorePlayer<int> sum = new AssignScoreSumFreeNode();

        StealTokenRule<int> stealTokenRule = new StealTokenRule<int>(new[] { steal }, new[] { conditionToPass }, steal);

        AssignScorePlayerRule<int> assignScorePlayerRule =
            new AssignScorePlayerRule<int>(new[] { sum }, new[] { conditionSum });

        WinnerGameRule<int> winnerGameRule = new WinnerGameRule<int>(new[] { winnerGame, winnerGameTranque },
            new[] { conditionWin, conditionNoValid });

        //IBeginGame<int> beginGame = new BeginGameToken<int>(new Token<int>(new[] { 6, 6 }));

        IBeginGame<int> beginGame = new BeginGameRandom<int>();

        BeginGameRule<int> beginGameRule = new BeginGameRule<int>(new[] { beginGame }, new[] { condition }, beginGame);

        // Printer print = new PrinterGeometry(1000);
        Printer print = new PrinterDomino(1000, true);
        //Printer print = new PrinterLongana(1000, true);
        // Printer print = new PrinterDimension(1000);

        ToPassTokenRule<int> qwe = new ToPassTokenRule<int>(new[] { new NoToPassToken() }, new[] { new ConditionDefault<int>() }, new NoToPassToken());

        InfoRules<int> rules = new InfoRules<int>(isValidRule, visibilityPlayerRule, turnPlayerRule, stealTokenRule,
            qwe,
            assignScorePlayerRule, winnerGameRule, scoreToken, beginGameRule);

        Judge<int> judge = new Judge<int>(new TournamentStatus(new List<InfoPlayerTournament>(), new List<InfoTeams<InfoPlayerTournament>>()), rules, game, players, print);


        return judge;
    }

    public static JudgeTournament<int> Tournament(BuildGame<int> game)
    {
        var player = new ClassicPlayerGame();

        var playerRule = new PlayerGameRule<int>(Array.Empty<IPlayerGame>(), Array.Empty<ICondition<int>>(), player);

        var score = new ClassicScorePlayerTournament<int>();

        var scoreRule = new ScorePlayerTournamentRule<int>(Array.Empty<IScorePlayerTournament<int>>(),
            Array.Empty<ICondition<int>>(), score);

        var distribution = new ClassicDistribution();

        var distributionRule = new DistributionPlayerRule<int>(Array.Empty<IDistributionPlayer>(),
            Array.Empty<ICondition<int>>(), distribution);

        var scoreTeam = new ClassicScoreTeamTournament<int>();

        var scoreTeamRule = new ScoreTeamTournamentRule<int>(Array.Empty<IScoreTeamTournament<int>>(),
            Array.Empty<ICondition<int>>(), scoreTeam);

        var teamsGame = new ClassicTeam();

        var teamsGameRule =
            new TeamsGameRule<int>(Array.Empty<ITeamsGame>(), Array.Empty<ICondition<int>>(), teamsGame);

        var winner = new ClassicWinnerTournament(50);

        var winnerRule =
            new WinnerTournamentRule<int>(Array.Empty<IWinnerTournament>(), Array.Empty<ICondition<int>>(), winner);

        var infoRules = new InfoRulesTournament<int>(playerRule, scoreRule, distributionRule, scoreTeamRule,
            teamsGameRule, winnerRule);

        IStrategy<int> random = new RandomPlayer<int>();
        List<Player.Player<int>> players = new List<Player<int>>();

        for (int i = 0; i < 4; i++)
        {
            players.Add(new RandomStrategyPlayer<int>(new IStrategy<int>[] { }, new ICondition<int>[] { }, random, i));
        }

        return new JudgeTournament<int>(new List<BuildGame<int>>() { game }, players, new List<(int, int)>() { (0, 0), (0, 1), (1, 2), (1, 3) },
            infoRules);
    }
}