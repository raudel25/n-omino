using Rules;

namespace InteractionGui;

public class SelectValidPlay<T> : IVariant<IValidPlay<T>, T>
{
    public List<IVariant<IValidPlay<T>, T>.Select> Values {get ; } = new List<IVariant<IValidPlay<T>, T>.Select>(){
        (comp) => new ValidPlayDimension<T>(comp.Comp!),
        comp => new ComodinToken<T>(comp.Token!),
        (comp) => new ValidPlayLongana<T>(comp.Comp!, comp.n),
        (comp) => new ValidPlayGeometry<T>(comp.Comp!)
    };

    public string Description => "Elija el tipo de juego";

    public List<ParamSelect> Param {get; } = new List<ParamSelect>(){
        new ParamSelect("Validador clásico","Valida las jugadas según el comparador que reciba", 0, 0, false, true),
        new ParamSelect("Con comodín","", 1, 0, false, true),
        new ParamSelect("Longana", "", 2, 0, false, true),
        new ParamSelect("Tablero geométrico", "", 3, 0, false, true)
    };
}