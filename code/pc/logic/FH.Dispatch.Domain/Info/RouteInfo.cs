/*----------------------------------------------------------------
// Copyright (C) 2016 重庆优纳科技有限公司 版权所有。 
//
// 文件名：Route.cs
// 文件功能描述： 领域层实体定义(Model)
//
// 
// 创建标识：   dxk -- 2016/12/8 10:15:29 
//
// 修改标识：   
// 修改描述：   
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Dispatch.Domain.Model;
using System.ComponentModel;

namespace FH.Dispatch.Domain.Info
{
    public class RouteInfo : Route, INotifyPropertyChanged
    {
        public string IsValidName
        {
            get
            {
                switch (IsValid)
                {
                    case 0: return "无效";
                    case 1: return "有效";
                }
                return "未知";
            }
        }

        private bool _ischecked;
        public bool IsChecked
        {
            get
            {
                return _ischecked;
            }
            set
            {
                _ischecked = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}