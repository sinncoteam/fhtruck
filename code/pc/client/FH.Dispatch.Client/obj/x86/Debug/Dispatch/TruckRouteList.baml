   M S B A M L   `   `   `  ����M  IFH.Dispatch.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:FH.Dispatch.Client.ControlsFH.Dispatch.Client.Controls  5   3   0    *FH.Dispatch.Client.Dispatch.TruckRouteList   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   8my)clr-namespace:FH.Dispatch.Client.Controls     5   3   -   5          � Title$  叉车运行轨迹数据��5   	   $	��600��6   $	��800��6'   .��5      � .+�5   
   )   #FH.Dispatch.Client.Controls.TopMenu  -   5          Name$ 
my_topmenu��5      $	��Top=�6)   $��0,0,0,0q�6A   $
��auto��6R   6b   &   System.Windows.Controls.DataGrid  5   
   -   5        $��	dataGrid1��5   k      AutoGenerateColumns .  $	��440��6/   $
��Left=�6<   $��
20,70,20,0q�6W   $	��Top=�6|   $
��auto��6�    
  css_dg#��[  6�      Columns 5	      0  *System.Windows.Controls.DataGridTextColumn  5
      ,  &System.Windows.Controls.DataGridColumn   Header$ 编号��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 1* 61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *Id+  5      $ 叉车��6%   $ 2* 61    6<   �� *	Truckcode+  5      $ RF位置��6%   $ 2* 63    6>   �� *	Routecode+  5      $ 运行状态��6%   $ 2* 63    6>   �� *	RunStatus+  5      $ 指令��6%   $ 4* 61    6<   �� *
FullCode+  5      $ 时间��6%   $ 3* 61    6<   �� *
Createtime+5      '   !FH.Dispatch.Client.Controls.Pager  5   
   -   5          Name$ 
dataPager1��5   q   '   !FH.Dispatch.Client.Controls.Pager   PageChanged$ dataPager1_PageChanged��6   	   PageChanging$	 dataPager1_PageChanging��68   
   PageSize$
 20��6a   $	��Top=�6�   $��20,515,20,0q�6�   �� 5   
   -   5        $��tb_truck��5   L   $��23��6   $
��Left=�6   $��	64,41,0,0q�69   $	��Top=�6\   $��75��6t   �� 5   
   -   5        $��label1��5   X   $��	叉车：��6   $��28��6   $
��Left=�6*   $��	20,39,0,0q�6E   $	��Top=�6f   �� 5   
   -   5        $��btn_confirm��5   �   ���	#FFF8F8F8���	#FFF8F8F8$��确定��6@   $��23��6M   $
��Left=�6Y   $��
145,41,0,0q�6t   $	��Top=�6�   $��66��6�   6�   5      5      