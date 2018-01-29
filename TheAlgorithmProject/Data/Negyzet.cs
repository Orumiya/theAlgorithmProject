using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TheAlgorithmProject.Data
{
    class Negyzet : GameElement
    {
        private static Random rnd = new Random();

        private int number;

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        private int negyzetszelesseg;

        public int Negyzetszelesseg
        {
            get { return negyzetszelesseg; }
            set { negyzetszelesseg = value; }
        }

        public bool VizsgalatAlatt { get; set; }
        public Negyzet()
        {
            Number = rnd.Next(0, 10);
            Negyzetszelesseg = 30;
            VizsgalatAlatt = false;
            Shape = new RectangleGeometry(new Rect(Location.X, Location.Y, Negyzetszelesseg,Negyzetszelesseg));
        }
    }
}
