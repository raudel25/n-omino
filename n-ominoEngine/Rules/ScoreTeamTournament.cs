using InfoGame;

namespace Rules;

public class ClassicScoreTeamTournament<T> : IScoreTeamTournament<T>
{
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules)
    {
        var score = 0;

        for (var i = 0; i < game.Teams.Count; i++)
            if (i != game.TeamWinner)
                for (var j = 0; j < game.Teams[i].Count; j++)
                    foreach (var token in game.Teams[i][j].Hand)
                        score += rules.ScoreToken(token);

        tournament.ScoreTeams[game.TeamWinner] += score;
    }
}

public class PlayersScoreTeamTournament<T> : IScoreTeamTournament<T>
{
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules)
    {
        for (var i = 0; i < game.Teams.Count; i++)
        {
            double sum = 0;
            for (var j = 0; j < game.Teams[i].Count; j++) sum += game.Teams[i][j].Score;

            tournament.ScoreTeams[game.Teams[i].Id] += sum;
        }
    }
}

public class PointForTeam<T> : IScoreTeamTournament<T>
{
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game, IAssignScoreToken<T> rules)
    {
        tournament.ScoreTeams[game.TeamWinner] += 100;
    }
}