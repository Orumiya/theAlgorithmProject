using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
                ThreadStart bubblethreadStart = new ThreadStart(Bubblesort);
                Thread bubblethread = new Thread(bubblethreadStart);

                bubblethread.Start();
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
                }
                else
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
                    if (!Animation.Array[i].VizsgalatAlatt)
                    {
                        drawingContext.DrawGeometry(Brushes.Azure, new Pen(Brushes.Black, 2), Animation.Array[i].GetTransformedGeometry());
                        FormattedText formattedText = new FormattedText(Animation.Array[i].Number.ToString(), new CultureInfo(1), FlowDirection.LeftToRight, new Typeface("Arial"), 20, Brushes.Black);
                        drawingContext.DrawText(formattedText, new Point(Animation.Array[i].Location.X + Animation.Array[i].Negyzetszelesseg / 3, Animation.Array[i].Location.Y + Animation.Array[i].Negyzetszelesseg / 4));
                    }
                    else
                    {
                        drawingContext.DrawGeometry(Brushes.Aquamarine, new Pen(Brushes.Black, 2), Animation.Array[i].GetTransformedGeometry());
                        FormattedText formattedText = new FormattedText(Animation.Array[i].Number.ToString(), new CultureInfo(1), FlowDirection.LeftToRight, new Typeface("Arial"), 20, Brushes.Black);
                        drawingContext.DrawText(formattedText, new Point(Animation.Array[i].Location.X + Animation.Array[i].Negyzetszelesseg / 3, Animation.Array[i].Location.Y + Animation.Array[i].Negyzetszelesseg / 4));
                    }
                    
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
                timer.Stop();
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
                    count = 0;
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
                    count = 0;
                }
            }
            
        }

        public void Bubblesort()
        {
            for (int i = Animation.Array.Count - 1; i > 1; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (Animation.Array[j].Number > Animation.Array[j + 1].Number)
                    {
                        Negyzet csere = Animation.Array[j];
                        Animation.Index = j;
                        Animation.Array[j].VizsgalatAlatt = true;
                        Animation.Array[j+1].VizsgalatAlatt = true;
                        Animation.Ide = j + 1;
                        Display();
                        Thread.Sleep(3000);
                        Animation.Array[j] = Animation.Array[j + 1];
                        Animation.Array[j + 1] = csere;
                        Animation.Array[j].VizsgalatAlatt = false;
                        Animation.Array[j + 1].VizsgalatAlatt = false;

                    }
                    else
                    {
                        Negyzet csere = Animation.Array[j];
                        Animation.Array[j].VizsgalatAlatt = true;
                        Animation.Array[j + 1].VizsgalatAlatt = true;
                        Animation.Index = j;
                        Animation.Ide = j;
                        Display();
                        Thread.Sleep(3000);
                        Animation.Array[j].VizsgalatAlatt = false;
                        Animation.Array[j + 1].VizsgalatAlatt = false;
                    }

                }
            }

        }

        public void Display()
        {
            timer.Start();
            
        }

    }

}
