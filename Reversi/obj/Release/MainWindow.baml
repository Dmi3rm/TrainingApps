   M S B A M L   `   `   `  ����   Reversi    Reversi.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$  Window�� � Title$ Реверси��$	��510��$	��650��.��]� 
���   �.+��� $��Bottom=�$��10��
���   �$��Bottom=�$	��484���� $	��Top=�$	��115������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ Black�� � Offset$ 1Q�� $ 	#FF664B4B��$ 0.819Q�.+��� -    ��Name$ FirstPlayer�� �� Left$	 322��$
��Wrap=�
 �� Top$ 40��$	��100��$��23��$��18.667(�$��Kristen ITC)�$��Computer���� -   $ SecondPlayer��$ 321.697��$
��Wrap=�$��Player��$��23��$ 70��$��18.667(�$��Kristen ITC)�$��79.543���� -   $ Computer��$��24(�$	 440��$
��Wrap=�$�� ��$ 31.076��$��30���� -   $ Player��$��24(�$	 440��$
��Wrap=�$�� ��$ 61.076��$��30��o� $��0q�$��21��$��72��
�������.@�m� $��0q�$��New Game��$��21��$��72��
�������.@�m� -   $��0q�$��Single Game��
�������m� -   $��0q�$��Two Players��
�������o� $��0q�$��21��$ 72��$��57��.@�m� $��0q�$��_Options��$��57��$��21��
�������.@�m� $��0q�$��Design��$	��Top=�$
��Left=�$��21��
�������.@�m� -   $��0q�$��Standard��
�������m� -	   $��0q�$��Classic��
�������m� -
   $��0q�$��Retro��
�������m� -   $��0q�$��Gold-silver��
�������m� -   $��0q�$��Random��
��������� -   $ 
TittleText��	 �� TextAlignment$	 Center=�$	 200��$
��Wrap=�$��294.303��$��36��$��24(�$��Single Game��$��Showcard Gothic)�$ -3���� -   $��0q�$��About��$	 129��$��48��$��21��
��������� $	��279��$��58.424��$ 19��$ 31.076������ 
 �� ImageSource$
 Picture.png�� j� Stretch$ Uniform=�� -    �Name$ 	gameField��$��Bottom=�$	��480��$	��480��
������� � ColumnDefinitions �� �� �� �� �� �� �� ��  � RowDefinitions �� �� �� �� �� �� �� �� 