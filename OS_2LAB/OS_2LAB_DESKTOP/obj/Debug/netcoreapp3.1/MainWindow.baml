   M S B A M L   `   `   `  ����J  FOS_2LAB_DESKTOP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:OS_2LAB_DESKTOPOS_2LAB_DESKTOP  5   	        OS_2LAB_DESKTOP.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:OS_2LAB_DESKTOP     5   	     � Title$  Поиск файлов��5   	   $	��507��6   $	��800��6+   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5        $��textBox_PathSearch��5   (   $��10,10,118,10q�6   6D   �� 5   
   -   5        $��Light"�5      $��687,10,10,10q�6$   .��6U   Выбор файла6b   &   System.Windows.Controls.DataGrid  5   
   -   5        $��
TableFiles��5   f   $
��Auto��6      
IsReadOnly$
 True��6!      CanUserAddRows$ False��63      AutoGenerateColumns$ False��6J   $��10,10,10,118q�6x   $��1��6�   $��2��6�   ��6�   �� *totalFileInfos+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Путь��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6Q   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� 	 �� Path$
	 Path�6Q   5  /System.Windows.Controls.DataGridHyperlinkColumn  5      $ Размер(байты)��6*   $
 Auto 6a    6A   �� $	 Length�6a     5      $ Дата создания��6%   $
 Auto 6b    6<   �� $	 DateCreation�6b     5      $% Дата модификации��6%   $
 Auto 6i    6?   �� $	 DateModification�6i   5      �� 5   
   $��2��6   $��	0,305,0,0q�6"   .+�5      � $��85��6   $	��784��6$   .+�5      � 
 � Orientation$
 Vertical=�6   $��84��63   $	��127��6?   .+�5       �� $��Center=�6    $��Light"�6=   $��Center=�6P   �� 6l   $!�Имя.Расширение��6p   6�   �� 5!      -   5        $��TextBox_FileName��5!      $��Center=�66   $	��112��6S   $��24��6_   .��6k   *.*6p   5"      � 5#      $
 Vertical=�6   $��84��63   $	��127��6?   .+�5$      �� $��Center=�6    $��Light"�6=   $��Center=�6P   �� 6l   $�Дата создания��6p   6�   (  "System.Windows.Controls.DatePicker  5%      -   5        $��DatePicker_FileCreation��5%   !   $��Center=�6@   $	��112��6]   $��24��6i   5&      � 5(      $
 Vertical=�6   $��84��63   $	��127��6?   .+�5)      �� $��Center=�6    $��Light"�6=   $��Center=�6P   �� 6l   $%�Дата модификации��6p   6�     5*      -   5        $!��DatePicker_FileModification��5*   !   $��Center=�6D   $	��112��6a   $��24��6m   5+      � 5-      $
 Vertical=�6   $��80��63   $	��138��6?   .+�5.      �� $��Center=�6    $��Light"�6=   $��Center=�6P   %#Минимальный размер6   ]� 5/      .+�50      �� -   5        $��TextBox_MinValue��50   "   $��Center=�6:   $��92��6W   $��19��6b   .��6n   06q   �� 51      -   5        $��ComboBox_MinSize��51   #   $��20��6<   $��45��6H   .@�52      ��  �� 
IsSelected$
 True��6+   .��6=   б6@   �� 53      .��6+   кб6/   �� 54      .��6+   мб6/   �� 55      .��6+   гб6/   56      57      58      � 5;      $
 Vertical=�6   $��80��63   $	��138��6?   .+�5<      �� $��Light"�6    $��Center=�63   '%Максимальный размер6c   ]� 5=      .+�5>      �� -	   5        $��TextBox_MaxValue��5>   "   $��19��6:   $��92��6F   6S   �� 5?      -
   5        $��ComboBox_MaxSize��5?   #   $��20��6;   $��45��6G   .@�5@      �� $
 True��6+   .��6=   б6@   �� 5A      .��6+   кб6/   �� 5B      .��6+   мб6/   �� 5C      .��6+   гб6/   5D      5E      5F      � 5H      $
 Vertical=�6   $��78��63   $	��122��6?   .+�5I      �� $��Center=�6    $��Light"�6=   $��Center=�6P   Набор символов6{   �� 5J      -   5        $��TextBox_OccurenceSymbols��5J       �� AcceptsReturn$
 True��6>   $��Center=�6S   $��41��6p   $	��128��6|   6�   5K      5L      ]� 5M      $��Center=�6   .+�5N      �� -   5        $��Light"�5N      $��28��6,   $	��111��68   $��Начать поиск��6D   $��0 0 15 0q�6v   �� 5O      -   5        $��Light"�5O      $��28��6,   $	��161��68   $2��,Посмотреть логи потоков��6D   6f   5P      5R      5S      5T      