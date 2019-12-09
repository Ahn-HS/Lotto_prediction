using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCom.Models
{
    public class ConfirmFileData
    {
        public string title { get; set; }
        public int total_cnt { get; set; } //몇개 항목이 동일한가
        public int check_cnt { get; set; } //몇개 항목이 동일한가
        public List<int> file_idxs { get; set; } // 관련 파일 그룹 갯수
        public int file_cnt { get; set; }
        public double file_per { get; set; }

        public int row_cnt { get; set; }
        public double row_per { get; set; }

        public int fail_cnt { get; set; }
    }
}
