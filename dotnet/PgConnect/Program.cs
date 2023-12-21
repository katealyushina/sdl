using Npgsql;

Console.WriteLine("User is ?");
var userName = Console.ReadLine();
Console.WriteLine("Password is ?");
var password = Console.ReadLine();
Console.WriteLine($"User is {userName}");
Console.WriteLine($"Password is {password}");

var connectionString = "Host=localhost;Username=kate;Password=Rfnz1601;Database=sdl";

// Проверяем соответствие введенных данных с данными из connection string
if (userName != "kate" || password != "Rfnz1601")
{
    Console.WriteLine("Вход неавторизованного пользователя");
}
else
{
    // Создаем подключение к базе данных
    await using var dataSource = NpgsqlDataSource.Create(connectionString);

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









