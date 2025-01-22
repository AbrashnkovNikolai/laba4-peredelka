using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using NPOI.SS.Formula.Functions;
using System.Runtime.CompilerServices;

public class Program
{
    public static void Main()
    {
        
        List<int> L = new List<int> { 1, 2, 1, 3 };
        Console.Write("текущий список: ");
        foreach (var item in L)
        {
            Console.Write(item + " ");
        }
        task_1(L);
        Console.WriteLine("\ntask_1 completed");
        Console.WriteLine("task_2: ");
        List<int> L_2 = new List<int> { 222, 2, 1, 3 };
        Console.Write("текущий список: ");
        foreach (var item in L_2)
        {
            Console.Write(item.ToString() + " ");
        }
        task_2(L_2);
        Console.WriteLine("");
        task_3(Gen_shoolFirms(), ["Фирма А", "Фирма Б", "Фирма В", "Фирма Г", "Фирма Д", "успешные ребятки"]);
        Console.WriteLine("task_4:\n");
        task_4("input.txt");
    }





    public static void PrintLinkedListReversed<T>(LinkedList<T> list)
    {
        // 
        LinkedListNode<T> current = list.Last;
        Console.Write("\nсписок в обратном порядке: ");
        // Проходим по списку в обратном порядке.
        while (current != null)
        {
            // Выводим текущий элемент.
            Console.Write(current.Value + " ");

            // Переходим к следующему элементу.
            current = current.Previous;
        }

        Console.WriteLine(); // Добавляем пустую строку в конце
    }

    private static void task_1<T>(List<T> L) where T : struct
    {
        Console.WriteLine("\nВведите элемент для удаления:");
        string input = Console.ReadLine();

        // Приводим ввод к типу T, предполагая, что T будет int
        if (int.TryParse(input, out int intValue))
        {
            T E = (T)(object)intValue; // Приводим int к T

            // Удаляем элементы из списка
            RemoveElements(L, E);

            // Выводим список после удаления.
            Console.WriteLine("Список после удаления элементов E:");
            foreach (var item in L)
            {
                Console.Write(item + " ");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
        }
    }

    // Метод для удаления всех вхождений элемента из списка
    private static void RemoveElements<T>(List<T> list, T element) where T : struct
    {
        // Перебираем список в обратном порядке, чтобы избежать пропуска элементов при удалении
        for (int i = list.Count - 1; i >= 0; i--)
        {
            // Если текущий элемент равен E, удаляем его
            if (list[i].Equals(element))
            {
                list.RemoveAt(i);
            }
        }
    }
    public static Dictionary<string, HashSet<string>> Gen_shoolFirms()
    {
        Console.WriteLine("входные данные для задания 3: \n");
        int n = 5;
        string[] schoolNames = { "Школа №1", "Гимназия №2", "Лицей №3", "Школа №4", "Гимназия №5" };
        string[] firmNames = { "Фирма А", "Фирма Б", "Фирма В", "Фирма Г", "Фирма Д", "успешные ребятки" };
        Console.WriteLine("школы: " + string.Join(", ", schoolNames));
        Console.WriteLine("фирмы: " + string.Join(", ", firmNames) + "\n");


        // Создаем словарь, где ключом является название учебного заведения,
        // а значением - множество фирм, у которых оно делало закупки.
        Dictionary<string, HashSet<string>> schoolFirms = new Dictionary<string, HashSet<string>>();
        for (int i = 0; i < n; i++)
        {
            schoolFirms[schoolNames[i]] = new HashSet<string>();
        }
        // Заполнение данных о закупках (пример):
        schoolFirms["Школа №1"].Add("Фирма Б");
        schoolFirms["Школа №1"].Add("Фирма Б");
        schoolFirms["Гимназия №2"].Add("Фирма В");
        schoolFirms["Лицей №3"].Add("Фирма В");
        schoolFirms["Лицей №3"].Add("Фирма Г");
        schoolFirms["Школа №4"].Add("Фирма Б");
        schoolFirms["Школа №4"].Add("Фирма Д");
        foreach (string name in schoolNames)
        {
            schoolFirms[name].Add(firmNames.Last());
        }
        return schoolFirms;
    }

    private static void task_2<T>(List<T> L) where T : struct
    {
        LinkedList<T> myLinkedList = new LinkedList<T>();

        foreach (T item in L)
        {
            myLinkedList.AddLast(item);
        }
        PrintLinkedListReversed(myLinkedList);
    }

    public static void task_3(Dictionary<string, HashSet<string>> schoolFirms, string[] firmNames)
    {
        // Ввод данных и выввод входнных данных в консоль
        var schoolNames = schoolFirms.Keys;


        Console.WriteLine("информация о закупках:");
        foreach (string school in schoolNames)
        {
            Console.WriteLine($"{school}: {string.Join(", ", schoolFirms[school])}");
        }
        Console.Write("\n");

        // 1) Фирмы, где закупались все учебные заведения
        var firmsAllSchools = new HashSet<string>(firmNames);
        foreach (string school in schoolNames)
        {
            firmsAllSchools.IntersectWith(schoolFirms[school]); // Пересечение множеств
        }
        Console.Write("\nПУНКТ 1. Фирмы, где закупались все учебные заведения:\nответ: ");
        Console.Write(string.Join(", ", firmsAllSchools));



        // 2) В каких фирмах закупка производилась хотя бы одним из заведений?
        Console.WriteLine("\nпункт 2: В каких фирмах закупка производилась хотя бы одним из заведений?");
        HashSet<string> firmsWithPurchases = new HashSet<string>();
        foreach (string school in schoolNames)
        {
            foreach (string firm in schoolFirms[school])
            {
                firmsWithPurchases.Add(firm);
            }
        }
        Console.WriteLine(string.Join(", ", firmsWithPurchases));


        // 3) В каких фирмах ни одно из заведений не закупало компьютеры?
        HashSet<string> firmsWithoutPurchases = new HashSet<string>(firmNames);
        firmsWithoutPurchases.ExceptWith(firmsWithPurchases);
        Console.Write("\nПункт 3: Фирмы, где ни одно из заведений не закупало компьютеры: ");
        Console.Write(string.Join(", ", firmsWithoutPurchases));
        Console.WriteLine();
    }


    public static void task_4(string filePath)
    {

        try
        {
            string text = File.ReadAllText(filePath);
            HashSet<char> characters = new HashSet<char>(text.ToLower());

            // Определение звонких согласных
            string voicedConsonants = "бвгджзлмнр";
            HashSet<char> voicedConsonantsSet = new HashSet<char>(voicedConsonants);
            voicedConsonantsSet.IntersectWith(characters);


            Console.WriteLine($"Исходный текст в файле: {text}");
            Console.WriteLine("Звонкие согласные в тексте в алфавитном порядке:");
            Console.WriteLine(string.Join(", ", voicedConsonantsSet));

        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Файл {filePath} не найден.");
        }
    }
}

