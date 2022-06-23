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
        TableGeometry<int> table = new TableTriangular<int>(new [] {(0, 0), (1, 1), (2, 0)});
        int[] array = new int[10];
        for (int i = 0; i < 10; i++)
        {
            array[i] = i;
        }

        //Jugadores
        ITokensMaker<int> maker = new TokensMakerCircular<int>();

        List<Token<int>> tokens = maker.MakeTokens(array, 3);

        IDealer<Token<int>> dealer = new RandomDealer<int>();

        InfoPlayer<int>[] playersInfo = new InfoPlayer<int>[4];

        for (int i = 0; i < 4; i++)
        {
            var anabel = dealer.Deal(tokens, 100);

            playersInfo[i] = new InfoPlayer<int>(anabel, 0, new Actions<int>(), 0, i);
        }

        Player.Player<int>[] players = new Player<int>[4];

        IStrategy<int> strategy = new RandomPlayer<int>();

        for (int i = 0; i < 4; i++)
        {
            players[i] = new PurePlayer<int>(i, strategy);
        }

        List<InfoPlayer<int>>[] team = new List<InfoPlayer<int>>[4];

        for (int i = 0; i < 4; i++)
        {
            team[i] = new List<InfoPlayer<int>>();
            team[i].Add(playersInfo[i]);
        }

        GameStatus<int> game = new GameStatus<int>(playersInfo, team, table, new[] {0, 1, 2, 3}, tokens);
        
        
//IValidPlay<int> valid = new ValidPlayDimension<int>(new ClassicComparison<int>());
        IValidPlay<int> valid = new ValidPlayGeometry<int>(new ClassicComparison<int>());
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
        ICondition<int> conditionTranque = new NoValidPLay<int>();

        IsValidRule<int> isValidRule = new IsValidRule<int>(new[] {valid}, new[] {condition}, valid);

        TurnPlayerRule<int> turnPlayerRule = new TurnPlayerRule<int>(new[] {turn}, new[] {condition}, turn);

        VisibilityPlayerRule<int> visibilityPlayerRule =
            new VisibilityPlayerRule<int>(new[] {visibilityPlayer}, new[] {condition}, visibilityPlayer);

        StealTokenRule<int> stealTokenRule = new StealTokenRule<int>(new[] {steal}, new[] {condition}, steal);

        AssignScorePlayerRule<int> assignScorePlayerRule =
            new AssignScorePlayerRule<int>(new[] {scorePlayer, scorePlayerNo}, new[] {conditionWin, conditionTranque});

        WinnerGameRule<int> winnerGameRule = new WinnerGameRule<int>(new[] {winnerGame, winnerGameTranque},
            new[] {conditionWin, conditionTranque});

        InfoRules<int> rules = new InfoRules<int>(isValidRule, visibilityPlayerRule, turnPlayerRule, stealTokenRule,
            null,
            assignScorePlayerRule, winnerGameRule, scoreToken);

        Judge<int> judge = new Judge<int>(rules, game, players);
        judge.t = new Token<int>(new[] {3, 3, 3});
        
        return judge;
    }
}