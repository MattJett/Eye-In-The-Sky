   M S B A M L   `   `   `  ���� F  BEyeInTheSky, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:EyeInTheSkyEyeInTheSky      EyeInTheSky.DataBasePage   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     +localclr-namespace:EyeInTheSky     -     N�Name$  DataBase�� N� 	KeepAlive .  N� Title$ DataBasePage��$	��421��$
��1105��&   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=� N� 
Background �	#FF58585F N� 
Foreground �	#FF58585F N� 
FontFamily$ Roboto)�.���  � RowDefinitions �� $	��29* ��� $	��46* ��� $
��375* � � ColumnDefinitions �� �� .+��� ���	#FF4E4E53$
��Left=�$	��375��$	��Top=�$
��1105��$��2��$��2��&   System.Windows.Controls.DataGrid  -   $��DataGrid_dbPortal��$
��Left=�$	��317��$��	14,15,0,0q�$	��Top=�$
��1064��	 �� IsTextSearchEnabled	 . 
   RowBackground
 �	#FFFFFAE8   AlternatingRowBackground
 �����   HeadersVisibility$ Column=� �� AlternationCount$ 1��   SelectionMode$ Single=�   
IsReadOnly . $��2��   MinColumnWidth$ 10Q�$��2��   AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Entry��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto    
IsReadOnly .    MinWidth$ 10Q�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *[0]+  $ Change��$ 58  . $ 10Q� �� *[1]+  $
 File��$	 194  . $ 10Q� �� *[2]+  $	 Ext��$ 50  . $ 10Q� �� *[3]+  $
 Path��$	 595  . $ 10Q� �� *[4]+  $ Modified��$	 130  . $ 10Q� �� *[5]+�� ���	#FF39393E$
��Left=�$��	492,7,0,7q�$��1��$	��306��$��2���� -    ��Name$ ComboBox_Filter�� �� 
IsEditable . $
��Left=�$��
530,11,0,0q�$	��Top=�$��70��$��Light"�   ComboBoxStyle1#���   $��21��$��1��$��2����Z� .@��� $�� �� �� 
IsSelected . �� $
��.pdf���� $
��.jpg���� $
��.txt���� $
��Left=�$��19��$��
508,12,0,0q�$	��Top=�$��20��$$��Resourse/fileExtensionIcon.png��$��1���� $��1�����	#FF4E4E53$
��Left=�$��76��$	��Top=�$	��299��$��3��$��253.5,0,0,0q��� -    ��Name$ CheckBox_HiddenFiles��$��Show Hidden Files��$
��Left=�$��73.5,0,0,14q�$��Bottom=����	#FFE2E2E2$��Roboto)�$ Ideal=�$��Bottom=�$	��108��
�������$��4,-1,0,1q�$��1��$��1��$��10(��� $��2�����	#FF4E4E53$
��Left=�$��26��$	��Top=�$
��1008���� ���	#FF39393E$
��Left=�$��85��$	��Top=�$	��250��$��	274,0,0,0q�$��1��$��3���� -    ��Name$ Button_ClearDataBase��$��Clear Database��$
��Left=�$��363.5,10,0,27q�$	��146�����	#FF403F3F���	#FFD6D6D6  Clear_ButtonGreyOut_Top#���  $��Roboto)�$��Medium"�$��1��$��2����Z� �� -   $ Button_LoadDataBase��$��Load Database��$
��Left=�$��363.5,16,0,345q�$	��146�����	#FF403F3F���	#FFD6D6D6  Clear_ButtonGreyOut_Bottom#���  $��Roboto)�$��Medium"�$��1��$��2��$��1��{�.  ��Z� �� -   $ 
Button_Run��$	��RUN��$��1��$
��Left=�$��74��$��	281,6,0,0q�$	��Top=�$��77��  Main_Button#���  
�������$��3��$��16(�$
��Bold"���Z� 