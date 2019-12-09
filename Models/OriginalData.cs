using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCom.Models
{
    public class OriginalData
    {
        private int cellCtn;
        private int rowIdx;
        private String rowData;

        private int checkCnt = 0;



        private List<CellData> cells;

        private Boolean isChecked = false;
        private Boolean isDelete = false;
        private Boolean isFilter = false;

        private int cntCheck = 0;
        public OriginalData(int idx, string data, int cnt) {
            rowIdx = idx;
            rowData = data;
            checkCnt = cnt;
        }

        public int CellCtn { get => cellCtn; set => cellCtn = value; }
        public string RowData { get => rowData; set => rowData = value; }
        public int CheckCnt { get => checkCnt; set => checkCnt = value; }

        public bool IsChecked { get => isChecked; set => isChecked = value; }
        public bool IsDelete { get => isDelete; set => isDelete = value; }
        public int CntCheck { get => cntCheck; set => cntCheck = value; }
        public List<CellData> Cells { get => cells; set => cells = value; }
        public int RowIdx { get => rowIdx; set => rowIdx = value; }
        public bool IsFilter { get => isFilter; set => isFilter = value; }
    }
}
