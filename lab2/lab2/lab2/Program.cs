
using System;
using System.Collections.Generic;
using System.Linq;

// Демонстрация спортивных соревнований
Console.WriteLine("СПОРТИВНЫЕ СОРЕВНОВАНИЯ");
Console.WriteLine();

// Создание участников
List<Athlete> athletes = new List<Athlete>
{
    new Runner("Александр", 20, 12.45),
    new Runner("Михаил", 22, 11.98),
    new Swimmer("Анна", 19, 58.30),
    new Swimmer("Елена", 21, 61.12),
    new Triathlete("Дмитрий", 23, 65.20, 72.40, 47.80)
};

// Создание соревнования
Competition competition = new Competition("Чемпионат");

foreach (Athlete athlete in athletes)
{
    competition.AddAthlete(athlete);
}

// Вывод участников
Console.WriteLine("Участники соревнований:");
Console.WriteLine();

foreach (Athlete athlete in competition.Athletes)
{
    Console.WriteLine(athlete);
}

Console.WriteLine();

// Вывод таблицы лидеров
Console.WriteLine("ТАБЛИЦА ЛИДЕРОВ");
Console.WriteLine();

List<Athlete> leaders = competition.GetLeaderboard();

for (int i = 0; i < leaders.Count; i++)
{
    Console.WriteLine($"{i + 1}. {leaders[i].Name} — {leaders[i].Result:F2} сек.");
}

Console.WriteLine();
Console.WriteLine($"Количество участников: {competition.Athletes.Count}");
Console.WriteLine($"Победитель: {competition.GetWinner()?.Name}");


// Абстрактный базовый класс спортсмена
public abstract class Athlete
{
    private string _name;
    private int _age;

    public string Name
    {
        get { return _name; }
    }

    public int Age
    {
        get { return _age; }
    }

    // Результат спортсмена в секундах
    public abstract double Result { get; }

    protected Athlete(string name, int age)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Имя не может быть пустым.");

        if (age <= 0 || age > 120)
            throw new ArgumentOutOfRangeException(nameof(age), "Возраст должен быть от 1 до 120.");

        _name = name;
        _age = age;
    }

    public abstract string GetDiscipline();

    public override string ToString()
    {
        return $"{GetDiscipline()}: {_name}, {_age} лет, результат: {Result:F2} сек.";
    }
}


// Класс бегуна
public class Runner : Athlete
{
    private double _runningTime;

    public Runner(string name, int age, double runningTime)
        : base(name, age)
    {
        if (runningTime <= 0)
            throw new ArgumentOutOfRangeException(nameof(runningTime), "Время должно быть положительным.");

        _runningTime = runningTime;
    }

    public override double Result
    {
        get { return _runningTime; }
    }

    public override string GetDiscipline()
    {
        return "Бегун";
    }

    public override string ToString()
    {
        return $"{base.ToString()} (бег)";
    }
}


// Класс пловца
public class Swimmer : Athlete
{
    private double _swimmingTime;

    public Swimmer(string name, int age, double swimmingTime)
        : base(name, age)
    {
        if (swimmingTime <= 0)
            throw new ArgumentOutOfRangeException(nameof(swimmingTime), "Время должно быть положительным.");

        _swimmingTime = swimmingTime;
    }

    public override double Result
    {
        get { return _swimmingTime; }
    }

    public override string GetDiscipline()
    {
        return "Пловец";
    }

    public override string ToString()
    {
        return $"{base.ToString()} (плавание)";
    }
}


// Класс многоборца
public class Triathlete : Athlete
{
    private double _runningTime;
    private double _swimmingTime;
    private double _cyclingTime;

    public Triathlete(string name, int age, double runningTime,
        double swimmingTime, double cyclingTime)
        : base(name, age)
    {
        if (runningTime <= 0 || swimmingTime <= 0 || cyclingTime <= 0)
            throw new ArgumentOutOfRangeException("Время дисциплин должно быть положительным.");

        _runningTime = runningTime;
        _swimmingTime = swimmingTime;
        _cyclingTime = cyclingTime;
    }

    // Суммарное время трех дисциплин
    public override double Result
    {
        get { return _runningTime + _swimmingTime + _cyclingTime; }
    }

    public override string GetDiscipline()
    {
        return "Многоборец";
    }

    public override string ToString()
    {
        return $"{base.ToString()} (три дисциплины)";
    }
}


// Класс соревнования
public class Competition
{
    private string _name;
    private List<Athlete> _athletes;

    public string Name
    {
        get { return _name; }
    }

    public IReadOnlyList<Athlete> Athletes
    {
        get { return _athletes.AsReadOnly(); }
    }

    public Competition(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название соревнования не может быть пустым.");

        _name = name;
        _athletes = new List<Athlete>();
    }

    public void AddAthlete(Athlete athlete)
    {
        if (athlete == null)
            throw new ArgumentException("Спортсмен не может быть null.");

        _athletes.Add(athlete);
    }

    // Сортировка участников по результату
    public List<Athlete> GetLeaderboard()
    {
        return _athletes.OrderBy(athlete => athlete.Result).ToList();
    }

    // Получение победителя
    public Athlete GetWinner()
    {
        return _athletes.OrderBy(athlete => athlete.Result).FirstOrDefault();
    }
}
