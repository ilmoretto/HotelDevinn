namespace HotelDevInn.Models;

// Models/Quarto.cs
public class Quarto
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public string Tipo { get; set; }
    public decimal PrecoPorNoite { get; set; }
    
    public Quarto()
    {
        
    }

    public Quarto(int id, int numero, string tipo, decimal precoPorNoite)
    {
        Id = id;
        Numero = numero;
        Tipo = tipo;
        PrecoPorNoite = precoPorNoite;
    }

    public override string ToString() => $"[Quarto N°: {Numero}] Tipo: {Tipo}, Diária: {PrecoPorNoite:C}";
}