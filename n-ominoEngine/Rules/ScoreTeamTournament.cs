using InfoGame;

namespace Rules;

public interface IScoreTeamTournament<T>
{
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind);
}

public class ClassicScoreTeamTournament<T>:IScoreTeamTournament<T>
{
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind)
    {
        int score = 0;

        for (int i = 0; i < game.Teams.Count; i++)
        {
            if (i != game.TeamWinner)
            {
                for (int j = 0; j < game.Teams[i].Count; j++)
                {
                    foreach (var token in game.Teams[i][j].Hand!)
                    {
                        score+=rules.ScoreToken.ScoreToken(token);
                    }
                }
            }
        }

        tournament.ScoreTeams[game.TeamWinner] += score;
    }
}