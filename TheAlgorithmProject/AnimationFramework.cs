using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using TheAlgorithmProject.Data;

namespace TheAlgorithmProject
{
    enum Allapotok
    {
        Helyen,
        FentVan,
        LentVan,
        BalraVan,
        JobbraVan,
        Helyrerak
    }

    class AnimationFramework : FrameworkElement
    {
        AlgorithmAnimation Animation { get; set; }
        DispatcherTimer timer;
        Allapotok holVan;
        int count;
        int index;
        int ide;
       

        public AnimationFramework()
        {
            this.Loaded += AnimationFramework_Loaded;
        }

        private void AnimationFramework_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this) == false)
            {
                index = 0;
                ide = 0;
                Animation = new AlgorithmAnimation(this.ActualWidth, this.ActualHeight);
                
                holVan = Allapotok.Helyen;
                timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(0, 0, 0, 0,1);
                timer.Tick += Timer_Tick;
                count = 0;
                Bubblesort();

                this.InvalidateVisual();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            index = Animation.Index;
            ide = Animation.Ide;
            if (holVan == Allapotok.Helyen)
            {
                Felfelemozgat(index);
            }
            else if (holVan == Allapotok.FentVan)
            {
                if (index - ide > 0)
                {
                    Balramozgat(index, ide);
                }else
                {
                    Jobbramozgat(index, ide);
                }
                

            }
            else if (holVan == Allapotok.BalraVan || holVan == Allapotok.JobbraVan)
            {
                HelyreTol(ide, index);

            }
            else if (holVan == Allapotok.Helyrerak)
            {
                Lefelemozgat(index);
            }
            
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (Animation != null)
            {
                for (int i = 0; i < Animation.Array.Count; i++)
                {
                    drawingContext.DrawGeometry(Brushes.Azure, new Pen(Brushes.Black, 2), Animation.Array[i].GetTransformedGeometry());
                    FormattedText formattedText = new FormattedText(Animation.Array[i].Number.ToString(), new CultureInfo(1), FlowDirection.LeftToRight, new Typeface("Arial"), 20, Brushes.Black);
                    drawingContext.DrawText(formattedText, new Point(Animation.Array[i].Location.X + Animation.Array[i].Negyzetszelesseg/3, Animation.Array[i].Location.Y + Animation.Array[i].Negyzetszelesseg / 4));
                }
            }
        }

        private void Felfelemozgat(int index)
        {
            Negyzet negyzet = Animation.Array[index];
            if (negyzet.Location.Y > 50)
            {
                negyzet.Location = new Point(negyzet.Location.X, negyzet.Location.Y - 1);
            }
            else
            {
                holVan = Allapotok.FentVan;
            }
        }

        private void Balramozgat(int index, int ide)
        {
            Negyzet negyzet = Animation.Array[index];
            if (negyzet.Location.X > Animation.Array[ide].Location.X)
            {
                negyzet.Location = new Point(negyzet.Location.X -1, negyzet.Location.Y);
            }
            else
            {
                holVan = Allapotok.BalraVan;
            }
        }

        private void Jobbramozgat(int index, int ide)
        {
            Negyzet negyzet = Animation.Array[index];
            if (negyzet.Location.X < Animation.Array[ide].Location.X)
            {
                negyzet.Location = new Point(negyzet.Location.X + 1, negyzet.Location.Y);
            }
            else
            {
                holVan = Allapotok.JobbraVan;
            }
        }

        private void Lefelemozgat(int index)
        {
            Negyzet negyzet = Animation.Array[index];
            if (negyzet.Location.Y < 100)
            {
                negyzet.Location = new Point(negyzet.Location.X, negyzet.Location.Y+1);
            }
            else
            {
                holVan = Allapotok.Helyen;
                //timer.Stop();
            }
        }

        private void HelyreTol(int innen, int ide)
        {
            if (ide > innen)
            {
                int elemszam = ide - innen;
                if (count < 30 * elemszam)
                {
                    for (int i = innen; i < ide; i++)
                    {
                        Animation.Array[i].Location = new Point(Animation.Array[i].Location.X + 1, Animation.Array[i].Location.Y);
                        count++;
                    }
                }
                else
                {
                    holVan = Allapotok.Helyrerak;
                }
            }else
            {
                int elemszam = innen - ide;
                if (count < 30 * elemszam)
                {
                    for (int i = innen; i > ide; i--)
                    {
                        Animation.Array[i].Location = new Point(Animation.Array[i].Location.X - 1, Animation.Array[i].Location.Y);
                        count++;
                    }
                }
                else
                {
                    holVan = Allapotok.Helyrerak;
                }
            }
            
        }

        public void Bubblesort()
        {
            for (int i = Animation.Array.Count - 1; i > 1; i--)
            {
                for (int j = 0; j < i - 1; j++)
                {
                    if (Animation.Array[j].Number > Animation.Array[j + 1].Number)
                    {
                        timer.Start();
                        int csere = Animation.Array[j].Number;
                        Animation.Index = j;
                        Animation.Ide = j + 1;
                        Animation.Array[j].Number = Animation.Array[j + 1].Number;
                        Animation.Array[j + 1].Number = csere;
                        
                    }
                    
                }
            }
        }

    }

}
