using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;

namespace ChartLineColor
{
    public partial class Form1 : Form
    {
        double level = 0;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateData();
        }

        private void PopulateData()
        {
            SeriesMaker sm = new SeriesMaker(Convert.ToDouble(textBox1.Text));

            chartControl1.DataSource = sm.Data;
            chartControl1.SeriesDataMember = "S";
            chartControl1.SeriesTemplate.View = new StepLineSeriesView();
            chartControl1.SeriesTemplate.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.FixedPoint;
            chartControl1.SeriesTemplate.Label.PointOptions.ValueNumericOptions.Precision = 2;
            chartControl1.SeriesTemplate.ArgumentDataMember = "X";
            chartControl1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "Y" });
            (chartControl1.Diagram as XYDiagram).AxisX.Alignment = AxisAlignment.Zero;
        }

        private void chartControl1_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            if (e.SeriesPoint.Values[0] == level)
            {
                (e.SeriesDrawOptions as LineDrawOptions).MarkerVisible = false;
                e.LabelText = "";
            }
        }

        private void chartControl1_BoundDataChanged(object sender, EventArgs e)
        {
            if (chartControl1.Series.Count > 1)
                chartControl1.Series[1].View.Color = Color.Blue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PopulateData();
            level = Convert.ToDouble(textBox1.Text);
        }
    }

    class SeriesMaker
    {
        private DataTable data = new DataTable("MyDataTable");
        private double level;

        public SeriesMaker (double level)
	    {
            this.level = level;
            Init();
	    }
              
        public void Init()
        {
            Random rnd = new Random(DateTime.Now.Millisecond*2);

            data.Columns.Add("S", typeof(string));
            data.Columns.Add("X", typeof(Int32));
            data.Columns.Add("Y", typeof(double));
    
            List<SeriesPoint> spc = new List<SeriesPoint>();

            for (int i = 0; i < 10; i++)
            {
                int x = i;
                double y = (rnd.NextDouble() - 0.5) * 2;

                spc.Add(new SeriesPoint(x, y));
            }

            bool bLastPointBelowZero = (spc[0].Values[0] < level);

            for (int i = 0; i < spc.Count; i++)
            {
                //data.Rows.Add(new object[] { "ActualSeries", spc[i].Argument, spc[i].Values[0] });
                //continue;

                // Work with ActualSeries
                if (spc[i].Values[0] >= level)
                {
                    if (bLastPointBelowZero && i > 0)
                        data.Rows.Add(new object[] { "ActualSeries", spc[i].Argument, level });

                    data.Rows.Add(new object[] { "ActualSeries", spc[i].Argument, spc[i].Values[0] });
                }
                else
                {
                    data.Rows.Add(new object[] { "ActualSeries", spc[i].Argument, level });
                    data.Rows.Add(new object[] { "ActualSeries", spc[i].Argument, null });
                }

                // Work with SecondarySeries
                if (spc[i].Values[0] >= level)
                {
                    data.Rows.Add(new object[] { "SecondarySeries", spc[i].Argument, level });
                    data.Rows.Add(new object[] { "SecondarySeries", spc[i].Argument, null });
                }
                else
                {
                    if (!bLastPointBelowZero && i > 0)
                        data.Rows.Add(new object[] { "SecondarySeries", spc[i].Argument, level });

                    data.Rows.Add(new object[] { "SecondarySeries", spc[i].Argument, spc[i].Values[0] });
                }

                bLastPointBelowZero = (spc[i].Values[0] < level);
            }
        }

        public DataTable Data
        {
            get { return data; }
        }

    }
}