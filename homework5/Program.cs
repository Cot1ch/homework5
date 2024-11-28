using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace homework5
{
    internal class Program
    {
        static void Main()
        {
            //Task1();
            //Task2();
            Task3();
            //Task4();

            Console.WriteLine("Press something...");
            Console.ReadKey();
        }

        /// <summary>
        /// Упражнение 6.1. В аргументы Main/консоль передаются названия файлов из папки resourses.
        /// Метод возвращает количество гласных и согласных в файле.
        /// </summary>
        /// <returns>-</returns>
        static void Task1()
        {
            Console.WriteLine("Задание 1\n");
        }

        /// <summary>
        /// Упражнение 6.1. В аргументы Main/консоль передаются названия файлов из папки resourses.
        /// Метод возвращает количество гласных и согласных в файле.
        /// </summary>
        /// <returns>-</returns>
        static void Task2()
        {
            Console.WriteLine("Задание 2\n");

            string[] paths = Directory.GetCurrentDirectory().Split('\\');
            string path = String.Empty;

            for (int i = 0; i < paths.Length - 3; i++)
            {
                path += paths[i] + "/";
            }


            path += "resourses/Студенты.txt";

            string[] readAllText = File.ReadAllLines(path);
            Dictionary<string, Student> students = new Dictionary<string, Student>();
            foreach (string line in readAllText)
            {
                Student student = new Student();
                student.firstName = line.Split(',')[0];
                student.lastName = line.Split(',')[1];
                student.birthYear = int.Parse(line.Split(',')[2]);
                student.exam = line.Split(',')[3];
                student.score = int.Parse(line.Split(',')[4]);

                students[student.lastName] = student;

            }
            bool flag = true;
            do
            {
                Console.WriteLine("Ввести нового студента - 'новый студент' (без кавычек)");
                Console.WriteLine("Удалить студента - 'удалить' (без кавычек)");
                Console.WriteLine("Сортироавть список студентов - 'сортировать' (без кавычек)");
                Console.WriteLine("Выйти - 'выход' (без кавычек)\n");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "выход":
                        flag = false;
                        break;
                    case "новый студент":
                        NewStudent(students);
                        break;
                    case "удалить":
                        DelStudent(students);
                        break;
                    case "сортировать":
                        SortStud(students);
                        break;
                    default:
                        Console.WriteLine("Неправильный ввод, попробуйте ещё раз");
                        break;
                }
            }
            while (flag);
        }

        static int EnterPosNumber()
        {
            bool flag = true;
            int number;
            do
            {
                bool isNumber = int.TryParse(Console.ReadLine(), out number);
                if (isNumber)
                {
                    flag = false;
                }
                else if (number <= 0)
                {
                    Console.WriteLine("Ну это прям хорошо меня вынесло. Но нет");
                }
                else
                {
                    Console.WriteLine("Неверный ввод - необходимо ввести целое число");
                }
            }
            while (flag);

            return number;
        }

        static int EnterBirthYear()
        {
            bool flag = true;
            int number;
            do
            {
                Console.WriteLine("Введите год рождения:");
                bool isNumber = int.TryParse(Console.ReadLine(), out number);
                if (isNumber)
                {
                    flag = false;
                }
                else if (number <= 0)
                {
                    Console.WriteLine("Ну ок");
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

        static void NewStudent(Dictionary<string, Student> studs)
        {
            Console.WriteLine("Введите имя");
            string firstName = Console.ReadLine();
            Console.WriteLine("Введите фамилию");
            string lastName = Console.ReadLine();

            int birthYear = EnterBirthYear();
            Console.WriteLine("Введите экзамен");
            string exam = Console.ReadLine();
            Console.WriteLine("Введите итоговую сумму баллов");
            int scores = EnterPosNumber();

            Student student = new Student();
            student.firstName = firstName;
            student.lastName = lastName;
            student.birthYear = birthYear;
            student.exam = exam;
            student.score = scores;

            studs[student.lastName] = student;
        }

        static void DelStudent(Dictionary<string, Student> studs)
        {
            Console.WriteLine("Введите фамилию");
            string lastName = Console.ReadLine();
            studs.Remove(lastName);

            Console.WriteLine($"Удаление прошло благополучно");
        }

        static void SortStud(Dictionary<string, Student> studs)
        {
            foreach (var stud in studs.OrderBy(stud => stud.Value.score))
            {
                stud.Value.Print();
            }
        }

        /// <summary>
        /// Оно как я. Не работает :(
        /// 
        /// </summary>
        /// <returns>-</returns>
        static void Task3()
        {
            Console.WriteLine("Задание 3\n");

            Queue<Grandma> grandmas = new Queue<Grandma>();
            Stack<Hospital> hospitals = new Stack<Hospital>();

            hospitals.Push(new Hospital()
            {
                name = "Городская поликлиника",
                capacity = 5,
                deseases = new List<string> { "грипп", "простуда", "астма", "склероз" }
            });
            hospitals.Push(new Hospital()
            {
                name = "Негородская поликлиника",
                capacity = 2,
                deseases = new List<string> { "кашель", "простуда", "обморожение" }
            });
            hospitals.Push(new Hospital()
            {
                name = "Максимально странная поликлиника",
                capacity = 3,
                deseases = new List<string> { "забыл, как называется", "склероз", "ещё что-то" }
            });

            //Ввод бабушек 
            bool flag = true;
            do
            {
                Console.WriteLine("Введите имя бабушки [либо 'выход' для выхода (логично)]");
                string input = Console.ReadLine();
                if (input.ToLower() == "выход")
                {
                    flag = false;
                }
                else
                {
                    Console.WriteLine("Введите возраст бабушки");
                    int ageInt = EnterPosNumber();

                    Console.WriteLine("Введите болезни бабушки. Если их несколько, используйте запятую как разделитель");
                    string[] desStr = Console.ReadLine().Split(',');

                    Console.WriteLine("Введите лекарства бабушки. Если их несколько, используйте запятую как разделитель");
                    string[] medStr = Console.ReadLine().Split(',');

                    Grandma newGrandma = new Grandma()
                    {
                        name = input,
                        age = ageInt,
                        deseases = desStr.ToList(),
                        medicines = medStr.ToList()
                    };

                    grandmas.Enqueue(newGrandma);
                }
            }
            while (flag);


            int countCryGrandmas = 0;
            Grandma grandma = new Grandma();
            do
            {
                grandma = grandmas.Dequeue();

                if (!String.IsNullOrEmpty(grandma.deseases[0]))
                {
                    Console.WriteLine(grandma.deseases);

                    foreach (var hospital in hospitals)
                    {
                        int des = hospital.deseases.Count(desease => grandma.deseases.Contains(desease));
                        Console.WriteLine(des);

                        if ((double)des > 0.5)
                        {
                            Console.WriteLine($"Бабушкино лечение продолжится в {hospital.name}");
                            flag = false;
                            grandma.status = $"{hospital.name}";
                            break;
                        }
                    }
                    if (flag)
                    {
                        Console.WriteLine("Бабушка отправляется плакать((");
                        countCryGrandmas++;
                    }

                }
                else
                {
                    while (hospitals.Count > 0)
                    {
                        Hospital hosp = hospitals.Pop();
                        if (hosp.waitList < hosp.capacity)
                        {
                            hosp.waitList++;
                            break;
                        }
                    }
                }

            }
            while (grandmas.Count > 0);

            Console.WriteLine(hospitals.Count);
            while (hospitals.Count > 0)
            {
                Hospital hosp = hospitals.Pop();
                Console.WriteLine($"{hosp.name} {hosp.waitList}");
            }
        }

        /// <summary>
        /// Упражнение 6.1. В аргументы Main/консоль передаются названия файлов из папки resourses.
        /// Метод возвращает количество гласных и согласных в файле.
        /// </summary>
        /// <returns>-</returns>
        static void Task4()
        {
            Console.WriteLine("Задание 4\n");
        }
    }
    struct Student
    {
        public string lastName;
        public string firstName;
        public int birthYear;
        public string exam;
        public int score;

        public void Print()
        {
            Console.WriteLine($"{lastName} {firstName}");
            Console.WriteLine($"Год рождения - {birthYear}");
            Console.WriteLine($"Экзамен - {exam}");
            Console.WriteLine($"Итоговые баллы - {score}\n");
        }
    }
    struct Grandma
    {
        public string name;
        public int age;
        public List<string> deseases;
        public List<string> medicines;
        public string status;
    }
    struct Hospital
    {
        public string name;
        public List<string> deseases;
        public int capacity;
        public int waitList;

    }
}

