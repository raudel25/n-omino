namespace Rules;

public class TurnPlayerClassic : ITurnPlayer
{
    public void Turn(int[] turns, int ind)
    {
    }
}

public class TurnPlayerInvert : ITurnPlayer
{
    public void Turn(int[] turns, int ind)
    {
        var i = ind;
        var j = ind;
        while (true)
        {
            i++;
            if (i == turns.Length) i = 0;
            if (i == j) break;
            j--;
            if (j < 0) j = turns.Length - 1;
            if (i == j) break;
            (turns[i], turns[j]) = (turns[j], turns[i]);
        }
    }
}

public class TurnPlayerRepeatPlay : ITurnPlayer
{
    public void Turn(int[] turns, int ind)
    {
        var i = ind;
        var j = ind - 1;
        var stop = ind == turns.Length - 1 ? 0 : ind + 1;
        var change = turns[ind];
        while (true)
        {
            if (i == -1) i = turns.Length - 1;
            if (j == -1) j = turns.Length - 1;
            if (i == stop) break;
            turns[i] = turns[j];
            j--;
            i--;
        }

        turns[stop] = change;
    }
}