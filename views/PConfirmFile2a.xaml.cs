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
    public partial class PConfirmFile2a : Window
    {
        public PConfirmFile2a()
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

            int winiCellNo = winings[0].Row_data.Split('\t').ToList().Count;

            int total_cnt = 0;
            int total_row = 0;
            int filedetail_cnt = 0;

            int is_0 = 0;
            int is_1 = 0;
            foreach (WinningNumber winningNum in winings)
            {
                total_cnt = 0;
                ConfirmFileDataVO cData = new ConfirmFileDataVO();
                cData.title = Convert.ToString(winningNum.Idx) + "회";
                cData.winningIdx = winningNum.Idx;
                cData.winningNums = winningNum.Row_data.TrimEnd();
                cData.check_cnt = 0;
                cData.none_check_cnt = 0;
                cData.file_idxs = new List<int>();
                cData.none_file_idxs = new List<int>();
                List<String> CheckNums = winningNum.Row_data.Split('\t').ToList();

                    foreach (FileDataInfo fdiItem in files.FileDataInfos) //그룹파일의 개별 파일의 한줄 단위
                    {
                        total_row++;
                        total_cnt++;
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
                        if (check_cnt >= files.NoMin && check_cnt <= files.NoMin)
                        {
                            cData.check_cnt += 1;
                            is_1++;
                        }
                        else
                        {
                            cData.none_check_cnt += 1;
                            is_0++;

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

            List<ConfirmFileData> checkDatas = new List<ConfirmFileData>();

            //int total_row_data = total_cnt * winings.Count();

            int max_check = confirms.Max(o => o.check_cnt);
            if (max_check == 0)
            {
                max_check = 1;
            }
            int filesCount = max_check;// total_cnt;
            //filesCount = max_check;//
            checkDatas.Add(new ConfirmFileData
            {
                title = "계"
                   ,
                check_cnt = -1,
                file_cnt = winings.Count(),
                file_per = 100,
                row_cnt = total_row,
                row_per = 100
            });
            for (int i = 0; i <= filesCount; i++)
            {
                ConfirmFileData nItem = new ConfirmFileData
                {
                    title = Convert.ToString(i)
                    ,
                    check_cnt = i,
                    file_cnt = 0,
                    file_per = 0
                };

                

                int item_count = confirms.Where(o => o.check_cnt == i).Count();
                int row_count = 0;// item_count * filesCount; //confirms.Where(o => o.check_cnt == i).Sum(o => o.check_cnt);
                if (i == 0)
                {
                    row_count = is_0;
                }else if (i == 1)
                {
                    row_count = is_1;
                }
                /*if (i > 0)
                {
                    row_count = item_count * i;
                }*/
                System.Diagnostics.Debug.WriteLine(i + ":" + item_count + ", " + row_count +  "," + total_row);

                nItem.check_cnt = i;
                nItem.file_cnt = item_count;
                nItem.row_cnt = row_count;
                if (nItem.file_cnt > 0)
                {
                    nItem.file_per = Math.Round(Convert.ToDouble(nItem.file_cnt) / Convert.ToDouble(winings.Count) * 100, 2);

                }
                if (nItem.row_cnt > 0)
                {
                    nItem.row_per = Math.Round(Convert.ToDouble(nItem.row_cnt) / Convert.ToDouble(total_row) * 100, 2);

                }
                checkDatas.Add(nItem);
            }
            



            //ConfirmFileData tConfirmFileData2 = checkDatas.Where(o => o.title == "계").FirstOrDefault();
            //tConfirmFileData2.file_cnt = filesCount;
            grid_check.ItemsSource = checkDatas;
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
