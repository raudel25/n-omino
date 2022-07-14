using InfoGame;
using Rules;
using Table;

TournamentStatus t=new TournamentStatus(new List<InfoPlayerTournament>(),new List<InfoTeams<InfoPlayerTournament>>());

t.DistributionPlayers=new List<(int, int)>(){(1,1),(1,2),(2,3),(2,4),(2,5),(3,6)};

var a=new ClassicDistribution();

a.DeterminateDistribution(t,0);

foreach (var i in t.DistributionPlayers)
{
    Console.WriteLine(i);
}