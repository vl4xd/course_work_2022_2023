   M S B A M L   `   `   `  ����N  Jmedical_family_card, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:medical_family_cardmedical_family_card  5   	   %    medical_family_card.StartWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:medical_family_card     5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   .fa%http://schemas.fontawesome.com/icons/  5
   	   -   5          � Title$  StartWindow��5   	   $	��550��6   $	��550��6*    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
������  � AllowsTransparency . .��5      � $��5q�6   .+�5   
   �� -   5         ��Name$ borderShodow��5       �� CornerRadius$ 25��5      
������� J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         ShadowDepth$ 0Q�6#   	   RenderingBias$	 Quality=�5   #   
   Color$
 	#FF009688��5   #   5      �� 5   
   $ 25��6   
�������$��1q�5!      ._�5#      �  � RowDefinitions 5$      �� 5%      $��30 �6$   �� 5&      .+�5)      �  � ColumnDefinitions 5*      �� 5+      �� 5,      $��30 �6+   �� 5-      $��30 �6+   �� 5.      $��5 �6+   .+�51      �� -   5         ��Name$ buttonMinimize��51      
�������$��0q�53       � Cursor$
 Hand��54      $��1��55      ��57      �� 58       �� 
TargetType" ��6.   .R�59   "   �� $��20��6)   $��20��5:   )   $ 25��5;   )   
�������$��1q�5=   )   #��z��5>   )   5?      5A      �� 5B      -   5        $ buttonClose��5B      
���  ��$��0q�5D      $
 Hand��5E      $��2��5F      ��5H      �� 5I      " ��6.   .R�5J   "   �� $��20��6)   $��20��5K   )   $ 25��5L   )   
�������$��1q�5N   )   #��z��5O   )   5Q      5S      5T      �� 5V      $	��450��6   $��1��5W       �� Orientation$ Vertical=�5X      $��	0,35,0,35q�5Y      .+�5[      �� -   5         ��Name$ textInfoRegim��5[       $&�� Войдите в аккаунт��5\       $��26(�5]       $
��Bold"�5^       $
��Left=�5_       �� 5a      $��или��6    $��16(�5b       ���	#FF6E6E6E$
��Left=�5d       �� 5f      -   5        $ textChangeRegime��5f       $(��"Зарегистрируйтесь��5g       ���	#FF009688$
 Hand��5i       $��18(�5j       $
��Left=�5k       5l       �� 5n      -   5         ��Name$ textBoxLogin��5n      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Введите логин��5o      $y�Visible=�5p      $��
10,30,10,5q�5q       %  !MaterialDesignFloatingHintTextBox#��[  5r      .  (MaterialDesignThemes.Wpf.TextFieldAssist   HasClearButton .  �� 	MaxLength$ 20��5t      
�������$��16(�5u   X   G� 5w      -   5         G�Name$ textBoxPassword1��5w   "   $! Введите пароль��5x   "   $y�Visible=�5y   "   $��	10,5,10,5q�5z   "    ) %MaterialDesignFloatingHintPasswordBox#��[ 5{   "    .  G� 	MaxLength$ 20��5}   "   
�������$��16(�5~   h   G� 5�      -	   5        $ textBoxPassword2��5�   "   $% Повторите пароль��5�   "   $y�Hidden=�5�   "   $��	10,5,10,5q�5�   "   #��[ 5�   "    . $ 20��5�   "   
�������$��16(�5�   h   �� 5�      -
   5        $ buttonStart��5�      $��
Войти��5�      $��
50,55,50,0q�5�      5�      5�      5�      5�      5�      5�      