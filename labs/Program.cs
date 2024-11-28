using System;

class Program
{
    static (int, int) ConsonantsAndVowels(char[] characters)
    {
        char[] vowelsList = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
        int consonantCount = 0;
        int vowelCount = 0;
        foreach (char c in characters)
        {
            if (char.IsLetter(c))
            {
                if (vowelsList.Contains(c))
                {
                    vowelCount++;
                }
                else
                {
                    consonantCount++;
                }
            }
        }
        return (vowelCount, consonantCount);
    }
    
    static void PrintMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{matrix[i, j],4}"); 
            }
            Console.WriteLine();
        }
    }
    static int[,] MultiplyMatrices(int[,] matrixA, int[,] matrixB)
    {
        int rowsA = matrixA.GetLength(0);
        int colsA = matrixA.GetLength(1);
        int rowsB = matrixB.GetLength(0);
        int colsB = matrixB.GetLength(1);
        
        if (colsA != rowsB)
        {
            throw new InvalidOperationException("Умножение невозможно: число столбцов первой матрицы не равно числу строк второй матрицы.");
        }
        
        int[,] result = new int[rowsA, colsB];
        
        for (int i = 0; i < rowsA; i++)
        {
            for (int j = 0; j < colsB; j++)
            {
                for (int k = 0; k < colsA; k++)
                {
                    result[i, j] += matrixA[i, k] * matrixB[k, j];
                }
            }
        }

        return result;
    }

    static double[] Avg(int[,] temperature)
    {
        int months = temperature.GetLength(0); 
        int days = temperature.GetLength(1);  
        double[] averages = new double[months];

        for (int i = 0; i < months; i++)
        {
            int sum = 0;
            for (int j = 0; j < days; j++)
            {
                sum += temperature[i, j]; 
            }
            averages[i] = (double)sum / days; 
        }

        return averages;
    }

    static (int, int) ConsonantsAndVowels(List<char> charList)
    {
        List<char> vowelsList = new List<char> { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

        int vowelCount = 0;
        int consonantCount = 0;

        foreach (char c in charList)
        {
            if (char.IsLetter(c)) 
            {
                if (vowelsList.Contains(c)) 
                {
                    vowelCount++;
                }
                else
                {
                    consonantCount++; 
                }
            }
        }

        return (vowelCount, consonantCount); 
    }
    
    static void PrintMatrix(LinkedList<LinkedList<int>> matrix)
    {
        foreach (var row in matrix)
        {
            foreach (var value in row)
            {
                Console.Write($"{value,4}");
            }
            Console.WriteLine();
        }
    }
    
    static LinkedList<LinkedList<int>> MultiplyMatrices(LinkedList<LinkedList<int>> matrixA, LinkedList<LinkedList<int>> matrixB)
    {
        int rowsA = matrixA.Count;
        int colsA = matrixA.First.Value.Count;
        int rowsB = matrixB.Count;
        int colsB = matrixB.First.Value.Count;
        
        if (colsA != rowsB)
        {
            throw new InvalidOperationException("Умножение невозможно: число столбцов первой матрицы не равно числу строк второй матрицы.");
        }
        
        LinkedList<LinkedList<int>> result = new LinkedList<LinkedList<int>>();
        
        List<List<int>> aList = ToListOfLists(matrixA);
        List<List<int>> bList = ToListOfLists(matrixB);

        for (int i = 0; i < rowsA; i++)
        {
            LinkedList<int> row = new LinkedList<int>();
            for (int j = 0; j < colsB; j++)
            {
                int sum = 0;
                for (int k = 0; k < colsA; k++)
                {
                    sum += aList[i][k] * bList[k][j];
                }
                row.AddLast(sum);
            }
            result.AddLast(row);
        }

        return result;
    }

    static List<List<int>> ToListOfLists(LinkedList<LinkedList<int>> linkedMatrix)
    {
        List<List<int>> listMatrix = new List<List<int>>();
        foreach (var row in linkedMatrix)
        {
            listMatrix.Add(new List<int>(row));
        }
        return listMatrix;
    }

    static Dictionary<string, double> Avg(Dictionary<string, int[]> temperatures)
    {
        Dictionary<string, double> averages = new Dictionary<string, double>();

        foreach (var entry in temperatures)
        {
            string month = entry.Key;
            int[] dailyTemperatures = entry.Value;

            double avg = 0;
            foreach (int temp in dailyTemperatures)
            {
                avg += temp;
            }
            avg /= dailyTemperatures.Length;

            averages[month] = avg; 
        }

        return averages;
    }
    
    static List<KeyValuePair<string, double>> SortByValue(Dictionary<string, double> avgTemperatures)
    {
        var sortedList = new List<KeyValuePair<string, double>>(avgTemperatures);
        sortedList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
        return sortedList;
    }
    
    static void Task1(string filik)
         {
             if (!File.Exists(filik))
             {
                 Console.WriteLine($"Файла {filik} не существует.");
                 return;
             }
     
             try
             {
                 string fileContent = File.ReadAllText(filik);
                 char[] charArray = fileContent.ToCharArray();
                 (int vowelCount, int consonantCount) = ConsonantsAndVowels(charArray);
                 Console.WriteLine($"Количество гласных: {vowelCount}");
                 Console.WriteLine($"Количество согласных: {consonantCount}");
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"Произошла ошибка: {ex.Message}");
             }
             
         }
    
    static void Task2()
    {
        int[,] matrixA = 
        {
            { 1, 2, 3 },
            { 4, 5, 6 }
        };

        int[,] matrixB = 
        {
            { 7, 8 },
            { 9, 10 },
            { 11, 12 }
        };
        
        Console.WriteLine("Матрица A:");
        PrintMatrix(matrixA);

        Console.WriteLine("Матрица B:");
        PrintMatrix(matrixB);
        
        int[,] resultMatrix = MultiplyMatrices(matrixA, matrixB);
        
        Console.WriteLine("Результат умножения матриц:");
        PrintMatrix(resultMatrix);
    }

    static void Task3()
    {
        int month = 12;
        int days = 30;
        Random rand = new Random();
        
        int[,] temperature = new int[month, days];
        for (int i = 0; i < month ; i++)
        {
            for (int j = 0; j < days; j++)
            {
                temperature[i, j] = rand.Next(-40, 40);
            }
        }
        double[] avg = Avg(temperature);
        for (int i = 0; i < avg.Length; i++)
        {
            Console.WriteLine($"Месяц {i+1}: {avg[i]:F2}");
        }
        Console.WriteLine("Отсортированные средние значения температур");
        Array.Sort(avg);
        for (int i = 0; i < avg.Length; i++)
        {
            Console.WriteLine($"Месяц {i+1}: {avg[i]:F2}");
        }
    }

    static void Hw1(string filik)
    {
        if (!File.Exists(filik))
        {
            Console.WriteLine($"Файла {filik} не существует.");
            return;
        }

        try
        {
            string fileContent = File.ReadAllText(filik);
            
            List<char> charList = new List<char>(fileContent);
            
            (int vowelCount, int consonantCount) = ConsonantsAndVowels(charList);
            
            Console.WriteLine($"Количество гласных: {vowelCount}");
            Console.WriteLine($"Количество согласных: {consonantCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
    
    static void Hw2()
    {
        LinkedList<LinkedList<int>> matrixA = new LinkedList<LinkedList<int>>(new[]
        {
            new LinkedList<int>(new[] { 1, 2, 3 }),
            new LinkedList<int>(new[] { 4, 5, 6 })
        });
        
        LinkedList<LinkedList<int>> matrixB = new LinkedList<LinkedList<int>>(new[]
        {
            new LinkedList<int>(new[] { 7, 8 }),
            new LinkedList<int>(new[] { 9, 10 }),
            new LinkedList<int>(new[] { 11, 12 })
        });
        
        Console.WriteLine("Матрица A:");
        PrintMatrix(matrixA);

        Console.WriteLine("Матрица B:");
        PrintMatrix(matrixB);
        
        LinkedList<LinkedList<int>> resultMatrix = MultiplyMatrices(matrixA, matrixB);
        
        Console.WriteLine("Результат умножения матриц:");
        PrintMatrix(resultMatrix);
    }
    
    static void Hw3()
    {
        string[] monthNames = 
        {
            "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
            "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
        };

        int days = 30;
        Random rand = new Random();

        Dictionary<string, int[]> temperatures = new Dictionary<string, int[]>();
        
        foreach (string month in monthNames)
        {
            int[] dailyTemperatures = new int[days];
            for (int i = 0; i < days; i++)
            {
                dailyTemperatures[i] = rand.Next(-40, 40); 
            }
            temperatures[month] = dailyTemperatures;
        }
        
        Dictionary<string, double> avgTemperatures = Avg(temperatures);
        
        Console.WriteLine("Средние температуры по месяцам:");
        foreach (var entry in avgTemperatures)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value:F2}");
        }
        
        Console.WriteLine("\nОтсортированные средние температуры:");
        foreach (var entry in SortByValue(avgTemperatures))
        {
            Console.WriteLine($"{entry.Key}: {entry.Value:F2}");
        }
    }

    
    static void Main(string[] args)
    {
        // Написать программу, которая вычисляет число гласных и согласных букв в
        // файле. Имя файла передавать как аргумент в функцию Main. Содержимое текстового файла
        // заносится в массив символов. Количество гласных и согласных букв определяется проходом
        // по массиву. Предусмотреть метод, входным параметром которого является массив символов.
        // Метод вычисляет количество гласных и согласных букв.
        Console.WriteLine("Упражнение 6.1");
        if (args.Length == 0)
        {
            Console.WriteLine("Ошибка: Укажите имя файла в аргументах.");
        }
        else
        {
            Task1(args[0]);
        }
        
        // Написать программу, реализующую умножению двух матриц, заданных в
        // виде двумерного массива. В программе предусмотреть два метода: метод печати матрицы,
        // метод умножения матриц (на вход две матрицы, возвращаемое значение – матрица).
        Console.WriteLine("\nУпражнение 6.2");
        Task2();
        
        // Написать программу, вычисляющую среднюю температуру за год. Создать
        // двумерный рандомный массив temperature[12,30], в котором будет храниться температура
        // для каждого дня месяца (предполагается, что в каждом месяце 30 дней). Сгенерировать
        // значения температур случайным образом. Для каждого месяца распечатать среднюю
        // температуру. Для этого написать метод, который по массиву temperature [12,30] для каждого
        // месяца вычисляет среднюю температуру в нем, и в качестве результата возвращает массив
        // средних температур. Полученный массив средних температур отсортировать по
        // возрастанию.
        Console.WriteLine("\nУпражнение 6.3");
        Task3();
        
        // Упражнение 6.1 выполнить с помощью коллекции List<T>.
        Console.WriteLine("\nДомашнее задание 6.1");
        if (args.Length == 0)
        {
            Console.WriteLine("Ошибка: Укажите имя файла в аргументах.");
        }
        else
        {
            Hw1(args[0]);
        }
        
        // Упражнение 6.2 выполнить с помощью коллекций
        // LinkedList<LinkedList<T>>.
        Console.WriteLine("\nДомашнее задание 6.2");
        Hw2();
        
        // Написать программу для упражнения 6.3, использовав класс
        // Dictionary<TKey, TValue>. В качестве ключей выбрать строки – названия месяцев, а в
        // качестве значений – массив значений температур по дням.
        Console.WriteLine("\nДомашнее задание 6.3");
        Hw3();
        
    }
}