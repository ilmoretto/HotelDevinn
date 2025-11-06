namespace HotelDevInn.Models;

// Models/Reserva.cs
using System; // Necessário para DateTime

public class Reserva
{
    public int Id { get; set; }
    public Quarto QuartoReservado { get; set; }
    public Hospede Hospede { get; set; }
    public Funcionario Funcionario { get; set; }
    public DateTime DataCheckIn { get; set; }
    public DateTime DataCheckOut { get; set; }

    public override string ToString()
    {
        return $"--- Reserva ID: {Id} ---\n" +
               $"\tHóspede: {Hospede.Nome}\n" +
               $"\tQuarto: {QuartoReservado.Numero} ({QuartoReservado.Tipo})\n" +
               $"\tPeríodo: {DataCheckIn:dd/MM/yyyy} a {DataCheckOut:dd/MM/yyyy}\n" +
               $"\tAtendente: {Funcionario.Nome}\n";
    }
}