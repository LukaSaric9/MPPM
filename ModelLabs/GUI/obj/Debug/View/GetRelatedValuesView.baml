   M S B A M L   `   `   `  ����>  :GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GUI.ViewGUI.View  5      -clr-namespace:GUI.ViewModelGUI.ViewModel  5      'clr-namespace:GUI.Helper
GUI.Helper  5      #    GUI.View.GetRelatedValuesView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      (localclr-namespace:GUI.View     5      1	viewModelclr-namespace:GUI.ViewModel     5      +helperclr-namespace:GUI.Helper     5      ���#1E2C3A  � DataContext  5	      -   'GUI.ViewModel.GetRelatedValuesViewModel  5
   
    � 	Resources 5       �� 
TargetType �� Property �� Value��%b  '��       �� 5   
   " ��6   .�5      ��  � ��$ 5q�6'   �� 5       � ��$
 10,5q�6(   �� 5       � ��$ SemiBold"�6+   �� 5       � ��
 �����6+   �� 5       � �� �#3C6EAF6+   �� 5       � �� �#26466E6,   �� 5       � ��$ 1q�60   �� 5       � ��$ 35��6'   5      5      .��5      � $��15q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5       $
��Auto �6   �� 5"      $
��Auto �6   �� 5#      $
��Auto �6   .+�5'   
   ��  �� Orientation$ 
Horizontal=�6   $��0,0,0,10q�6.   .+�5(      �� $	��200��6   $��14(�5+      
���   �
���������5)      �� *AvailableTypes+ �� SelectedItem 5*      �� *SelectedType+5-      �� 5.      $	��200��6   $��10,0,0,0q�5/      $��14(�52      
���   �
���������50      �� *AvailableReferences+ 51      �� *SelectedReference+54      55      �� 58   
   $��1��6    �� SelectionMode$ Extended=�5:      $��14(�5;      ���#2B3F54
�������$	��150��5?      $��0,0,0,10q�5@      ��59      �� *AvailableProperties+&    GUI.Helper.SelectedItemsBehavior	   SelectedItems	 5>      �� *SelectedProperties+5@      R� 5C   
   $��2��6   ���#2B3F54
�������$	��150��5G      $��14(�5H      $��0,10,0,0q�5I      ���#26466E$��1q�5K      ��5D      �� *ResultProperties+��5L      �� 5M      
 �� ItemsSource
 6+   �� *
Properties+.R�5N      �� $��SemiBold"�6<   
�����F���6    �� *
ParentName+6R    �� ItemTemplate 5O      �� 5P      .R�5Q      �� $ 
Horizontal=�6)   .+�5R   "   �� 
���������6,   �� *	ModelCode+6G   �� 5S   "   $��: ��6,   
�������66   �� 5T   "   
������$��SemiBold"�6[   ��6,   �� *Value+6[   5U      5V      5X      5Z      �� 5]   
   $��3��6   $ 
Horizontal=�5^      $��Right=�5_      $��0,10,0,0q�5`      .+�5a      �� $��Execute��6   $	��100��5b      ��5c      �� * ExecuteGetRelatedValuesCommand+5d      5g      5h      