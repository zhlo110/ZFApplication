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
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using ZFApplication.Listeners;
using ZFApplication.Model;
using ZFApplication.UI;
using ZFApplication.Uitility;

namespace ZFApplication.UserControl
{
    public partial class StatisticTableControl : DevExpress.XtraEditors.XtraUserControl
    {
        public StatisticTableControl()
        {
            InitializeComponent();
        }
        public object ShowCallback(SingleBarInterface listener)
        {
            return StatisticUtility.Statistic(listener);
        }

        public void EndCallback(object obj)
        {
            BindingList<StatisticInfo> list1 = new BindingList<StatisticInfo>();
            Hashtable table = (Hashtable)obj;
            //  IList visited = new ArrayList();
            //  IList novisit = new ArrayList();
            foreach (DictionaryEntry entry in table)
            {
                string key = (string)entry.Key;
                if (key.Equals("全国")) continue;
                StatisticInfo info = null;
                if (key == null || key.Length <= 0)
                {
                    info = (StatisticInfo)entry.Value;
                    info.Name = "未分类";
                    key = "未分类";
                }
                else
                {
                    info = (StatisticInfo)entry.Value;
                }

              //  StatisticInfo info = (StatisticInfo)entry.Value;
                SeriesPoint haspoint = new SeriesPoint(key, new object[] { ((object)(info.VisitNumber)) });
                mChartControl.Series[0].Points.Add(haspoint);
                SeriesPoint nospoint = new SeriesPoint(key, new object[] { ((object)(info.Number - info.VisitNumber)) });
                mChartControl.Series[1].Points.Add(nospoint);
            }
        }

        private void StatisticTableControl_Enter(object sender, EventArgs e)
        {
            mChartControl.Series[0].Points.Clear();
            mChartControl.Series[1].Points.Clear();
          //  SingleProgressBar progress = new SingleProgressBar();
          //  progress.setShowCallback(ShowCallback);
          //  progress.setEndCallback(EndCallback);
           // progress.ShowDialog();
           
            
            
            
            //   Hashtable table = StatisticUtility.Statistic(null);

            //   mChartControl.Series[0].Points.AddRange(visited.list);
            //   mChartControl.Series[1].Points.AddRange(novisit);
            // mGridControl.DataSource = list1;
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
