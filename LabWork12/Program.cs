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
            Console.WriteLine("2 - Меню работы с Двунаправленным списком");
            Console.WriteLine("3 - Меню работы с Идеально сбалансированным деревом");
            Console.WriteLine("4 - Меню работы с Обобщенной коллекцией");
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
            Console.WriteLine("0 - Выход из меню");
            Console.WriteLine("================================================================");
        }

        // Функция обработки меню ( Первое: 1 )
        public static void Process_FirstMenu1()
        {
            bool flag = true;
            HashTable<Card> hashTable = new HashTable<Card>(10); // Создание новой хеш-Таблицы
            Card timeCard;
            int size;

            while (flag)
            {
                try
                {
                    Print_FirstMenu1();
                    int choice = (int)InputHelper.InputUintNumber("Выберите действие: ");

                    switch (choice)
                    {
                        case 1:
                            // Формирование Хеш-Таблицы
                            size = (int)InputHelper.InputUintNumber($"Введите размер Хеш-Таблицы: ");
                            if (size <= 0)
                            {
                                Console.WriteLine("Ошибка: Размер Хеш-Таблицы должен быть больше 0.");
                                break;
                            }
                            hashTable = new HashTable<Card>(size);
                            break;
                        case 2:
                            // Добавление элементов в Хеш-Таблицу
                            size = (int)InputHelper.InputUintNumber($"Сколько элементов хотите добавить? ");
                            for (int i = 0; i < size; i++)
                            {
                                timeCard = new Card();
                                timeCard.RandomInit();
                                hashTable.AddPoint(timeCard);
                            }
                            break;
                        case 3:
                            // Удаление элемента из Хеш-Таблицы
                            Console.WriteLine($"Введите элемент для удаления:");
                            timeCard = new Card();
                            timeCard.Init(); // Инициализация объекта Card для удаления
                            bool isRemoved = hashTable.RemoveData(timeCard);
                            if (isRemoved)
                            {
                                Console.WriteLine($"Элемент {timeCard} успешно удален.");
                            }
                            else
                            {
                                Console.WriteLine($"Элемент {timeCard} не найден для удаления.");
                            }
                            break;
                        case 4:
                            // Печать Хеш-Таблицы
                            Console.WriteLine($"\n============ Хеш-Таблицы ============");
                            hashTable.PrintTable();
                            break;
                        case 0:
                            // Назад
                            flag = false;
                            break;
                        default:
                            // Обработка некорректного ввода
                            Console.WriteLine("Ошибка! Введите корректный номер операции.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка! Некорректный ввод числа.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}");
                }
            }
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
            Console.WriteLine("6 - Демонстрация клонирования списка");
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
                        case 5:
                            // Удаление списка из памяти
                            myBiList.Dispose();
                            break;
                        case 6:
                            // Демонстрация клонирования списка
                            BiList<Card> CloneBiList = myBiList.DeepClone(); // Создаем клон списка
                            Console.WriteLine("Исходный список:");
                            myBiList.PrintList();

                            Console.WriteLine("\nГлубокая копия списка:");
                            CloneBiList.PrintList();

                            Console.WriteLine("\nСписок изменяется....\n");

                            Card timeCard = new Card();
                            myBiList.beg.Data = timeCard;

                            Console.WriteLine("Исходный список:");
                            myBiList.PrintList();

                            Console.WriteLine("\nГлубокая копия списка:");
                            CloneBiList.PrintList();
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
                        case 1: //
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
            if (biList.beg != biList.end)
            {
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
                                if (biList.Length() == 1)
                                {
                                    Console.WriteLine("В списке всего 1 элемент");
                                }
                                else
                                {
                                    biList.RemoveEven();
                                }
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
            else
            {
                Console.WriteLine("Ошибка: Список Пуст");
            }
            return biList;
        }


        // Функция вывода меню  ( Первое: 3 ) - Вывод
        public static void Print_FirstMenu3()
        {
            Console.WriteLine("\n\n======== Меню работы с идеально сбалансированным деревом ========");
            Console.WriteLine("1 - Формирование дерева");
            Console.WriteLine("2 - Демонстрация глубокого копирования");
            Console.WriteLine("3 - Печать дерева");
            Console.WriteLine("4 - Поиск среднего ID");
            Console.WriteLine("5 - Удаление дерево из памяти");
            Console.WriteLine("6 - Преобразование ИСБД в дерево поиска");
            Console.WriteLine("7 - Удаление элемент с заданым ключом из дерева поиска");
            Console.WriteLine("0 - Выход из меню");
            Console.WriteLine("=================================================================");
        }

        // Фукция обработки меню ( Первое: 3 )  - Обработка
        public static void Process_FirstMenu3()
        {
            bool flag = true;
            MyTree<Card> myTree = new MyTree<Card>(0); // Дерево для дальнейшей работы (базовое значение - 4)
            MyTree<Card> searchTree = new MyTree<Card>(0); // Дерево поиска для дальнейшей работы (базовое значение - 4)
            int size;

            while (flag)
            {
                // Поиск исключений
                try
                {
                    Print_FirstMenu3();
                    int choice = (int)InputHelper.InputUintNumber("Выберите действие: \t"); // Считываем выбор пользователя

                    switch (choice)
                    {
                        case 1: //
                            // Формирование дерева
                            size = (int)InputHelper.InputUintNumber("Введите желаемую длину списка: \t");
                            myTree = new MyTree<Card>(size);
                            break;
                        case 2:
                            if (myTree.Count > 0)
                            {
                                // Демонстрация глубокого копирования

                                Console.WriteLine("Исходное дерево:");
                                myTree.PrintTree();

                                MyTree<Card> copiedTree = myTree.DeepCopy(); // Глубокое копирование дерева

                                // Внесем изменения в копию дерева
                                Console.WriteLine("\nИзменим данные в копии дерева:");
                                copiedTree.ChangeTreeData();

                                Console.WriteLine("\nИсходное дерево (без изменений):");
                                myTree.PrintTree();

                                Console.WriteLine("\nИзмененная копия дерева:");
                                copiedTree.PrintTree();
                            }
                            else
                            {
                                PrintError("Ошибка: Ваше дерево не содержит элементов!");
                            }
                            break;
                        case 3:
                            // Печать дерева
                            bool flagPrint = true;
                            while (flagPrint)
                            {
                                Console.WriteLine("\n\n================= Печать  =================");
                                Console.WriteLine("1 - Распечатать: Идеально сбалансированное дерево");
                                Console.WriteLine("2 - Распечатать: Дерево поиска");
                                Console.WriteLine("0 - Назад");
                                Console.WriteLine("===========================================");
                                int choicePrint = (int)InputHelper.InputUintNumber("Выберите действие: \t");
                                switch (choicePrint)
                                {
                                    case 1:
                                        if (myTree.Count <= 0)
                                        {
                                            PrintError("Ошибка: Ваше дерево не содержит элементов!");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"====== Идеально сбалансированное Бинарное Дерево ======\t");
                                            myTree.PrintTree();
                                        }
                                        break;
                                    case 2:
                                        if (searchTree.Count <= 0)
                                        {
                                            PrintError("Ошибка: Ваше дерево не содержит элементов!");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"============ Дерево Поиска ======\t");
                                            searchTree.PrintTree();
                                        }
                                        break;
                                    case 0:
                                        // Назад
                                        flagPrint = false;
                                        break;
                                    default:
                                        // Прочий ввод
                                        PrintError("Ошибка! Данного номера не существует");
                                        break;
                                }
                            }
                            break;
                        case 4:
                            // Поиск среднего ID
                            if (myTree.Count > 0)
                            {
                                // Поиск среднего ID
                                Console.WriteLine($"================== Поиск среднего ID ==================\t");

                                // Вычисление среднего значения
                                double average = myTree.CalculateAverage();
                                Console.WriteLine($"\nСреднее значение num.number: {average}");
                            }
                            else
                            {
                                PrintError("Ошибка: Ваше дерево не содержит элементов!");
                            }
                            break;
                        case 5:
                            // Удаление дерева из памяти
                            // Печать дерева
                            bool flagDel = true;
                            while (flagDel)
                            {
                                Console.WriteLine("\n\n================= Удаление  =================");
                                Console.WriteLine("1 - Удалить: Идеально сбалансированное дерево");
                                Console.WriteLine("2 - Удалить: Дерево поиска");
                                Console.WriteLine("0 - Назад");
                                Console.WriteLine("===========================================");
                                int choicePrint = (int)InputHelper.InputUintNumber("Выберите действие: \t");
                                switch (choicePrint)
                                {
                                    case 1:
                                        if (myTree.Count <= 0)
                                        {
                                            PrintError("Ошибка: Ваше дерево не содержит элементов!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nУдаление дерева...");
                                            myTree.DeleteTree();
                                        }
                                        break;
                                    case 2:
                                        if (searchTree.Count <= 0)
                                        {
                                            PrintError("Ошибка: Ваше дерево не содержит элементов!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nУдаление дерева...");
                                            searchTree.DeleteTree();
                                        }
                                        break;
                                    case 0:
                                        // Назад
                                        flagDel = false;
                                        break;
                                    default:
                                        // Прочий ввод
                                        PrintError("Ошибка! Данного номера не существует");
                                        break;
                                }
                            }
                            break;
                        case 6:
                            // Преобразование ИСБД в дерево поиска
                            if (myTree.Count > 0)
                            {
                                Console.WriteLine("Идеально сбалансированное дерево:");
                                myTree.PrintTree();

                                // Преобразование идеально сбалансированного дерева в дерево поиска
                                searchTree = myTree.CreateSearchTree();


                                // Вывод идеально сбалансированного дерева
                                Console.WriteLine("\nДерево поиска:");
                                searchTree.PrintTree();

                                // Вывод дерева поиска
                                Console.WriteLine("\nИдеально сбалансированное дерево:");
                                myTree.PrintTree();
                            }
                            else
                            {
                                PrintError("Ошибка: Ваше дерево не содержит элементов!");
                            }
                            break;
                        case 7:
                            // Удаление элемент с заданым ключом из дерева поиска
                            if (searchTree.Count > 0)
                            {
                                Console.WriteLine("Введите элемент для удаления:");
                                Card timeCard = new Card();
                                timeCard.Init();
                                searchTree.Delete(timeCard);
                            }

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



        // Функция вывода меню  ( Первое: 4 ) - Вывод
        public static void Print_FirstMenu4()
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
        public static void Process_FirstMenu4()
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
                                Process_FirstMenu3(); // Вывод Второстпенного меню - 3
                                break;
                            case 4:
                                Process_FirstMenu4(); // Вывод Второстпенного меню - 4
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
