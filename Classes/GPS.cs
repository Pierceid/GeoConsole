﻿namespace GeoConsole {
    public class GPS : IKey<GPS> {
        private string sirka;
        private double x;
        private string dlzka;
        private double y;

        public GPS(string sirka, double x, string dlzka, double y) {
            this.Sirka = sirka;
            this.x = x;
            this.dlzka = dlzka;
            this.y = y;
        }

        public int Compare(GPS other, int level) {
            if (level % 4 == 0) {
                return this.CompareStrings(this.Sirka, other.Sirka);
            } else if (level % 4 == 1) {
                return this.ComparePositions(this.x, other.X);
            } else if (level % 4 == 2) {
                return this.CompareStrings(this.dlzka, other.dlzka);
            } else {
                return this.ComparePositions(this.y, other.Y);
            }
        }

        public bool Equals(GPS other) {
            return this.x == other.X && this.y == other.Y && this.sirka == other.sirka && this.dlzka == other.dlzka;
        }

        private int ComparePositions(double value1, double value2) {
            if (value1 < value2) return -1;
            if (value1 > value2) return 1;
            return 0;
        }

        private int CompareStrings(string value1, string value2) {
            if (string.Compare(value1, value2) < 0) return -1;
            if (string.Compare(value1, value2) > 0) return 1;
            return 0;
        }

        public string GetKeys() {
            return $"GPS,{this.x.ToString().Replace(',', '.')},{this.sirka},{this.y.ToString().Replace(',', '.')},{this.dlzka}";
        }

        public double X { get => x; set => x = value; }

        public double Y { get => y; set => y = value; }

        public string Sirka { get => sirka; set => sirka = value; }

        public string Dlzka { get => dlzka; set => dlzka = value; }
    }
}
