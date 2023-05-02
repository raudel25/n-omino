using InfoGame;

namespace Rules;

public class ClassicVisibilityPlayer<T> : IVisibilityPlayer<T>
{
    public void Visibility(GameStatus<T> game, int ind)
    {
        for (var i = 0; i < game.Players.Count; i++)
            if (i != game.Turns[ind])
                game.Players[i].Hand = new Hand<T>();
    }
}

public class TeamVisibilityPlayer<T> : IVisibilityPlayer<T>
{
    public void Visibility(GameStatus<T> game, int ind)
    {
        List<Hand<T>> aux = new();
        var team = game.FindTeamPlayer(game.Players[game.Turns[ind]].Id);

        //Guardamos las manos de los miembros del equipo
        for (var i = 0; i < game.Teams[team].Count; i++) aux.Add(game.Teams[team][i].Hand);

        for (var i = 0; i < game.Players.Count; i++)
            if (i != game.Turns[ind])
                game.Players[i].Hand = new Hand<T>();

        //Reasignamos las manos de los miembros del equipo
        for (var i = 0; i < game.Teams[team].Count; i++) game.Teams[team][i].Hand = aux[i];
    }
}