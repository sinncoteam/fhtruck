   M S B A M L   `   `   `  ����M  IFH.Dispatch.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:FH.Dispatch.Client.ControlsFH.Dispatch.Client.Controls  5   <   .    (FH.Dispatch.Client.Manage.RackLayerIndex   6	   0my)clr-namespace:FH.Dispatch.Client.Controls  6<   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  仓位层位管理��5   	   $	��680��6   $
��1000��6%    � WindowStartupLocation$ CenterScreen=�62   .��5      � .+�5   
   )   #FH.Dispatch.Client.Controls.TopMenu  -   5          Name$ 
my_topmenu��5      $	��Top=�6)   6D   �� 5   
   -   5        $��chk_all��5   Z   $��全选��6   $��16��6    $
��Left=�6,   $��	20,47,0,0q�6G   $	��Top=�6i   6�   �� 5   
   -   5        $��btn_isvalid��5   Z   $��设为有效��6   $��23��6    $
��Left=�6,   $��	83,42,0,0q�6G   $	��Top=�6m   $��97��6�      css_btn#��[  6�   �� 5	   
   -   5        $��btn_del��5	   Y   $��删除��6   $��23��6   $
��Left=�6*   $��
203,42,0,0q�6E   $	��Top=�6h   $��97��6�   #��[  6�   �� 5
   
   -   5        $��btn_newtruck��5
   ?   $��新建层位��6   $��23��6    $��	0,41,20,0q�6,   $	��Top=�6S   $��Right=�6�   $	��135��6�   #��[  6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��	dataGrid1��5   �      AutoGenerateColumns .  $	��530��6/   $
��Left=�6<      CanUserAddRows .  $��
20,70,20,0q�6n   $	��Top=�6�    
 css_dg#��[ 6�      Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 选择��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 65      CellTemplate 5      �� 5      .R�5      �� 	 ��Name$
	 c_id��6T   -   5        
 d� 	IsChecked
 5   '   �� *	IsChecked+ �� Mode$ TwoWay=�
 � Tag 6b   �� *ID+6u   5      5      0  *System.Windows.Controls.DataGridTextColumn  5      $ 编号��6%   $ 2* 61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *RackLayerCode+  5      $ 所在仓位��6%   $ 2* 63      
IsReadOnly .  6>   �� *
RackCode+6[     5      $ 所在层位��6%   $ 2* 63    6>   �� *RackLayerNum+  5      $ 状态��6%   $ 2* 61    .  6<   �� *RackStatusName+6_     5      $ 货物条码��6%   $ 2* 63    .  6>   �� *PackageCode+6^     5      $ 外部编号��6%   $ 2* 63    .  6>   �� *OutRackLayerCode+6c     5      $ 创建时间��6%   $ 3* 63    .  6?   �� *
Createtime+6^     5      $ 是否有效��6%   $ 2* 63    .  6?   �� *IsValidName+6_   5      '   !FH.Dispatch.Client.Controls.Pager  5   
   -	   5          Name$ 
dataPager1��5   I   '	   !FH.Dispatch.Client.Controls.Pager 	  PageChanged$ dataPager1_PageChanged��6      PageSize$ 20��69   $	��Top=�6]   $��20,605,20,0q�6u   5      5       