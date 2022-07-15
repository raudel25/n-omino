using InfoGame;
using Rules;
using Table;

TournamentStatus t =
    new TournamentStatus(new List<InfoPlayerTournament>(), new List<InfoTeams<InfoPlayerTournament>>());


var a = new ClassicDistribution();

a.DeterminateDistribution(t, 0);

foreach (var i in t.DistributionPlayers)
{
    Console.WriteLine(i);
}