   M S B A M L   `   `   `  ����>  :GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GUI.ViewGUI.View  5      -clr-namespace:GUI.ViewModelGUI.ViewModel  5      'clr-namespace:GUI.Helper
GUI.Helper  5      "    GUI.View.GetExtentValuesView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:GUI.View     5      1	viewModelclr-namespace:GUI.ViewModel     5      +helperclr-namespace:GUI.Helper     5      ���#1E2C3A  � DataContext  5      ,   &GUI.ViewModel.GetExtentValuesViewModel  5   
    � 	Resources 5       �� 
TargetType �� Property �� Value��%�  '��       �� 5   
   " ��6   .�5      ��  � ��$ 5q�6'   �� 5       � ��$
 10,5q�6(   �� 5       � ��$ SemiBold"�6+   �� 5       � ��
 �����6+   �� 5       � �� �#3C6EAF6+   �� 5       � �� �#26466E6,   �� 5       � ��$ 1q�60   �� 5       � ��$ 35��6'   �� 5       � ��$ 14(�6)   5      5      .��5      � $��15q�6    � RowDefinitions 5   
   �� 5       $��* �6   �� 5!      $
��Auto �6    � ColumnDefinitions 5#   
   �� 5$      $
��1.2* �6   �� 5%      $
��1.8* �6   .+�5)   
   �� $��0��6   $��0��6   
�����F�$��1q�5*   )    �� CornerRadius$ 10��6=    �� Padding$ 10q�6O   $��0,0,10,0q�6\   ���#243447._�5+      �� .+�5,      �� $��Get Extent Values��6   $��22(�5-      $��SemiBold"�5.      $��Center=�5/      
�������50      �� 52      $��DMS Type��6   $��14(�53      $��SemiBold"�54      $��Center=�55      
�������$��0,15,0,5q�57      �� 58      $��14(�5:      $��Regular"�5;      
���   �
�������$��0,0,0,15q�5>      ��58      �� *Types+	 �� SelectedItem	 59      �� *SelectedType+5>      �� 5@      $��
Properties��6   $��14(�5A      $��SemiBold"�5B      $��Center=�5C      
�������$��0,0,0,5q�5E      �� 5F      
 �� SelectionMode$
 Extended=�6   $��14(�5H      $��Regular"�5I      ���#2B3F54
�������$	��200��5L      $��0,0,0,15q�5N      ��5G      �� *
Properties+&    GUI.Helper.SelectedItemsBehavior   SelectedItems 5M      �� *SelectedItems+5N      � 5P      $��Center=�6   $��0,0,0,10q�69   .+�5Q      �� $��Clear Selected Props��6   
����=H�
����=H�$��2q�5R   T   ��5Q   <   �� *ClearPropertiesCommand+5R   T   �� 5S      $��	Reset All��6   
���""��
���""��$��2q�5T   L   $��10,0,0,0q�6`   ��5S   1   �� *ResetAllCommand+5T   `   5U      �� 5W      $!��Get Values ~ Apply Criteria��6   
��� d �
��� d �$��2q�5Y   H   $��SemiBold"�5Z      ��5X      �� *GetExtentValuesCommand+5Z      5[      5\      �� 5_   
   $��0��6   $��1��6   
�����F�$��1q�5`   )   $ 10��6=   $ 10q�6O   ���#243447._�5a      �� .+�5b      �� $7��1Listing of available values for selected criteria��6   $��22(�5c      $��SemiBold"�5d      $��Center=�5e      
�������$��0,0,0,10q�5g      �� 5i      
�����F�$	��0.5q�61   $ 5��6G   
�������._�5j      �� $
��Auto=�6#   $
��Auto=�6F   $	��550��6k   .��5k      R� $��14(�6J   $��Regular"�6X   
�������
���   ���6#   �� *ListedEntities+��5l      �� 5m   "    �� ItemsSource 6;   �� *
Properties+.R�5n   &   �� 
�����F�$��SemiBold"�6c   ��60   �� *
ParentName+6c    �� ItemTemplate 5o   &   �� 5p   *   .R�5q   .   ��  �� Orientation$ 
Horizontal=�69   .+�5r   2   �� ��6<   �� *	ModelCode+�� 5s   2   $	�� - ��6<   �� 5t   2   $��SemiBold"�6S   ��6<   �� *Value+6S   5u   /   5v   +   5x   #   5z      5{      5|      5}      5~      5�      5�      