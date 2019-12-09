using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCom.Models
{
    public class CellData
    {
        private int cell_idx;
        private String cell_no;
        private Boolean isCheck_cell;

        public int Cell_idx { get => cell_idx; set => cell_idx = value; }
        public string Cell_no { get => cell_no; set => cell_no = value; }
        public bool IsCheck_cell { get => isCheck_cell; set => isCheck_cell = value; }
    }
}
