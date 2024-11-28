using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Tumakov5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task1(args);
            Task2();
            Task3();
            Task4(args);
            Task5();
            Task6();

            Console.WriteLine("Press F...");
            Console.ReadKey();
        }

        /// <summary>
        /// Считывает строку символов с консоли и преобразует ее к целому числу. Ввод продолжается до тех пор, 
        /// пока пользователь не введет число.
        /// </summary>
        /// <returns>Число типа int</returns>
        static int EnterNumber()
        {
            bool flag = true;
            int number;
            do
            {
                Console.WriteLine("Введите целое число:");
                bool isNumber = int.TryParse(Console.ReadLine(), out number);
                if (isNumber)
                {
                    flag = false;
                }
                else
                {
                    Console.WriteLine("Неверный ввод - необходимо ввести целое число");
                }
            }
            while (flag);

            return number;
        }

        /// <summary>
        /// Считывает положительное число с консоли - размер одного из измерений матрицы.
        /// Ждет число ло победного
        /// </summary>
        /// <returns>Число типа int</returns>
        static int EnterMatrixSize()
        {
            bool flag = true;
            int number = 0;
            do
            {
                try
                {
                    bool isNumber = int.TryParse(Console.ReadLine(), out number);
                    if (isNumber && number> 0)
                    {
                        flag = false;
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод - необходимо ввести целое положительное число, не превышающее 255");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            while (flag);

            return number;
        }

        /// <summary>
        /// Упражнение 6.1. В аргументы Main/консоль передаются названия файлов из папки resourses.
        /// Метод возвращает количество гласных и согласных в файле
        /// </summary>
        /// <returns>-</returns>
        static void Task1(string[] args1)
        {
            Console.WriteLine("Упражнение 6.1");
            if (args1 == null)
            {
                Console.WriteLine("Введите названия файлов через запятую БЕЗ ПРОБЕЛОВ");
                args1 = Console.ReadLine().Split(',');
            }
            foreach (string arg in args1)
            {
                string[] paths = Directory.GetCurrentDirectory().Split('\\');
                string path = String.Empty;

                for (int i = 0; i < paths.Length - 3; i++)
                {
                    path += paths[i] + "/";
                }

                
                char[] text = OpenAndReadF($"{path}/resourses/{arg}").ToCharArray();
                Console.WriteLine($"Файл {arg}:");
                
                CountLettaz(text);
            }
        }

        /// <summary>
        /// Упражнение 6.2. Метод находит произведение двух матриц, вводимых с консоли
        /// Пользователь вводит размеры матриц и их содержимое
        /// </summary>
        /// <returns>-</returns>
        static void Task2()
        {
            Console.WriteLine("\nУпражнение 6.2");
            Console.WriteLine("Введите количество строк первой матрицы");
            int lenFirst = EnterMatrixSize();

            Console.WriteLine("Введите количество столбцов первой (и количество строк второй) матрицы");
            int widFirst = EnterMatrixSize();

            Console.WriteLine("Введите количество столбцов второй матрицы");
            int widSecond = EnterMatrixSize();

            int[,] firstMatrix = new int[lenFirst, widFirst];
            int[,] secondMatrix = new int[widFirst, widSecond];

            Console.WriteLine("Вводим первую матрицу");
            for (int i = 0; i < lenFirst; i++)
            {
                Console.WriteLine("Новая строка");
                for (int j = 0; j < widFirst; j++)
                {
                    firstMatrix[i, j] = EnterNumber();
                }
            }
            PrintMatrix(firstMatrix);

            Console.WriteLine("Вводим вторую матрицу");
            for (int i = 0; i < widFirst; i++)
            {
                Console.WriteLine("Новая строка");
                for (int j = 0; j < widSecond; j++)
                {
                    secondMatrix[i, j] = EnterNumber();
                }
            }
            PrintMatrix(secondMatrix);

            Console.WriteLine("Результат умножения равен:");
            PrintMatrix(MulMatrixs(firstMatrix, secondMatrix));
        }

        /// <summary>
        /// К Task2. Метод перемножает матрицы
        /// </summary>
        /// <returns>Матрица int[,]</returns>
        static int[,] MulMatrixs(int[,] fMatrix, int[,] sMatrix)
        {
            int n = fMatrix.GetLength(0);
            int m = fMatrix.GetLength(1);
            int l = sMatrix.GetLength(1);

            int[,] answer = new int[n, l];
            int dop = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < l; j++)
                {
                    for (int k = 0; k < m; k++)
                    {
                        dop += fMatrix[i, k] * sMatrix[k, j];
                    }
                    answer[i, j] = dop;
                    dop = 0;
                }
            }

            return answer;
        }

        /// <summary>
        /// К Task2. Метод печатает матрицу
        /// </summary>
        /// <returns>-</returns>
        static void PrintMatrix(int[,] matrix)
        {
            Console.WriteLine("Матрица:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Упражнение 6.3. Генерируется двумерный массив температур по месяцам и считается среднее значение по каждому месяцу
        /// Вывод - отсортированный по возрастанию массив средних температур
        /// </summary>
        /// <returns>-</returns>
        static void Task3()
        {
            Console.WriteLine("\nУпражнение 6.3");
            int[,] temperature = new int[12, 30];
            string[] months = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };

            Random rnd = new Random();

            Console.WriteLine("Средняя температура по месяцам");
            for (int i = 0; i < temperature.GetLength(0); i++)
            {
                Console.WriteLine(months[i]);
                for (int j = 0; j < temperature.GetLength(1); j++)
                {
                    temperature[i, j] = rnd.Next(-30, 30);
                    Console.Write($"{temperature[i, j],3}|");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Средние температуры:");
            double[] ans = GetMeanTemp(temperature);

            for (int i = 0; i < ans.Length; i++) 
            {
                Console.Write($"{ans[i]:F1}{"\u00B0"}C ");
            }
        }

        /// <summary>
        /// Считывает строку символов с консоли и преобразует ее к целому числу. Ввод продолжается до тех пор, 
        /// пока пользователь не введет число.
        /// </summary>
        /// <returns>Число типа int</returns>
        static double[] GetMeanTemp(int[,] arr)
        {
            double[] meanTemp = new double[12];

            for (int month = 0; month < 12; month++)
            {
                int mean = 0;
                for (int day = 0; day < 30; day++)
                {
                    mean += arr[month, day];
                }
                meanTemp[month] = mean / 12.0;
            }
            Array.Sort(meanTemp);
            return meanTemp;
        }

        /// <summary>
        /// Считывает строку символов с консоли и преобразует ее к целому числу. Ввод продолжается до тех пор, 
        /// пока пользователь не введет число.
        /// </summary>
        /// <returns>Число типа int</returns>
        static void Task4(string[] args4)
        {
            Console.WriteLine("\nДомашнее задание 6.1");
            foreach (string arg in args4)
            {
                string[] paths = Directory.GetCurrentDirectory().Split('\\');
                string path = String.Empty;

                for (int i = 0; i < paths.Length - 3; i++)
                {
                    path += paths[i] + "/";
                }

                List<char> text = new List<char>();

                foreach (char letter in OpenAndReadF($"{path}/resourses/{arg}"))
                {
                    text.Add(char.ToLower(letter));
                }
                Console.WriteLine($"Файл {arg}:");
                CountLettaz(text);
            }
        }

        /// <summary>
        /// Считывает строку символов с консоли и преобразует ее к целому числу. Ввод продолжается до тех пор, 
        /// пока пользователь не введет число.
        /// </summary>
        /// <returns>Число типа int</returns>
        static void CountLettaz(char[] letters)
        {
            long vowelsCount = 0;
            long consonantsCount = 0;
            foreach (char letter in letters)
            {
                if ("eyuioaуеыаоёяию".Contains(letter))
                {
                    vowelsCount++;
                }
                else if ("йцкнгшщзхфвпрлджчсмтъьбqwrtpsdfghjklzxcvbnm".Contains(letter))
                {
                    consonantsCount++;
                }
            }
            
            Console.WriteLine($"Количество гласных = {vowelsCount}, согласных = {consonantsCount}\n");
        }

        /// <summary>
        /// Считывает строку символов с консоли и преобразует ее к целому числу. Ввод продолжается до тех пор, 
        /// пока пользователь не введет число.
        /// </summary>
        /// <returns>Число типа int</returns>
        static void CountLettaz(List<char> letters)
        {
            long vowelsCount = 0;
            long consonantsCount = 0;
            foreach (char letter in letters)
            {
                if ("eyuioaуеыаоёяию".Contains(letter))
                {
                    vowelsCount++;
                }
                else if ("йцкнгшщзхфвпрлджчсмтъьбqwrtpsdfghjklzxcvbnm".Contains(letter))
                {
                    consonantsCount++;
                }
            }

            Console.WriteLine($"Количество гласных = {vowelsCount}, согласных = {consonantsCount}");
        }

        /// <summary>
        /// Считывает строку символов с консоли и преобразует ее к целому числу. Ввод продолжается до тех пор, 
        /// пока пользователь не введет число.
        /// </summary>
        /// <returns>Число типа int</returns>
        static string OpenAndReadF(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string readContent = File.ReadAllText(filePath);
                    return readContent;
                }
                else
                {
                    throw new Exception("Файл не найден");
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Считывает строку символов с консоли и преобразует ее к целому числу. Ввод продолжается до тех пор, 
        /// пока пользователь не введет число.
        /// </summary>
        /// <returns>Число типа int</returns>
        static void Task5() ///Дописать!!!!!!!!!!!
        {
            Console.WriteLine("\nДомашнее задание 6.2");

            Console.WriteLine("Введите количество строк первой матрицы");
            int lenFirst = EnterMatrixSize();

            Console.WriteLine("Введите количество столбцов первой (и количество строк второй) матрицы");
            int widFirst = EnterMatrixSize();

            Console.WriteLine("Введите количество столбцов второй матрицы");
            int widSecond = EnterMatrixSize();

            LinkedList<LinkedList<int>> firstMatrix = new LinkedList<LinkedList<int>>();    
            LinkedList<LinkedList<int>> secondMatrix = new LinkedList<LinkedList<int>>();

            LinkedList<int> line = new LinkedList<int>();

            for (int i = 0; i < lenFirst; i++)
            {
                for (int j = 0; j < widFirst; j++)
                {
                    line.AddLast(EnterNumber());
                }
                firstMatrix.AddLast(line);
            }
            Console.WriteLine(secondMatrix);

            for (int i = 0; i < widFirst; i++)
            {
                for (int j = 0; j < widSecond; j++)
                {
                    line.AddLast(EnterNumber());
                }
                firstMatrix.AddLast(line);
            }
            Console.WriteLine(secondMatrix);
        }

        /// <summary>
        /// Считывает строку символов с консоли и преобразует ее к целому числу. Ввод продолжается до тех пор, 
        /// пока пользователь не введет число.
        /// </summary>
        /// <returns>Число типа int</returns>
        static void Task6()
        {
            Console.WriteLine("\nДомашнее задание 6.3");

            Random rnd = new Random();
            Dictionary<string, int[]> temperature = new Dictionary<string, int[]>();
            string[] months = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
            
            for (int i = 0; i < months.Length; i++)
            {
                int[] days = new int[30];
                Console.WriteLine(months[i]);

                for (int j = 0; j < 30;  j++)
                {
                    days[j] = rnd.Next(-30, 30);
                    Console.Write($"{days[j], 3}|");
                }
                Console.WriteLine();
                temperature[months[i]] = days;
            }

            Console.WriteLine("Средние температуры: ");
            foreach (string month in months)
            {
                Console.WriteLine($"{month}: {temperature[month].Sum()/12.0:F1}{"\u00B0"}C");
            }
        }

    }
}
