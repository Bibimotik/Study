using System;
using System.Windows;
using System.Windows.Controls;

namespace Lab07
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
        
        public int CurrentNumber
        {
            get { return (int)GetValue(CurrentNumberProperty); }
            set { SetValue(CurrentNumberProperty, value); }
        }
        
        public static readonly DependencyProperty CurrentNumberProperty =
            DependencyProperty.Register("CurrentNumber", typeof(int), typeof(UserControl1),
                new FrameworkPropertyMetadata(10,
                    FrameworkPropertyMetadataOptions.None,
                    new PropertyChangedCallback(CurrentNumberChanged),
                    new CoerceValueCallback(CoerceCurrentNumber)), 
                new ValidateValueCallback(ValidateCurrentNumber));

        public static bool ValidateCurrentNumber(object value)
        {
            int intValue = (int)value;
            return intValue >= 0 && intValue <= 500;
        }

        private static object CoerceCurrentNumber(DependencyObject d, object baseValue)
        {
            int coercedValue = (int)baseValue;
           
            if (coercedValue < 0)
                return 0;
            else if (coercedValue > 500)
                return 500;
            else
                return coercedValue;
        }

        private static void CurrentNumberChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs args)
        {
            UserControl1 s = (UserControl1)depObj;
            Label theLabel = s.numberDisplay;
            theLabel.Content = args.NewValue.ToString();
        }
    }
}
