   M S B A M L   `   `   `  ����N  Jmedical_family_card, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:medical_family_card.Viewsmedical_family_card.Views  5   	   .    (medical_family_card.Views.FamiliarWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:medical_family_card.Views     5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   
   -   5          � Title$  WightWindow��5   	   $	��600��5   	   $	��800��5   	    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5   	   .��5      � $��5q�6   .+�5   
   ��  �� CornerRadius$ 25��6   
������� J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         ShadowDepth$ 0Q�6#      RenderingBias$ Quality=�5      	   Color$	 	#FF009688��5      5      �� 5   
   
�������$ 25��5       $��1q�5!      ._�5"      � 
 � RowDefinitions
 5#      �� 5$      $��50 �6$   �� 5%      $��* �6$   �� 5&      $��50 �6$   .+�5*      o� -   5         o�Name$ menuChangeItems��5*      $��0��5+      $��Right=�5,      $��0,0,25,0q�5-      $��Center=�5.      .@�5/      m� -   5         m�Name$ menuItemClose��5/      $y�Visible=�50      $��Закрыть��51      52      53      �� 55      -   5         ��Name$ comboBoxMenu��55      $��0��56      $
��Left=�57      $��25,0,0,0q�58      $��18(�59      $��Center=�5:      .@�5<      �� $��
Карта��6#    �� 
IsSelected . $��18(�5>   #   �� 5?      $��Аллергии��6#   $��18(�5@   #   5A      �� 5C      $��1��6   ��. $
��Auto=�5E      $
��Auto=�5F      .��5G      �� -   5         ��Name$ contentControlFamiliar��5G   %   $��1��5H   %   $��5q�5I   %   5J      5K      5L      5M      5N      