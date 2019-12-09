using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterCom.Models
{
    public class WinningNumber
    {
        private int _idx = 0;
        private int _no_item = 4;
        private String _row_data;
        private String[] _row_datas;

        private int _check_cnt = 0;

        private String _no_1;
        private String _no_2;
        private String _no_3;
        private String _no_4;
        private String _no_5;
        private String _no_6;
        private String _no_7;
        private String _no_8;
        private String _no_9;
        private String _no_10;
        private String _no_11;
        private String _no_12;
        private String _no_13;
        private String _no_14;

        public int Idx { get => _idx; set => _idx = value; }
        public int No_item { get => _no_item; set => _no_item = value; }
        public string No_1 { get => _no_1; set => _no_1 = value; }
        public string No_2 { get => _no_2; set => _no_2 = value; }
        public string No_3 { get => _no_3; set => _no_3 = value; }
        public string No_4 { get => _no_4; set => _no_4 = value; }
        public string No_5 { get => _no_5; set => _no_5 = value; }
        public string No_6 { get => _no_6; set => _no_6 = value; }
        public string No_7 { get => _no_7; set => _no_7 = value; }
        public string No_8 { get => _no_8; set => _no_8 = value; }
        public string No_9 { get => _no_9; set => _no_9 = value; }
        public string No_10 { get => _no_10; set => _no_10 = value; }
        public string No_11 { get => _no_11; set => _no_11 = value; }
        public string No_12 { get => _no_12; set => _no_12 = value; }
        public string No_13 { get => _no_13; set => _no_13 = value; }
        public string No_14 { get => _no_14; set => _no_14 = value; }
        public string Row_data { get => _row_data; set => _row_data = value; }
        public string[] Row_datas { get => _row_datas; set => _row_datas = value; }
        public int Check_cnt { get => _check_cnt; set => _check_cnt = value; }


    }
}
