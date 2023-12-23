using Npgsql;
using System;
using System.IO;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("User is ?");
        var userName = Console.ReadLine();
        Console.WriteLine("Password is ?");
        var password = Console.ReadLine();

        Console.WriteLine($"User is {userName}");
        Console.WriteLine($"Password is {password}");

        StreamReader sr = new StreamReader("../../../sdl.conf");
        var connectionString = sr.ReadLine();

        Npgsql.NpgsqlConnectionStringBuilder csb = new Npgsql.NpgsqlConnectionStringBuilder(connectionString);

        csb.Username = userName;
        csb.Password = password;

        try
        {
            await using var dataSource = NpgsqlDataSource.Create(csb);

            // Retrieve all rows
            await using (var cmd = dataSource.CreateCommand("SELECT VERSION();"))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Console.WriteLine(reader.GetString(0));
                }
            }
        }
        catch (Npgsql.PostgresException)
        {
            Console.WriteLine("Вход неавторизованного пользователя");
        }
    }
}







