using System;
using System.Windows;
using System.Windows.Controls;

namespace Lab07
{
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
        }

        public object Secret
        {
            get { return GetValue(SecretProperty); }
            set { SetValue(SecretProperty, value); }
        }
        
        public static readonly DependencyProperty SecretProperty =
            DependencyProperty.Register("Secret", typeof(object), typeof(UserControl2),
                new FrameworkPropertyMetadata("I am visible",
                    FrameworkPropertyMetadataOptions.None,
                    new PropertyChangedCallback(OnSecretChanged),
                    new CoerceValueCallback(CoerceSecretValue),
                    true,
                    System.Windows.Data.UpdateSourceTrigger.PropertyChanged),
                new ValidateValueCallback(IsValidSecret));

        private static bool IsValidSecret(object value)
        {
            return value is string;
        }

        private static object CoerceSecretValue(DependencyObject d, object baseValue)
        {
            return (baseValue as string)?.ToUpper();
        }

        private static void OnSecretChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var newSecret = e.NewValue;
        }
        
        private void Spoiler_Click(object sender, RoutedEventArgs e)
        {
            if (spoilerGrid.Visibility == Visibility.Visible)
            {
                contentGrid.Visibility = Visibility.Visible;
                spoilerGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                contentGrid.Visibility = Visibility.Collapsed;
                spoilerGrid.Visibility = Visibility.Visible;
            }
        }
    }
}
