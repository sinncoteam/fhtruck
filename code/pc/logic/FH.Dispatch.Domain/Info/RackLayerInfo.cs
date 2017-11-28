using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Dispatch.Domain.Model;
using System.ComponentModel;

namespace FH.Dispatch.Domain.Info
{
    public class RackLayerInfo : RackLayer, INotifyPropertyChanged
    {
        public string RackCode { get; set; }
        public string RackName { get; set; }
        public string RackStatusName
        {
            get
            {
                switch (RackStatus)
                {
                    case 0: return "空闲";
                    case 1: return "存货";
                }
                return "未知";
            }
        }
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
