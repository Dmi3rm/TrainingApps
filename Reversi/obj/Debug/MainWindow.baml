   M S B A M L   `   `   `  ����  Reversi    Reversi.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6?   -   5          �Name$  Window��5       � Title$ Реверси��5      $	��510��5      $	��650��6   .��5
      ]� 
���   �.+�5   
   �� $��Bottom=�6   $��10��6)   
���   �$��Bottom=�6H   $	��484��6c   �� 5   
   $	��Top=�6   $	��115��6&   ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ Black��6#    � Offset$ 1Q�61   � 5      $ 	#FF664B4B��6#   $ 0.819Q�65   5      .+�5      �� -   5         ��Name$ FirstPlayer��5       �� Left$	 322��6-   $
��Wrap=�6?   
 �� Top$ 40��6S   $	��100��6c   $��23��6o   $��18.667(�6{   $��Kristen ITC)�6�   $��Computer��6�   �� 5      -   5        $ SecondPlayer��5      $ 321.697��6.   $
��Wrap=�6D   $��Player��6X   $��23��6f   $ 70��6r   $��18.667(�6�   $��Kristen ITC)�6�   $��79.543��6�   �� 5      -   5        $ Computer��5      $��24(�6*   $	 440��68   $
��Wrap=�6J   $�� ��6^   $ 31.076��6f   $��30��6z   �� 5      -   5        $ Player��5      $��24(�6(   $	 440��66   $
��Wrap=�6H   $�� ��6\   $ 61.076��6d   $��30��6x   o� 5      $��0q�6   $��21��6'   $��72��63   
�������.@�5      m� $��0q�6   $��New Game��6/   $��21��6A   $��72��6M   
�������.@�5      m� -   5        $��0q�5      $��Single Game��63   
�������6c   m� 5      -   5        $��0q�5      $��Two Players��63   
�������6c   5      5      o� 5      $��0q�6   $��21��6'   $ 72��63   $��57��6D   .@�5      m� $��0q�6   $��_Options��6/   $��57��6A   $��21��6L   
�������.@�5       m� $��0q�6   $��Design��63   $	��Top=�6C   $
��Left=�6[   $��21��6v   
�������.@�5!      m� -   5        $��0q�5!   #   $��Standard��67   
�������6`   m� 5"      -	   5        $��0q�5"   #   $��Classic��67   
�������6_   m� 5#      -
   5        $��0q�5#   #   $��Retro��67   
�������6]   m� 5$      -   5        $��0q�5$   #   $��Gold-silver��67   
�������6c   m� 5%      -   5        $��0q�5%   #   $��Random��67   
�������6^   5&      5'   %   5(      �� 5)      -   5        $ 
TittleText��5)      	 �� TextAlignment$	 Center=�6,   $	 200��6C   $
��Wrap=�6U   $��294.303��6i   $��36��6y   $��24(�6�   $��Single Game��6�   $��Showcard Gothic)�6�   $ -3��6�   �� 5*      -   5        $��0q�5*   *   $��About��6>   $	 129��6N   $��48��6`   $��21��6k   
�������6w   �� 5+      $	��279��6   $��58.424��6!   $ 19��61   $ 31.076��6B   ��5,      �� 5-      
 �� ImageSource$
 Picture.png��6!    j� Stretch$ Uniform=�6;   5/      50      � 51   
   -   5         �Name$ 	gameField��51   ,   $��Bottom=�6   $	��480��6?   $	��480��6L   
������� � ColumnDefinitions 52      �� 53      �� 54      �� 55      �� 56      �� 57      �� 58      �� 59      �� 5:       � RowDefinitions 5<      �� 5=      �� 5>      �� 5?      �� 5@      �� 5A      �� 5B      �� 5C      �� 5D      5F      5G      5H      