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
    /// PConfirmFile2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PConfirmFile2 : Window
    {
        public PConfirmFile2()
        {
            InitializeComponent();
        }

        public void initData(FileData files, List<WinningNumber> winings)
        {
            if (winings == null)
            {
                MessageBox.Show("당첨번호가 존재하지 않습니다.");
                return;
            }

            if (winings.Count == 0)
            {
                MessageBox.Show("당첨번호가 존재하지 않습니다.");
                return;
            }

            if (winings[0].Row_data == null || winings[0].Row_data.Equals(""))
            {
                MessageBox.Show("당첨번호가 존재하지 않습니다. - 첫번째 항목 당첨번호 부재");
                return;
            }
            List<ConfirmFileDataVO> confirms = new List<ConfirmFileDataVO>();
    
            int total_cnt = 0;
   
            foreach (WinningNumber winningNum in winings)
            {
                total_cnt = 0;
                ConfirmFileDataVO cData = new ConfirmFileDataVO();
                cData.title = Convert.ToString(winningNum.Idx) + "회";
                cData.check_cnt = 0;
                cData.none_check_cnt = 0;
                cData.file_idxs = new List<int>();
                List<String> CheckNums = winningNum.Row_data.Split('\t').ToList();
       
                    foreach (FileDataInfo fdiItem in files.FileDataInfos) //그룹파일의 개별 파일의 한줄 단위
                    {
                        total_cnt++;
                        fdiItem.CheckCnt = 0;
  
                        foreach (String cItem in CheckNums)
                        {
                            int duplCnt = fdiItem.Cells.Where(o => o.Cell_no == cItem).Count();
                            if (duplCnt > 0)
                            {
                              fdiItem.CheckCnt++;
                            }
                        }
                        if (fdiItem.CheckCnt >= files.NoMin && fdiItem.CheckCnt <= files.NoMax)
                        {
                            cData.check_cnt ++;
                        }
                        else
                        {
                            cData.none_check_cnt ++;

                        }
                }

                cData.total_file = total_cnt;
                if (cData.check_cnt == 0 || total_cnt == 0)
                {
                    cData.check_per = 0;
                }
                else
                {
                    cData.check_per = Math.Round(Convert.ToDouble(cData.check_cnt) / Convert.ToDouble(total_cnt) * 100, 2);
                }

                confirms.Add(cData);

            }

            grid_check.ItemsSource = confirms;
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
