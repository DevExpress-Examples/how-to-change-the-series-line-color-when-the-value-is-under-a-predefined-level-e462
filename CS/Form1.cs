using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;

namespace ChartLineColor {
    public partial class Form1 : Form {
        double level = 0;
        double maxPointValue = 0;
        double minPointValue = 0;
        public Form1() {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e) {
            chartControl1.BoundDataChanged += OnBoundDataChanged;
            InitializeSeries();
            ApplyColorizer();
        }
        public void InitializeSeries() {      
            chartControl1.Series.Clear();
           
            Series series = new Series();
            series.BindToData(PointGenerator.Generate(), "X", "Y");
            series.ColorDataMember = "Y";
            chartControl1.Series.Add(series);
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series.Label.TextPattern = "{V:N2}";            
            (chartControl1.Diagram as XYDiagram).AxisX.Alignment = AxisAlignment.Zero;
        }
        public void ApplyColorizer() {
            level = Convert.ToDouble(textBox1.Text);
            Series series = chartControl1.Series[0];
            StepLineSeriesView view = new StepLineSeriesView();
            series.View = view;
            RangeSegmentColorizer colorizer = new RangeSegmentColorizer();
            colorizer.RangeStops.AddRange(new double[] { minPointValue, level, maxPointValue });
            colorizer.LegendItemPattern = "{V1:0.###} - {V2:0.###}";
            view.SegmentColorizer = colorizer;
        }

        public void OnBoundDataChanged(object sender, EventArgs e) {
            Series series = chartControl1.Series[0];
            minPointValue = series.Points.Min(p => p.UserValues[0]);
            maxPointValue = series.Points.Max(p => p.UserValues[0]);
        }

        private void button1_Click(object sender, EventArgs e) {
            InitializeSeries();
            ApplyColorizer();
        }
    }

    class PointGenerator {
        public static List<SimpleDataPoint> Generate() {
            Random rnd = new Random(DateTime.Now.Millisecond*2);
            List<SimpleDataPoint> list = new List<SimpleDataPoint>();
            for(int i = 0; i < 10; i++) {
                int x = i;
                double y = (rnd.NextDouble() - 0.5) * 2;
                list.Add(new SimpleDataPoint(x, y));
            }
            return list;
        }
    }
    public class SimpleDataPoint {
        public double X { get; private set; }
        public double Y { get; private set; }
        public SimpleDataPoint(double arg, double val) {
            X = arg;
            Y = val;
        }
    }
}