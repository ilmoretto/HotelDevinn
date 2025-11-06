using HotelDevInn.Models;

namespace HotelDevInn.Views;

// Views/HotelView.cs
using System;
using System.Collections.Generic;
using System.Linq;

public class HotelView
{
    public void ExibirTitulo(string titulo)
    {
        Console.WriteLine($"\n===== {titulo.ToUpper()} ======");
    }

    public void ExibirMensagem(string mensagem) => Console.WriteLine(mensagem);

    // Uma view genérica para exibir qualquer lista de objetos
    public void ExibirLista<T>(IEnumerable<T> lista)
    {
        if (lista == null || !lista.Any())
        {
            Console.WriteLine("Nenhum item encontrado.");
            return;
        }
        foreach (var item in lista)
        {
            Console.WriteLine(item); // Usa o .ToString() de cada model
        }
    }

    public void ExibirCusto(Reserva reserva, decimal custo)
    {
        Console.WriteLine($"Custo total da reserva ID {reserva.Id} ({reserva.Hospede.Nome}): {custo:C}");
    }
}