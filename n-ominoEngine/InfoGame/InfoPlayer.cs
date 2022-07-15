using Table;

namespace InfoGame;

public class InfoPlayer<T> : ICloneable<InfoPlayer<T>>
{
    //ID del jugador
    public int Id { get; set; }

    //mano del jugador
    public Hand<T> Hand { get; set; }

    //cantidad de fichas en la mano del jugador
    public int HandCount => Hand.Count();

    //cantidad de pases que se ha dado el jugador
    public int Passes => History.Passes;
    
    //guarda el historial de jugadas de ese jugador
    public History<T> History { get; set; }

    //puntuación del jugador
    public double Score { get; set; }
    
    public string Name { get; private set; }

    public InfoPlayer(Hand<T> hand, History<T> history, double score, int id,string name)
    {
        this.Name = name;
        this.Hand = hand;
        this.History = history;
        this.Score = score;
        this.Id = id;
    }

    public InfoPlayer<T> Clone()
    {
        return new InfoPlayer<T>(Hand!.Clone(), History.Clone(), Score, Id,this.Name);
    }

    object ICloneable.Clone()
    {
        return this.Clone();
    }
}