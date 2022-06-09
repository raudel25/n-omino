namespace Rules;

public class TurnPlayerClasic : ITurnPlayer
{
    public void Turn(int[] turns, int ind)
    {
    }
}

public class TurnPlayerInvert : ITurnPlayer
{
    public void Turn(int[] turns, int ind)
    {
        int i = ind;
        int j = ind;
        while (true)
        {
            i++;
            if (i == turns.Length) i = 0;
            if (i == j) break;
            j--;
            if (j < 0) j = turns.Length - 1;
            if (i == j) break;
            int change = turns[i];
            turns[i] = turns[j];
            turns[j] = change;
        }
    }
}

public class TurnPlayerRepeatPlay : ITurnPlayer
{
    public void Turn(int[] turns, int ind)
    {
        int i = ind;
        int j = ind - 1;
        int stop = (ind == turns.Length - 1) ? 0 : ind + 1;
        int change = turns[ind];
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