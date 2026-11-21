
using System.Globalization;


Console.WriteLine("1. Факториал");
Console.WriteLine("2. Числа Фибоначчи");
Console.WriteLine("3. Вычисление функции");
Console.WriteLine("4. Ряд Тейлора (cos x)");
Console.WriteLine("0. Выход");

while (true)
{
    Console.Write("\nВыберите задание: ");

    if (!int.TryParse(Console.ReadLine(), out int choice))
    {
        Console.WriteLine("Ошибка: введите целое число.");
        continue;
    }

    switch (choice)
    {
        case 1:
            Task1();
            break;
        case 2:
            Task2();
            break;
        case 3:
            Task3();
            break;
        case 4:
            Task4();
            break;
        case 0:
            Console.WriteLine("Программа завершена.");
            return;
        default:
            Console.WriteLine("Такого задания нет.");
            break;
    }
}

// Задание 1.
static void Task1()
{
    Console.Write("Введите n (от 0 до 20): ");

    if (!int.TryParse(Console.ReadLine(), out int n) || n < 0 || n > 20)
    {
        Console.WriteLine("Ошибка: n должно быть от 0 до 20.");
        return;
    }

    Console.WriteLine($"{n}! = {Factorial(n)}");
}

static long Factorial(int n)
{
    long result = 1;

    for (int i = 2; i <= n; i++)
    {
        result *= i;
    }

    return result;
}

// Задание 2. 
static void Task2()
{
    Console.Write("Введите n: ");

    if (!int.TryParse(Console.ReadLine(), out int n) || n < 0)
    {
        Console.WriteLine("Ошибка: n должно быть неотрицательным.");
        return;
    }

    Console.WriteLine("Последовательность Фибоначчи:");

    long a = 0;
    long b = 1;

    while (a <= n)
    {
        Console.Write(a);

        long next = a + b;
        a = b;
        b = next;

        if (a <= n)
        {
            Console.Write(", ");
        }
    }

    Console.WriteLine();
}

// Задание 3. 
static void Task3()
{
    Console.Write("Введите x: ");

    if (!double.TryParse(Console.ReadLine(), NumberStyles.Float,
        CultureInfo.InvariantCulture, out double x))
    {
        Console.WriteLine("Ошибка: введено некорректное число.");
        return;
    }

    if (x < -2)
    {
        Console.WriteLine("Ошибка: x должен быть >= -2.");
        return;
    }

    double result = Math.Exp(Math.Cos(x)) / (1 + x * x)
                    + Math.Atan(Math.Sqrt(x + 2));

    Console.WriteLine($"A = {result:F6}");
}

    // Задание 4. 
    static void Task4()
    {
        Console.Write("Введите x (в радианах): ");

        if (!double.TryParse(Console.ReadLine(), System.Globalization.NumberStyles.Float,
            System.Globalization.CultureInfo.InvariantCulture, out double x))
        {
            Console.WriteLine("Ошибка: введено некорректное число.");
            return;
        }

        const double epsilon = 1e-6;

        double sum = 1.0;
        double term = 1.0;
        int count = 1;

        while (true)
        {
            term *= -x * x / ((2 * count - 1) * (2 * count));

            if (Math.Abs(term) <= epsilon)
                break;

            sum += term;
            count++;
        }

        double libraryResult = Math.Cos(x);

        Console.WriteLine($"Результат ряда Тейлора: {sum:F10}");
        Console.WriteLine($"Math.Cos(x): {libraryResult:F10}");
        Console.WriteLine($"Разница: {Math.Abs(sum - libraryResult):F10}");
        Console.WriteLine($"Количество членов ряда: {count}");
    }