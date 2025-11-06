namespace HotelDevInn.Models;

// Models/Funcionario.cs
public class Funcionario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    
    public Funcionario()
    {
        
    }

    public Funcionario(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }

    public override string ToString() => $"[Funcionário] Nome: {Nome}";
}