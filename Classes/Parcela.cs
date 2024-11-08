﻿using System;

namespace GeoConsole {
    public class Parcela : Item {
        private int cisParcely;
        private string popis;
        private GPS pozicia;

        public Parcela(int cisParcely, string popis, GPS pozicia) {
            this.cisParcely = cisParcely;
            this.popis = popis;
            this.pozicia = pozicia;
        }

        public override void PrintInfo() {
            Console.WriteLine($"Parcela: {this.Id} - {this.cisParcely} - {this.popis} - [{this.pozicia.X}°; {this.pozicia.Y}°]");
        }

        public override string GetInfo() {
            return $"{this.Id},{this.cisParcely},{this.popis},{this.pozicia.X.ToString().Replace(',', '.')},{this.pozicia.Y.ToString().Replace(',', '.')}";
        }

        public int CisParcely { get => cisParcely; set => cisParcely = value; }

        public string Popis { get => popis; set => popis = value; }

        public GPS Pozicia { get => pozicia; set => pozicia = value; }
    }
}
