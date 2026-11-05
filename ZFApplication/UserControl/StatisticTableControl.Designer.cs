
namespace ZFApplication.UserControl
{
    partial class StatisticTableControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SeriesPoint seriesPoint1 = new DevExpress.XtraCharts.SeriesPoint("贵州省", new object[] {
            ((object)(20D))});
            DevExpress.XtraCharts.SeriesPoint seriesPoint2 = new DevExpress.XtraCharts.SeriesPoint("云南省", new object[] {
            ((object)(10D))});
            DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView1 = new DevExpress.XtraCharts.StackedBarSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SeriesPoint seriesPoint3 = new DevExpress.XtraCharts.SeriesPoint("贵州省", new object[] {
            ((object)(10D))});
            DevExpress.XtraCharts.SeriesPoint seriesPoint4 = new DevExpress.XtraCharts.SeriesPoint("云南省", new object[] {
            ((object)(5D))});
            DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView2 = new DevExpress.XtraCharts.StackedBarSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            this.mChartControl = new DevExpress.XtraCharts.ChartControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.GenButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.mChartControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mChartControl
            // 
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.mChartControl.Diagram = xyDiagram1;
            this.mChartControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mChartControl.Location = new System.Drawing.Point(0, 0);
            this.mChartControl.Name = "mChartControl";
            series1.Name = "已走访";
            series1.Points.AddRange(new DevExpress.XtraCharts.SeriesPoint[] {
            seriesPoint1,
            seriesPoint2});
            stackedBarSeriesView1.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(208)))), ((int)(((byte)(80)))));
            stackedBarSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(208)))), ((int)(((byte)(80)))));
            series1.View = stackedBarSeriesView1;
            series2.Name = "未走访";
            series2.Points.AddRange(new DevExpress.XtraCharts.SeriesPoint[] {
            seriesPoint3,
            seriesPoint4});
            series2.View = stackedBarSeriesView2;
            this.mChartControl.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
            this.mChartControl.SeriesSorting = DevExpress.XtraCharts.SortingMode.Descending;
            this.mChartControl.SeriesTemplate.DataSorted = true;
            this.mChartControl.SeriesTemplate.SeriesPointsSorting = DevExpress.XtraCharts.SortingMode.Descending;
            this.mChartControl.SeriesTemplate.SeriesPointsSortingKey = DevExpress.XtraCharts.SeriesPointKey.Value_1;
            this.mChartControl.Size = new System.Drawing.Size(687, 397);
            this.mChartControl.TabIndex = 0;
            chartTitle1.Text = "线索统计图";
            this.mChartControl.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.GenButton);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 397);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(687, 38);
            this.panelControl1.TabIndex = 1;
            // 
            // GenButton
            // 
            this.GenButton.Location = new System.Drawing.Point(312, 6);
            this.GenButton.Name = "GenButton";
            this.GenButton.Size = new System.Drawing.Size(75, 23);
            this.GenButton.TabIndex = 0;
            this.GenButton.Text = "生成统计图";
            this.GenButton.Click += new System.EventHandler(this.GenButton_Click);
            // 
            // StatisticTableControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mChartControl);
            this.Controls.Add(this.panelControl1);
            this.Name = "StatisticTableControl";
            this.Size = new System.Drawing.Size(687, 435);
            this.Enter += new System.EventHandler(this.StatisticTableControl_Enter);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mChartControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl mChartControl;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton GenButton;
    }
}
