   M S B A M L   `   `   `  ����M  IFH.Dispatch.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:FH.Dispatch.Client.ControlsFH.Dispatch.Client.Controls  5   	   '    !FH.Dispatch.Client.Dispatch.Index   0my)clr-namespace:FH.Dispatch.Client.Controls  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  叉车调度中心��5   	   $	��600��6   $	��702��6%   ���	#FFF8F8F8 � WindowStartupLocation$ CenterScreen=�6b    � 
ResizeMode$ 	CanResize=�6�   .��5      � .+�5   
   )   #FH.Dispatch.Client.Controls.TopMenu  -   5          Name$ 
my_topmenu��5      $	��Top=�6)   6E   �� 5   
   -   5        $��cb_packtype��5   M   $��23��6   $
��Left=�6   $��	37,59,0,0q�6:   $	��Top=�6`   $��95��6x   .@�5      �� $��	出   库��6   
 � Tag$ 01��6+    �� 
IsSelected . 64   �� 5	      $��	入   库��6   $ 02��6+   5
      �� 5   
   -   5        $��label4��5   [   $��取货仓位：��6   $��28��6    $
��Left=�6,   $��
163,35,0,0q�6G   $	��Top=�6i   $��69��6�   �� 5   
   -   5        $��cb_pack_route��5   P   $��23��6   $
��Left=�6   $��
163,59,0,0q�6<   $	��Top=�6e   $	��242��6}   5      �� 5   
   -   5        $��	btn_start��5   |   $��开 始��6   $��36��6      css_btn#��[  6+   $
��Left=�6L   $��163,162,0,0q�6g   $	��Top=�6�   $	��242��6�   $��18(�6�   6�   �� 5   
   -   5        $��
rtb_result��5   T   $	��219��6   $
��Left=�6$   $��163,216,0,0q�6?   $	��Top=�6g   $	��461��6   �� 5   
   -   5        $��btn_showroute��5   ~   $��查看路径��6   #��[  6    $��18(�6A   $
��Left=�6O   $��
37,272,0,0q�6j   $	��104��6�   $��36��6�   $	��Top=�6�   6�   �� 5   
   -	   5        $��cb_downpack_route��5   O   $��23��6   $
��Left=�6   $��163,122,0,0q�6:   $	��Top=�6h   $	��242��6�   6�   �� 5   
   -
   5        $��label7��5   [   $��卸货仓位：��6   $��28��6    $
��Left=�6,   $��
163,96,0,0q�6G   $	��Top=�6i   $��69��6�   �� 5   
   -   5        $��cb_pack_layer��5   N   $��23��6   $
��Left=�6   $��
517,59,0,0q�6:   $	��Top=�6c   $��84��6{   5      �� 5   
   -   5        $��cb_downpack_layer��5   O   $��23��6   $
��Left=�6   $��517,122,0,0q�6:   $	��Top=�6h   $��84��6�   5      �� 5   
   -   5        $��label9��5   \   $��叉车当前位置：��6   $��28��6"   $
��Left=�6.   $��	33,95,0,0q�6I   $	��Top=�6j   $��95��6�   �� 5   
   -   5        $��lb_truck_status��5   V   $�� ��6   $��28��6   $
��Left=�6'   $��
37,118,0,0q�6B   $	��Top=�6m   $��95��6�   �� 5   
   -   5        $��btn_showtruck��5   �   $��查看监控��6   #��[  6    $��18(�6A   $��36��6O   $
��Left=�6[   $��
37,328,0,0q�6v   $	��Top=�6�   $	��104��6�   6�   )   #FH.Dispatch.Client.Controls.FootBar  5   
   -   5          Name$ x_sbar��5      $��Bottom=�6%   �� 5   
   -   5        $��label10��5   W   $	��层��6   $��28��6   $
��Left=�6(   $��
603,59,0,0q�6C   $	��Top=�6f   $��40��6~   �� 5    
   -   5        $��label11��5    X   $	��层��6   $��28��6   $
��Left=�6(   $��603,121,0,0q�6C   $	��Top=�6g   $��40��6   �� 5!   
   -   5        $��	btn_clear��5!   i   $��清除日志��6   $��18(�6    $��36��6.   $
��Left=�6:   $��
37,385,0,0q�6U   #��[  6z   $	��Top=�6�   $	��104��6�   6�   �� 5"   
   -   5        $��btn_stop��5"   i   $��停 车��6   $��18(�6   $��36��6-   $
��Left=�69   $��521,162,0,0q�6T   #��[  6y   $	��Top=�6�   $	��103��6�   6�   �� 5#   
   -   5        $��btn_select1��5#   z   $��选择��6   $��23��6   #��[  6*   $
��Left=�6K   $��
411,60,0,0q�6f   $	��Top=�6�   $	��100��6�   6�   �� 5$   
   -   5        $��btn_select2��5$   {   $��选择��6   $��23��6   #��[  6*   $
��Left=�6K   $��411,122,0,0q�6f   $	��Top=�6�   $	��100��6�   6�   �� 5%   
   -   5        $��btn_refresh��5%   h   $��刷 新��6   $��18(�6   $��36��6-   $
��Left=�69   $��
37,216,0,0q�6T   #��[  6{   $	��Top=�6�   $	��104��6�   6�   �� 5&   
   -   5        $��lb_inout_status��5&   �   $��	出   库��6   $��48��6    $
��Left=�6,   $��Center=�6G   $��Center=�6k   $��
37,150,0,0q�6�   $	��Top=�6�   $	��104��6�   $��26(�6�   $
��Bold"�6�   ���	#FFE815156�   �� 5'   
   -   5        $��cb_trucklist��5'   �   $��36��6   $
��Left=�6   $��Center=�6:   $��Center=�6\   $��453,162,0,0q�6�   $	��Top=�6�   $��58��6�   �� 5(   
   -   5        $��label1��5(   Z   $��	叉车：��6   $��28��6   $
��Left=�6*   $��410,168,0,0q�6E   $	��Top=�6h   $��48��6�   5)      5*      