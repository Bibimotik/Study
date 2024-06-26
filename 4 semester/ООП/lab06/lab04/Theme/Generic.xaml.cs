using System;
using System.Windows;
using System.Windows.Controls;

namespace lab04.Theme
{
    public partial class Generic : UserControl
    {
        public Generic()
        {
            InitializeComponent();
        }
        
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы нажали на кнопку!");
        }
    }
}