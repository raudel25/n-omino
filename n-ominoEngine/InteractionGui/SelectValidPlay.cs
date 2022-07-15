using Rules;
using Table;

namespace InteractionGui;

public class SelectValidPlay<T> : IVariant<IValidPlay<T>, T>
{
    public List<IVariant<IValidPlay<T>, T>.Select> Values { get; } = new List<IVariant<IValidPlay<T>, T>.Select>()
    {
        (comp) => new ComodinToken<T>(comp.Token!)
    };

    public string Description => "Elija el tipo de juego";

    public List<ParamSelect> Param { get; } = new List<ParamSelect>()
    {
        new ParamSelect("Con comodín", "", 0, false, false, false, false, true)
    };

    public SelectValidPlay(TableGame<T> table)
    {
        if (table is TableDimension<T>)
        {
            Values.Add((comp) => new ValidPlayDimension<T>(comp.Comp!));
            Param.Add(new ParamSelect("Validador clásico",
                "Valida las jugadas según el comparador que reciba para un tablero de n-omino", 1,
                false, false, true));
        }

        if (table is TableGeometry<T>)
        {
            Values.Add((comp) => new ValidPlayGeometry<T>(comp.Comp!));
            Param.Add(new ParamSelect("Tablero geométrico",
                "Valida las jugadas según el comparador que reciba para un tablero de n-omino geometrico", 1, false,
                false, true));
        }

        if (table is TableLongana<T>)
        {
            Values.Add((comp) => new ValidPlayLongana<T>(comp.Comp!));
            Param.Add(new ParamSelect("Longana",
                "Valida las jugadas según el comparador que reciba para un tablero de n-omino de longana", 2, false,
                false, true));
            Values.Add((comp) => new ValidPlayLonganaComplement<T>(comp.Comp!));
            Param.Add(new ParamSelect("Complemento para la longana", "Permite jugar por la rama del jugador anterior",
                3, false, false, true));
        }
    }
}