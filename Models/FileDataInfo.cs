using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCom.Models
{
    public class FileDataInfo
    {
        private int _no_item = 4;
        private String _fileName;
        private String _filePath;
        private String _rowData;
        private int _checkCnt = 0;
        private int _rowIdx = 0;
        private Boolean _isOR = false;
        List<OriginalData> _filterResult; //결과 데이터
        List<OriginalData> _filterResultOR; //결과 데이터
        
        private List<CellData> cells;

        public int No_item {
            get => _no_item;
            set => _no_item = value;
        }
        public string FileName { get => _fileName; set => _fileName = value; }
        public string FilePath { get => _filePath; set => _filePath = value; }
        
        public string RowData { get => _rowData; set => _rowData = value; }
        public int CheckCnt { get => _checkCnt; set => _checkCnt = value; }
        public List<CellData> Cells { get => cells; set => cells = value; }
        public int RowIdx { get => _rowIdx; set => _rowIdx = value; }
        public bool IsOR { get => _isOR; set => _isOR = value; }
        public List<OriginalData> FilterResult { get => _filterResult; set => _filterResult = value; }
        public List<OriginalData> FilterResultOR { get => _filterResultOR; set => _filterResultOR = value; }
    }
}
