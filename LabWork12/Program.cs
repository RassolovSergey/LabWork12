using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        // Функция вывода второстепенного меню меню v1 
        public static void PrintSecondMenuV1()
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

        // Функция вывода второстепенного меню меню v2
        public static void PrintSecondMenuV2()
        {
            int point = 1;
            while (point != 0)
            {
                Console.WriteLine("\n\n=========== Меню работы с двунаправленными списком ===========");
                Console.WriteLine("1 - Формирование двунаправленного списка");
                Console.WriteLine("2 - Добавление элемента в список");
                Console.WriteLine("3 - Удаление элемента из списка");
                Console.WriteLine("4 - Печать списка");
                Console.WriteLine("5 - Удаление из списка всех четных элементов");
                Console.WriteLine("6 - Удаление списка из памяти");
                Console.WriteLine("0 - Выход из меню");
                Console.WriteLine("================================================================");

                point = (int)InputHelper.InputUintNumber("");

                switch (point)
                {
                    case 1:
                        Console.WriteLine("============= Формирование двунаправленного списка =============");
                        BiList_01<Card> myList = new BiList_01<Card>(5); // Создание списка длинной 5
                        break;
                    case 2:
                        Console.WriteLine("================= Добавление элемента в список =================");
                        break;
                    case 3:
                        Console.WriteLine("================== Удаление элемента из списка ==================");
                        break;
                    case 4:
                        Console.WriteLine("========================= Печать списка =========================");
                        break;
                    case 5:
                        Console.WriteLine("============ Удаление из списка всех четных элементов ===========");
                        break;
                    case 6:
                        Console.WriteLine("=================== Удаление списка из памяти ===================");
                        break;
                    case 0:
                        Console.WriteLine("Выход...");
                        break;
                    default:    // Прочие значения: Ошибка!
                        PrintError("Некорректное значение.");
                        break;
                }
            }
        }

        // Функция вывода второстепенного меню меню v3
        public static void PrintSecondMenuV3()
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

        // Функция вывода второстепенного меню меню v4
        public static void PrintSecondMenuV4()
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
        static void Main(string[] args)
        {
            {
                // Декор
                Console.WriteLine("==================== Лабораторная работа №12 ====================");
                Console.WriteLine("=================================================================");

                int answer = 1; // Переменная для работы с меню 1 уровня
                bool flag = true; // Переменная для работы меню

                // Цикл - Главное меню
                while (answer != 0)
                {
                    // Поиска ошибок
                    try     
                    {
                        PrintMainMenu();        // Вывод Главного меню
                        answer = (int)InputHelper.InputUintNumber("");  // Считываем число пользователя

                        //   switch - Главное меню
                        switch (answer)
                        {
                            case 1: // Хеш-Таблица
                                PrintSecondMenuV1();                                    // Вывод второго меню
                                int answerV2 = (int)InputHelper.InputUintNumber("");    // Считываем число пользователя
                                flag = true;                                       // Флаг для Цикла
                                while (flag)
                                {
                                    //   switch - Второстепенное меню
                                    switch (answerV2)
                                    {
                                        case 1:
                                            Console.WriteLine("============ Формирование однонаправленного списка =============");
                                            break;
                                        case 2:
                                            Console.WriteLine("================= Добавление элемента в список =================");
                                            break;
                                        case 3:
                                            Console.WriteLine("================== Удаление элемента из списка ==================");
                                            break;
                                        case 4:
                                            Console.WriteLine("========================= Печать списка =========================");
                                            break;
                                        case 5:
                                            Console.WriteLine("=================== Удаление списка из памяти ===================");
                                            break;
                                        case 6:
                                            Console.WriteLine("======================== Очистка истории ========================");
                                            break;
                                        case 0:
                                            Console.WriteLine("Выход...");
                                            flag = false;
                                            break;
                                        default:    // Прочие значения: Ошибка!
                                            PrintError("Некорректное значение.");
                                            break;
                                    }
                                }
                                break;
                            case 2:
                                PrintSecondMenuV2();                                // Вывод второго меню
                                int answerV3 = (int)InputHelper.InputUintNumber("");    // Считываем число пользователя
                                flag = true;                                       // Флаг для Цикла
                                while (flag)
                                {
                                    //   switch - Второстепенное меню
                                    switch (answerV3)
                                    {
                                        case 1:
                                            Console.WriteLine("============= Формирование двунаправленного списка =============");
                                            break;
                                        case 2:
                                            Console.WriteLine("================= Добавление элемента в список =================");
                                            break;
                                        case 3:
                                            Console.WriteLine("================== Удаление элемента из списка ==================");
                                            break;
                                        case 4:
                                            Console.WriteLine("======================= Печать списка ===========================");
                                            break;
                                        case 5:
                                            Console.WriteLine("======================= Удаление списка =========================");
                                            break;
                                        case 6:
                                            Console.WriteLine("=================== Удаление списка из памяти ===================");
                                            break;
                                        case 0:
                                            Console.WriteLine("Выход...");
                                            flag = false;
                                            break;
                                        default:    // Прочие значения: Ошибка!
                                            PrintError("Некорректное значение.");
                                            break;
                                    }
                                }
                                break;
                            case 3:
                                PrintSecondMenuV3();
                                break;
                            case 4:
                                PrintSecondMenuV4();
                                break;
                            case 0:     // Завершение работы
                                Console.WriteLine("Завершение...");
                                break;
                            default:    // Прочие значения: Ошибка!
                                PrintError("Некорректное значение.");
                                break;
                        }
                    }
                    catch (Exception ex) { Console.WriteLine(ex); } // Выводим системную ошибку
                }
            }
        }
    }
}
