   M S B A M L   `   `   `  ����  FH.Dispatch.Client'    !FH.Dispatch.Client.Controls.Pager   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5          >�Name$  userControl��5   :    � 	Resources 5         NormalTextBlockStyle �� 
TargetType �� Property �� Value  PageButtonTemplate �� 
TargetType ��Name � Cursor �� Triggers	 P� Property
 P� Value �� 
TargetName��%�  &	        &	 t     �� 5	   
   " ��6-   .�5
      ��  � ��$ NoWrap=�6-   �� 5       � ��$ Center=�62   5      �� 5   
   " ��65   .R�5      �� $ 	textBlock��6   $��Stretch=�6+   #��z��6G   $��Stretch=�6h   $
 Hand��6�   
����  �6�    5      P� 5      	 � {�
 .  . �5      �� $ 	textBlock��63    � �� �	#FFABA5A56J   �� 5       �@�Cursor$
 None��6/   5      5      5      .��5      ��  �� Orientation$ 
Horizontal=�6   $��Right=�6*   .+�5   
   �� #���   6   $	��共��6C   �� 5   
   #���   6I   $	��4,0q�6x   ��6   �� *
TotalCount+ �� ElementName$ userControl��6x   �� 5   
   #���   6   $��行，每页��6C   �� 5   
   #���   6G   $	��4,0q�6v   ��6   �� *
PageSize+$ userControl��6v   �� 5   
   $��	行，第��6   #���   6   �� 5   
   #���   6H   $��4,0,2,0q�6w   ��6   �� *	PageIndex+$ userControl��6w   �� 5   
   $��/��6   #���   6   �� 5   
   #���   6U   $��2,0,4,0q�6�   ��6   �� *	PageCount+$ userControl�� �� Mode$ OneWay=�6�   �� 5    
   $	��页��6   #���   6   �� 5!   
   -   5         ��Name$ btnFirst��5!      $��首页��6#   #���  60   $��Center=�6`   $��10,0,0,0q�6{   {�6�   �� *CanGoFirstOrPrev+$ userControl���� 5"   
   -   5        $ btnPrev��5"      $��	上一页��6"   #���  60   $��Center=�6`   $��10,0,0,0q�6{   {�6�   �� *CanGoFirstOrPrev+$ userControl���� 5#   
   -   5        $ btnNext��5#      $��	下一页��6"   $��Center=�60   #���  6K   $��10,0,0,0q�6{   {�6�   �� *CanGoLastOrNext+$ userControl���� 5$   
   -   5        $ btnLast��5$      $��Center=�6"   #���  6=   $��10,0,0,0q�6m   $��末页��6�   {�6�   �� *CanGoLastOrNext+$ userControl��6�   �� 5%   
   -   5         ��Name$ txtPageIndex��5%      $��40��6(   $��Center=�63   $��10,0,0,0q�6N   ��6`   �� *	PageIndex+$ userControl��$ OneWay=��� 5&   
   -   5        $ btnGoTo��5&      $��转到��6"   #���  6/   $��Center=�6_   $��10,0,0,0q�6z   6�   5'      5(      