   M S B A M L   `   `   `  ����@  <lab04, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:lab04.Themelab04.Theme  5          lab04.Theme.Generic   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:lab04.Theme     5        � 	Resources  5	       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property P� Value	 N� EnterActions
 �� TargetProperty	 �� To g� Duration N� ExitActions :� RoutedEvent R� KeyTime  System.Windows.Visibility   	Collapsed R� Value g� 	BeginTime   Visible��%�  '��       �� 5
   
   " ��6   .�5      ��  � ��
 �����6+   �� 5       � ��
 �����6+   �� 5       � ��
 ���� 6,   �� 5       � ��$ 0q�60   �� 5       � ��$ 10q�6(   �� 5       � ��$ 5q�6'   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ 16(�6)   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��6K   #��z��5   !   $ 20��6U   ._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       �@J�IsMouseOver . 	 5   "   �� 5   &   .��5   *   �� .��5   .   �� $1
 +(Button.Background).(SolidColorBrush.Color)�6=   $ 	LightPinkX�5    =   $ 0:0:0.2D�6L   5!   +   5"   '    5$   "   �� 5%   &   .��5&   *   �� .��5'   .   �� $1
 +(Button.Background).(SolidColorBrush.Color)�6=   $
 PinkX�5(   =   $ 0:0:0.2D�6G   5)   +   5*   '   5,      P� 5-       � �� . 	 5.   "   �� 5/   &   .��50   *   �� .��51   .   �� $1
 +(Button.Background).(SolidColorBrush.Color)�6=   $ #AA336AX�52   =   $ 0:0:0.2D�6J   53   +   54   '    56   "   �� 57   &   .��58   *   �� .��59   .   �� $1
 +(Button.Background).(SolidColorBrush.Color)�6=   $
 PinkX�5:   =   $ 0:0:0.2D�6G   5;   +   5<   '   5>      :� 5?      $ Button.Click��6+   [�5@   "   �� 5A   &   .��5B   *   �� .��5C   .   T� $
 
Visibility�6L   ..�5D   2   i� $ 0:0:0��6I   # Z 6Y   5E   /   T� 5F   .   $
 
Visibility�6L   $ 0:0:0.2X�6s   ..�5G   2   i� $ 0:0:0��6I   # Z 6Y   5H   /   5I   +   5J   '   5L      5N      5P      5Q      5R      .��5S      �� -   5           But#���   5S      $��80��6-   $��30��68   $��Mansory)�6D   $��10q�6Y   6e   5T      