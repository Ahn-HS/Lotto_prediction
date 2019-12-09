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
    public partial class PFilterCheck : Window
    {
        public PFilterCheck()
        {
            InitializeComponent();
        }

        public void initData(List<FileGroupData> fGroups, List<String> CheckNums)
        {
            

            ConfirmFileData tConfirmFileData = new ConfirmFileData();
            tConfirmFileData.check_cnt = 0;
            tConfirmFileData.file_per = 0;
            tConfirmFileData.file_idxs = new List<int>();

            ConfirmFileData tConfirmFileData2 = new ConfirmFileData();
            tConfirmFileData2.check_cnt = 0;
            tConfirmFileData2.file_per = 0;
            tConfirmFileData2.file_idxs = new List<int>();

            List<FilterCheckGroupFileData> arrCheck = new List<FilterCheckGroupFileData>();


            WinningNumber wNum_Group = new WinningNumber();
            WinningNumber wNum_File = new WinningNumber();
            int target_files = 0;
            foreach (FileGroupData fgroup in fGroups)
            {
                wNum_Group.Check_cnt = 0;
                target_files++;
                int file_check_cnt = 0;
                int file_check_cnt_none = 0;
                foreach (FileData fItem in fgroup.FileDatas) //개별 파일 단위
                {
                    if (!fItem.IsChecked) continue; //체크되지 않았을 경우 스킵
                    wNum_File.Check_cnt = 0;
                    FilterCheckGroupFileData checkItem = new FilterCheckGroupFileData();
                    checkItem.group_id = fgroup.Idx;
                    checkItem.file_id = fItem.Idx;

                    int line_idx = 0;
                    int or_cnt = 0;
                    int min_cnt = 0;
                    int fileCheckCnt = 0;
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
                        /*
                        if (fdiItem.CheckCnt >= fItem.NoMin && fdiItem.CheckCnt <= fItem.NoMax)
                        {
                            fileCheckCnt++;
                        }
                        */
                        if (fdiItem.CheckCnt >= fItem.NoMin && fdiItem.CheckCnt <= fItem.NoMax)
                        {
                            wNum_File.Check_cnt++;
                            /*
                            if (tConfirmFileData.file_idxs.Where(o => o == fgroup.Idx).Count() == 0)
                            {
                                tConfirmFileData.file_idxs.Add(fgroup.Idx);

                                if (tConfirmFileData2.file_idxs.Where(o => o == fgroup.Idx).Count() > 0)
                                {
                                    tConfirmFileData2.file_idxs.Remove(fgroup.Idx);
                                }
                            }
                            */
                        }
                        else
                        {
                            /*
                            if (tConfirmFileData.file_idxs.Where(o => o == fgroup.Idx).Count() == 0)
                            {
                                if (tConfirmFileData2.file_idxs.Where(o => o == fgroup.Idx).Count() == 0)
                                {
                                    tConfirmFileData2.file_idxs.Add(fgroup.Idx);
                                }
                            }
                            */
                        }

                    }
                    if (fItem.OrStart <= wNum_File.Check_cnt && fItem.OrEnd >= wNum_File.Check_cnt)  // OR  처리
                    {
                        wNum_Group.Check_cnt++;
                    }
                    //checkItem.cnt_check = fileCheckCnt;

                }//개별 파일 단위  foreach (FileData fItem in fDatas) //개별 파일 단위

                if (fgroup.F_ORStart <= wNum_Group.Check_cnt && fgroup.F_OREnd >= wNum_Group.Check_cnt)  // OR  처리
                {
                    tConfirmFileData.file_idxs.Add(fgroup.Idx);
                }
                else
                {
                    tConfirmFileData2.file_idxs.Add(fgroup.Idx);
                }

            }
            


            if (target_files == 0) {
                isView2.Visibility = Visibility.Visible;
                isView1.Visibility = Visibility.Collapsed;
                return;
            }

            int CheckNo1 = tConfirmFileData.file_idxs.Count();
            int CheckNo2 = tConfirmFileData2.file_idxs.Count();

            String file1_datas = String.Join(",", tConfirmFileData.file_idxs.ToArray());
            String file2_datas = String.Join(",", tConfirmFileData2.file_idxs.ToArray());

            Double per_checkNo1 = 0;
            Double per_checkNo2 = 0;

            if (CheckNo1 == 0 || target_files == 0)
            {
                per_checkNo1 = 0;
            }
            else
            {
                per_checkNo1 = Math.Round(Convert.ToDouble(CheckNo1) / Convert.ToDouble(target_files) * 100, 2);
            }

            if (CheckNo2 == 0 || target_files == 0)
            {
                per_checkNo2 = 0;
            }
            else
            {
                per_checkNo2 = Math.Round(Convert.ToDouble(CheckNo2) / Convert.ToDouble(target_files) * 100, 2);
            }

            this.lbl_group_cnt.Content = Convert.ToString(target_files);
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
