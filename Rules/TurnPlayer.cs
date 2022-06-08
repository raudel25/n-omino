namespace Rules;

public class TurnPlayerClasic : ITurnPlayer
{
    public int[] Turn(int[] turns, int ind)
    {
        return turns;
    }
}

public class TurnPlayerToPass : ITurnPlayer
{
    public int[] Turn(int[] turns, int ind)
    {
        var aux = new int[turns.Length];
        var i = ind + 1;
        var j = ind - 1;
        aux[ind] = turns[ind];
        while (i != j)
        {
            if (i == turns.Length) i = 0;
            if (j < 0) j = turns.Length - 1;
            aux[i] = turns[j];
            i++;
            j--;
        }

        return aux;
    }
}

public class TurnPlayerRepeatPlay : ITurnPlayer
{
    public int[] Turn(int[] turns, int ind)
    {
        var aux = new int[turns.Length];
        var i = ind;
        var j = ind + 1;
        while (true)
        {
            if (i == turns.Length) i = 0;
            if (j == turns.Length) j = 0;
            aux[j] = turns[i];
            if (i + 1 == ind) break;
            i++;
            j++;
        }

        return aux;
    }
}