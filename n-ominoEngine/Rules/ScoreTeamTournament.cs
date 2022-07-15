using InfoGame;

namespace Rules;

public interface IScoreTeamTournament<T>
{
    /// <summary>
    /// Asignar score a los equipos durante el torneo
    /// </summary>
    /// <param name="tournament">Estado del torneo</param>
    /// <param name="game">Estado del juego actual</param>
    /// <param name="rules">Reglas del actual juego</param>
    /// <param name="ind">Indice del torneo</param>
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind);
}

public class ClassicScoreTeamTournament<T> : IScoreTeamTournament<T>
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
                    foreach (var token in game.Teams[i][j].Hand)
                    {
                        score += rules.ScoreToken.ScoreToken(token);
                    }
                }
            }
        }

        tournament.ScoreTeams[game.TeamWinner] += score;
    }
}

public class PlayersScoreTeamTournament<T> : IScoreTeamTournament<T>
{
    public void AssignScore(TournamentStatus tournament, GameStatus<T> game, InfoRules<T> rules, int ind)
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