using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TheAlgorithmProject.Data;

namespace TheAlgorithmProject
{
    class AnimationFramework : FrameworkElement
    {
        AlgorithmAnimation Animation { get; set; }

        public AnimationFramework()
        {
            this.Loaded += AnimationFramework_Loaded;
        }

        private void AnimationFramework_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this) == false)
            {
                Animation = new AlgorithmAnimation(this.ActualWidth, this.ActualHeight);
                this.InvalidateVisual();
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (Animation != null)
            {
                for (int i = 0; i < Animation.Array.Count; i++)
                {
                    drawingContext.DrawGeometry(Brushes.Azure, new Pen(Brushes.Black, 2), Animation.Array[i].GetTransformedGeometry());
                }
            }
        }
    }
}
