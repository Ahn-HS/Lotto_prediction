using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace FilterCom.Models
{
    public class FileGroupData : ViewModelBase
    {
        private int _idx = 1;
        private Boolean _isData = false;
        private string[] _files;
        private List<FileData> _fileDatas;
        private int _rowcount = 0;
        private int _checkcount = 0;
        private Boolean _isOR = false;  //체크박스 체크
        private Boolean _isGroupCheck = false;  //체크박스 체크

        public bool IsGroupCheck
        {
            get => _isGroupCheck;
            set
            {
                _isGroupCheck = value;
                OnPropertyChanged("IsGroupCheck");
            }
        }

        public int Idx { get => _idx; set => _idx = value; }
        public string[] Files { get => _files; set => _files = value; }
        public int Rowcount { get => _rowcount; set => _rowcount = value; }
        public int Checkcount { get => _checkcount; set => _checkcount = value; }
        public List<FileData> FileDatas { get => _fileDatas; set => _fileDatas = value; }

        List<OriginalData> _filterResult;
        List<OriginalData> _filterResultOR;
        public bool IsOR
        {
            get { return _isOR; }
            set
            {
                _isOR = value;
            }
        }

        //랜덤숫자 관련
        private Boolean _R_IsCheck = false;


        private int _R_NumStart = 1;
        private int _R_NumEnd = 1;
        private int _R_CreateCnt = 1;
        private Boolean _R_AutoCheck = false;
        private Boolean _R_BalanceCheck = false;

        public bool R_IsCheck
        {
            get => _R_IsCheck;
            set
            {
                _R_IsCheck = value;
                OnPropertyChanged("R_IsCheck");
            }
        }
        public int R_NumStart
        {
            get => _R_NumStart;
            set
            {
                _R_NumStart = value;
                OnPropertyChanged("R_NumStart");
            }
        }
        public int R_NumEnd
        {
            get => _R_NumEnd;
            set
            {
                _R_NumEnd = value;
                OnPropertyChanged("R_NumEnd");
            }
        }
        public int R_CreateCnt { get => _R_CreateCnt; set => _R_CreateCnt = value; }
        public bool R_AutoCheck {
            get => _R_AutoCheck;

            set
            {
                _R_AutoCheck = value;
                OnPropertyChanged("R_AutoCheck");
            }
        }
        public bool R_BalanceCheck { get => _R_BalanceCheck;

            set
            {
                _R_BalanceCheck = value;
                OnPropertyChanged("R_BalanceCheck");
            }
        }
        private int _R_NumMax = 0;
        public int R_NumMax {
            get => _R_NumMax;
            set
            {
                _R_NumMax = value;
                OnPropertyChanged("R_NumMax");
            }
        }



        //필터적용 관련
        private String _FileGroupName = "";
        private Boolean _F_IsCheck = false;
        private int _F_ORStart = 0;
        private int _F_OREnd = 0;


        public string FileGroupName { get => _FileGroupName; set => _FileGroupName = value; }
        public bool F_IsCheck
        {
            get => _F_IsCheck;
            set
            {
                _F_IsCheck = value;
                OnPropertyChanged("F_IsCheck");
            }
        }
        public int F_ORStart
        {
            get => _F_ORStart;
            set
            {
                _F_ORStart = value;
                OnPropertyChanged("F_ORStart");
            }
        }
        public int F_OREnd
        {
            get => _F_OREnd;
            set
            {
                _F_OREnd = value;
                OnPropertyChanged("F_OREnd");
            }
        }
        public bool IsData { get => _isData; set => _isData = value; }
        public List<OriginalData> FilterResult { get => _filterResult; set => _filterResult = value; }
        public List<OriginalData> FilterResultOR { get => _filterResultOR; set => _filterResultOR = value; }
    }
}
