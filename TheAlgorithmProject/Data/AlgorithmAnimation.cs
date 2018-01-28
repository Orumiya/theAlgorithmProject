using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TheAlgorithmProject.Data
{
    class AlgorithmAnimation
    {
        public List<Negyzet> Array { get; set; }
        public double Screenwidth { get; set; }
        public double Screenheight { get; set; }


        public AlgorithmAnimation(double screenwidth, double screenheight)
        {
            this.Screenheight = screenheight;
            this.Screenwidth = screenwidth;
            Array = new List<Negyzet>();
            Array.Add(new Negyzet() { Location = new Point(100, 100)});
            Array.Add(new Negyzet() { Location = new Point(130, 100) });
            Array.Add(new Negyzet() { Location = new Point(160, 100) });
        }
    }
}
