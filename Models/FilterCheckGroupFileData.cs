using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCom.Models
{
    public class FilterCheckGroupFileData
    {
        public int group_id { get; set; }
        public int file_id { get; set; }
        public int cnt_check { get; set; }
        public int cnt_or { get; set; }
    }
}
