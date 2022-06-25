using Table;
namespace InfoGame;
public class InfoPlayer<T>
{
    public List<Token<T>>? Hand { get; set; }
    public int HandCount { get {return Hand!.Count();} }
    public int Passes {get; set;}
    //guarda el historial de jugadas de ese jugador
    public Actions<T> Actions {get; set;}
    public double Score { get; set; }
    public int Id { get; set; }

    public InfoPlayer(List<Token<T>> hand, int passes, Actions<T> actions, double score, int id)
    {
        this.Hand = hand;
        this.Passes = passes;
        this.Actions = actions;
        this.Score = score;
        this.Id = id;
    }
    public InfoPlayer<T> Clone()
    {
        return new InfoPlayer<T>(Clone(Hand!), Passes, this.Actions, Score, Id);
    }

    private List<Token<T>> Clone(List<Token<T>> collection)
    {
        List<Token<T>> aux = new List<Token<T>>();
        foreach (var item in collection)
            aux.Add(item.Clone());
        return aux;
    }
    private Actions<T> Clone(Actions<T> x)
    {
        return (Actions<T>)x.Clone();
    }
}


    public class Actions<T>
{
    public Actions()
    {
        
    }

    public Object Clone()
    {
        throw new NotImplementedException();
    }
}

