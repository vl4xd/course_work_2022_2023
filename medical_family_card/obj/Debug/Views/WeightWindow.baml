   M S B A M L   `   `   `  ����N  Jmedical_family_card, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:medical_family_card.Viewsmedical_family_card.Views  5   	   ,    &medical_family_card.Views.WeightWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:medical_family_card.Views     5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$  WightWindow��5   	   $	��300��5   	   $	��450��5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5   	   .��5      � $��5q�6   .+�5   
   ��  �� CornerRadius$ 25��6   
������� J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         ShadowDepth$ 0Q�6#      RenderingBias$ Quality=�5      	   Color$	 	#FF009688��5      5      �� 5    
   
�������$ 25��5!      $��1q�5"      ._�5#      � 
 � RowDefinitions
 5$      �� 5%      $��50 �6$   �� 5&      $��* �6$   �� 5'      $��50 �6$   .+�5+      o� -   5         o�Name$ menuChangeItems��5+      $��0��5,      $��Right=�5-      $��0,0,25,0q�5.      .@�50      m� -   5         m�Name$ menuItemSave��50      $��Сохранить��51      $y�Hidden=�52      
��� � �54      m� 55      -   5        $ menuItemCancel��55      $��Отмена��56      $y�Hidden=�57      
���  ��59      m� 5:      -   5        $ menuItemClose��5:      $y�Visible=�5;      $��Закрыть��5<      5=      5>      �� 5@      $��1��6   ��. $
��Auto=�5B      $
��Auto=�5C      .��5D      �  � ColumnDefinitions 5E      �� 5F      $��* �6/   �� 5G      $��* �6/   .+�5K      �� $��0��6%   $��Center=�5L   %   .+�5M      �� $��0��6(   $��Center=�5N   $   $��Right=�5O   $   $��Вес:��5P   $   $��0,5,20,5q�5Q   $   $��18(�5R   $   $
��Bold"�5S   $   
�������5T   $   �� 5U      $��0��6(   $��Center=�5V   $   $��Right=�5W   $   $"��Дата измерения:��5X   $   $��0,5,20,5q�5Y   $   $��18(�5Z   $   $
��Bold"�5[   $   
�������5\   $   5]      �� 5`      $��2��6%   $��Center=�5a   %   .+�5b      �� -   5         ��Name$ textBoxWeightValue��5b   &   $��0,1,0,0q�5c   &   $
��Left=�5d   &   $�� ��5e   &   $��18(�5f   &   $
��Bold"�5g   &   $	��120��5h   &   {�.   �� 	MaxLength$ 5��5j   &   .  (MaterialDesignThemes.Wpf.TextFieldAssist!   CharacterCounterVisibility$ Hidden=�5k   &   5l   &   (  "System.Windows.Controls.DatePicker  5m      -   5          Name$ datePickerWeightDate��5m   )   $��0,1,0,0q�5n   )   $
��Left=�5o   )   $��18(�5p   )   $
��Bold"�5q   )   $	��120��5r   )   {�.     SelectedDate$ 
01.01.2000X�5t   )   5u   )   5v      5w      5x      5y      5z      5{      5|      