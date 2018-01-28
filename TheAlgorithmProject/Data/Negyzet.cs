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
        public Negyzet()
        {
            Shape = new RectangleGeometry(new Rect(Location.X, Location.Y, 30,30));
        }
    }
}
