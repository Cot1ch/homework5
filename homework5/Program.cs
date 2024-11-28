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
            Task2();
            //Task3();
            //Task4();

            Console.WriteLine("Press something...");
            Console.ReadKey();
        }
        static void Task1()
        {
            
        }

        static void Task2()
        {
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
                Console.WriteLine("Выйти - 'выход' (без кавычек)");

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

        static void Task3()
        {
                        
        }
        
        static void Task4()
        {

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

}
