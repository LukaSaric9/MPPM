   M S B A M L   `   `   `  ����>  :GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:GUI.ViewGUI.View  5   	   'clr-namespace:GUI.Helper
GUI.Helper  5   	   -clr-namespace:GUI.ViewModelGUI.ViewModel  5   	       GUI.View.GetValuesView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   (localclr-namespace:GUI.View     5   	   +helperclr-namespace:GUI.Helper     5   	   *vmclr-namespace:GUI.ViewModel     5   	   ���#1E2C3A  � 	Resources  5	       �� 
TargetType �� Property �� Value��%b  '��       �� 5
   
   " ��6   .�5      ��  � ��$ 5q�6'   �� 5       � ��$
 10,5q�6(   �� 5       � ��$ SemiBold"�6+   �� 5       � ��
 �����6+   �� 5       � �� �#3C6EAF6+   �� 5       � �� �#26466E6,   �� 5       � ��$ 1q�60   �� 5       � ��$ 35��6'   5      5      .��5      � $��15q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��1.2* �6   �� 5      $
��1.8* �6   .+�5!   
   �� $��0��6   $��0��6$   $
��Auto=�64   .��5"      ��  �� Orientation$ Vertical=�6   $��0,0,10,0q�60   .+�5$      �� $��0,0,0,10q�5&      $��14(�5'      
���   �
���������5$      �� *Types+ �� SelectedItem 5%      �� *SelectedType+ �� Mode$ TwoWay=�5)      �� 5,      $��0,0,0,10q�5.      $��14(�5/      
���   �
���������5,      �� *Gids+ 5-      �� *SelectedGid+$ TwoWay=�51      �� 54      	 �� SelectionMode$	 Extended=�55      ���#2B3F54
�������$	��200��59      $��0,0,0,10q�5:      ��54      �� *
Properties+&    GUI.Helper.SelectedItemsBehavior
   SelectedItems
 56      �� *SelectedModels+5:      � 5=      $��Center=�6   .+�5>      �� $��
Get Values��6   ��5?      �� *GetValuesCommand+�� 5A      $��Clear Properties��6   ��5B      �� *ClearProperties+�� 5D      $��Reset��6   ���#7B1E1E���#5A1212��5E      �� *ResetFormCommand+5G      5H      5I      5J      � 5M   
   $��0��6   $��1��6   .+�5N      �� 
�����F�$��1q�5O       �� CornerRadius$ 10��5P       �� Padding$ 10q�5Q      ._�5R      �� .+�5S      �� $7��1Listing of available values for selected criteria��6   $��20(�5T      $��SemiBold"�5U      $��Center=�5V      
�������5W      &   System.Windows.Controls.DataGrid  5Y         AutoGenerateColumns .     CanUserSortColumns .     CanUserAddRows .     CanUserResizeColumns .  $��0,10,0,0q�5^         HeadersVisibility$ Column=�5_         
IsReadOnly . 
�������
���   ���5Y      �� *ListedProperties+   Columns 5c      0  *System.Windows.Controls.DataGridTextColumn  5d      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Model Code��61      Width5  /System.Windows.Controls.DataGridLengthConverter$	 250 5f   -   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5e   -   �� *	ModelCode+5f   -     5g      $ Value��61   $ * 5i   -    5h   -   �� *Value+5i   -   5k      5l      5m      5n      5p      5q      