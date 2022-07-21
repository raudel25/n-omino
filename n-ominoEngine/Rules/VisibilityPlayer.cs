using InfoGame;

namespace Rules;

public class ClassicVisibilityPlayer<T> : IVisibilityPlayer<T>
{
    public void Visibility(GameStatus<T> game, int ind)
    {
        for (int i = 0; i < game.Players.Count; i++)
        {
            if (i != game.Turns[ind])
            {
                game.Players[i].Hand = new Hand<T>();
            }
        }
    }
}

public class TeamVisibilityPlayer<T> : IVisibilityPlayer<T>
{
    public void Visibility(GameStatus<T> game, int ind)
    {
        List<Hand<T>> aux = new();
        int team = game.FindTeamPlayer(game.Turns[ind]);

        //Guardamos las manos de los miembros del equipo
        for (int i = 0; i < game.Teams[team].Count; i++)
        {
            aux.Add(game.Teams[team][i].Hand);
        }

        for (int i = 0; i < game.Players.Count; i++)
        {
            if (i != game.Turns[ind])
            {
                game.Players[i].Hand = new Hand<T>();
            }
        }

        //Reasignamos las manos de los miembros del equipo
        for (int i = 0; i < game.Teams[team].Count; i++)
        {
            game.Teams[team][i].Hand = aux[i];
        }
    }
}