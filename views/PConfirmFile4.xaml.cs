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
    public partial class PConfirmFile4 : Window
    {
        public PConfirmFile4()
        {
            InitializeComponent();
        }

        public void initData(List<FileData> files, List<WinningNumber> winings)
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

            int winiCellNo = winings[0].Row_data.Split('\t').ToList().Count;
           
            int total_cnt = 0;
            WinningNumber wNum = new WinningNumber();
            // int CheckFileCount = files.Count;
            foreach (WinningNumber winningNum in winings)
            {
                total_cnt = 0;
                ConfirmFileDataVO cData = new ConfirmFileDataVO();
                cData.title = Convert.ToString(winningNum.Idx) + "회";
                cData.winningIdx = winningNum.Idx;
                cData.winningNums = winningNum.Row_data.TrimEnd();
                cData.file_idxs = new List<int>();
                cData.none_file_idxs = new List<int>();
                List<String> CheckNums = winningNum.Row_data.Split('\t').ToList();

                foreach (FileData fItem in files)
                {
                    winningNum.Check_cnt = 0;
                    total_cnt++;
                    foreach (FileDataInfo fdiItem in fItem.FileDataInfos) //그룹파일의 개별 파일의 한줄 단위
                    {
                        fdiItem.CheckCnt = 0;
                        int CheckCount = fdiItem.Cells.Count();
                        int check_cnt = 0;
                        foreach (String cItem in CheckNums)
                        {
                            int duplCnt = fdiItem.Cells.Where(o => o.Cell_no == cItem).Count();
                            if (duplCnt > 0)
                            {
                                //fdiItem.CheckCnt++;
                                check_cnt++;
                            }
                        }

                        if (check_cnt >= fItem.NoMin && check_cnt <= fItem.NoMax)
                        {
                            winningNum.Check_cnt++;
                      
                        }
                    }
                    if (fItem.OrStart <= winningNum.Check_cnt && fItem.OrEnd >= winningNum.Check_cnt)  // OR  처리
                    {
                        cData.file_idxs.Add(fItem.Idx);
                    }
                    else
                    {
                        cData.none_file_idxs.Add(fItem.Idx);
                    }




                }



                //cData.file_idxs_str = String.Join(",", cData.file_idxs.ToArray());
                //cData.none_file_idxs_str = String.Join(",", cData.none_file_idxs.ToArray());
                int CheckNo1 = cData.file_idxs.Count();
                int CheckNo2 = cData.none_file_idxs.Count();
                cData.check_cnt = CheckNo1;
                cData.none_check_cnt = CheckNo2;
                cData.total_file = total_cnt;
                if (CheckNo1 == 0 || total_cnt == 0)
                {
                    cData.check_per = 0;
                }
                else
                {
                    cData.check_per = Math.Round(Convert.ToDouble(CheckNo1) / Convert.ToDouble(total_cnt) * 100, 2);
                }
/*
                if (CheckNo2 == 0 || total_cnt == 0)
                {
                    cData.none_check_per = 0;
                }
                else
                {
                    cData.none_check_per = Math.Round(Convert.ToDouble(CheckNo2) / Convert.ToDouble(total_cnt) * 100, 2);
                }
                */
                confirms.Add(cData);
                
            }

            /*

            ConfirmFileDataVO tConfirmFileData2 = confirms.Where(o => o.title == "평균").FirstOrDefault();
            tConfirmFileData2.total_file = total_cnt;

            tConfirmFileData2.check_cnt = Convert.ToInt32(confirms.Where(o=>o.winningIdx != 0).Average(o => o.check_cnt));
            tConfirmFileData2.none_check_cnt = Convert.ToInt32(confirms.Where(o => o.winningIdx != 0).Average(o => o.none_check_cnt));
            tConfirmFileData2.check_per = Math.Round(confirms.Where(o => o.winningIdx != 0).Average(o => o.check_per), 2);



            ConfirmFileDataVO tConfirmFileData_min = confirms.Where(o => o.title == "최소").FirstOrDefault();
            tConfirmFileData_min.total_file = total_cnt;

            tConfirmFileData_min.check_cnt = Convert.ToInt32(confirms.Where(o => o.winningIdx != 0).Min(o => o.check_cnt));
            tConfirmFileData_min.none_check_cnt = Convert.ToInt32(confirms.Where(o => o.winningIdx != 0).Min(o => o.none_check_cnt));
            tConfirmFileData_min.check_per = Math.Round(confirms.Where(o => o.winningIdx != 0).Min(o => o.check_per), 2);


            ConfirmFileDataVO tConfirmFileData_max = confirms.Where(o => o.title == "최대").FirstOrDefault();
            tConfirmFileData_max.total_file = total_cnt;

            tConfirmFileData_max.check_cnt = Convert.ToInt32(confirms.Where(o => o.winningIdx != 0).Max(o => o.check_cnt));
            tConfirmFileData_max.none_check_cnt = Convert.ToInt32(confirms.Where(o => o.winningIdx != 0).Max(o => o.none_check_cnt));
            tConfirmFileData_max.check_per = Math.Round(confirms.Where(o => o.winningIdx != 0).Max(o => o.check_per), 2);
            */
            for (int i = 0; i <= 10; i++)
            {
                int item_count = confirms.Where(o => o.check_cnt == i).Count();
                System.Diagnostics.Debug.WriteLine(Convert.ToString(i) + " : " + Convert.ToString(item_count));
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
