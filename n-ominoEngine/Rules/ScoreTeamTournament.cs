using InfoGame;

namespace Rules;

public class ClassicScoreTeamTournament<T> : IScoreTeamTournament<T>
{
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules)
    {
        int score = 0;

        for (int i = 0; i < game.Teams.Count; i++)
        {
            if (i != game.TeamWinner)
            {
                for (int j = 0; j < game.Teams[i].Count; j++)
                {
                    foreach (var token in game.Teams[i][j].Hand)
                    {
                        score += rules.ScoreToken(token);
                    }
                }
            }
        }

        tournament.ScoreTeams[game.TeamWinner] += score;
    }
}

public class PlayersScoreTeamTournament<T> : IScoreTeamTournament<T>
{
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules)
    {
        for (int i = 0; i < game.Teams.Count; i++)
        {
            double sum = 0;
            for (int j = 0; j < game.Teams[i].Count; j++)
            {
                sum += game.Teams[i][j].Score;
            }

            tournament.ScoreTeams[game.Teams[i].Id] += sum;
        }
    }
}