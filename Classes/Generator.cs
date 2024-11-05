using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoConsole {
    public class Generator {
        private KDTree<Parcela, GPS> parcelaTree = new KDTree<Parcela, GPS>(2);
        private KDTree<Nehnutelnost, GPS> nehnutelnostTree = new KDTree<Nehnutelnost, GPS>(2);
        private KDTree<Item, GPS> itemTree = new KDTree<Item, GPS>(2);

        private Random random = new Random();

        private List<string> ids = new List<string>();
        private List<Parcela> parcely = new List<Parcela>();
        private List<Nehnutelnost> nehnutelnosti = new List<Nehnutelnost>();

        public void InsertToTree(int treeType) {
            int cislo = random.Next();
            string popis = GenerateRandomString(10);

            double x = Math.Round(random.NextDouble() * 100, 1);
            double y = Math.Round(random.NextDouble() * 100, 1);
            GPS pozicia1 = new GPS(x, y);

            x = Math.Round(random.NextDouble() * 100, 1);
            y = Math.Round(random.NextDouble() * 100, 1);
            GPS pozicia2 = new GPS(x, y);

            Parcela parcela1, parcela2;
            Nehnutelnost nehnutelnost1, nehnutelnost2;
            Item item1, item2;

            switch (treeType) {
                case 0:
                    parcela1 = new Parcela(cislo, popis, pozicia1);
                    item1 = parcela1 as Item;
                    parcela2 = new Parcela(cislo, popis, pozicia2);
                    item2 = parcela2 as Item;

                    parcelaTree.InsertNode(ref parcela1, pozicia1);
                    parcelaTree.InsertNode(ref parcela2, pozicia2);

                    itemTree.InsertNode(ref item1, pozicia1);
                    itemTree.InsertNode(ref item2, pozicia2);

                    parcely.Add(parcela1);
                    parcely.Add(parcela2);

                    ids.Add(item1.Id);
                    ids.Add(item2.Id);
                    break;
                case 1:
                    nehnutelnost1 = new Nehnutelnost(cislo, popis, pozicia1);
                    item1 = nehnutelnost1 as Item;
                    nehnutelnost2 = new Nehnutelnost(cislo, popis, pozicia2);
                    item2 = nehnutelnost2 as Item;

                    nehnutelnostTree.InsertNode(ref nehnutelnost1, pozicia1);
                    nehnutelnostTree.InsertNode(ref nehnutelnost2, pozicia2);

                    itemTree.InsertNode(ref item1, pozicia1);
                    itemTree.InsertNode(ref item2, pozicia2);

                    nehnutelnosti.Add(nehnutelnost1);
                    nehnutelnosti.Add(nehnutelnost2);

                    ids.Add(item1.Id);
                    ids.Add(item2.Id);
                    break;
                case 2:
                    if (random.NextDouble() < 0.5) {
                        parcela1 = new Parcela(cislo, popis, pozicia1);
                        item1 = parcela1 as Item;
                        parcela2 = new Parcela(cislo, popis, pozicia2);
                        item2 = parcela2 as Item;

                        parcely.Add(parcela1);
                        parcely.Add(parcela2);

                        parcelaTree.InsertNode(ref parcela1, pozicia1);
                        parcelaTree.InsertNode(ref parcela2, pozicia2);
                    } else {
                        nehnutelnost1 = new Nehnutelnost(cislo, popis, pozicia1);
                        item1 = nehnutelnost1 as Item;
                        nehnutelnost2 = new Nehnutelnost(cislo, popis, pozicia2);
                        item2 = nehnutelnost2 as Item;

                        nehnutelnosti.Add(nehnutelnost1);
                        nehnutelnosti.Add(nehnutelnost2);

                        nehnutelnostTree.InsertNode(ref nehnutelnost1, pozicia1);
                        nehnutelnostTree.InsertNode(ref nehnutelnost2, pozicia2);
                    }

                    itemTree.InsertNode(ref item1, pozicia1);
                    itemTree.InsertNode(ref item2, pozicia2);

                    ids.Add(item1.Id);
                    ids.Add(item2.Id);
                    break;
                default:
                    Console.WriteLine("Invalid tree type");
                    break;
            }
        }

        public void FindInTree(int treeType, GPS gps) {
            switch (treeType) {
                case 0:
                    parcelaTree.FindNodes(gps);
                    break;
                case 1:
                    nehnutelnostTree.FindNodes(gps);
                    break;
                case 2:
                    itemTree.FindNodes(gps);
                    break;
                default:
                    Console.WriteLine("Invalid tree type");
                    break;
            }
        }

        public void DeleteFromTree(int treeType, string id) {
            ids.Remove(id);

            switch (treeType) {
                case 0:
                    Parcela parcela1 = parcely.Find(p => p.Id == id);
                    Item item1 = parcela1 as Item;

                    if (parcela1 != null && item1 != null) {
                        parcelaTree.DeleteNode(ref parcela1, parcela1.Pozicia);
                        itemTree.DeleteNode(ref item1, parcela1.Pozicia);
                        parcely.Remove(parcela1);
                    }
                    break;
                case 1:
                    Nehnutelnost nehnutelnost1 = nehnutelnosti.Find(n => n.Id == id);
                    Item item2 = nehnutelnost1 as Item;

                    if (nehnutelnost1 != null && item2 != null) {
                        nehnutelnostTree.DeleteNode(ref nehnutelnost1, nehnutelnost1.Pozicia);
                        itemTree.DeleteNode(ref item2, nehnutelnost1.Pozicia);
                        nehnutelnosti.Remove(nehnutelnost1);
                    }
                    break;
                case 2:
                    Parcela parcela2 = parcely.Find(p => p.Id == id);
                    Item item3 = parcela2 as Item;

                    if (parcela2 != null && item3 != null) {
                        parcelaTree.DeleteNode(ref parcela2, parcela2.Pozicia);
                        itemTree.DeleteNode(ref item3, parcela2.Pozicia);
                        parcely.Remove(parcela2);
                    }

                    Nehnutelnost nehnutelnost2 = nehnutelnosti.Find(n => n.Id == id);
                    Item item4 = nehnutelnost2 as Item;

                    if (nehnutelnost2 != null && item4 != null) {
                        nehnutelnostTree.DeleteNode(ref nehnutelnost2, nehnutelnost2.Pozicia);
                        itemTree.DeleteNode(ref item4, nehnutelnost2.Pozicia);
                        nehnutelnosti.Remove(nehnutelnost2);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid tree type");
                    break;
            }
        }

        public async Task GenerateOperations(int treeType, int operationCount) {
            (int insert, int find, int delete) = (0, 0, 0);

            for (int i = 0; i < operationCount; i++) {
                var operation = random.NextDouble();

                if (operation < 0.5) {
                    await Task.Run(() => InsertToTree(treeType));

                    insert++;
                } else if (operation < 0.75) {
                    GPS gps = null;

                    if (random.NextDouble() < 0.5) {
                        if (parcely.Count > 0) {
                            gps = parcely[random.Next(parcely.Count)].Pozicia;
                        }
                    } else {
                        if (nehnutelnosti.Count > 0) {
                            gps = nehnutelnosti[random.Next(nehnutelnosti.Count)].Pozicia;
                        }
                    }

                    if (gps == null) continue;

                    await Task.Run(() => FindInTree(treeType, gps));

                    find++;
                } else {
                    if (ids.Count == 0) continue;

                    ids.Clear();

                    List<Node<Item, GPS>> allNodes = this.itemTree.GetAllNodes();

                    foreach (var node in allNodes) {
                        foreach (var item in node.NodeData) {
                            ids.Add(item.Id);
                        }
                    }

                    string id = ids[random.Next(ids.Count)];

                    await Task.Run(() => DeleteFromTree(treeType, id));

                    delete++;
                }
            }

            Console.WriteLine("------------------");
            Console.WriteLine($"Insert: {insert}");
            Console.WriteLine($"Find: {find}");
            Console.WriteLine($"Delete: {delete}");
            Console.WriteLine("------------------");
        }

        public async Task Insert(int treeType, int nodeCount) {
            for (int i = 0; i < nodeCount / 2; i++) {
                await Task.Run(() => InsertToTree(treeType));
            }
        }

        public async Task Find(int treeType, int nodeCount) {
            for (int i = 0; i < nodeCount; i++) {
                GPS gps;

                if (random.NextDouble() < 0.5) {
                    if (parcely.Count == 0) return;

                    gps = parcely[random.Next(parcely.Count)].Pozicia;
                } else {
                    if (nehnutelnosti.Count == 0) return;

                    gps = nehnutelnosti[random.Next(nehnutelnosti.Count)].Pozicia;
                }

                await Task.Run(() => FindInTree(treeType, gps));
            }
        }

        public async Task Delete(int treeType, int nodeCount) {
            for (int i = 0; i < nodeCount; i++) {
                if (ids.Count == 0) continue;

                string id = ids[random.Next(ids.Count)];

                await Task.Run(() => DeleteFromTree(treeType, id));
            }
        }

        public void InsertItem(int itemType, ref Item item) {
            switch (itemType) {
                case 0:
                    if (item is Parcela p) {
                        parcelaTree.InsertNode(ref p, p.Pozicia);
                        itemTree.InsertNode(ref item, p.Pozicia);

                        parcely.Add(p);
                        ids.Add(item.Id);
                    }
                    break;

                case 1:
                    if (item is Nehnutelnost n) {
                        nehnutelnostTree.InsertNode(ref n, n.Pozicia);
                        itemTree.InsertNode(ref item, n.Pozicia);

                        nehnutelnosti.Add(n);
                        ids.Add(item.Id);
                    }
                    break;

                default:
                    Console.WriteLine("Invalid tree type");
                    break;
            }
        }

        public List<Item> FindItem(int itemType, GPS gps) {
            List<Item> result = new List<Item>();

            try {
                Console.WriteLine($"KEYS (GPS): [{gps.X};{gps.Y}]");
                Console.WriteLine("-------------------------------------------------------------------------------------------");

                int index = 0;

                switch (itemType) {
                    case 0:
                        parcelaTree.FindNodes(gps).ForEach(x => { Console.Write(index++ + ". "); x.PrintInfo(); result.Add(x); });
                        break;

                    case 1:
                        nehnutelnostTree.FindNodes(gps).ForEach(x => { Console.Write(index++ + ". "); x.PrintInfo(); result.Add(x); });
                        break;

                    case 2:
                        itemTree.FindNodes(gps).ForEach(x => { Console.Write(index++ + ". "); x.PrintInfo(); result.Add(x); });
                        break;

                    default:
                        Console.WriteLine("Invalid item type");
                        break;
                }

                Console.WriteLine("-------------------------------------------------------------------------------------------");
                Console.WriteLine();

                return result;
            } catch (NullReferenceException) {
                return result;
            }
        }

        public void DeleteItem(int itemType, Parcela parcela = null, Nehnutelnost nehnutelnost = null) {
            try {
                switch (itemType) {
                    case 0:
                        if (parcela != null) {
                            Parcela par = parcely.Find(p => p.Id == parcela.Id);
                            Item item = par as Item;

                            parcelaTree.DeleteNode(ref parcela, parcela.Pozicia);
                            itemTree.DeleteNode(ref item, parcela.Pozicia);

                            parcely.Remove(parcela);
                            ids.Remove(item.Id);
                        }
                        break;

                    case 1:
                        if (nehnutelnost != null) {
                            Nehnutelnost neh = nehnutelnosti.Find(n => n.Id == nehnutelnost.Id);
                            Item item = neh as Item;

                            nehnutelnostTree.DeleteNode(ref nehnutelnost, nehnutelnost.Pozicia);
                            itemTree.DeleteNode(ref item, nehnutelnost.Pozicia);

                            nehnutelnosti.Remove(nehnutelnost);
                            ids.Remove(item.Id);
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid item type");
                        break;
                }
            } catch (NullReferenceException) {
                return;
            }
        }


        public void DeleteItem(int itemType, ref Item item) {
            try {
                switch (itemType) {
                    case 0:
                        if (item is Parcela p) {
                            Console.WriteLine("ITEM IS Parcela");
                            parcelaTree.DeleteNode(ref p, p.Pozicia);
                            itemTree.DeleteNode(ref item, p.Pozicia);

                            parcely.Remove(p);
                            ids.Remove(item.Id);
                        }
                        break;

                    case 1:
                        if (item is Nehnutelnost n) {
                            Console.WriteLine("ITEM IS Nehnutelnost");
                            nehnutelnostTree.DeleteNode(ref n, n.Pozicia);
                            itemTree.DeleteNode(ref item, n.Pozicia);

                            nehnutelnosti.Remove(n);
                            ids.Remove(item.Id);
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid item type");
                        break;
                }
            } catch (NullReferenceException) {
                Console.WriteLine("SHIT HAPPENED");
                return;
            }
        }

        public void PrintTreeInOrder(int treeType) {
            switch (treeType) {
                case 0:
                    parcelaTree.PrintInOrder();
                    break;
                case 1:
                    nehnutelnostTree.PrintInOrder();
                    break;
                case 2:
                    itemTree.PrintInOrder();
                    break;
                default:
                    Console.WriteLine("Invalid tree type");
                    break;
            }
        }

        private string GenerateRandomString(int length) {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[this.random.Next(s.Length)]).ToArray());
        }

        private void RefreshListOfIDs() {
            ids.Clear();

            List<Node<Item, GPS>> allNodes = this.itemTree.GetAllNodes();

            foreach (var node in allNodes) {
                foreach (var item in node.NodeData) {
                    ids.Add(item.Id);
                }
            }
        }
    }
}
