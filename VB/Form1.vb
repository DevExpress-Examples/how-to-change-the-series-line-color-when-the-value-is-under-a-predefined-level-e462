Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraCharts

Namespace ChartLineColor

    Public Partial Class Form1
        Inherits Form

        Private level As Double = 0

        Private maxPointValue As Double = 0

        Private minPointValue As Double = 0

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs)
            AddHandler chartControl1.BoundDataChanged, AddressOf OnBoundDataChanged
            InitializeSeries()
            ApplyColorizer()
        End Sub

        Public Sub InitializeSeries()
            chartControl1.Series.Clear()
            Dim series As Series = New Series()
            series.BindToData(PointGenerator.Generate(), "X", "Y")
            series.ColorDataMember = "Y"
            chartControl1.Series.Add(series)
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True
            series.Label.TextPattern = "{V:N2}"
            TryCast(chartControl1.Diagram, XYDiagram).AxisX.Alignment = AxisAlignment.Zero
        End Sub

        Public Sub ApplyColorizer()
            level = Convert.ToDouble(textBox1.Text)
            Dim series As Series = chartControl1.Series(0)
            Dim view As StepLineSeriesView = New StepLineSeriesView()
            series.View = view
            Dim colorizer As RangeSegmentColorizer = New RangeSegmentColorizer()
            colorizer.RangeStops.AddRange(New Double() {minPointValue, level, maxPointValue})
            colorizer.LegendItemPattern = "{V1:0.###} - {V2:0.###}"
            view.SegmentColorizer = colorizer
        End Sub

        Public Sub OnBoundDataChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim series As Series = chartControl1.Series(0)
            maxPointValue = series.Points(0).Values(0)
            minPointValue = maxPointValue
            For i As Integer = 1 To series.Points.Count - 1
                Dim value As Double = series.Points(i).Values(0)
                If value < minPointValue Then minPointValue = value
                If value > maxPointValue Then maxPointValue = value
            Next
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs)
            InitializeSeries()
            ApplyColorizer()
        End Sub
    End Class

    Friend Class PointGenerator

        Public Shared Function Generate() As List(Of SimpleDataPoint)
            Dim rnd As Random = New Random(Date.Now.Millisecond * 2)
            Dim list As List(Of SimpleDataPoint) = New List(Of SimpleDataPoint)()
            For i As Integer = 0 To 10 - 1
                Dim x As Integer = i
                Dim y As Double =(rnd.NextDouble() - 0.5) * 2
                list.Add(New SimpleDataPoint(x, y))
            Next

            Return list
        End Function
    End Class

    Public Class SimpleDataPoint

        Private _X As Double, _Y As Double

        Public Property X As Double
            Get
                Return _X
            End Get

            Private Set(ByVal value As Double)
                _X = value
            End Set
        End Property

        Public Property Y As Double
            Get
                Return _Y
            End Get

            Private Set(ByVal value As Double)
                _Y = value
            End Set
        End Property

        Public Sub New(ByVal arg As Double, ByVal val As Double)
            X = arg
            Y = val
        End Sub
    End Class
End Namespace
