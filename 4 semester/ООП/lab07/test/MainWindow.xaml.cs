using System.Windows;
using System.Windows.Input;

namespace Lab07
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            grid4.Visibility = grid4.Visibility == Visibility.Collapsed ?
                Visibility.Visible : Visibility.Collapsed;
        }

        public static readonly RoutedEvent CustomBubbleEvent = EventManager.RegisterRoutedEvent(
            "CustomBubble", 
            RoutingStrategy.Bubble, 
            typeof(RoutedEventHandler),
            typeof(MainWindow) 
        );

        public static readonly RoutedEvent CustomTunnelEvent = EventManager.RegisterRoutedEvent(
            "CustomTunnel", 
            RoutingStrategy.Tunnel, 
            typeof(RoutedEventHandler), 
            typeof(MainWindow) 
        );

        public static readonly RoutedEvent CustomDirectEvent = EventManager.RegisterRoutedEvent(
            "CustomDirect",
            RoutingStrategy.Direct, 
            typeof(RoutedEventHandler),
            typeof(MainWindow) 
        );

        public event RoutedEventHandler CustomBubble
        {
            add { AddHandler(CustomBubbleEvent, value); }
            remove { RemoveHandler(CustomBubbleEvent, value); }
        }

        public event RoutedEventHandler CustomTunnel
        {
            add { AddHandler(CustomTunnelEvent, value); }
            remove { RemoveHandler(CustomTunnelEvent, value); }
        }

        public event RoutedEventHandler CustomDirect
        {
            add { AddHandler(CustomDirectEvent, value); }
            remove { RemoveHandler(CustomDirectEvent, value); }
        }

        private void Bubble_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(CustomBubbleEvent);
            RaiseEvent(args);
            MessageBox.Show($"Вы нажали на кнопку с {nameof(CustomBubbleEvent)}");
            biba.Text += "sender: " + sender.ToString() + "\n";
            biba.Text += "source: " + e.Source.ToString() + "\n";
        }

        private void Tunnel_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(CustomTunnelEvent);
            RaiseEvent(args);
            MessageBox.Show($"Вы нажали на кнопку с {nameof(CustomTunnelEvent)}");
            boba.Text += "sender: " + sender.ToString() + "\n";
            boba.Text += "source: " + e.Source.ToString() + "\n";
        }

        private void Direct_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(CustomDirectEvent);
            RaiseEvent(args);
            MessageBox.Show($"Вы нажали на кнопку с {nameof(CustomDirectEvent)}");
            text.Text += "sender: " + sender.ToString() + "\n";
            text.Text += "source: " + e.Source.ToString() + "\n";
        }
    }
}