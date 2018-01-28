using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TheAlgorithmProject.Data
{
    class GameElement
    {
        protected Geometry Shape { get; set; }

        public Point Location { get; set; }

        public Geometry GetTransformedGeometry()
        {
            Geometry copy = Shape.Clone();
            copy.Transform = new TranslateTransform(Location.X, Location.Y);
            return copy.GetFlattenedPathGeometry();
            
        }

    }
}
