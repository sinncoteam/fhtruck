   M S B A M L   `   `   `  ����M  IFH.Dispatch.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:FH.Dispatch.Client.ControlsFH.Dispatch.Client.Controls  5   	   *    $FH.Dispatch.Client.Manage.RouteIndex   0my)clr-namespace:FH.Dispatch.Client.Controls  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  RF路径管理��5   	   $	��680��6   $
��1000��6%    � WindowStartupLocation$ CenterScreen=�6b   .��5      � .+�5   
   )   #FH.Dispatch.Client.Controls.TopMenu  -   5          Name$ 
my_topmenu��5      $	��Top=�6)   $��0,0,0,0q�6A   $
��auto��6R   6b   &   System.Windows.Controls.DataGrid  5	   
   -   5        $��	dataGrid1��5	   �      AutoGenerateColumns .  $	��530��6/   $
��Left=�6<      CanUserAddRows .  $��
20,70,20,0q�6n   $	��Top=�6�   $
��auto��6�    
  css_dg#��[  6
     Columns 5
      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 选择��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 65      CellTemplate 5      �� 5      .R�5      �� 	 ��Name$
	 c_id��6T   -   5        
 d� 	IsChecked
 5   '   �� *	IsChecked+ �� Mode$ TwoWay=�
 � Tag 6b   �� *Id+6u   5      5      0  *System.Windows.Controls.DataGridTextColumn  5      $ 编码��6%   $ 2* 61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *	Routecode+  5      $ 起点��6%   $ 2* 61    6<   �� *	Isstart+  5      $ 岔路��6%   $ 2* 61    6<   �� *Routewaycount+  5      $	 前��6%   $ 2* 60    6;   �� *Nextroutecode+  5      $	 值��6%   $ 2* 60    6;   �� *Nextroutevalue+  5      $	 左��6%   $ 2* 60    6;   �� *Leftroutecode+  5      $	 值��6%   $ 2* 60    6;   �� *Leftroutevalue+  5      $	 右��6%   $ 2* 60    6;   �� *Rightroutecode+  5      $	 值��6%   $ 2* 60    6;   �� *Rightroutevalue+  5      $	 后��6%   $ 2* 60    6;   �� *Backroutecode+  5      $	 值��6%   $ 2* 60    6;   �� *Backroutevalue+  5      $ X��6%   $ 2* 60    6;   �� *X+  5      $ Y��6%   $ 2* 60    6;   �� *Y+  5       $ 是否有效��6%   $ 2* 63    6?   �� *IsValidName+  5!      $ 2* 6*   $ 备注��65    5"      �� 5#      .R�5$      �� .+�5%   "   �� $	��无��6,    �� TextAlignment$ Center=�65   6N   5&      5'      5)      5+      '   !FH.Dispatch.Client.Controls.Pager  5,   
   -   5          Name$ 
dataPager1��5,   q   '	   !FH.Dispatch.Client.Controls.Pager 	  PageChanged$ dataPager1_PageChanged��6    	  PageChanging$ dataPager1_PageChanging��68      PageSize$ 20��6a   $	��Top=�6�   $��20,605,20,0q�6�   �� 5-   
   -   5        $��btn_newroute��5-   c   $��新建RF节点��6     css_btn#��[ 6"   $��23��6C   $��
0,42,141,0q�6O   $	��Top=�6w   $��Right=�6�   $��97��6�   �� 5.   
   -   5        $��button1��5.   Z   $��刷 新��6   $��23��6   $��Right=�6+   $��	0,42,20,0q�6G   $	��Top=�6i   $��97��6�   6�   �� 5/   
   -   5        $��btn_isvalid��5/   {   $��设为有效��6   #��[ 6    $��23��6A   $
��Left=�6M   $��	83,42,0,0q�6h   $	��Top=�6�   $��97��6�   6�   �� 50   
   -	   5        $��btn_del��50   z   $��删除��6   #��[ 6   $��23��6?   $
��Left=�6K   $��
203,42,0,0q�6f   $	��Top=�6�   $��97��6�   6�   �� 51   
   -
   5        $��chk_all��51   Z   $��全选��6   $��16��6    $
��Left=�6,   $��	20,47,0,0q�6G   $	��Top=�6i   6�   �� 52   
   -   5        $��btn_showpath��52   |   $��查 看��6   #��[ 6   $��23��6@   $��Right=�6L   $��
0,42,261,0q�6h   $	��Top=�6�   $��97��6�   6�   53      54      