using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClassLibraryLab10;

namespace LabWork12
{
    internal class Program
    {

        // Функция: Цветовая ошибка
        public static void PrintError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }


        // Функция вывода главного меню
        public static void PrintMainMenu()
        {
            Console.WriteLine("\n\n======================== Меню приложения ========================");
            Console.WriteLine("1 - Меню работы с Хеш-таблицей");
            Console.WriteLine("2 - Меню работы с двунаправленным списком");
            Console.WriteLine("3 - Меню работы с идеально сбалансированным деревом");
            Console.WriteLine("4 - Меню работы с обобщенной коллекцией");
            Console.WriteLine("0 - Выйти из приложения");
            Console.WriteLine("=================================================================");
        }


        // Функция вывода меню ( Первое: 1 )
        public static void Print_FirstMenu1()
        {
            Console.WriteLine("\n\n============ Меню работы с однонаправленным списком ============");
            Console.WriteLine("1 - Формирование однонаправленного списка");
            Console.WriteLine("2 - Добавление элемента в список");
            Console.WriteLine("3 - Удаление элемента из списка");
            Console.WriteLine("4 - Печать списка");
            Console.WriteLine("5 - Удаление списка из памяти");
            Console.WriteLine("6 - Очистка истории");
            Console.WriteLine("0 - Выход из меню");
            Console.WriteLine("================================================================");
        }

        // Функция обработки меню ( Первое: 1 )
        public static void Process_FirstMenu1()
        {

        }





        // Функция вывода  меню ( Первое: 2) - Вывод
        public static void Print_FirstMenu2()
        {
            Console.WriteLine("\n\n=========== Меню работы с двунаправленными списком ===========");
            Console.WriteLine("1 - Формирование двунаправленного списка");
            Console.WriteLine("2 - Добавление элемента в список");
            Console.WriteLine("3 - Удаление элемента из списка");
            Console.WriteLine("4 - Печать списка");
            Console.WriteLine("5 - Удаление списка из памяти");
            Console.WriteLine("0 - Назад");
            Console.WriteLine("==================================================================");
        }

