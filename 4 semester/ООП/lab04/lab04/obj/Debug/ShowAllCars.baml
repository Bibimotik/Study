   M S B A M L   `   `   `  ����@  <lab04, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:lab04lab04  5   	       lab04.ShowAllCars   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   %localclr-namespace:lab04     5   	      AppIcon  � Icon#  [  5   	    � Title$ ShowAllCars��5	   	   $	��500��6   $	��900��6*   $	��480��66    � 	Resources 5
       �� BasedOn �� 
TargetType �� Property �� Value��%M  '��       7���� 5   
   8   " ��5      .�5      ��  � ��
 �����6+   �� 5       � ��$ Center=�62   �� 5       � ��$
 Left=�64   �� 5       � ��$ Medium"�6+   �� 5       � ��$ 14(�6)   �� 5       � ��$ Mansory)�6+   �� 5       � ��$ 0,0,0,0q�6'   5      5      .��5      �� ���#272537._�5   
   � -   5         �Name$ YourLayoutGrid��5       � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� 	 �� Orientation$	 
Horizontal=�6   $��0��62   .+�5      �� -   5        $��
SearchText��5      $	��200��6,   $��30��68   $��10q�6D   �� 5       -   5        $��SearchButton��5        
 Search#���  6-   $��80��6P   $��30��6[   $��10q�6g   $��Mansory)�6s   6�   �� 5!      -   5        
 ��Name$
 CategoryComboBox��5!      $��Center=�65   $	��100��6P   $��30��6\   $��Mansory)�6h   .@�5"      ��   Price_Up#���  6#   $��Mansory)�6H   �� 5#        
Price_Down#���  6#   $��Mansory)�6J   �� 5$        	In_Stock1#���  6#   $��Mansory)�6I   �� 5%        Not_In_Stock1#���  6#   $��Mansory)�6M   5&      5'      �� 5)      $	 
Horizontal=�6   $��1��62   .+�5*      ��   Start_Price#���  6   �� 5+      -   5        $��
StartPrice��5+      $	��100��6,   $��30��68   $��10q�6D   �� 5,        	End_Price#���  6   �� 5-      -   5        $��EndPrice��5-      $	��100��6*   $��30��66   $��10q�6B   5.      �� 50      $��2��6   $
��Auto=�6(   .��51      �� -   5         ��Name$ StackPanelLayout��51      $	 
Horizontal=�67   53      54      55      56      57      