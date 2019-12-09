using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace FilterCom.Models
{
    public class FileData : ViewModelBase
    {

        private int _idx = 1;
        private int _no_item = 4;
        private String _fileName;
        private String _filePath;
        private int _rowCount;
        private int _orStart;
        private int _orEnd;
        private int _noMin;
        private int _noMax;
        private int _leftCount; //남은 조합 개수
        private Boolean _isChecked = false;  //체크박스 체크
        private Boolean _isOR = false;  //체크박스 체크
        private String dscription; //비고
        private String devidedstr = "~"; //비고
        private List<FileDataInfo> _FileDataInfos;

        List<OriginalData> _filterResult;
        List<OriginalData> _filterResultOR;

        public int Idx { get => _idx; set => _idx = value; }
        public int No_item { get => _no_item; set => _no_item = value; }
        public string FileName { get => _fileName; set => _fileName = value; }
        public string FilePath { get => _filePath; set => _filePath = value; }
        public int RowCount { get => _rowCount; set => _rowCount = value; }
        public int OrStart { get => _orStart;
            set
            {
                _orStart = value;
                OnPropertyChanged("OrStart");
            }
        }
        public int OrEnd
        {
            get => _orEnd;
            set
            {
                _orEnd = value;
                OnPropertyChanged("OrEnd");
            }
        }
        public int NoMin { get => _noMin;
            set
            {
                _noMin = value;
                OnPropertyChanged("NoMin");
            }
        }
        public int NoMax { get => _noMax;
            set
            {
                _noMax = value;
                OnPropertyChanged("NoMax");
            }
        }
        public int LeftCount { get => _leftCount; set => _leftCount = value; }
        public string Dscription { get => dscription; set => dscription = value; }
        public List<FileDataInfo> FileDataInfos { get => _FileDataInfos; set => _FileDataInfos = value; }
        public string Devidedstr { get => devidedstr; set => devidedstr = value; }
        public bool IsChecked {
            get { return _isChecked; }
            set { _isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
        public bool IsOR
        {
            get { return _isOR; }
            set
            {
                _isOR = value;
            }
        }
        public List<OriginalData> FilterResult { get => _filterResult; set => _filterResult = value; }
        public List<OriginalData> FilterResultOR { get => _filterResultOR; set => _filterResultOR = value; }
    }
}
