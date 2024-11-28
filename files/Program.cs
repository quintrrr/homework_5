using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


class Program
{

    class Student
    {
        public string LastName;
        public string FirstName;
        public int BirthYear;
        public string Exam;
        public int Score;

        public Student(string lastName, string firstName, int birthYear, string exam, int score)
        {
            LastName = lastName;
            FirstName = firstName;
            BirthYear = birthYear;
            Exam = exam;
            Score = score;
        }

        public string Print()
        {
            return $"{LastName} {FirstName}, {BirthYear}, Экзамен: {Exam}, Баллы: {Score}";
        }
    }

    static void AddStudent(Dictionary<string, Student> students)
    {
        Console.Write("Введите фамилию: ");
        string lastName = Console.ReadLine();
        Console.Write("Введите имя: ");
        string firstName = Console.ReadLine();
        Console.Write("Введите год рождения: ");
        if (!int.TryParse(Console.ReadLine(), out int birthYear))
        {
            Console.WriteLine("Ошибка: введите корректный год рождения.");
            return;
        }

        Console.Write("Введите экзамен: ");
        string exam = Console.ReadLine();
        Console.Write("Введите баллы: ");
        if (!int.TryParse(Console.ReadLine(), out int score))
        {
            Console.WriteLine("Ошибка: введите корректные баллы.");
            return;
        }

        Student student = new Student
        (
            lastName,
            firstName,
            birthYear,
            exam,
            score
        );

        string key = lastName + " " + firstName;
        students[key] = student;

        Console.WriteLine($"Студент {student} добавлен.");
    }

    static void RemoveStudent(Dictionary<string, Student> students)
    {
        Console.Write("Введите фамилию: ");
        string lastName = Console.ReadLine();
        Console.Write("Введите имя: ");
        string firstName = Console.ReadLine();
        string key = lastName + " " + firstName;
        if (students.Remove(key))
        {
            Console.WriteLine($"Студент {lastName} {firstName} удалён.");
        }
        else
        {
            Console.WriteLine("Студент не найден.");
        }
    }

    static void SortStudents(Dictionary<string, Student> students)
    {
        List<Student> sortedStudents = students.Values.OrderBy(s => s.Score).ToList();

        Console.WriteLine("\nСтуденты, отсортированные по баллам:");
        foreach (Student student in sortedStudents)
        {
            Console.WriteLine(student.Print());
        }
    }

    static void ShowStudents(Dictionary<string, Student> students)
    {
        Console.WriteLine("\nСписок студентов:");
        foreach (Student student in students.Values)
        {
            Console.WriteLine(student.Print());
        }
    }

    static void SaveToFile(Dictionary<string, Student> students, string filePath)
    {
        List<string> lines = new List<string>();
        foreach (Student student in students.Values)
        {
            lines.Add($"{student.LastName}, {student.FirstName}, {student.BirthYear}, {student.Exam}, {student.Score}");
        }

        File.WriteAllLines(filePath, lines);
    }


    static void ShuffleList(List<FileInfo> list)
    {
        Random random = new Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    static void PrintImageList(List<FileInfo> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {list[i].Name}");
        }
    }
    
