namespace HotelDevInn.Repositories;

// Repositories/GenericRepository.cs
using System;
using System.Collections.Generic;
using System.Linq; // Essencial para .Where() e .FirstOrDefault()!

public class GenericRepository<T>
{
    // Nossos dados (o "banco de dados" em memória)
    private readonly List<T> _items;

    // O construtor recebe a lista de dados
    public GenericRepository(List<T> items) => _items = items;

    // Retorna todos os itens
    public List<T> GetAll() => new List<T>(_items);

    // Encontra itens usando uma Expressão Lambda (o "predicado")
    public List<T> Find(Func<T, bool> consulta) => _items.Where(consulta).ToList();

    // Encontra o primeiro item que satisfaz a condição
    public T FirstOrDefault(Func<T, bool> consulta) => _items.FirstOrDefault(consulta);
}