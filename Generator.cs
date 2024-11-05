using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoConsole {
    public class Generator {
        private KDTree<Parcela, GPS> parcelaTree = new KDTree<Parcela, GPS>();
        private KDTree<Nehnutelnost, GPS> nehnutelnostTree = new KDTree<Nehnutelnost, GPS>();
        private KDTree<Item, GPS> itemTree = new KDTree<Item, GPS>();

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
            for (int i = 0; i < operationCount; i++) {
                var operation = random.NextDouble();

                if (operation < 0.5) {
                    await Task.Run(() => InsertToTree(treeType));
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
                }
            }
        }

        public async Task Insert(int treeType, int nodeCount) {
            for (int i = 0; i < nodeCount; i++) {
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
