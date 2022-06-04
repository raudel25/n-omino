using Table;
using Rules;

namespace Judge;

public class Judge
{
    public InfoRules JudgeRules { get; private set; }
    private int cantplayer = 4;
    private TableGame table;
    private int[] _turns { get; set; }
    public Judge(InfoRules infoRules)
    {
        this.JudgeRules = infoRules;
        this._turns = new int[cantplayer];
        this.table = null!;
        for (int i = 0; i < cantplayer; i++)
        {
            this._turns[i] = i;
        }
    }
    private void Game()
    {
        int i = 0;
        while (true)
        {
            if (this.ValidPlayPlayer(new Token[4], table))
            {

            }
            i++;
            if (i == cantplayer) i = 0;
        }
    }
    /// <summary>Determina si el jugador tiene opciones para jugar</summary>
    /// <param name="tokens">Fichas de las que dispone el jugador</param>
    /// <param name="table">Mesa para jugar</param>
    /// <returns>El jugador tiene opciones para jugar</returns>
    private bool ValidPlayPlayer(Token[] tokens, TableGame table)
    {
        foreach (var item in table.FreeNode)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                if (this.JudgeRules.ValidPlays(item, tokens[i], table).Count != 0) return true;
            }
        }
        return false;
    }

}