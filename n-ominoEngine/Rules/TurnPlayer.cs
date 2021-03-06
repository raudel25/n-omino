namespace Rules;

public interface ITurnPlayer
{
    /// <summary>Determina la distibucion de los turnos de los jugadores</summary>
    /// <param name="turns">Distribucion de los turnos de los jugadores</param>
    /// <param name="ind">Indice del jugador que le corresponde jugar</param>
    public void Turn(int[] turns, int ind);
}

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
            (turns[i], turns[j]) = (turns[j], turns[i]);
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