    static List<int> FindShortestPath(Dictionary<int, List<int>> graph, int start, int target)
    {
        Queue<List<int>> queue = new Queue<List<int>>();
        queue.Enqueue(new List<int> { start });
        HashSet<int> visited = new HashSet<int>();
        visited.Add(start);

        while (queue.Count > 0)
        {
            List<int> path = queue.Dequeue();
            int currentNode = path[^1];

            if (currentNode == target)
                return path;

            if (graph.ContainsKey(currentNode))
            {
                foreach (int neighbor in graph[currentNode])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        List<int> newPath = new List<int>(path);
                        newPath.Add(neighbor);
                        queue.Enqueue(newPath);
                    }
                }
            }
        }

        return new List<int>();
    }

    static void Ex1()
    {
        string folder = "/Users/arinagomza/RiderProjects/homework_5/images";

        if (!Directory.Exists(folder))
        {
            Console.WriteLine($"Папка '{folder}' не найдена.");
            return;
        }

        FileInfo[] imageFiles = new DirectoryInfo(folder).GetFiles("*.jpg");

        List<FileInfo> imageList = new List<FileInfo>();

        for (int i = 0; i < 32; i++)
        {
            imageList.Add(imageFiles[i]);
            imageList.Add(imageFiles[i]);
        }

        Console.WriteLine("Изначальный список:");
        PrintImageList(imageList);

        ShuffleList(imageList);

        Console.WriteLine("\nПеремешанный список:");
        PrintImageList(imageList);
    }

    static void Ex2()
    {
        Dictionary<string, Student> students = new Dictionary<string, Student>();
        string[] paths = Directory.GetCurrentDirectory().Split('/');
        string filePath = String.Empty;

        for (int i = 0; i < paths.Length - 3; i++)
        {
            filePath += paths[i] + "/";
        }

        Console.WriteLine(filePath);

        filePath += "students.txt";

        if (File.Exists(filePath))
        {
            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] parts = line.Split(',');
                if (parts.Length == 5 &&
                    int.TryParse(parts[2].Trim(), out int birthYear) &&
                    int.TryParse(parts[4].Trim(), out int score))
                {
                    Student student = new Student
                    (
                        parts[0].Trim(),
                        parts[1].Trim(),
                        birthYear,
                        parts[3].Trim(),
                        score
                    );
                    students[student.LastName + " " + student.FirstName] = student;
                }
            }
        }

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Новый студент");
            Console.WriteLine("2. Удалить студента");
            Console.WriteLine("3. Сортировать студентов по баллам");
            Console.WriteLine("4. Показать всех студентов");
            Console.WriteLine("5. Выход");

            Console.Write("Введите номер действия: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStudent(students);
                    break;
                case "2":
                    RemoveStudent(students);
                    break;
                case "3":
                    SortStudents(students);
                    break;
                case "4":
                    ShowStudents(students);
                    break;
                case "5":
                    SaveToFile(students, filePath);
                    Console.WriteLine("Данные сохранены.");
                    return;
                default:
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                    break;
            }
        }
    }

    static void Ex4()
    {
        Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 2, 3 } },
            { 2, new List<int> { 4, 5 } },
            { 3, new List<int> { 6 } },
            { 4, new List<int> { 7 } },
            { 5, new List<int> { 7 } },
            { 6, new List<int> { 7 } },
            { 7, new List<int>() }
        };

        Console.WriteLine("Введите начальную вершину:");
        if (!int.TryParse(Console.ReadLine(), out int start))
        {
            Console.WriteLine("Ошибка: некорректный ввод числа для начальной вершины.");
            return;
        }

        Console.WriteLine("Введите целевую вершину:");
        if (!int.TryParse(Console.ReadLine(), out int target))
        {
            Console.WriteLine("Ошибка: некорректный ввод числа для целевой вершины.");
            return;
        }

        List<int> shortestPath = FindShortestPath(graph, start, target);

        if (shortestPath.Count > 0)
        {
            Console.WriteLine("Кратчайший путь:");
            Console.WriteLine(string.Join(" -> ", shortestPath));
        }
        else
        {
            Console.WriteLine("Путь не найден.");
        }
    }


    public static void Main(string[] args)
    {
        // Создать List на 64 элемента, скачать из интернета 32 пары картинок (любых). В List
        // должно содержаться по 2 одинаковых картинки. Необходимо перемешать List с
        // картинками. Вывести в консоль перемешанные номера (изначальный List и полученный
        // List). Перемешать любым способом.
        Console.WriteLine("Упражнение 1");
        Ex1();

        // Создать студента из вашей группы (фамилия, имя, год рождения, с каким экзаменом
        // поступил, баллы). Создать словарь для студентов из вашей группы (10 человек).
        // Необходимо прочитать информацию о студентах с файла. В консоли необходимо создать
        // меню:
        // a. Если пользователь вводит: Новый студент, то необходимо ввести
        // информацию о новом студенте и добавить его в List
        // b. Если пользователь вводит: Удалить, то по фамилии и имени удаляется
        // студент
        // c. Если пользователь вводит: Сортировать, то происходит сортировка по баллам
        // (по возрастанию)
        Console.WriteLine("Упражнение 2");
        Ex2();

        // Написать метод для обхода графа в глубину или ширину - вывести на экран кратчайший
        // путь.
        Console.WriteLine("Упражнение 4");
        Ex4();
    }
}