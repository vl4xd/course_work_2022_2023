   M S B A M L   `   `   `  ����N  Jmedical_family_card, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:medical_family_card.Viewsmedical_family_card.Views  5   	   1    +medical_family_card.Views.FamiliarAddWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:medical_family_card.Views     5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$  WightWindow��5   	   $	��300��5   	   $	��550��5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5   	   .��5      � $��5q�6   .+�5   
   ��  �� CornerRadius$ 25��6   
������� J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         ShadowDepth$ 0Q�6#      RenderingBias$ Quality=�5      	   Color$	 	#FF009688��5      5      �� 5   
   
�������$ 25��5       $��1q�5!      ._�5"      � 
 � RowDefinitions
 5#      �� 5$      $��50 �6$   �� 5%      $��* �6$   �� 5&      $��50 �6$   .+�5*      o� -   5         o�Name$ menuChangeItems��5*      $��0��5+      $��Right=�5,      $��0,0,25,0q�5-      .@�5/      m� -   5         m�Name$ menuItemAdd��5/      $��Добавить��50      $y�Visible=�51      
��� � �53      m� 54      -   5        $ menuItemCancel��54      $��Отмена��55      $y�Visible=�56      
���  ��58      59      �� 5;      $��1��6   ��. $
��Auto=�5=      $
��Auto=�5>      .��5?      �  � ColumnDefinitions 5@      �� 5A      $��* �6/   �� 5B      $��* �6/   .+�5F      �� $��0��6%   $��Center=�5G   %   .+�5H      �� $��0��6(   $��Center=�5I   $   $��Right=�5J   $   $*��$Логин пользователя:��5K   $   $��0,5,20,5q�5L   $   $��18(�5M   $   $
��Bold"�5N   $   
�������5O   $   5P      �� 5S      $��2��6%   $��Center=�5T   %   .+�5U      �� -   5         ��Name$ textBoxPatientLogin��5U   &   $��0,1,0,0q�5V   &   $
��Left=�5W   &   $�� ��5X   &   $��18(�5Y   &   $
��Bold"�5Z   &   $	��120��5[   &   {�.  �� 	MaxLength$ 20��5]   &   .  (MaterialDesignThemes.Wpf.TextFieldAssist!   CharacterCounterVisibility$ Hidden=�5^   &   5_   &   5`      5a      5b      5c      5d      5e      5f      