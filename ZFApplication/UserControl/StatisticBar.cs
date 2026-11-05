using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ZFApplication.Listeners;
using ZFApplication.Model;
using ZFApplication.UI;
using ZFApplication.Uitility;

namespace ZFApplication.UserControl
{
    public partial class StatisticBar : DevExpress.XtraEditors.XtraUserControl
    {
        public StatisticBar()
        {
            InitializeComponent();
            

        }

        private void StatisticBar_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("aa");
        }

        public object ShowCallback(SingleBarInterface listener)
        {
            return StatisticUtility.Statistic(listener);
        }

        public void EndCallback(object obj)
        {
            Hashtable table = (Hashtable)obj;
            BindingList<StatisticInfo> list1 = new BindingList<StatisticInfo>();
            foreach (DictionaryEntry entry in table)
            {
                string key = (string)entry.Key;
                if (key == null || key.Length <= 0)
                {
                    StatisticInfo info = (StatisticInfo)entry.Value;
                    info.Name = "未分类";
                    list1.Add(info);
                    // key = "未分类";
                }
                else
                {
                    StatisticInfo info = (StatisticInfo)entry.Value;
                    list1.Add(info);
                }
            }
            mGridControl.DataSource = list1;
        }
        private void StatisticBar_Enter(object sender, EventArgs e)
        {
         //   SingleProgressBar progress = new SingleProgressBar();
         //   progress.setShowCallback(ShowCallback);
          //  progress.setEndCallback(EndCallback);
           // progress.ShowDialog();
        }

        private void GenButton_Click(object sender, EventArgs e)
        {
            SingleProgressBar progress = new SingleProgressBar();
            progress.setShowCallback(ShowCallback);
            progress.setEndCallback(EndCallback);
            progress.ShowDialog();
        }
    }
}
