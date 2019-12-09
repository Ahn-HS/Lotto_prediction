using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FilterCom.Models;
namespace FilterCom.views
{
    /// <summary>
    /// PConfirmFile1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PConfirmFile1 : Window
    {
        public PConfirmFile1()
        {
            InitializeComponent();
        }

        public void initData(int CheckFileCount, int CheckCount, List<ConfirmFileData> confirmDatas)
        {
            ConfirmFileData file1 = confirmDatas.Where(o => o.check_cnt == CheckCount).FirstOrDefault();
            ConfirmFileData file2 = confirmDatas.Where(o => o.check_cnt < CheckCount).FirstOrDefault();
            int CheckNo1 = file1.file_idxs.Count();
            int CheckNo2 = 0;// confirmDatas.Where(o => o.check_cnt < CheckCount).Count();

            String file1_datas = String.Join(",", file1.file_idxs.ToArray());
            String file2_datas = "";
            CheckNo2 = file2.file_idxs.Count();
            file2_datas = String.Join(",", file2.file_idxs.ToArray());

            Double per_checkNo1 = 0;
            Double per_checkNo2 = 0;

            if (CheckNo1 == 0 || CheckFileCount == 0)
            {
                per_checkNo1 = 0;
            }
            else
            {
                per_checkNo1 = Math.Round( Convert.ToDouble(CheckNo1) / Convert.ToDouble(CheckFileCount) *100, 2);
            }

            if (CheckNo2 == 0 || CheckFileCount == 0)
            {
                per_checkNo2 = 0;
            }
            else
            {
                per_checkNo2 = Math.Round(Convert.ToDouble(CheckNo2) / Convert.ToDouble(CheckFileCount) * 100, 2);
            }

            this.lbl_group_cnt.Content = Convert.ToString(CheckFileCount);
            this.lbl_cnt1.Content = Convert.ToString(CheckNo1);
            this.lbl_cnt2.Content = Convert.ToString(CheckNo2);
            this.lbl_per1.Content = Convert.ToString(per_checkNo1);
            this.lbl_per2.Content = Convert.ToString(per_checkNo2);
            this.lbl_file1.Content = file1_datas;
            this.lbl_file2.Content = file2_datas;

        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                this.Close();
            }
        }
    }
}
