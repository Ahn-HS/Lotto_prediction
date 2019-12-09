using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using System.IO;
using Microsoft.Win32;

using FilterCom.Models;
using FilterCom.views;
using FilterCom.vms;

namespace FilterCom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<List<int>> prevRandomData = new List<List<int>>();
        List<int> randomData =null;
        List<int> randomData2;
        int currentFileGroupIdx = 1;
        RadRadioButton currentBtn = null;
        Boolean CheckCmdIsAllCheck = false; // 숫자 체크박스 전체 수정 시 다른 이벤트 적용되지 않게 처리
        List<FileGroupData> FileGroupDatas = new List<FileGroupData>();
        List<FileGroupData> FileGroupItems = new List<FileGroupData>();

        List<WinningNumber> WinningNumbers = new List<WinningNumber>(); // 당첨번호 목록 탭구분자

        List<RadRadioButton> GroupButton = new List<RadRadioButton>();

        Boolean is_file_list_check = false;
        Boolean is_group_random_check = false;
        Boolean is_group_filter_check = false;

        private MainVM vm;
        public MainWindow()
        {
            InitializeComponent();
            for(int i=0;i<20;i++)
                prevRandomData.Add(randomData);

        }

        private void onLoade(object sender, RoutedEventArgs e)
        {
            vm = new MainVM();
            currentBtn = this.btn_filegroup1;

            GroupButton.Add(this.btn_filegroup1);
            GroupButton.Add(this.btn_filegroup2);
            GroupButton.Add(this.btn_filegroup3);
            GroupButton.Add(this.btn_filegroup4);
            GroupButton.Add(this.btn_filegroup5);
            GroupButton.Add(this.btn_filegroup6);
            GroupButton.Add(this.btn_filegroup7);
            GroupButton.Add(this.btn_filegroup8);
            GroupButton.Add(this.btn_filegroup9);
            GroupButton.Add(this.btn_filegroup10);
            GroupButton.Add(this.btn_filegroup11);
            GroupButton.Add(this.btn_filegroup12);
            GroupButton.Add(this.btn_filegroup13);
            GroupButton.Add(this.btn_filegroup14);
            GroupButton.Add(this.btn_filegroup15);
            GroupButton.Add(this.btn_filegroup16);
            GroupButton.Add(this.btn_filegroup17);
            GroupButton.Add(this.btn_filegroup18);
            GroupButton.Add(this.btn_filegroup19);
            GroupButton.Add(this.btn_filegroup20);



            this.DataContext = vm;
            for (int i = 1; i <= 20; i++)
            {
                FileGroupDatas.Add(new FileGroupData
                {
                    Idx = i,
                    Files = null,
                    FileDatas = new List<FileData>()

                });
            }

            NumberSetting();
            //string baseDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            String fileData = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + @"data\NumData.txt");
            System.Diagnostics.Debug.WriteLine(fileData);
            String[] rows = fileData.Split('\n');
            int idx = 0;
            foreach (String data in rows)
            {
                idx++;
                WinningNumber tWinningNumber = new WinningNumber();
                tWinningNumber.Idx = idx;
                tWinningNumber.Row_data = data;
                String[] values = data.Split('\t');
                tWinningNumber.Row_datas = values;
                if (values.Length > 0) tWinningNumber.No_1 = values[0];
                if (values.Length > 1) tWinningNumber.No_2 = values[1];
                if (values.Length > 2) tWinningNumber.No_3 = values[2];
                if (values.Length > 3) tWinningNumber.No_4 = values[3];
                if (values.Length > 4) tWinningNumber.No_5 = values[4];
                if (values.Length > 5) tWinningNumber.No_6 = values[5];
                if (values.Length > 6) tWinningNumber.No_7 = values[6];
                if (values.Length > 7) tWinningNumber.No_8 = values[7];
                if (values.Length > 8) tWinningNumber.No_9 = values[8];
                if (values.Length > 9) tWinningNumber.No_10 = values[9];
                if (values.Length > 10) tWinningNumber.No_11 = values[10];
                if (values.Length > 11) tWinningNumber.No_12 = values[11];
                if (values.Length > 12) tWinningNumber.No_13 = values[12];
                if (values.Length > 13) tWinningNumber.No_14 = values[13];
                tWinningNumber.No_item = values.Length;
                WinningNumbers.Add(tWinningNumber);
            }
            

            lblWinning_Cnt.Text = String.Format("{0:#,###}", WinningNumbers.Count);
        }

        FileGroupData currFileGroupData = null;

        //기준 파일 그룹 로딩
        private void Btn_fileDataLoad(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                string[] files = openFileDialog.FileNames;

                currFileGroupData = FileGroupDatas.Where(o => o.Idx == currentFileGroupIdx).First();
                currFileGroupData.IsGroupCheck = true;
                currFileGroupData.IsData = true;
                currFileGroupData.FileGroupName = "파일그룹" + Convert.ToString(currentFileGroupIdx);
                currFileGroupData.Files = files;
                currFileGroupData.FileDatas.Clear();
                currFileGroupData.R_IsCheck = false;
                currFileGroupData.F_IsCheck = false;
                currFileGroupData.R_AutoCheck = false;
                currFileGroupData.R_BalanceCheck = false;
                currFileGroupData.R_CreateCnt = 1;
                if (files.Length > 0)
                {
                    currFileGroupData.F_ORStart = 0;
                    currFileGroupData.F_OREnd = 0;

                    currFileGroupData.R_NumStart = 1;
                    currFileGroupData.R_NumEnd = files.Length;
                    currFileGroupData.R_NumMax = files.Length;

                }
                else
                {
                    currFileGroupData.F_ORStart = 0;
                    currFileGroupData.F_OREnd = 0;
                    currFileGroupData.R_NumStart = 0;
                    currFileGroupData.R_NumEnd = 0;
                    currFileGroupData.R_NumMax = 0;
                }
                currFileGroupData.R_CreateCnt = 1;
                //currFileGroupData.R_NumMax = files.Length;

                int cnt_filgrouprows = 0;
                int FileDataRow = 0;
                foreach (String fileItem in files)
                {
                    FileData tFileData = new FileData();
                    FileDataRow++;

                    String fileData = File.ReadAllText(fileItem);
                    tFileData.FilePath = fileItem;
                    tFileData.FileName = fileItem.Split('\\').Last();
                    tFileData.FileDataInfos = new List<FileDataInfo>();

                    String[] rows = fileData.Split('\n');
                    tFileData.Idx = FileDataRow;
                    tFileData.RowCount = rows.Count() - 1;
                    tFileData.NoMin = 0;
                    tFileData.NoMax = 0;
                    tFileData.OrEnd = rows.Count() - 1;
                    tFileData.OrStart = 1;
                    int rowIdx = 0;
                    foreach (String data in rows)
                    {
                        if (data.Trim() == "") continue;
                        rowIdx++;
                        FileDataInfo tfileDataInfo = new FileDataInfo();
                        String[] values = data.Split(' ');
                        tfileDataInfo.No_item = values.Count();
                        tfileDataInfo.FileName = fileItem;
                        tfileDataInfo.RowData = data;
                        tfileDataInfo.RowIdx = rowIdx;
                        tfileDataInfo.Cells = new List<CellData>();
                        for (int didx = 1; didx <= values.Count(); didx++)
                        {
                            if (values[didx - 1].Trim() == "") continue;
                            String val = values[didx - 1].Trim();
                            CellData tCellData = new CellData();
                            tCellData.IsCheck_cell = false;
                            tCellData.Cell_no = val;
                            tCellData.Cell_idx = didx;
                            tfileDataInfo.Cells.Add(tCellData);
                            cnt_filgrouprows++;

                        }
                        tFileData.FileDataInfos.Add(tfileDataInfo);

                    }

                    currFileGroupData.FileDatas.Add(tFileData);

                }
                currFileGroupData.Rowcount = cnt_filgrouprows;
                //grid_filegroup.ItemsSource = currFileGroupData.FileDatas;
                vm.FileDatas = null;
                vm.FileDatas = currFileGroupData.FileDatas;
                vm.FileGroupItems = FileGroupDatas.Where(o => o.IsData == true).ToList();
                /*this.GridFilter.SelectedItem = currFileGroupData;
                this.GridRandom.SelectedItem = currFileGroupData;*/
            }
            int GroupCount = FileGroupDatas.Where(o => o.IsData == true).Count();

            this.vm.FileGroupCheck_Cnt = GroupCount;
            this.TotalOREnd.Maximum = GroupCount;
            this.TotalORStart.Maximum = GroupCount;
            this.TotalOREnd.Value = 0;
            if (GroupCount > 0)
            {
                this.TotalORStart.Value = 0;

                Color myRgbColor = new Color();
                myRgbColor = Color.FromRgb(198, 224, 180);

                currentBtn.Background = new SolidColorBrush(myRgbColor);
            }
            else
            {
                this.TotalORStart.Value = 0;
            }
            
        }
        
        //파일 그룹 체크
        private void RBTN_FileGroup_Click(object sender, RoutedEventArgs e)
        {
            currentBtn = (RadRadioButton)sender;
            currentFileGroupIdx = Convert.ToInt16(currentBtn.Tag);

            if (currFileGroupData != null)
            {
                currFileGroupData.IsGroupCheck = false;
            }

            currFileGroupData = FileGroupDatas.Where(o => o.Idx == currentFileGroupIdx).First();
            currFileGroupData.IsGroupCheck = true;
            this.vm.FileDatas = currFileGroupData.FileDatas;
            /*
            if (currFileGroupData.IsData)
            {
                this.GridFilter.SelectedItem = currFileGroupData;
                this.GridRandom.SelectedItem = currFileGroupData;
            }
            else
            {
                this.GridFilter.SelectedItem = null;
                this.GridRandom.SelectedItem = null;
            }
            */
            


        }

        //원본 파일 목록
        private String File_Original = "";
        private List<CheckResultItem> CheckResultItems = new List<CheckResultItem>();
        private void Btn_OriginalDataLoad(object sender, RoutedEventArgs e)
        {
            BusyIndicateView.IsBusy = true;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                string file = openFileDialog.FileName;
                File_Original = openFileDialog.FileName;


                try
                {
                    String fileData = File.ReadAllText(File_Original);
                    String[] rows = fileData.Split('\n');

                    OriginalDatas.Clear();
                    int oIdx = 0;
                    foreach (String data in rows)
                    {
                        if (data.Trim() == "") continue;
                        oIdx++;
                        String[] values = data.Split(' ');

                        OriginalData item = new OriginalData(oIdx,data, 0);
                        item.CellCtn = values.Length;
                        item.IsChecked = false;
                        item.Cells = new List<CellData>();
                        for (int didx = 1; didx <= values.Count(); didx++)
                        {
                            if (values[didx - 1].Trim() == "") continue;

                            //System.Diagnostics.Debug.WriteLine("TEST111");
                            String val = values[didx - 1].Trim();
                            CellData cellItem = new CellData();

                            cellItem.Cell_idx = didx;
                            cellItem.Cell_no = val;
                            cellItem.IsCheck_cell = false;
                            item.Cells.Add(cellItem);
                        }

                        OriginalDatas.Add(item);


                    }
                    OriginalDataSet();
                }
                catch (Exception ex)
                {
                    BusyIndicateView.IsBusy = false;
                    MessageBox.Show(ex.Message);
                }

            }
            BusyIndicateView.IsBusy = false;
            this.vm.LastDelPer_Cnt = 0;
            this.vm.TotalDelPer_Cnt = 0;
        }
        //원본 파일과 당첨 번호 체크
        List<OriginalData> OriginalDatas = new List<OriginalData>();
        private void OriginalDataSet()
        {
            try
            {
                //List<OriginalData> OriginalDatas_Target = OriginalDatas.Where(o => o.IsDelete == false).ToList(); //미 삭제 대상 설정
                // System.Diagnostics.Debug.WriteLine("TEST1");
                foreach (OriginalData item in OriginalDatas)
                {

                    item.CntCheck = 0;
                    // System.Diagnostics.Debug.WriteLine("OriginalDatas:" + item.RowData);
                    foreach (CellData cell in item.Cells)
                    {

                        // System.Diagnostics.Debug.WriteLine("OriginalDatas cell :" + cell.Cell_no + "::");
                        cell.IsCheck_cell = false;

                        if (NumList.Where(o => o == cell.Cell_no).Count() > 0)
                        {
                            item.CntCheck++;
                            cell.IsCheck_cell = true;
                            //   System.Diagnostics.Debug.WriteLine("OriginalDatas cell :" + cell.Cell_no + "::ISCheck");
                        }
                    }

                    // System.Diagnostics.Debug.WriteLine("OriginalDatas CellCtn :" + Convert.ToString(item.CellCtn) + ":" + Convert.ToString(item.CntCheck));
                    if (item.CellCtn == item.CntCheck) //전체 당첨인것
                    {
                        item.IsChecked = true;
                    }


                }
                this.vm.Original_RowCount = OriginalDatas.Count();
                this.vm.Original_RowExtCount = OriginalDatas.Where(o => o.IsDelete == false).Count();
                this.lblCtnOriginal.Text = Convert.ToString(this.vm.Original_RowCount);
                this.lblCtnOriginalChecked.Text = Convert.ToString(this.vm.Original_RowExtCount);
                CheckResultItems.Clear();
                if (OriginalDatas.Count > 0)
                {
                    //listBoxResult.item
                    int tCellCtn = OriginalDatas[0].CellCtn;
                    //  System.Diagnostics.Debug.WriteLine("OriginalDatas Check :" + Convert.ToString(tCellCtn));
                    for (int i = 0; i <= tCellCtn; i++)
                    {
                        //  System.Diagnostics.Debug.WriteLine("CheckResultItems Check :" + Convert.ToString((i + 1) + ":" + Convert.ToString(OriginalDatas.Where(o => o.CntCheck == (i + 1)).Count()) ) );
                        CheckResultItems.Add(new CheckResultItem
                        {
                            no_order = i,
                            cnt_check = i,
                            cnt_count = OriginalDatas.Where(o => o.CntCheck == i && o.IsDelete == false).Count()
                        });


                    }
                }
                CheckResultItems = CheckResultItems.OrderByDescending(o => o.no_order).ToList();
                listBoxResult.ItemsSource = null;
                listBoxResult.ItemsSource = CheckResultItems;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void NumValueChangeEvent(object sender, RadRangeBaseValueChangedEventArgs e)
        {
            if (!CheckCmdIsAllCheck) NumberSetting();
        }

        private void NumCheckEvent(object sender, RoutedEventArgs e)
        {
            if (!CheckCmdIsAllCheck) NumberSetting();
        }
        List<String> NumList = new List<string>();
        private void NumberSetting()
        {
            if (this.Num_1 == null) return;
            if (this.Num_2 == null) return;
            if (this.Num_3 == null) return;
            if (this.Num_4 == null) return;
            if (this.Num_5 == null) return;
            if (this.Num_6 == null) return;
            if (this.Num_7 == null) return;
            if (this.Num_8 == null) return;
            if (this.Num_9 == null) return;
            if (this.Num_10 == null) return;
            if (this.Num_11 == null) return;
            if (this.Num_12 == null) return;
            if (this.Num_13 == null) return;
            if (this.Num_14 == null) return;
            NumList.Clear();
            String numberString = "";
            NumberSettingAdd(this.Chk_1.IsChecked, this.Num_1.Value);
            NumberSettingAdd(this.Chk_2.IsChecked, this.Num_2.Value);
            NumberSettingAdd(this.Chk_3.IsChecked, this.Num_3.Value);
            NumberSettingAdd(this.Chk_4.IsChecked, this.Num_4.Value);
            NumberSettingAdd(this.Chk_5.IsChecked, this.Num_5.Value);
            NumberSettingAdd(this.Chk_6.IsChecked, this.Num_6.Value);
            NumberSettingAdd(this.Chk_7.IsChecked, this.Num_7.Value);
            NumberSettingAdd(this.Chk_8.IsChecked, this.Num_8.Value);
            NumberSettingAdd(this.Chk_9.IsChecked, this.Num_9.Value);
            NumberSettingAdd(this.Chk_10.IsChecked, this.Num_10.Value);
            NumberSettingAdd(this.Chk_11.IsChecked, this.Num_11.Value);
            NumberSettingAdd(this.Chk_12.IsChecked, this.Num_12.Value);
            NumberSettingAdd(this.Chk_13.IsChecked, this.Num_13.Value);
            NumberSettingAdd(this.Chk_14.IsChecked, this.Num_14.Value);
            /*
            if (this.Chk_1.IsChecked == true)
            {
                //만일 다른 위치 같은 숫자가 동시에 들어가면 안될경우
                if (!NumList.Contains(String10CheckDouble(this.Num_1.Value)))
                {
                    NumList.Add(String10CheckDouble(this.Num_1.Value));
                }

                NumList.Add(String10CheckDouble(this.Num_1.Value));
            }
            if (this.Chk_2.IsChecked == true) NumList.Add(String10CheckDouble(this.Num_2.Value));
            if (this.Chk_3.IsChecked == true) NumList.Add(String10CheckDouble(this.Num_3.Value));
            if (this.Chk_4.IsChecked == true) NumList.Add(String10CheckDouble(this.Num_4.Value));
            if (this.Chk_5.IsChecked == true) NumList.Add(String10CheckDouble(this.Num_5.Value));
            if (this.Chk_6.IsChecked == true) NumList.Add(String10CheckDouble(this.Num_6.Value));
            if (this.Chk_7.IsChecked == true) NumList.Add(String10CheckDouble(this.Num_7.Value));
            if (this.Chk_8.IsChecked == true) NumList.Add(String10CheckDouble(this.Num_8.Value));
            if (this.Chk_9.IsChecked == true) NumList.Add(String10CheckDouble(this.Num_9.Value));
            if (this.Chk_10.IsChecked == true) NumList.Add(String10CheckDouble(this.Num_10.Value));
            if (this.Chk_11.IsChecked == true) NumList.Add(String10CheckDouble(this.Num_11.Value));
            if (this.Chk_12.IsChecked == true) NumList.Add(String10CheckDouble(this.Num_12.Value));
            if (this.Chk_13.IsChecked == true) NumList.Add(String10CheckDouble(this.Num_13.Value));
            if (this.Chk_14.IsChecked == true) NumList.Add(String10CheckDouble(this.Num_14.Value));
            */
            numberString = String.Join(",", NumList.ToArray());
            System.Diagnostics.Debug.WriteLine(numberString);
            OriginalDataSet();//주파일과 당첨번호 설정


        }
        private void NumberSettingAdd(bool? isChecked, Double? val)
        {
            if (isChecked == true)
            {
                String valStr = String10CheckDouble(val);
                //만일 다른 위치 같은 숫자가 동시에 들어가면 안될경우
                /*
                if (!NumList.Contains(valStr))
                {
                    NumList.Add(valStr);
                }*/
                //기본적으로
                NumList.Add(valStr);
            }
            else
            {
                NumList.Add("00");
            }
        }
        private String String10CheckDouble(Double? val)
        {
            String rtnStr = "";
            if (val == null) rtnStr = "00";
            if (val < 10)
            {
                rtnStr = "0" + Convert.ToString(val);
            }
            else
            {
                rtnStr = Convert.ToString(val);
            }
            return rtnStr;
        }

        private void radMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (((RadMenuItem)e.Source).Tag != null)
            {
                String tag = (String)((RadMenuItem)e.Source).Tag;
                if (tag == "2_1") //계약리스트
                {
                    //this.viewBox.Content = vContractList1;

                }
            }
        }

        private PDivideNumber _pDivideNumber;

        //나눠지는 숫자 찾기
        private void BtnDivideNumber(object sender, RoutedEventArgs e)
        {
            //Convert.ToInt32(NumDivideTarget.Value)
            if (Convert.ToInt32(NumDivideTarget.Value) < 2)
            {
                MessageBox.Show("나눠지는 숫자의 기준은 2보다 커야 합니다.");
                return;
            }

            if (_pDivideNumber == null)
            {
                _pDivideNumber = new PDivideNumber();
            }
            else
            {
                _pDivideNumber.Close();
                _pDivideNumber = null;
                _pDivideNumber = new PDivideNumber();
            }

            _pDivideNumber.CheckNumber(Convert.ToInt32(NumDivideTarget.Value));
            _pDivideNumber.Show();
        }

        //전체체크
        private void chk_all_check_event(object sender, RoutedEventArgs e)
        {
            checkbox_num_all_check(true);
        }
        //전체체크해제
        private void chk_all_uncheck_event(object sender, RoutedEventArgs e)
        {
            checkbox_num_all_check(false);
        }
        private void checkbox_num_all_check(Boolean isChecked)
        {
            CheckCmdIsAllCheck = true;
            Chk_1.IsChecked = isChecked;
            Chk_2.IsChecked = isChecked;
            Chk_3.IsChecked = isChecked;
            Chk_4.IsChecked = isChecked;
            Chk_5.IsChecked = isChecked;
            Chk_6.IsChecked = isChecked;
            Chk_7.IsChecked = isChecked;
            Chk_8.IsChecked = isChecked;
            Chk_9.IsChecked = isChecked;
            Chk_10.IsChecked = isChecked;
            Chk_11.IsChecked = isChecked;
            Chk_12.IsChecked = isChecked;
            Chk_13.IsChecked = isChecked;
            Chk_14.IsChecked = isChecked;
            NumberSetting();
            CheckCmdIsAllCheck = false;
        }
        
        private void Btn_Filegroup_checked(object sender, RoutedEventArgs e)
        {
            if (vm.FileDatas == null) return;
            if (vm.FileDatas.Count == 0) return;

            RadButton btn_check = (RadButton)sender;
            if (is_file_list_check)
            {
                is_file_list_check = false;
            }
            else
            {
                is_file_list_check = true;
            }
            foreach (FileData item in vm.FileDatas)
            {
                item.IsChecked = is_file_list_check;
            }
            file_list_check_count();
        }

        private void Btn_filegroup_all_min(object sender, RoutedEventArgs e)
        {
            if (vm.FileDatas == null) return;
            if (vm.FileDatas.Count == 0) return;

            int minData = vm.FileDatas[0].NoMin;
            foreach (FileData item in vm.FileDatas)
            {
                item.NoMin = minData;
            }
        }

        private void grid_filegroup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("grid_filegroup_MouseUp");
        }

        private void Btn_Filter_checked(object sender, RoutedEventArgs e)
        {
            if (vm.FileGroupItems == null) return;
            if (vm.FileGroupItems.Count == 0) return;

            RadButton btn_check = (RadButton)sender;
            if (this.is_group_filter_check)
            {
                is_group_filter_check = false;
            }
            else
            {
                is_group_filter_check = true;
            }

            foreach (FileGroupData item in vm.FileGroupItems)
            {
                item.F_IsCheck = is_group_filter_check;
            }

            //this.vm.FileGroupCheck_Cnt = this.vm.FileGroupItems.Where(o => o.F_IsCheck == true).Count();
        }
        private void Btn_filter_or_end(object sender, RoutedEventArgs e)
        {
            if (vm.FileDatas == null) return;
            if (vm.FileDatas.Count == 0) return;

            int minData = vm.FileGroupItems[0].F_OREnd;
            foreach (FileGroupData item in vm.FileGroupItems)
            {
                item.F_OREnd = minData;
            }
        }

        private void Btn_filter_or_start(object sender, RoutedEventArgs e)
        {
            if (vm.FileDatas == null) return;
            if (vm.FileDatas.Count == 0) return;

            int minData = vm.FileGroupItems[0].F_ORStart;
            foreach (FileGroupData item in vm.FileGroupItems)
            {
                item.F_ORStart = minData;
            }
        }

        private void Btn_random_no_end(object sender, RoutedEventArgs e)
        {
            if (vm.FileDatas == null) return;
            if (vm.FileDatas.Count == 0) return;

            int minData = vm.FileGroupItems[0].R_NumEnd;
            foreach (FileGroupData item in vm.FileGroupItems)
            {
                item.R_NumEnd = minData;
            }
        }

        private void Btn_random_no_start(object sender, RoutedEventArgs e)
        {
            if (vm.FileDatas == null) return;
            if (vm.FileDatas.Count == 0) return;

            int minData = vm.FileGroupItems[0].R_NumStart;
            foreach (FileGroupData item in vm.FileGroupItems)
            {
                item.R_NumStart = minData;
            }
        }

        private void Btn_random_checked(object sender, RoutedEventArgs e)
        {
            if (vm.FileGroupItems == null) return;
            if (vm.FileGroupItems.Count == 0) return;

            RadButton btn_check = (RadButton)sender;
            if (this.is_group_random_check)
            {
                is_group_random_check = false;
            }
            else
            {
                is_group_random_check = true;
            }
            foreach (FileGroupData item in vm.FileGroupItems)
            {
                item.R_IsCheck = is_group_random_check;
            }
        }

        private void Btn_filegroup_or_end(object sender, RoutedEventArgs e)
        {
            if (vm.FileDatas == null) return;
            if (vm.FileDatas.Count == 0) return;

            int minData = vm.FileDatas[0].OrEnd;
            foreach (FileData item in vm.FileDatas)
            {
                item.OrEnd = minData;
            }
        }

        private void Btn_filegroup_or_start(object sender, RoutedEventArgs e)
        {
            if (vm.FileDatas == null) return;
            if (vm.FileDatas.Count == 0) return;

            int minData = vm.FileDatas[0].OrStart;
            foreach (FileData item in vm.FileDatas)
            {
                item.OrStart = minData;
            }
        }

        private void Btn_filegroup_all_max(object sender, RoutedEventArgs e)
        {
            if (vm.FileDatas == null) return;
            if (vm.FileDatas.Count == 0) return;

            int minData = vm.FileDatas[0].NoMax;
            foreach (FileData item in vm.FileDatas)
            {
                item.NoMax = minData;
            }
        }
        int num = 1;
        private void BtnRandomItemCreate(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(((FileGroupData)GridRandom.SelectedItem).FileGroupName);
            FileGroupData selFileGroupData = (FileGroupData)((RadButton)sender).Tag;
            ///System.Diagnostics.Debug.WriteLine(((RadButton)sender).Tag);

            if (selFileGroupData.R_IsCheck == false)
            {
                MessageBox.Show("체크하지 않은 항목 입니다.");
                return;
            }

            GridRandom.SelectedItem = selFileGroupData;

            //selFileGroupData.R_NumStart
            //selFileGroupData.R_NumEnd

            //selFileGroupData.R_AutoCheck
            //selFileGroupData.R_BalanceCheck

            //selFileGroupData.R_CreateCnt
            randomData = new List<int>();
            Random rand = new Random();

            int checkData = (selFileGroupData.R_NumEnd + 1) - selFileGroupData.R_NumStart;
            if (checkData < selFileGroupData.R_CreateCnt)
            {
                selFileGroupData.R_CreateCnt = checkData;
            }
            String randDatas = "";


            System.Diagnostics.Debug.WriteLine("selFileGroupData.R_BalanceCheck" + Convert.ToString(selFileGroupData.R_BalanceCheck));
            System.Diagnostics.Debug.WriteLine("selFileGroupData.R_CreateCnt" + Convert.ToString(selFileGroupData.R_CreateCnt));

            if (selFileGroupData.R_BalanceCheck == true && selFileGroupData.R_CreateCnt > 1) //균형
            {
                int balanceCnt_Data = checkData % selFileGroupData.R_CreateCnt;
                if (balanceCnt_Data > 0)
                {
                    MessageBox.Show("균형랜덤 처리 할 수 없는 생성 숫자 입니다.");
                    return;
                }
                int balanceCnt = checkData / selFileGroupData.R_CreateCnt;
                int balStart = selFileGroupData.R_NumStart;
                int balEnd = (selFileGroupData.R_NumStart + balanceCnt) - 1;
                for (int i = 0; i < selFileGroupData.R_CreateCnt; i++)
                {

                    System.Diagnostics.Debug.WriteLine("balStart" + Convert.ToString(balStart));
                    System.Diagnostics.Debug.WriteLine("balEnd" + Convert.ToString(balEnd));

                    int ranVal = rand.Next(balStart, (balEnd + 1));
                    System.Diagnostics.Debug.WriteLine("ranVal" + Convert.ToString(ranVal));

                    randomData.Add(ranVal);

                    if (randDatas != "") { randDatas += ","; }
                    randDatas += Convert.ToString(ranVal);

                    balStart = balEnd + 1;
                    balEnd = (balStart + balanceCnt) - 1;

                }

            }
            else
            {
                while (true)
                {
                    int ranVal = rand.Next(selFileGroupData.R_NumStart, (selFileGroupData.R_NumEnd + 1));
                    System.Diagnostics.Debug.WriteLine("ranVal" + Convert.ToString(ranVal));
                    if (randomData.Where(o => o == ranVal).Count() == 0)
                    {
                        randomData.Add(ranVal);

                        if (randDatas != "") { randDatas += ","; }
                        randDatas += Convert.ToString(ranVal);
                    }

                    if (randomData.Count >= selFileGroupData.R_CreateCnt)
                    {
                        break;
                    }

                }
            }

            if (selFileGroupData.R_AutoCheck)//자동 체크
            {
                foreach (FileData tFileData in selFileGroupData.FileDatas)
                {
                    tFileData.IsChecked = false;
                }

                foreach (int idx in randomData)
                {
                    selFileGroupData.FileDatas[idx - 1].IsChecked = true;
                }

            }
            else
            {
                MessageBox.Show(randDatas);
            }


        }

        private void WiningTimeChangeEvent(object sender, RadRangeBaseValueChangedEventArgs e)
        {
            CheckCmdIsAllCheck = true;

            if (this.Num_1 == null) return;
            if (this.Num_2 == null) return;
            if (this.Num_3 == null) return;
            if (this.Num_4 == null) return;
            if (this.Num_5 == null) return;
            if (this.Num_6 == null) return;
            if (this.Num_7 == null) return;
            if (this.Num_8 == null) return;
            if (this.Num_9 == null) return;
            if (this.Num_10 == null) return;
            if (this.Num_11 == null) return;
            if (this.Num_12 == null) return;
            if (this.Num_13 == null) return;
            if (this.Num_14 == null) return;
            if (this.NudWiningTime.Value > 0)
            {
                if (this.WinningNumbers.Count >= this.NudWiningTime.Value)
                {
                    CheckCmdIsAllCheck = true;

                    Num_1.Value = 0;
                    Num_2.Value = 0;
                    Num_3.Value = 0;
                    Num_4.Value = 0;
                    Num_5.Value = 0;
                    Num_6.Value = 0;
                    Num_7.Value = 0;
                    Num_8.Value = 0;
                    Num_9.Value = 0;
                    Num_10.Value = 0;
                    Num_11.Value = 0;
                    Num_12.Value = 0;
                    Num_13.Value = 0;
                    Num_14.Value = 0;


                    Chk_1.IsChecked = false;
                    Chk_2.IsChecked = false;
                    Chk_3.IsChecked = false;
                    Chk_4.IsChecked = false;
                    Chk_5.IsChecked = false;
                    Chk_6.IsChecked = false;
                    Chk_7.IsChecked = false;
                    Chk_8.IsChecked = false;
                    Chk_9.IsChecked = false;
                    Chk_10.IsChecked = false;
                    Chk_11.IsChecked = false;
                    Chk_12.IsChecked = false;
                    Chk_13.IsChecked = false;
                    Chk_14.IsChecked = false;


                    WinningNumber tWinningNumber = this.WinningNumbers[Convert.ToInt32(NudWiningTime.Value) - 1];
                    if (tWinningNumber.No_item >= 1)
                    {
                        Num_1.Value = Convert.ToDouble(tWinningNumber.No_1);
                        Chk_1.IsChecked = true;
                    }
                    if (tWinningNumber.No_item >= 2)
                    {
                        Num_2.Value = Convert.ToDouble(tWinningNumber.No_2);
                        Chk_2.IsChecked = true;
                    }
                    if (tWinningNumber.No_item >= 3)
                    {
                        Num_3.Value = Convert.ToDouble(tWinningNumber.No_3);
                        Chk_3.IsChecked = true;
                    }
                    if (tWinningNumber.No_item >= 4)
                    {
                        Num_4.Value = Convert.ToDouble(tWinningNumber.No_4);
                        Chk_4.IsChecked = true;
                    }
                    if (tWinningNumber.No_item >= 5)
                    {
                        Num_5.Value = Convert.ToDouble(tWinningNumber.No_5);
                        Chk_5.IsChecked = true;
                    }
                    if (tWinningNumber.No_item >= 6)
                    {
                        Num_6.Value = Convert.ToDouble(tWinningNumber.No_6);
                        Chk_6.IsChecked = true;
                    }
                    if (tWinningNumber.No_item >= 7)
                    {
                        Num_7.Value = Convert.ToDouble(tWinningNumber.No_7);
                        Chk_7.IsChecked = true;
                    }
                    if (tWinningNumber.No_item >= 8)
                    {
                        Num_8.Value = Convert.ToDouble(tWinningNumber.No_8);
                        Chk_8.IsChecked = true;
                    }
                    if (tWinningNumber.No_item >= 9)
                    {
                        Num_9.Value = Convert.ToDouble(tWinningNumber.No_9);
                        Chk_9.IsChecked = true;
                    }
                    if (tWinningNumber.No_item >= 10)
                    {
                        Num_10.Value = Convert.ToDouble(tWinningNumber.No_10);
                        Chk_10.IsChecked = true;
                    }
                    if (tWinningNumber.No_item >= 11)
                    {
                        Num_11.Value = Convert.ToDouble(tWinningNumber.No_11);
                        Chk_11.IsChecked = true;
                    }
                    if (tWinningNumber.No_item >= 12)
                    {
                        Num_12.Value = Convert.ToDouble(tWinningNumber.No_12);
                        Chk_12.IsChecked = true;
                    }
                    if (tWinningNumber.No_item >= 13)
                    {
                        Num_13.Value = Convert.ToDouble(tWinningNumber.No_13);
                        Chk_13.IsChecked = true;
                    }
                    if (tWinningNumber.No_item >= 14)
                    {
                        Num_14.Value = Convert.ToDouble(tWinningNumber.No_14);
                        Chk_14.IsChecked = true;
                    }
                    NumberSetting();
                    CheckCmdIsAllCheck = false;
                }
                else
                {
                    MessageBox.Show("당첨 횟수 범위를 벗어났습니다.");
                }
            }


        }


        //필터 적용
        private void BtnFilterCMD_Click(object sender, RoutedEventArgs e)
        {
            int FType = 1;
            //필터 적용
            if (this.RBtn_FTeyp1.IsChecked == true)
            {
                FType = 1; //남기기
            }
            else
            {
                if (this.RBtn_FTeyp2.IsChecked == true)
                {
                    FType = 2; //제거
                }
            }

            //파일별

            List<FileGroupData> fGroups = FileGroupDatas.Where(o => o.IsData == true && o.F_IsCheck == true).ToList();
            List<OriginalData> targetOriDatas_fileCheck = OriginalDatas.Where(o => o.IsDelete == false).ToList(); //현재 삭제 대상이 아닌것 //전체
            //FilterCheckDatas.Clear();

            if (fGroups == null || fGroups.Count() == 0)
            {
                MessageBox.Show("필터 체크된 파일그룹이 없습니다.");
                return;
            }

            int tTotalORStart = Convert.ToInt32(this.TotalORStart.Value); //전체OR
            int tTotalOREnd = Convert.ToInt32(this.TotalOREnd.Value); //전체OR

            int target_files = 0;

            int or_idx_filegroup = 1;
            int total_or_start = 0;
            int total_or_end = 0;

            if (this.TotalORStart.Value != null)
            {
                total_or_start = Convert.ToInt16(this.TotalORStart.Value);
            }
            if (this.TotalOREnd.Value != null)
            {
                total_or_end = Convert.ToInt16(this.TotalOREnd.Value);
            }

            List<OriginalData> rOriginalData = new List<OriginalData>();
            foreach (OriginalData oData in targetOriDatas_fileCheck)
            {
                rOriginalData.Add(new OriginalData(oData.RowIdx, oData.RowData, 0));
            }
            //--------------------------------------------------------------------------------------------------------------------
            foreach (FileGroupData fgItem in fGroups) //파일 그룹 단위
            {
                List<FileData> fDatas = fgItem.FileDatas.Where(o => o.IsChecked == true).ToList();
                fgItem.FilterResult = new List<OriginalData>(); //개별 파일 결과값
                foreach (OriginalData oData in targetOriDatas_fileCheck)
                {
                    fgItem.FilterResult.Add(new OriginalData(oData.RowIdx, oData.RowData, 0));
                }
                //--------------------------------------------------------------------------------------------------------------------
                foreach (FileData fItem in fDatas) //개별 파일 단위
                {
                    if (!fItem.IsChecked)
                        continue; //체크되지 않았을 경우 스킵
                    fItem.FilterResult = new List<OriginalData>(); //개별 파일 결과값
                    fItem.FilterResultOR = new List<OriginalData>(); //개별 파일 결과값
                    List<OriginalData> FilterCheckDatas = new List<OriginalData>(); // 마지막 필터 대상
                    List<OriginalData> targetOriDatas_fileitem = new List<OriginalData>();//파일 아이템

                    target_files++;
                    //--------------------------------------------------------------------------------------------------------------------
                    foreach (FileDataInfo fdiItem in fItem.FileDataInfos) //그룹파일의 개별 파일의 한줄 단위
                    {
                        //--------------------------------------------------------------------------------------------------------------------
                        foreach (OriginalData oData in targetOriDatas_fileCheck)
                        {
                            fdiItem.CheckCnt = 0;
                            //--------------------------------------------------------------------------------------------------------------------
                            foreach (CellData cItem in oData.Cells)
                            { 
                                int duplCnt = fdiItem.Cells.Where(o => o.Cell_no == cItem.Cell_no).Count();
                                if (duplCnt > 0)
                                {
                                    fdiItem.CheckCnt++;
                                }
                            }
  
                            if (fdiItem.CheckCnt >= fItem.NoMin && fdiItem.CheckCnt <= fItem.NoMax) //Min - Max 찾기
                            {
                                if (fItem.FilterResult.Where(o => o.RowIdx == oData.RowIdx).Count() == 0)
                                {
                                   fItem.FilterResult.Add(new OriginalData(oData.RowIdx, oData.RowData,1));
                                }
                                else
                                {
                                    OriginalData ChkDataItem = fItem.FilterResult.Where(o => o.RowIdx == oData.RowIdx).FirstOrDefault();
                                    ChkDataItem.CheckCnt = ChkDataItem.CheckCnt + 1;
                                }
                            }
                            else
                            {
                                if (fItem.FilterResult.Where(o => o.RowIdx == oData.RowIdx).Count() == 0)
                                {
                                    fItem.FilterResult.Add(new OriginalData(oData.RowIdx, oData.RowData, 0));
                                }
                            }
                        }
                    }

                    fItem.FilterResult = fItem.FilterResult.Where(o => o.CheckCnt >= fItem.OrStart && o.CheckCnt <= fItem.OrEnd).ToList();
                    foreach (OriginalData oData in fItem.FilterResult)
                    {
                        OriginalData ChkDataItem = fgItem.FilterResult.Where(o => o.RowIdx == oData.RowIdx).FirstOrDefault();
                        ChkDataItem.CheckCnt = ChkDataItem.CheckCnt + 1;
                    }
                }//개별 파일 단위  foreach (FileData fItem in fDatas) //개별 파일 단위

                fgItem.FilterResult = fgItem.FilterResult.Where(o => o.CheckCnt >= fgItem.F_ORStart && o.CheckCnt <= fgItem.F_OREnd).ToList();
                foreach (OriginalData oData in fgItem.FilterResult)
                {
                    OriginalData ChkDataItem = rOriginalData.Where(o => o.RowIdx == oData.RowIdx).FirstOrDefault();
                    ChkDataItem.CheckCnt = ChkDataItem.CheckCnt + 1;
                }

            }
        
            List<OriginalData> ResultOriginalData = rOriginalData.Where(o => o.CheckCnt >= total_or_start && o.CheckCnt <= total_or_end).ToList();

            ResultOriginalData = ResultOriginalData.OrderBy(o => o.RowIdx).ToList();

            if (target_files == 0)
            {
                MessageBox.Show("체크된 대상 파일이 하나도 없습니다. 0");
                return;
            }

            if (FType == 1)//남기기
            {
                List<OriginalData> FilterResultORs =
                    (
                        from f in targetOriDatas_fileCheck
                        where !ResultOriginalData.Any(j => j.RowIdx == f.RowIdx)
                        select f
                    ).ToList();


                if (FilterResultORs.Count() == targetOriDatas_fileCheck.Count())
                {
                    MessageBox.Show("남는 데이터가 하나도 없습니다. 1"); return;
                }
                this.vm.LastDel_Cnt = FilterResultORs.Count();
                foreach (OriginalData oData in FilterResultORs)
                {
                    OriginalData oDataCheck = OriginalDatas.Where(o => o.RowIdx == oData.RowIdx).First();
                    oDataCheck.IsDelete = true;
                }
            }
            else //제거
            {
                if (ResultOriginalData.Count() == targetOriDatas_fileCheck.Count())
                {
                    MessageBox.Show("남는 데이터가 하나도 없습니다. 2"); return;
                }
                
                foreach (OriginalData oData in ResultOriginalData)
                {
                    OriginalData oDataCheck = OriginalDatas.Where(o => o.RowIdx == oData.RowIdx).First();
                    oDataCheck.IsDelete = true;
                }

                this.vm.LastDel_Cnt = ResultOriginalData.Count();
            }
            OriginalDataSet();
            ++this.vm.Filter_Cnt;
          
            if (vm.Original_RowCount > 0)
            {
                vm.LastDelPer_Cnt = Math.Round(Convert.ToDouble(vm.LastDel_Cnt) / Convert.ToDouble(vm.Original_RowCount) * 100, 2);
                vm.TotalDelPer_Cnt = Math.Round(Convert.ToDouble(vm.TotalDel_Cnt) / Convert.ToDouble(vm.Original_RowCount) * 100, 2);
                vm.ExtPer_Cnt = Math.Round(Convert.ToDouble(vm.Original_RowExtCount) / Convert.ToDouble(vm.Original_RowCount) * 100, 2);

            }
            MessageBox.Show(" 필터 완료 : " + Convert.ToString(this.vm.LastDel_Cnt) + "건 제거");
        }
        

        private void BtnOriginalFileReLoad(object sender, RoutedEventArgs e)
        {
            if (OriginalDatas == null || OriginalDatas.Count == 0)
            {
                MessageBox.Show("원본데이터가 없습니다."); return;
            }
            foreach (OriginalData oData in OriginalDatas)
            {
                oData.IsDelete = false;
            }
            this.vm.Filter_Cnt = 0;
            this.vm.LastDel_Cnt = 0;
            this.vm.TotalDel_Cnt = 0;
            this.vm.LastDelPer_Cnt = 0;
            this.vm.TotalDelPer_Cnt = 0;

            OriginalDataSet();
            MessageBox.Show("원본데이터 다시 불러오기 완료");
        }

        private void Btn_SaveFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "TXT files|*.txt";
            saveFileDialog1.Title = "Save an Text File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {

                string new_filename = saveFileDialog1.FileName.Substring(0, saveFileDialog1.FileName.Length - 4);
                List<OriginalData> saveList = OriginalDatas.Where(o => o.IsDelete == false).ToList();
                new_filename += "_" + Convert.ToString(saveList.Count) + ".txt";
                StreamWriter outputFile = new StreamWriter(new_filename);

                foreach (OriginalData item in saveList)
                {
                    outputFile.WriteLine(item.RowData.TrimEnd());
                }
                outputFile.Close();
            }



        }
        //적중회차 찾기
        private void BtnWininingNumberSearch(object sender, RoutedEventArgs e)
        {
            //WinningNumbers.
            if (WinningNumbers == null) return;
            if (WinningNumbers.Count == 0) return;

            int cnt_winning = WinningNumbers[0].No_item;

            String num_string = String.Join(",", NumList.Take(cnt_winning).ToArray());
            //numberString = String.Join(",", NumList.ToArray());
            System.Diagnostics.Debug.WriteLine(num_string);
            foreach (WinningNumber wno in WinningNumbers)
            {
                if (wno.Row_data.TrimEnd().Replace('\t', ',').Equals(num_string))
                {
                    NudWiningTime.Value = wno.Idx;
                    MessageBox.Show("적중회차 ["+ wno.Idx.ToString() + "] 확인 처리 하였습니다.");
                    return;
                }
            }
            MessageBox.Show("적중회차가 없습니다.");
        }

        private void BtnFileCheck_Current(object sender, RoutedEventArgs e)
        {
            if (vm.FileDatas == null || vm.FileDatas.Where(o => o.IsChecked == true).Count() == 0)
            {
                MessageBox.Show("검증할 파일그룹표 1개 이상 체크하세요.");
                return;
            }

            List<FileData> checkData = vm.FileDatas.Where(o => o.IsChecked == true).ToList();

            List<String> CheckNums = NumList.Where(o => o != "00").ToList();

            if (checkData[0].FileDataInfos == null && checkData[0].FileDataInfos.Count() == 0)
            {
                MessageBox.Show("검증할 파일이 없습니다.");
                return;
            }
            if (checkData[0].FileDataInfos[0].Cells == null && checkData[0].FileDataInfos[0].Cells.Count() == 0)
            {
                MessageBox.Show("검증할 파일이 없습니다.");
                return;
            }

            if (WinningNumbers == null || WinningNumbers.Count == 0)
            {
                MessageBox.Show("검증할 당첨번호가 없습니다.");
                return;
            }

            //int CheckFileCount = checkData.Count();

            PConfirmFile3 popup = new PConfirmFile3();
            popup.initData(CheckNums, checkData);
            popup.ShowDialog();
        }

        private void BtnFileCheck_All(object sender, RoutedEventArgs e)
        {
            if (vm.FileDatas == null || vm.FileDatas.Where(o => o.IsChecked == true).Count() == 0)
            {
                MessageBox.Show("검증할 파일그룹표 1개 이상 체크하세요.");
                return;
            }

            List<FileData> checkData = vm.FileDatas.Where(o => o.IsChecked == true).ToList();
            
            List<String> CheckNums = NumList.Where(o => o != "00").ToList();

            if (checkData[0].FileDataInfos == null && checkData[0].FileDataInfos.Count() == 0)
            {
                MessageBox.Show("검증할 파일이 없습니다.");
                return;
            }
            if (checkData[0].FileDataInfos[0].Cells == null && checkData[0].FileDataInfos[0].Cells.Count() == 0)
            {
                MessageBox.Show("검증할 파일이 없습니다.");
                return;
            }

            if (WinningNumbers == null || WinningNumbers.Count == 0)
            {
                MessageBox.Show("검증할 당첨번호가 없습니다.");
                return;
            }

            PConfirmFile4 popup = new PConfirmFile4();
            popup.initData(checkData, WinningNumbers);
            popup.ShowDialog();
        }

        private void BtnFileGroupCheckList_All(object sender, RoutedEventArgs e)
        {
            if (vm.FileDatas == null || vm.FileDatas.Where(o => o.IsChecked == true).Count() == 0)
            {
                MessageBox.Show("검증할 파일그룹표 1개 이상 체크하세요.");
                return;
            }

            List<FileData> checkData = vm.FileDatas.Where(o => o.IsChecked == true).ToList();

            List<String> CheckNums = NumList.Where(o => o != "00").ToList();

            if (checkData[0].FileDataInfos == null && checkData[0].FileDataInfos.Count() == 0)
            {
                MessageBox.Show("검증할 파일이 없습니다.");
                return;
            }
            if (checkData[0].FileDataInfos[0].Cells == null && checkData[0].FileDataInfos[0].Cells.Count() == 0)
            {
                MessageBox.Show("검증할 파일이 없습니다.");
                return;
            }

            if (WinningNumbers == null || WinningNumbers.Count == 0)
            {
                MessageBox.Show("검증할 당첨번호가 없습니다.");
                return;
            }

            PConfirmFile4a popup = new PConfirmFile4a();
            popup.initData(checkData, WinningNumbers);
            popup.ShowDialog();
        }

        private void OnCheckedGrid(object sender, RoutedEventArgs e)
        {
          //  System.Diagnostics.Debug.WriteLine("OnCheckedGrid");
            //file_list_check_count();
        }

        private void OnUncheckedGrid(object sender, RoutedEventArgs e)
        {
           // System.Diagnostics.Debug.WriteLine("OnUncheckedGrid");
           // file_list_check_count();
        }
        private void file_list_check_count()
        {
            if (vm.FileDatas != null)
            {
                vm.FileCheck_Cnt = vm.FileDatas.Where(o => o.IsChecked == true).Count();
            }
            
        }

        private void grid_filegroup_CellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
        {
           // System.Diagnostics.Debug.WriteLine("grid_filegroup_CellEditEnded");
            file_list_check_count();
            /*if (e.Cell.Name.Equals("CheckColumn")){
               
            }*/
            
        }

        private void AllUnCheck_Click(object sender, RoutedEventArgs e)
        {

            List<FileGroupData> fGroups = FileGroupDatas.Where(o => o.IsData == true).ToList();
            foreach (FileGroupData fgItem in fGroups) //파일 그룹 단위
            {
                List<FileData> fDatas = fgItem.FileDatas.Where(o => o.IsChecked == true).ToList();
                foreach (FileData fItem in fDatas) //개별 파일 단위
                {
                    fItem.IsChecked = false;
                }

            }

            file_list_check_count();
        }
        //그룹표 검증표 현재회차
        private void BtnFileCheckGroup_Current(object sender, RoutedEventArgs e)
        {
            
            if (vm.FileDatas == null || vm.FileDatas.Where(o => o.IsChecked == true).Count() == 0)
            {
                MessageBox.Show("검증할 그룹표를 1개 체크하세요.");
                return;
            }
            if (vm.FileDatas == null || vm.FileDatas.Where(o => o.IsChecked == true).Count() > 1)
            {
                MessageBox.Show("검증할 그룹표를 1개만 체크하세요.");
                return;
            }


            FileData checkData = vm.FileDatas.Where(o => o.IsChecked == true).First();
            List<String> CheckNums = new List<String>();

            //WinningNumber tWinningNumber = WinningNumbers[Convert.ToInt32(NudWiningTime.Value) - 1];
            if ((int)NudWiningTime.Value != 0)
            {
                CheckNums = WinningNumbers[(int)NudWiningTime.Value - 1].Row_data.Split('\t').ToList();
            }
            else
                return;

            if (checkData.FileDataInfos == null && checkData.FileDataInfos.Count() == 0)
            {
                MessageBox.Show("검증할 파일의 검증데이터가 비정상적입니다. 확입 하여주세요.");
                return;
            }
            if (checkData.FileDataInfos[0].Cells == null && checkData.FileDataInfos[0].Cells.Count() == 0)
            {
                MessageBox.Show("검증할 파일의 검증데이터가 비정상적입니다. 확입 하여주세요.");
                return;
            }

            List<ConfirmFileData> confirmDatas = new List<ConfirmFileData>();
            int CheckCount = checkData.FileDataInfos[0].Cells.Count();

            ConfirmFileData tConfirmFileData = new ConfirmFileData();
            tConfirmFileData.check_cnt = CheckCount;
            tConfirmFileData.file_idxs = new List<int>();
            confirmDatas.Add(tConfirmFileData);

            ConfirmFileData tConfirmFileData2 = new ConfirmFileData();
            tConfirmFileData2.check_cnt = 0;
            tConfirmFileData2.file_idxs = new List<int>();
            confirmDatas.Add(tConfirmFileData2);
            int fileRow_idx = 0;
            foreach (FileDataInfo fdiItem in checkData.FileDataInfos) //그룹파일의 개별 파일의 한줄 단위
            {
                fileRow_idx++;
                fdiItem.CheckCnt = 0;

                foreach (String cItem in CheckNums)
                {
                    int duplCnt = fdiItem.Cells.Where(o => o.Cell_no == cItem).Count();
                    if (duplCnt > 0)
                    {
                        fdiItem.CheckCnt++;
                    }
                }

                if (fdiItem.CheckCnt >= checkData.NoMin && fdiItem.CheckCnt <= checkData.NoMax)
                {
                    tConfirmFileData.file_idxs.Add(fileRow_idx);
                }
                else
                {
                    tConfirmFileData2.file_idxs.Add(fileRow_idx);
                }
            }
            /*

            String Message = "체크된 그룹표 개수 " + Convert.ToString(CheckFileCount) + "\n\n\n";
            Message += "부분 ORO에 적중한 그룹표 개수 : " + Convert.ToString(CheckNo1) + "(" + Convert.ToString(per_checkNo1) + "%)" + "\n\n\n";
            Message += "-파일그룹표 : " + Convert.ToString(CheckNo1) + "(" + Convert.ToString(per_checkNo1) + "%)" + "\n\n\n";
            Message += "부분 ORO에 미적중한 그룹표 개수 : " + Convert.ToString(CheckNo2) + "(" + Convert.ToString(per_checkNo2) + "%)" + "\n\n\n";
            */
            int CheckFileCount = checkData.FileDataInfos.Count();
            PConfirmFile1 popup = new PConfirmFile1();
            popup.initData(CheckFileCount, CheckCount, confirmDatas);
            popup.ShowDialog();

            /*
            List<FileGroupData> fGroups = FileGroupDatas.Where(o => o.IsData == true).ToList();

            if (fGroups == null || fGroups.Count == 0)
            {
                MessageBox.Show("검증할 파일이 없습니다.");
                return;

            }

            List<String> CheckNums = NumList.Where(o => o != "00").ToList();
            if (CheckNums == null || CheckNums.Count == 0)
            {
                MessageBox.Show("검증할 데이터가 없습니다.");
                return;
            }
            PConfirmFile3 popup = new PConfirmFile3();
            popup.initData(CheckNums, fGroups);
            popup.ShowDialog();
            */
        }

        private void BtnFileCheckList_All(object sender, RoutedEventArgs e)
        {
            if (vm.FileDatas == null || vm.FileDatas.Where(o => o.IsChecked == true).Count() == 0)
            {
                MessageBox.Show("검증할 파일그룹표 1개 이상 체크하세요.");
                return;
            }

            FileData checkData = vm.FileDatas.Where(o => o.IsChecked == true).First();

            if (this.WinningNumbers == null)
            {
                MessageBox.Show("검증할 당첨번호가 없습니다.");
                return;
            }
            if (this.WinningNumbers.Count() == 0)
            {
                MessageBox.Show("검증할 당첨번호가 없습니다.");
                return;
            }

            if (checkData.FileDataInfos == null && checkData.FileDataInfos.Count() == 0)
            {
                MessageBox.Show("검증할 파일이 없습니다.");
                return;
            }
            if (checkData.FileDataInfos[0].Cells == null && checkData.FileDataInfos[0].Cells.Count() == 0)
            {
                MessageBox.Show("검증할 파일이 없습니다.");
                return;
            }

            PConfirmFile2a popup = new PConfirmFile2a();
            popup.initData(checkData, WinningNumbers);
            popup.ShowDialog();
        }

        private void BtnFileCheckGroup_All(object sender, RoutedEventArgs e)
        {

            if (vm.FileDatas == null || vm.FileDatas.Where(o => o.IsChecked == true).Count() == 0)
            {
                MessageBox.Show("검증할 파일그룹표 1개 이상 체크하세요.");
                return;
            }

            FileData checkData = vm.FileDatas.Where(o => o.IsChecked == true).First();
            List<OriginalData> targetOriDatas_fileCheck = OriginalDatas.Where(o => o.IsDelete == false).ToList();

            if (WinningNumbers == null || WinningNumbers.Count == 0)
            {
                MessageBox.Show("검증할 원본파일이 존재하지 않습니다.");
                return;
            }

            PConfirmFile2 popup = new PConfirmFile2();
            popup.initData(checkData, WinningNumbers);
            popup.ShowDialog();
        }

        private void BtnFilterCheck_Click(object sender, RoutedEventArgs e)
        {
            List<FileGroupData> fGroups = FileGroupDatas.Where(o => o.F_IsCheck == true).ToList();

            if (fGroups == null || fGroups.Count == 0)
            {
                MessageBox.Show("검증할 파일이 없습니다.");
                return;

            }

           
            List<String> CheckNums = NumList.Where(o => o != "00").ToList();

            if (CheckNums == null || CheckNums.Count == 0)
            {
                MessageBox.Show("검증할 당첨번호가 없습니다.");
                return;
            }

            PFilterCheck popup = new PFilterCheck();
            popup.initData(fGroups, CheckNums);
            popup.ShowDialog();
        }


        int i = 0;
        private void BtnAllRandomNumber_Click(object sender, RoutedEventArgs e)
        {
            i = 0;
            foreach (FileGroupData selFileGroupData in FileGroupDatas)
            {
                if (selFileGroupData.R_IsCheck == false) continue;

                GridRandom.SelectedItem = selFileGroupData;

                randomData = new List<int>();
                Random rand = new Random(num++);

                int checkData = (selFileGroupData.R_NumEnd + 1) - selFileGroupData.R_NumStart;

                if (checkData < selFileGroupData.R_CreateCnt)
                {
                    selFileGroupData.R_CreateCnt = checkData;
                }

                if (selFileGroupData.R_BalanceCheck == true && selFileGroupData.R_CreateCnt > 1) //균형
                {
                    int balanceCnt_Data = checkData % selFileGroupData.R_CreateCnt;

                    if (balanceCnt_Data > 0)
                    {
                        continue;
                    }

                    int balanceCnt = checkData / selFileGroupData.R_CreateCnt;
                    int balStart = selFileGroupData.R_NumStart;
                    int balEnd = (selFileGroupData.R_NumStart + balanceCnt) - 1;

                    for (int i = 0; i < selFileGroupData.R_CreateCnt; i++)
                    {
                        int ranVal = rand.Next(balStart, (balEnd + 1));
                        randomData.Add(ranVal);

                        balStart = balEnd + 1;
                        balEnd = (balStart + balanceCnt) - 1;
                    }
                }
                else
                {
                    while (true)
                    {
                        int ranVal = rand.Next(selFileGroupData.R_NumStart, (selFileGroupData.R_NumEnd + 1));
                        System.Diagnostics.Debug.WriteLine("ranVal" + Convert.ToString(ranVal));
                        if (randomData.Where(o => o == ranVal).Count() == 0)
                        {
                            randomData.Add(ranVal);
                        }

                        if (randomData.Count >= selFileGroupData.R_CreateCnt)
                        {
                            break;
                        }

                    }
                }

                if (selFileGroupData.R_AutoCheck)//자동 체크
                {
                    if(prevRandomData[i] != null) { 
                        foreach (int idx in prevRandomData[i])
                        {
                            selFileGroupData.FileDatas[idx - 1].IsChecked = false;
                        }
                    }

                    foreach (int idx in randomData)
                    {
                        selFileGroupData.FileDatas[idx - 1].IsChecked = true;
                    }
                    prevRandomData[i]=randomData;
                }
                i++;
            }
        }

        private void BtnGrid1Click(object sender, RoutedEventArgs e)
        {
            FileData selFileGroupData = (FileData)((RadButton)sender).Tag;
            if (selFileGroupData.IsChecked)
            {
                selFileGroupData.IsChecked = false;
            }
            else
            {
                selFileGroupData.IsChecked = true;
            }
            if (this.vm.FileDatas != null) {
                this.vm.FileCheck_Cnt = this.vm.FileDatas.Where(o=>o.IsChecked == true).Count();
            }
            

        }

        private void BtnGrid2Click(object sender, RoutedEventArgs e)
        {
            FileGroupData selFileGroupData = (FileGroupData)((RadButton)sender).Tag;
            if (selFileGroupData.R_IsCheck)
            {
                selFileGroupData.R_IsCheck = false;
                selFileGroupData.R_AutoCheck = false;
                selFileGroupData.R_BalanceCheck = false;
            }
            else
            {
                selFileGroupData.R_IsCheck = true;
                selFileGroupData.R_AutoCheck = true;
            }
            
        }

        private void BtnGrid3Click(object sender, RoutedEventArgs e)
        {
            FileGroupData selFileGroupData = (FileGroupData)((RadButton)sender).Tag;
            if (selFileGroupData.F_IsCheck)
            {
                selFileGroupData.F_IsCheck = false;
            }
            else
            {
                selFileGroupData.F_IsCheck = true;
            }

           // this.vm.FileGroupCheck_Cnt = this.vm.FileGroupItems.Where(o => o.F_IsCheck == true).Count();
        }

        private void BtnGrid2Click_AutoCheck(object sender, RoutedEventArgs e)
        {
            FileGroupData selFileGroupData = (FileGroupData)((RadButton)sender).Tag;
            if (selFileGroupData.R_AutoCheck)
            {
                selFileGroupData.R_AutoCheck = false;
            }
            else
            {
                selFileGroupData.R_AutoCheck = true;
            }
        }

        private void BtnGrid2Click_BalanceCheck(object sender, RoutedEventArgs e)
        {
            FileGroupData selFileGroupData = (FileGroupData)((RadButton)sender).Tag;
            if (selFileGroupData.R_BalanceCheck)
            {
                selFileGroupData.R_BalanceCheck = false;
            }
            else
            {
                selFileGroupData.R_BalanceCheck = true;
            }
        }

        private void GridRandom_SelectChange(object sender, SelectionChangeEventArgs e)
        {
            if (currFileGroupData != null) currFileGroupData.IsGroupCheck = false;

             FileGroupData selFileGroupData = (e.AddedItems.First() as FileGroupData);


            currentFileGroupIdx = Convert.ToInt16(currentBtn.Tag);
            currFileGroupData = selFileGroupData;
            currFileGroupData.IsGroupCheck = true;
            this.vm.FileDatas = currFileGroupData.FileDatas;
            currentFileGroupIdx = selFileGroupData.Idx;
            currentBtn = GroupButton[currentFileGroupIdx - 1];
            currentBtn.IsChecked = true;
        }

        private void GridFilter_SelectChange(object sender, SelectionChangeEventArgs e)
        {
            if (currFileGroupData != null) currFileGroupData.IsGroupCheck = false;
            FileGroupData selFileGroupData = (e.AddedItems.First() as FileGroupData);


            currentFileGroupIdx = Convert.ToInt16(currentBtn.Tag);
            currFileGroupData = selFileGroupData;
            currFileGroupData.IsGroupCheck = true;
            this.vm.FileDatas = currFileGroupData.FileDatas;
            currentFileGroupIdx = selFileGroupData.Idx;
            currentBtn = GroupButton[currentFileGroupIdx - 1];
            currentBtn.IsChecked = true;
        }
        private SolidColorBrush Color_Black = new SolidColorBrush(Colors.Black);
        private SolidColorBrush Color_Red = new SolidColorBrush(Colors.Red);
        private void CheckFilterTypeChange(object sender, RoutedEventArgs e)
        {
            if (this.RBtn_FTeyp1 != null)
            {
                if (this.RBtn_FTeyp1.IsChecked == true) this.RBtn_FTeyp1.Foreground = Color_Red;
                else this.RBtn_FTeyp1.Foreground = Color_Black;
            }
            

            if (this.RBtn_FTeyp2 != null)
            {
                if (this.RBtn_FTeyp2.IsChecked == true) this.RBtn_FTeyp2.Foreground = Color_Red;
                else this.RBtn_FTeyp2.Foreground = Color_Black;
            }
            
        }

    }
}
