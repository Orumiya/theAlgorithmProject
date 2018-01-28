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

        public int Index { get; set; }
        public int Ide { get; set; }

        public AlgorithmAnimation(double screenwidth, double screenheight)
        {
            this.Screenheight = screenheight;
            this.Screenwidth = screenwidth;
            Array = new List<Negyzet>();
            for (int i = 0; i < 10; i++)
            {
                Array.Add(new Negyzet() { Location = new Point(100 + i*30, 100) });
            }
            

        }

        public void Bubblesort()
        {
            for (int i = Array.Count - 1; i > 1; i--)
            {
                for (int j = 0; j < i - 1; j++)
                {
                    if (Array[j].Number > Array[j + 1].Number)
                    {
                        int csere = Array[j].Number;
                        Index = j;
                        Ide = j + 1;
                        Array[j].Number = Array[j + 1].Number;
                        Array[j + 1].Number = csere;

                    }
                }
            }
        }
    }
}
