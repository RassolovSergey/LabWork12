using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClassLibraryLab10;
using LabWork12.Part_4;

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
            Console.WriteLine("\n\n================== Меню работы с Хеш-Таблицей ==================");
            Console.WriteLine("1 - Формирование Хеш-Таблицы");
            Console.WriteLine("2 - Добавление элемента в список");
            Console.WriteLine("3 - Удаление элемента из списка");
            Console.WriteLine("4 - Печать списка");
            Console.WriteLine("5 - Найти элемент в Хеш-Таблице");
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
                                hashTable.Add(timeCard);
                            }
                            break;
                        case 3:
                            // Удаление элемента из Хеш-Таблицы
                            if (hashTable.Count > 0)
                            {
                                Console.WriteLine($"Введите элемент для удаления:");
                                timeCard = new Card();
                                timeCard.Init(); // Инициализация объекта Card для удаления
                                bool isRemoved = hashTable.Remove(timeCard);
                                if (isRemoved)
                                {
                                    Console.WriteLine($"Элемент {timeCard} успешно удален.");
                                }
                                else
                                {
                                    Console.WriteLine($"Элемент {timeCard} не найден для удаления.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ошибка: Ваша таблица пуста!");
                            }
                            break;
                        case 4:
                            // Печать Хеш-Таблицы
                            Console.WriteLine($"\n============ Хеш-Таблицы ============");
                            hashTable.PrintTable();
                            break;
                        case 5:
                            // Поиск элемента в таблице
                            if (hashTable.Count > 0)
                            {
                                Console.WriteLine("============== Поиск элемента Таблицы ==============");
                                timeCard = new Card();
                                timeCard.Init();        // Инициализация объекта Card для удаления
                                Card foundCard = hashTable.Find(timeCard);
                                if (foundCard != null)
                                {
                                    Console.WriteLine($"Результат поиска: {foundCard}");
                                }
                                else
                                {
                                    Console.WriteLine($"Карта: {timeCard} - Не была найдена");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ошибка: Ваша таблица пуста!");
                            }
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
                            myBiList.Clear();
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
            Console.WriteLine("2 - Список определенной длинны.");
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
            Console.WriteLine("2 - Добавить элемент в список после элемента с определенным значением (ДСЧ)");
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
                            biList.Add(timeCard);
                            Console.WriteLine("Элемент добавлен!");
                            break;
                        case 2:
                            if (biList.beg != null && biList.end != null)
                            {
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
                            }
                            else
                            {
                                PrintError("Ошибка: Список пуст!");
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
            if (biList.beg != null && biList.end != null)
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
                                biList.Remove(pointRemove);
                                break;
                            case 2:
                                // Удалить все четные элементы из списка
                                if (biList.beg == biList.end)
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
            MyTree<Card> myTree = new MyTree<Card>(0);      // Дерево для дальнейшей работы (базовое значение - 0)
            MyTree<Card> searchTree = new MyTree<Card>(0);  // Дерево поиска для дальнейшей работы (базовое значение - 0)
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
                                Console.WriteLine($"\nСреднее значение ID: {average}");
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
                                            myTree.Clear();
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
                                            searchTree.Clear();
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

                                // Вывод дерева поиска
                                Console.WriteLine("\nДерево поиска:");
                                searchTree.PrintTree();
                            }
                            else
                            {
                                PrintError("Ошибка: Ваше дерево не содержит элементов!");
                            }
                            break;
                        case 7:
                            // Удаление элемента с заданным ключом из дерева поиска
                            if (searchTree.Count > 0)
                            {
                                Console.WriteLine("Введите данные элемента для удаления:");
                                Card timeCard = new Card();
                                timeCard.Init();
                                bool wasRemoved = searchTree.Remove(timeCard);
                            }
                            else
                            {
                                PrintError("Ошибка: Ваше дерево не содержит элементов!");
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
            Console.WriteLine("============ Коллекция на базе ИСБД и Дерева поиска ============");
            Console.WriteLine("1 - Создать коллекцию");
            Console.WriteLine("2 - Добавление объекта");
            Console.WriteLine("3 - Вывод количества объектов");
            Console.WriteLine("4 - Демонстрация копирования");
            Console.WriteLine("5 - Удалить объект из коллекции");
            Console.WriteLine("6 - Удалить коллекцию");
            Console.WriteLine("7 - Печать");
            Console.WriteLine("0 - Назад");
            Console.WriteLine("================================================================");
        }


        // Фукция обработки меню  ( Первое: 4 ) - Обработка
        public static void Process_FirstMenu4()
        {

            MyCollectionTree<Card> myCollection = new MyCollectionTree<Card>();
            MyCollectionTree<Card> myCollectionCopy = new MyCollectionTree<Card>();

            bool flag4Menu = true;
            int size;

            while (flag4Menu)
            {
                try
                {
                    Print_FirstMenu4();
                    int choice = (int)InputHelper.InputUintNumber("Выберите действие: \t"); // Считываем выбор пользователя
                    switch (choice)
                    {
                        case 1:

                            bool flagMinMenu = true;

                            while (flagMinMenu)
                            {
                                Console.WriteLine("====================== Создание коллекции ======================");
                                Console.WriteLine("1 - Создать пустую коллекцию");
                                Console.WriteLine("2 - Создать коллекцию с указанной длинной");
                                Console.WriteLine("3 - Создать коллекцию с использованием массива элементов");
                                Console.WriteLine("0 - Назад");
                                Console.WriteLine("================================================================");

                                int choiceLvl2 = (int)InputHelper.InputUintNumber("Выберите действие: \t"); // Считываем выбор пользователя
                                switch (choiceLvl2)
                                {
                                    case 1:
                                        myCollection = new MyCollectionTree<Card>();
                                        Console.WriteLine("Коллекция создана!");
                                        break;
                                    case 2:
                                        size = (int)InputHelper.InputUintNumber("Введите желаемую длинну коллекции: \t");
                                        myCollection = new MyCollectionTree<Card>(size);
                                        Console.WriteLine("Коллекция создана!");
                                        break;
                                    case 3:
                                        size = (int)InputHelper.InputUintNumber("Введите желаемую длинну массива: \t");
                                        Card[] cardArray = new Card[size];
                                        for (int i = 0; i < size; i++)
                                        {
                                            Card card = new Card();
                                            card.RandomInit();
                                            cardArray[i] = card;
                                        }
                                        myCollection = new MyCollectionTree<Card>(cardArray);
                                        Console.WriteLine("Коллекция создана!");
                                        break;
                                    case 0:
                                        flagMinMenu = false;
                                        break;
                                    default:
                                        PrintError("Ошибка! Такого пункта не существует.");
                                        break;
                                }
                            }
                            break;
                        case 2:
                            if (myCollection != null)
                            {
                                Card timeCard = new Card();
                                timeCard.Init();
                                myCollection.Add(timeCard);
                                Console.WriteLine("Объект добавлен! (сгенерировано ДСЧ)");
                            }
                            else
                            {
                                PrintError("Коллекция отсутствует!");
                            }
                            break;
                        case 3:
                            Console.WriteLine($"Кол-во объектов в коллекции: {myCollection.Count}"); ;
                            break;
                        case 4:
                            if (myCollection.Count > 0 && myCollection != null)
                            {
                                Console.WriteLine("===== Демонстрация копирования а массив (CopyTo) =====");
                                Console.WriteLine($"\n\nИсходное дерево:");
                                myCollection.PrintTree();


                                // Создание массива для копирования
                                Card[] cardArray = new Card[myCollection.Count];

                                // Копирование элементов коллекции в массив
                                myCollection.CopyTo(cardArray, 0);

                                // Вывод содержимого массива на консоль для демонстрации
                                Console.WriteLine("Содержимое массива после копирования: \n");


                                foreach (Card card in cardArray)
                                {
                                    card.Print();
                                    Console.WriteLine();
                                }
                            }
                            else
                            {
                                PrintError("Ваша коллекция пуста!");
                            }
                            break;
                        case 5:
                            // Удалить объект из коллекции
                            if (myCollection.Count > 0)
                            {
                                Console.WriteLine("Введите объект для удаления: \t");
                                Card timeCard = new Card();
                                timeCard.Init();
                                myCollection.Remove(timeCard);
                            }
                            else
                            {
                                PrintError("Ваша коллекция пуста!");
                            }
                            break;
                        case 6:
                            // Удалить коллекцию
                            if (myCollection.Count > 0)
                            {
                                myCollection.Clear();
                                Console.WriteLine("Коллекция удалена!");
                            }
                            else
                            {
                                PrintError("Ваша коллекция пуста!");
                            }
                            break;
                        case 7:
                            if (myCollection.Count > 0)
                            {
                                Console.WriteLine("Ваше дерево:");
                                myCollection.PrintTree();

                                Console.WriteLine("Вывод при помощи (foreach): ");
                                Console.WriteLine("");
                                foreach (var item in myCollection)
                                {
                                    item.Print();
                                    Console.WriteLine();
                                }
                            }
                            else
                            {
                                PrintError("Ваша коллекция пуста!");
                            }
                            break;
                        case 0:
                            flag4Menu = false;
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
