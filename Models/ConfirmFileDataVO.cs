using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCom.Models
{
    public class ConfirmFileDataVO
    {
        public string title { get; set; }
        public int winningIdx { get; set; }
        public string winningNums { get; set; }
        public int total_file { get; set; }

        public int check_cnt { get; set; } //몇개 항목이 동일한가
        public double check_per { get; set; } //몇개 항목이 동일한가
        public List<int> file_idxs { get; set; } // 관련 파일 그룹 갯수
        public String file_idxs_str { get; set; }



        public int none_check_cnt { get; set; } //몇개 항목이 동일한가
        public double none_check_per { get; set; } //몇개 항목이 동일한가
        public List<int> none_file_idxs { get; set; } // 관련 파일 그룹 갯수
        public String none_file_idxs_str { get; set; }
    }
}