        // Функция обработки меню ( Первое: 2) - Обработка
        public static void Process_FirstMenu2()
        {
            bool flag = true;
            BiList<Card> myBiList = new BiList<Card>(); // Создание списка

            while (flag)
            {
                // Поиск исключений
                try
                {
                    Print_FirstMenu2();
                    int choice = (int)InputHelper.InputUintNumber(""); // Считываем выбор пользователя

                    switch (choice)
                    {
                        case 1:
                            // Формирование двунаправленного списка
                            myBiList = Process_FirstMenu2_SecondMenu1();
                            break;
                        case 2:
                            // Добавление элемента в список
                            myBiList = Process_FirstMenu2_SecondMenu2(myBiList);
                            break;
                        case 3:
                            // Удаление элемента из списка
                            myBiList = Process_FirstMenu2_ThirdMenu3(myBiList);
                            break;
                        case 4:
                            // Печать списка
                            myBiList.PrintList();
                            break;
                        case 0:
                            // Назад
                            flag = false;
                            break;
                        default:
                            // Прочий ввод
                            PrintError("Ошибка! Данного номера не существует");
                            break;
                    }
                }
                // Вывод исключений
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }


        // Функция вывода меню ( Первое: 2 | Второе: 1) - Вывод
        public static void Print_FirstMenu2_SecondMenu1()
        {
            Console.WriteLine("\n\n============ Формирование двунаправленного списка ============");
            Console.WriteLine("1 - Пустой список");
            Console.WriteLine("2 - Список определенной длинны. (ДСЧ)");
            Console.WriteLine("3 - Список определенной длинны. (Ручной ввод)");
            Console.WriteLine("0 - Назад");
            Console.WriteLine("==================================================================");
        }

        // Функция обработки меню ( Первое: 2 | Второе: 1 ) - Обработка
        public static BiList<Card> Process_FirstMenu2_SecondMenu1()
        {
            bool flag = true;
            int length;
            BiList<Card> myBiListTime = new BiList<Card>(); // Создаем новый список

            while (flag)
            {
                // Поиск исключений
                try
                {
                    Print_FirstMenu2_SecondMenu1();
                    int choice = (int)InputHelper.InputUintNumber(""); // Считываем выбор пользователя

                    switch (choice)
                    {
                        case 1: // +
                            // Пустой список
                            Console.WriteLine("Список создан!");
                            break;
                        case 2:
                            // Список определенной длинны. (ДСЧ)
                            length = (int)InputHelper.InputUintNumber("Введите желаемую длинну списка: \t");
                            myBiListTime = new BiList<Card>(length); // Создание нового двунаправленного списка с случайными данными
                            Console.WriteLine("Список создан!");
                            break;
                        case 3:
                            // Список определенной длинны. (Ручной ввод)
                            length = (int)InputHelper.InputUintNumber("Введите желаемую длинну списка: \t");
                            myBiListTime.CreateListInit(length);    // Заполняем список данными, введенными пользователем
                            Console.WriteLine("Список создан!");
                            break;
                        case 0:
                            // Назад
                            flag = false;
                            break;
                        default:
                            // Прочий ввод
                            PrintError("Ошибка! Данного номера не существует");
                            break;
                    }
                }
                // Вывод исключений
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            return myBiListTime;
        }


        // Функция вывода меню  ( Первое: 2 | Второе: 2 ) - Вывод
        public static void Print_FirstMenu2_SecondMenu2()
        {
            Console.WriteLine("\n\n============== Добавление элемента в список ==============");
            Console.WriteLine("1 - Добавить элемент в конец списка (ДСЧ)");
            Console.WriteLine("2 - Добавить элемент в конец списка (Ручной ввод)");
            Console.WriteLine("3 - Добавить элемент в список после элемента с определенным значением (ДСЧ)");
            Console.WriteLine("4 - Добавить элемент в список после элемента с определенным значением (Ручной ввод)");
            Console.WriteLine("0 - Назад");
        }

        // Функция обработки меню ( Первое: 2 | Второе: 2 ) - Обработка
        public static BiList<Card> Process_FirstMenu2_SecondMenu2(BiList<Card> biList)
        {
            bool flag = true;
            Card timeCard = new Card();
            Card afterCad = new Card();

            while (flag)
            {
                // Поиск исключений
                try
                {
                    Print_FirstMenu2_SecondMenu2();
                    int choice = (int)InputHelper.InputUintNumber(""); // Считываем выбор пользователя

                    switch (choice)
                    {
                        case 1:
                            // Добавление в конец списка ( ДСЦ )

                            timeCard.RandomInit();
                            biList.AddToEnd(timeCard);
                            Console.WriteLine("Элемент добавлен!");
                            break;
                        case 2:
                            // Добавление в конец списка ( Ручной ввод )
                            timeCard.Init();
                            biList.AddToEnd(timeCard);
                            Console.WriteLine("Элемент добавлен!");
                            break;
                        case 3:
                            // Добавление в список после эл. с опр. значением ( data ) ДСЧ
                            timeCard.RandomInit();
                            Console.WriteLine("Данные элемента после которого необходимо вставить новый элемент:");
                            afterCad.Init();
                            if (biList.FindItem(afterCad))
                            {
                                biList.AddAfter(afterCad, timeCard);
                                Console.WriteLine("Элемент добавлен!");
                            }
                            else
                            {
                                PrintError("Данный объект не был найден!");
                            }
                            break;
                        case 4:
                            // Добавление в список после эл. с опр. значением ( data ) Ручной ввод
                            timeCard.RandomInit();
                            Console.WriteLine("Данные элемента после которого необходимо вставить новый элемент:");
                            afterCad.Init();
                            timeCard.Init();
                            biList.AddAfter(afterCad, timeCard);
                            Console.WriteLine("Элемент добавлен!");
                            break;
                        case 0:
                            // Назад
                            flag = false;
                            break;
                        default:
                            // Прочий ввод
                            PrintError("Ошибка! Данного номера не существует");
                            break;
                    }
                }
                // Вывод исключений
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            return biList;
        }


        // Функция вывода меню  ( Первое: 2 | Второе: 3 ) - Вывод
        public static void Print_FirstMenu2_hirdMenu3()
        {
            Console.WriteLine("\n\n============ Удаление элемента из списка ============");
            Console.WriteLine("1 - Удалить элемент с определынным значением");
            Console.WriteLine("2 - Удалить все четные элементы из списка");
            Console.WriteLine("0 - Назад");
        }

        // Функция обработки меню ( Первое: 2 | Второе: 3 ) - Обработка
        public static BiList<Card> Process_FirstMenu2_ThirdMenu3(BiList<Card> biList)
        {
            bool flag = true;

            while (flag)
            {
                // Поиск исключений
                try
                {
                    Print_FirstMenu2_hirdMenu3();
                    int choice = (int)InputHelper.InputUintNumber(""); // Считываем выбор пользователя

                    switch (choice)
                    {
                        case 1:
                            // Удалить элемент с определынным значением
                            Console.WriteLine("Введите данные элемента для удаления: ");
                            Card timeCard = new Card();
                            timeCard.Init();
                            PointBiList<Card> pointRemove = new PointBiList<Card>(timeCard);
                            biList.RemoveT(pointRemove); // Удаляем найденный узел
                            break;
                        case 2:
                            // Удалить все четные элементы из списка
                            biList.RemoveEven();
                            break;
                        case 0:
                            // Назад
                            flag = false;
                            break;
                        default:
                            // Прочий ввод
                            PrintError("Ошибка! Данного номера не существует");
                            break;
                    }
                }
                // Вывод исключений
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            return biList;
        }


        // Функция вывода меню  ( Первое: 3 ) - Вывод
        public static void Print_FirstMenu2_SecondMenu3()
        {
            Console.WriteLine("\n\n======== Меню работы с идеально сбалансированным деревом ========");
            Console.WriteLine("1 - Формирование дерева");
            Console.WriteLine("2 - Добавление элемента в дерево");
            Console.WriteLine("3 - Печать дерева");
            Console.WriteLine("4 - Поиск максимального элемента в дереве");
            Console.WriteLine("5 - Удаление дерева из памяти");
            Console.WriteLine("6 - Создание дерева поиска");
            Console.WriteLine("7 - Очистка истории");
            Console.WriteLine("0 - Выход из меню");
        }

        // Фукция обработки меню ( Первое: 3 )  - Обработка
        public static void Process_FirstMenu2_SecondMenu3()
        {

        }



        // Функция вывода меню  ( Первое: 4 ) - Вывод
        public static void Print_FirstMenu2_SecondMenu4()
        {
            Console.WriteLine("\n\n============== Меню работы с обобщенной коллекцией =============");
            Console.WriteLine("1 - Формирование дерева");
            Console.WriteLine("2 - Добавление элемента в дерево");
            Console.WriteLine("3 - Добавление элементов в дерево");
            Console.WriteLine("4 - Печать дерева");
            Console.WriteLine("5 - Поиск элемента по значению в дереве");
            Console.WriteLine("6 - Поверхностное клонирование дерева");
            Console.WriteLine("7 - Идеальное клонирование дерева");
            Console.WriteLine("8 - Удаление дерева из памяти");
            Console.WriteLine("9 - Очистка истории");
            Console.WriteLine("0 - Выход из меню");
        }

        // Фукция обработки меню  ( Первое: 4 ) - Обработка
        public static void Process_FirstMenu2_SecondMenu4()
        {

        }


        static void Main(string[] args)
        {
            {
                Console.WriteLine("==================== Лабораторная работа №12 ====================");
                Console.WriteLine("=================================================================");

                BiList<Card> myBiList = new BiList<Card>(); // Создаем новый список
                bool flagMainMenu = true;

                while (flagMainMenu)
                {
                    // Поиск исключений
                    try
                    {
                        PrintMainMenu(); // Вывод Главного меню
                        int choice = (int)InputHelper.InputUintNumber(""); // Считываем выбор пользователя


                        switch (choice)
                        {
                            case 1:
                                Process_FirstMenu1(); // Вывод Второстпенного меню - 1
                                break;
                            case 2:
                                Process_FirstMenu2(); // Вывод Второстпенного меню - 2
                                break;
                            case 3:
                                Process_FirstMenu2_SecondMenu3(); // Вывод Второстпенного меню - 3
                                break;
                            case 4:
                                Process_FirstMenu2_SecondMenu4(); // Вывод Второстпенного меню - 4
                                break;
                            case 0:
                                flagMainMenu = false; // Завершение работы программы...
                                break;
                            default:
                                PrintError("Ошибка! Такого пункта не существует.");
                                break;
                        }
                    }
                    // Вывод исключений
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }

                }
            }
        }
    }
}
