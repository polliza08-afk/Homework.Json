// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
Console.InputEncoding = System.Text.Encoding.UTF8;
Console.OutputEncoding = System.Text.Encoding.UTF8;

string filePath = "subscribers.json";

while (true)
{
    Console.WriteLine("\n0. Вихід");
    Console.WriteLine("1. Додати абонента");
    Console.WriteLine("2. Показати усіх абонентів");
    Console.Write("-> ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "0":
            return;

        case "1":
            AddSubscriber();
            break;

        case "2":
            ShowSubscribers();
            break;

        default:
            Console.WriteLine("Невірний вибір!");
            break;
    }
}

void AddSubscriber()
{
    var subscribers = LoadSubscribers();

    Console.Write("Введіть ім'я: ");
    string name = Console.ReadLine();

    Console.Write("Введіть телефон: ");
    string phone = Console.ReadLine();

    Console.Write("Введіть email: ");
    string email = Console.ReadLine();

    subscribers.Add(new Subscriber(name, phone, email));

    SaveSubscribers(subscribers);

    Console.WriteLine("Абонента додано!");
}

void ShowSubscribers()
{
    var subscribers = LoadSubscribers();

    if (subscribers.Count == 0)
    {
        Console.WriteLine("Список порожній.");
        return;
    }

    Console.WriteLine("\nСписок абонентів:");
    foreach (var s in subscribers)
    {
        Console.WriteLine("----------------------");
        Console.WriteLine($"Ім'я: {s.Name}");
        Console.WriteLine($"Телефон: {s.Phone}");
        Console.WriteLine($"Email: {s.Email}");
    }
}

List<Subscriber> LoadSubscribers()
{
    if (!File.Exists(filePath))
        return new List<Subscriber>();

    string json = File.ReadAllText(filePath);

    if (string.IsNullOrWhiteSpace(json))
        return new List<Subscriber>();

    return JsonSerializer.Deserialize<List<Subscriber>>(json);
}

void SaveSubscribers(List<Subscriber> subscribers)
{
    var options = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    string json = JsonSerializer.Serialize(subscribers, options);
    File.WriteAllText(filePath, json);
}

record Subscriber(string Name, string Phone, string Email);
