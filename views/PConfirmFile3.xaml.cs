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
    public partial class PConfirmFile3 : Window
    {
        public PConfirmFile3()
        {
            InitializeComponent();
        }

        public void initData(List<String> CheckNums, List<FileData> fGroups)
        {

            ConfirmFileData tConfirmFileData = new ConfirmFileData();
            tConfirmFileData.check_cnt = 0;
            tConfirmFileData.file_per = 0;
            tConfirmFileData.file_idxs = new List<int>();

            ConfirmFileData tConfirmFileData2 = new ConfirmFileData();
            tConfirmFileData2.check_cnt = 0;
            tConfirmFileData2.file_per = 0;
            tConfirmFileData2.file_idxs = new List<int>();

            WinningNumber wNum = new WinningNumber();

            int total_file_cnt = 0;
            foreach (FileData fItem in fGroups)
            {
                if (fItem.IsChecked == false) continue;
                wNum.Check_cnt = 0;
                total_file_cnt++;
                foreach (FileDataInfo fdiItem in fItem.FileDataInfos) //그룹파일의 개별 파일의 한줄 단위
                {
                    fdiItem.CheckCnt = 0;

                    foreach (String cItem in CheckNums)
                    {
                        int duplCnt = fdiItem.Cells.Where(o => o.Cell_no == cItem).Count();
                        if (duplCnt > 0)
                        {
                            fdiItem.CheckCnt++;
                        }
                    }
                    System.Diagnostics.Debug.WriteLine("fdiItem.CheckCn:" + Convert.ToString(fdiItem.CheckCnt)
                        + ",fItem.NoMin:" + Convert.ToString(fItem.NoMin)
                        + ",fItem.NoMax:" + Convert.ToString(fItem.NoMax)
                        );
                    if (fdiItem.CheckCnt >= fItem.NoMin && fdiItem.CheckCnt <= fItem.NoMax)
                    {
                        wNum.Check_cnt++;
                        /*
                        if (tConfirmFileData.file_idxs.Where(o => o == fItem.Idx).Count() == 0)
                        {
                            tConfirmFileData.file_idxs.Add(fItem.Idx);

                            if (tConfirmFileData2.file_idxs.Where(o => o == fItem.Idx).Count() > 0)
                            {
                                tConfirmFileData2.file_idxs.Remove(fItem.Idx);
                            }
                        }
                        */
                    }
                    else
                    {
                        /*
                        if (tConfirmFileData.file_idxs.Where(o => o == fItem.Idx).Count() == 0)
                        {
                            if (tConfirmFileData2.file_idxs.Where(o => o == fItem.Idx).Count() == 0)
                            {
                                tConfirmFileData2.file_idxs.Add(fItem.Idx);
                            }
                        }
                        */
                    }
                }
                System.Diagnostics.Debug.WriteLine("CHECK ::::: " + Convert.ToString(fItem.Idx)  + "::" + Convert.ToString(wNum.Check_cnt));
                if (fItem.OrStart <= wNum.Check_cnt && fItem.OrEnd >= wNum.Check_cnt)  // OR  처리
                {
                    tConfirmFileData.file_idxs.Add(fItem.Idx);
                }
                else
                {
                    tConfirmFileData2.file_idxs.Add(fItem.Idx);
                }



            }

            int CheckNo1 = tConfirmFileData.file_idxs.Count();
            int CheckNo2 = tConfirmFileData2.file_idxs.Count();

            //String file1_datas = String.Join(",", tConfirmFileData.file_idxs.ToArray());
            //String file2_datas = "";
                
            //file2_datas = String.Join(",", tConfirmFileData2.file_idxs.ToArray());

            Double per_checkNo1 = 0;
            Double per_checkNo2 = 0;

            if (CheckNo1 == 0 || total_file_cnt == 0)
            {
                per_checkNo1 = 0;
            }
            else
            {
                per_checkNo1 = Math.Round(Convert.ToDouble(CheckNo1) / Convert.ToDouble(total_file_cnt) * 100, 2);
            }

            if (CheckNo2 == 0 || total_file_cnt == 0)
            {
                per_checkNo2 = 0;
            }
            else
            {
                per_checkNo2 = Math.Round(Convert.ToDouble(CheckNo2) / Convert.ToDouble(total_file_cnt) * 100, 2);
            }
                
            this.lbl_group_cnt.Content = Convert.ToString(total_file_cnt);
            this.lbl_cnt1.Content = Convert.ToString(CheckNo1);
            this.lbl_cnt2.Content = Convert.ToString(CheckNo2);
            this.lbl_per1.Content = Convert.ToString(per_checkNo1);
            this.lbl_per2.Content = Convert.ToString(per_checkNo2);
            //this.lbl_file1.Content = file1_datas;
           // this.lbl_file2.Content = file2_datas;

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
