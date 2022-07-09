namespace InteractionGui;

public class LocationGui
{
    /// <summary>
    /// Filas y las columnas para ubicar cada ficha
    /// </summary>
    public (int, int, int, int) Location { get; set; }

    /// <summary>
    /// Valores de la ficha
    /// </summary>
    public string[] Values { get; set; }

    /// <summary>
    /// Condicion para el tipo de visualizacion 
    /// </summary>
    public bool Condition { get; set; }

    public Printer.TypeToken TypeToken { get; set; }

    public LocationGui((int, int, int, int) location, string[] values, Printer.TypeToken type)
    {
        this.Location = location;
        this.Values = values;
        this.TypeToken = type;
    }
}