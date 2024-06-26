using System.Windows.Input;

namespace Lab07
{
    class Commands
    {
        public static RoutedUICommand Visible { get; set; }

        static Commands()
        {
            Visible = new RoutedUICommand("Visible", "name", typeof(MainWindow));
        }
    }
}
