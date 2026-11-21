
using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("СЛИЯНИЕ ТЕКСТОВЫХ ФАЙЛОВ");
        Console.WriteLine();

        try
        {
            Console.Write("Введите путь к папке с TXT-файлами: ");
            string? sourceFolder = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(sourceFolder))
            {
                Console.WriteLine("Путь не может быть пустым.");
                return;
            }

            if (!Directory.Exists(sourceFolder))
            {
                Console.WriteLine("Указанная папка не существует.");
                return;
            }

            Console.Write("Введите папку для сохранения результата: ");
            string? destinationFolder = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(destinationFolder))
            {
                Console.WriteLine("Путь для сохранения не может быть пустым.");
                return;
            }

            if (!Directory.Exists(destinationFolder))
            {
                Console.WriteLine("Папка для сохранения не существует.");
                return;
            }

            Console.Write("Введите имя итогового файла (например, merged.txt): ");
            string? outputName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(outputName))
            {
                Console.WriteLine("Имя файла не может быть пустым.");
                return;
            }

            if (Path.GetFileName(outputName) != outputName)
            {
                Console.WriteLine("Введите только имя файла, без пути к папке.");
                return;
            }

            if (!outputName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            {
                outputName += ".txt";
            }

            string outputPath = Path.Combine(destinationFolder, outputName);

            if (File.Exists(outputPath))
            {
                Console.Write("Файл уже существует. Перезаписать? (да/нет): ");
                string? answer = Console.ReadLine();

                if (answer?.Trim().ToLower() != "да")
                {
                    Console.WriteLine("Сохранение отменено.");
                    return;
                }
            }

            int count = FileMerger.MergeFiles(sourceFolder, outputPath);

            Console.WriteLine();
            Console.WriteLine("Слияние успешно завершено!");
            Console.WriteLine($"Объединено файлов: {count}");
            Console.WriteLine($"Результат сохранён: {outputPath}");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Ошибка: недостаточно прав доступа к файлу или папке.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка ввода-вывода: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}
