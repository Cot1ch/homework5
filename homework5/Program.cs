using System;
using System.Collections.Generic;
using System.Linq;

namespace homework5
{
    internal class Program
    {
        static void Main()
        {
            Task1();
            Task2();
            Task3();
            Task4();

            Console.WriteLine("Press something...");
            Console.ReadKey();
        }
        static void Task1()
        {
            
        }

        static void Task2()
        {
            Dictionary<string, object[]> students = new Dictionary<string, object[]>();
            students["Целоусов_Игорь"] = new object[] { 2005, "физика", 244 };
            students["Осипов_Семён"] = new object[] { 2005, "информатика", 252 };
            students["Кузьмина_Анастасия"] = new object[] { 2006, "информатика", 246 };
            students["Гомза_Арина"] = new object[] { 2006, "информатика", 250 };
            students["Харламова_Анна"] = new object[] { 2006, "английский", 249 };
            students["Квятковский_Всеволод"] = new object[] { 2006, "физика", 301 };
            students["Закиров_Айназ"] = new object[] { 2006, "информатика", 252 };
            students["Боронин_Никита"] = new object[] { 2006, "физика", 247 };
            students["Садриев_Салават"] = new object[] { 2006, "информатика", 240 };
            students["Калимуллин_Алмаз"] = new object[] { 2006, "информатика", 245 };

            bool flag = true;
            do
            {
                Console.WriteLine("Если вы хотите добавить студента, введите 'новый студент' в любом регистре без кавычек");
                Console.WriteLine("Если вы хотите удалить студента, введите 'удалить' в любом регистре без кавычек");
                Console.WriteLine("Если вы хотите вывести отсортированный список студентов, введите 'сортировать' в любом регистре без кавычек");
                Console.WriteLine("Чтобы завершить сеанс, нажмите Enter");

                string input = Console.ReadLine();
                if (String.IsNullOrEmpty(input))
                {
                    flag = false;
                }
                else
                {
                    switch (input.ToLower())
                    {
                        case "новый студент":
                            object[] newStud = new object[5];
                            newStud = NewStudent();

                            string key = newStud[1].ToString() + "_" + newStud[0].ToString();
                            object[] value = new object[3];
                            for (int i = 0; i < newStud.Length - 2; i++)
                            {
                                value[i] = newStud[i + 2];
                            }

                            students[key] = value;
                            Console.WriteLine("Студент добавлен");
                            break;
                        case "удалить":
                            DelStudent(students);
                            break;
                        case "сортировать":
                            SortStud(students);
                            break;
                        default:
                            Console.WriteLine("Почти, но не то. Попробуйте снова. Рахмэт");
                            break;
                    }
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

        static object[] NewStudent()
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

            object[] ret = new object[] { firstName, lastName, birthYear, exam, scores };
            return ret;
        }

        static void DelStudent(Dictionary<string, object[]> dict)
        {
            Console.WriteLine("Введите имя");
            string firstName = Console.ReadLine();
            Console.WriteLine("Введите фамилию");
            string lastName = Console.ReadLine();
            dict.Remove(lastName + "_" + firstName);

            Console.WriteLine($"{firstName} {lastName} благополучно удалён");
        }

        static void SortStud(Dictionary<string, object[]> dict)
        {
            Console.WriteLine("Отсортированный список:");
            foreach (var info in dict.OrderBy(info => info.Value[2]))
            {
                foreach (string str in info.Key.Split('_'))
                {
                    Console.Write(str + " ");
                }
                Console.Write($"\nГод рождения: {info.Value[0]}; ");
                Console.Write($"Экзамен: {info.Value[1] + ";",-12} ");
                Console.Write($"Итоговая сумма баллов: {info.Value[2]}\n");
            }
        }

        static void Task3()
        {
            Queue<Grandma> grandmas = new Queue<Grandma>();
            Stack<Hospital> hospitals = new Stack<Hospital>();

            hospitals.Push(new Hospital()
            {
                name = "Городская поликлиника",
                capacity = 5,
                deseases = new List<string> { "грипп", "простуда" }
            });
            hospitals.Push(new Hospital()
            {
                name = "Негородская поликлиника",
                capacity = 2,
                deseases = new List<string> { "кашель", "простуда" }
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

                if (String.IsNullOrEmpty(grandma.deseases[0]))
                {
                    Console.WriteLine(grandma.deseases);
                
                    foreach (var hospital in hospitals)
                    {
                        int des = grandma.deseases.Count(desease => hospital.deseases.Contains(desease));
                        
                        if ((double)des >= 0.5)
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
                            Console.WriteLine(hosp.waitList);
                            break;
                        }
                    }
                }

            }
            while (grandmas.Count > 0);

            foreach (Hospital hospital in hospitals)
            {
                Console.WriteLine(hospital.waitList);
            }
            
        }
        
        static void Task4()
        {

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
