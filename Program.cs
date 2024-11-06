using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoConsole {
    internal class Program {
        public static void PrintOptions() {
            Console.ResetColor();
            Console.WriteLine("Options: ");
            Console.WriteLine("1 - generate operations");
            Console.WriteLine("2 - insert (node count)");
            Console.WriteLine("3 - find (node count)");
            Console.WriteLine("4 - delete (node count)");
            Console.WriteLine("5 - insert (node parameters)");
            Console.WriteLine("6 - find (node parameters)");
            Console.WriteLine("7 - delete (node parameters)");
            Console.WriteLine("8 - print tree");
            Console.WriteLine("9 - end");
            Console.WriteLine();
            Console.Write("Your choice: ");
        }

        static void Main(string[] args) {
            Generator generator = new Generator();

            int option = -1, type = -1, count = 0, number = 0, index = -1;
            string description = "", sirka = "", dlzka = "";
            double x = 0f, y = 0f;

            while (option != 9) {
                option = -1;
                type = -1;
                count = 0;
                number = 0;
                description = "";
                sirka = "";
                dlzka = "";
                x = 0f;
                y = 0f;

                PrintOptions();

                int.TryParse(Console.ReadLine(), out option);

                Console.Clear();

                switch (option) {
                    case 1:
                        Console.Write("Enter operations count to execute: ");
                        int.TryParse(Console.ReadLine(), out count);
                        Console.WriteLine();

                        Task.Run(async () => await generator.GenerateOperations(2, count)).Wait();
                        break;

                    case 2:
                        while (type != 0 && type != 1 && type != 2) {
                            Console.Clear();
                            Console.WriteLine("Choose tree type:");
                            Console.WriteLine("[0] - parcela  [1] - nehnutelnost  [2] - both");
                            int.TryParse(Console.ReadLine(), out type);
                            Console.WriteLine();
                        }

                        Console.Write("Enter node count to insert: ");
                        int.TryParse(Console.ReadLine(), out count);
                        Console.WriteLine();

                        Task.Run(async () => await generator.Insert(type, count)).Wait();
                        break;

                    case 3:
                        while (type != 0 && type != 1 && type != 2) {
                            Console.Clear();
                            Console.WriteLine("Choose tree type:");
                            Console.WriteLine("[0] - parcela  [1] - nehnutelnost  [2] - both");
                            int.TryParse(Console.ReadLine(), out type);
                            Console.WriteLine();
                        }

                        Console.Write("Enter node count to find: ");
                        int.TryParse(Console.ReadLine(), out count);
                        Console.WriteLine();

                        Task.Run(async () => await generator.Find(type, count)).Wait();
                        break;

                    case 4:
                        while (type != 0 && type != 1 && type != 2) {
                            Console.Clear();
                            Console.WriteLine("Choose tree type:");
                            Console.WriteLine("[0] - parcela  [1] - nehnutelnost  [2] - both");
                            int.TryParse(Console.ReadLine(), out type);
                            Console.WriteLine();
                        }

                        Console.Write("Enter node count to delete: ");
                        int.TryParse(Console.ReadLine(), out count);
                        Console.WriteLine();

                        Task.Run(async () => await generator.Delete(type, count)).Wait();
                        break;

                    case 5:
                        while (type != 0 && type != 1) {
                            Console.Clear();
                            Console.WriteLine("Choose item type:");
                            Console.WriteLine("[0] - parcela  [1] - nehnutelnost");
                            int.TryParse(Console.ReadLine(), out type);
                            Console.WriteLine();
                        }

                        Console.WriteLine("Enter number:");
                        int.TryParse(Console.ReadLine(), out number);
                        Console.WriteLine("Enter description:");
                        description = Console.ReadLine();
                        Console.WriteLine("Enter latitude:");
                        sirka = Console.ReadLine();
                        Console.WriteLine("Enter position X:");
                        double.TryParse(Console.ReadLine(), out x);
                        Console.WriteLine("Enter longitude:");
                        dlzka = Console.ReadLine();
                        Console.WriteLine("Enter position Y:");
                        double.TryParse(Console.ReadLine(), out y);
                        Console.WriteLine();

                        GPS gps = new GPS(sirka, x, dlzka, y);
                        Item item;

                        if (type == 0) {
                            item = new Parcela(number, description, gps) as Item;
                            generator.InsertItem(type, ref item);
                        } else if (type == 1) {
                            item = new Nehnutelnost(number, description, gps) as Item;
                            generator.InsertItem(type, ref item);
                        }
                        break;

                    case 6:
                        while (type != 0 && type != 1 && type != 2) {
                            Console.Clear();
                            Console.WriteLine("Choose item type:");
                            Console.WriteLine("[0] - parcela  [1] - nehnutelnost [2] - both");
                            int.TryParse(Console.ReadLine(), out type);
                            Console.WriteLine();
                        }

                        Console.WriteLine("Enter number:");
                        int.TryParse(Console.ReadLine(), out number);
                        Console.WriteLine("Enter description:");
                        description = Console.ReadLine();
                        Console.WriteLine("Enter latitude:");
                        sirka = Console.ReadLine();
                        Console.WriteLine("Enter position X:");
                        double.TryParse(Console.ReadLine(), out x);
                        Console.WriteLine("Enter longitude:");
                        dlzka = Console.ReadLine();
                        Console.WriteLine("Enter position Y:");
                        double.TryParse(Console.ReadLine(), out y);
                        Console.WriteLine();

                        _ = generator.FindItem(type, new GPS(sirka, x, dlzka, y));
                        break;

                    case 7:
                        while (type != 0 && type != 1) {
                            Console.Clear();
                            Console.WriteLine("Choose item type:");
                            Console.WriteLine("[0] - parcela  [1] - nehnutelnost");
                            int.TryParse(Console.ReadLine(), out type);
                            Console.WriteLine();
                        }

                        Console.WriteLine("Enter number:");
                        int.TryParse(Console.ReadLine(), out number);
                        Console.WriteLine("Enter description:");
                        description = Console.ReadLine();
                        Console.WriteLine("Enter latitude:");
                        sirka = Console.ReadLine();
                        Console.WriteLine("Enter position X:");
                        double.TryParse(Console.ReadLine(), out x);
                        Console.WriteLine("Enter longitude:");
                        dlzka = Console.ReadLine();
                        Console.WriteLine("Enter position Y:");
                        double.TryParse(Console.ReadLine(), out y);
                        Console.WriteLine();

                        List<Item> result = generator.FindItem(type, new GPS(sirka, x, dlzka, y));

                        if (result.Count == 0) return;

                        Console.WriteLine("Enter index:");
                        int.TryParse(Console.ReadLine(), out index);
                        Console.WriteLine();

                        if (index < 0 || index >= result.Count) return;

                        Parcela parcela = null;
                        Nehnutelnost nehnutelnost = null;

                        if (type == 0) {
                            parcela = result[index] as Parcela;
                        } else if (type == 1) {
                            nehnutelnost = result[index] as Nehnutelnost;
                        }

                        generator.DeleteItem(type, parcela, nehnutelnost);
                        break;

                    case 8:
                        while (type != 0 && type != 1 && type != 2) {
                            Console.Clear();
                            Console.WriteLine("Choose tree type:");
                            Console.WriteLine("[0] - parcela  [1] - nehnutelnost  [2] - both");
                            int.TryParse(Console.ReadLine(), out type);
                            Console.WriteLine();
                        }

                        generator.PrintTreeInOrder(type);
                        break;

                    case 9:
                        Console.Clear();
                        break;

                    default:
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
