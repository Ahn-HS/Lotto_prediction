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

namespace FilterCom.views
{
    /// <summary>
    /// PDivideNumber.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PDivideNumber : Window
    {
        public PDivideNumber()
        {
            InitializeComponent();
        }
        List<int> nums = new List<int>();
        public void CheckNumber(int tNumber)
        {
            bool flag = false;
            String num_str = "";
            for (int i = 2; i <= tNumber; i++)
            {
                if (tNumber % i == 0)
                {
                    flag = true;
                }

                if (flag == true)
                {
                    nums.Add(i);
                    if (num_str != "") num_str += ", ";
                    num_str += Convert.ToString(i);
                }
                flag = false;
            }

            lbl_Nums.Text = num_str;
        }
    }
}
