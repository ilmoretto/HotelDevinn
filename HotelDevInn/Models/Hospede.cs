namespace HotelDevInn.Models;

// Models/Hospede.cs
public class Hospede
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public Hospede()
    {
        
    }

    public Hospede(int id, string nome, string cpf)
    {
        Id = id;
        Nome = nome;
        CPF = cpf;
    }


    public override string ToString() => $"[Hóspede] Nome: {Nome}, CPF: {CPF}";
}