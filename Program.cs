using System;
using System.Threading.Tasks;

namespace GeoConsole {
    internal class Program {
        public static void PrintOptions() {
            Console.ResetColor();
            Console.WriteLine("Options: ");
            Console.WriteLine("0 - generate operations");
            Console.WriteLine("1 - insert (node count)");
            Console.WriteLine("2 - find (node count)");
            Console.WriteLine("3 - delete (node count)");
            Console.WriteLine("4 - print tree");
            Console.WriteLine("9 - end");
            Console.WriteLine();
            Console.Write("Your choice: ");
        }

        static void Main(string[] args) {
            Generator generator = new Generator();

            int option = -1, type = -1, count = 0;

            while (option != 9) {
                PrintOptions();

                int.TryParse(Console.ReadLine(), out option);

                Console.Clear();

                switch (option) {
                    case 0:
                        Console.Write("Enter operations count to execute: ");
                        int.TryParse(Console.ReadLine(), out count);
                        Console.WriteLine();

                        Task.Run(async () => await generator.GenerateOperations(2, count)).Wait();
                        break;

                    case 1:
                        type = -1;

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

                    case 2:
                        type = -1;

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

                    case 3:
                        type = -1;

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

                    case 4:
                        type = -1;

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
