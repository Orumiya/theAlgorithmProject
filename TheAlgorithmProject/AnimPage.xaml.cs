﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheAlgorithmProject
{
    /// <summary>
    /// Interaction logic for AnimPage.xaml
    /// </summary>
    public partial class AnimPage : Page
    {
        private Frame mainFrame;
        public AnimPage(Frame mainFrame)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
        }
    }
}
