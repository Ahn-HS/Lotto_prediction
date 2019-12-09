using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using FilterCom.Models;

namespace FilterCom.vms
{
    public class MainVM : ViewModelBase
    {
        
        public MainVM()
        {
            //RadGridViewCommandsDelete
            //
            //    public ICommand RadGridViewCommandsDelete { get { return new RelayCommand(BuildValueChanged); }

            RadGridViewCommandsDelete = new RelayCommand(testCmd);
        }

        private ICommand RadGridViewCommandsDelete { get; set; }

        public void testCmd(object obj)
        {
            MessageBox.Show("testCmd");
        }
       // public RelayCommand RadGridViewCommandsDelete { get; set; }

        private List<FileData> _FileDatas;
        private List<FileGroupData> _FileGroupItems;


        public List<FileData> FileDatas
        {
            get
            {
                return _FileDatas;
            }
            set
            {
                _FileDatas = value;
                if (FileDatas == null)
                {
                    FileCheck_Cnt = 0;
                    FileList_Cnt = 0;
                    LastDelPer_Cnt = 0;
                    TotalDelPer_Cnt = 0;
                }
                else
                {
                    FileCheck_Cnt = FileDatas.Where(o => o.IsChecked == true).Count();
                    FileList_Cnt = FileDatas.Count();
                }
                OnPropertyChanged("FileDatas");
                OnPropertyChanged("FileCheck_Cnt");
                OnPropertyChanged("FileList_Cnt");
            }
        }

        public List<FileGroupData> FileGroupItems
        {
            get
            {
                return _FileGroupItems;
            }
            set
            {
                _FileGroupItems = value;
                OnPropertyChanged("FileGroupItems");
            }
        }
        private int _FileCheck_Cnt = 0;
        public int FileCheck_Cnt
        {
            get {
                return _FileCheck_Cnt;// FileDatas.Where(o=>o.IsChecked==true).Count();
            }set
            {
                _FileCheck_Cnt = value;
                OnPropertyChanged("FileCheck_Cnt");
            }
            
        }

        private int _FileGroupCheck_Cnt = 0;
        public int FileGroupCheck_Cnt
        {
            get
            {
                return _FileGroupCheck_Cnt;// FileDatas.Where(o=>o.IsChecked==true).Count();
            }
            set
            {
                _FileGroupCheck_Cnt = value;
                OnPropertyChanged("FileGroupCheck_Cnt");
            }

        }
        private int _FileList_Cnt = 0;
        public int FileList_Cnt
        {
            get
            {

                return _FileList_Cnt;// FileDatas.Count();
            }
            set
            {
                _FileList_Cnt = value;
                OnPropertyChanged("FileList_Cnt");
            }


        }

        private int _Filter_Cnt = 0;
        public int Filter_Cnt
        {
            get{ return _Filter_Cnt;}
            set
            {
                _Filter_Cnt = value;
                OnPropertyChanged("Filter_Cnt");
            }
        }

        public int Original_RowCount = 0;
        public int Original_RowExtCount = 0;
        private int _LastDel_Cnt = 0;
        public int LastDel_Cnt
        {
            get { return _LastDel_Cnt; }
            set
            {
                _LastDel_Cnt = value;
                TotalDel_Cnt += value;
                OnPropertyChanged("LastDel_Cnt");
            }
        }
        private double _LastDelPer_Cnt = 0;
        public double LastDelPer_Cnt
        {
            get { return _LastDelPer_Cnt; }
            set
            {
                _LastDelPer_Cnt = value;
                OnPropertyChanged("LastDelPer_Cnt");
            }
        }

        private double _TotalDelPer_Cnt = 0;
        public double TotalDelPer_Cnt
        {
            get { return _TotalDelPer_Cnt; }
            set
            {
                _TotalDelPer_Cnt = value;
                OnPropertyChanged("TotalDelPer_Cnt");
            }
        }


        private double _ExtPer_Cnt = 0;
        public double ExtPer_Cnt
        {
            get { return _ExtPer_Cnt; }
            set
            {
                _ExtPer_Cnt = value;
                OnPropertyChanged("ExtPer_Cnt");
            }
        }
        private int _TotalDel_Cnt = 0;
        public int TotalDel_Cnt
        {
            get { return _TotalDel_Cnt; }
            set
            {
                _TotalDel_Cnt = value;
                OnPropertyChanged("TotalDel_Cnt");
            }
        }

    }
}
