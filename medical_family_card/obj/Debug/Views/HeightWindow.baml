   M S B A M L   `   `   `  ����N  Jmedical_family_card, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:medical_family_card.Viewsmedical_family_card.Views  5   	   ,    &medical_family_card.Views.HeightWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:medical_family_card.Views     5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$  HeightWindow��5   	   $	��300��5   	   $	��450��5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5   	   .��5      � $��5q�6   .+�5   
   ��  �� CornerRadius$ 25��6   
������� J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         ShadowDepth$ 0Q�6#      RenderingBias$ Quality=�5      	   Color$	 	#FF009688��5      5      �� 5   
   
�������$ 25��5       $��1q�5!      ._�5"      � 
 � RowDefinitions
 5#      �� 5$      $��50 �6$   �� 5%      $��* �6$   �� 5&      $��50 �6$   .+�5*      o� -   5         o�Name$ menuChangeItems��5*      $��0��5+      $��Right=�5,      $��0,0,25,0q�5-      .@�5/      m� -   5         m�Name$ menuItemSave��5/      $��Сохранить��50      $y�Hidden=�51      
��� � �53      m� 54      -   5        $ menuItemCancel��54      $��Отмена��55      $y�Hidden=�56      
���  ��58      m� 59      -   5        $ menuItemClose��59      $y�Visible=�5:      $��Закрыть��5;      5<      5=      �� 5?      $��1��6   ��. $
��Auto=�5A      $
��Auto=�5B      .��5C      �  � ColumnDefinitions 5D      �� 5E      $��* �6/   �� 5F      $��* �6/   .+�5J      �� $��0��6%   $��Center=�5K   %   .+�5L      �� $��0��6(   $��Center=�5M   $   $��Right=�5N   $   $��	Рост:��5O   $   $��0,5,20,5q�5P   $   $��18(�5Q   $   $
��Bold"�5R   $   
�������5S   $   �� 5T      $��0��6(   $��Center=�5U   $   $��Right=�5V   $   $"��Дата измерения:��5W   $   $��0,5,20,5q�5X   $   $��18(�5Y   $   $
��Bold"�5Z   $   
�������5[   $   5\      �� 5_      $��2��6%   $��Center=�5`   %   .+�5a      �� -   5         ��Name$ textBoxHeightValue��5a   &   $��0,1,0,0q�5b   &   $
��Left=�5c   &   $�� ��5d   &   $��18(�5e   &   $
��Bold"�5f   &   $	��120��5g   &   {�.   �� 	MaxLength$ 5��5i   &   .  (MaterialDesignThemes.Wpf.TextFieldAssist!   CharacterCounterVisibility$ Hidden=�5j   &   5k   &   (  "System.Windows.Controls.DatePicker  5l      -   5          Name$ datePickerHeightDate��5l   )   $��0,1,0,0q�5m   )   $
��Left=�5n   )   $��18(�5o   )   $
��Bold"�5p   )   $	��120��5q   )   {�.     SelectedDate$ 
01.01.2000X�5s   )   5t   )   5u      5v      5w      5x      5y      5z      5{      