Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraCharts

Namespace ChartLineColor
	Partial Public Class Form1
		Inherits Form

		Private level As Double = 0
		Private maxPointValue As Double = 0
		Private minPointValue As Double = 0
		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			AddHandler chartControl1.BoundDataChanged, AddressOf OnBoundDataChanged
			InitializeSeries()
			ApplyColorizer()
		End Sub
		Public Sub InitializeSeries()
			chartControl1.Series.Clear()

			Dim series As New Series()
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
			Dim view As New StepLineSeriesView()
			series.View = view
			Dim colorizer As New RangeSegmentColorizer()
			colorizer.RangeStops.AddRange(New Double() { minPointValue, level, maxPointValue })
			colorizer.LegendItemPattern = "{V1:0.###} - {V2:0.###}"
			view.SegmentColorizer = colorizer
		End Sub

		Public Sub OnBoundDataChanged(ByVal sender As Object, ByVal e As EventArgs)
			Dim series As Series = chartControl1.Series(0)
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: minPointValue = maxPointValue = series.Points[0].Values[0];
			maxPointValue = series.Points(0).Values(0)
			minPointValue = maxPointValue
			For i As Integer = 1 To series.Points.Count - 1
				Dim value As Double = series.Points(i).Values(0)
				If value < minPointValue Then
					minPointValue = value
				End If
				If value > maxPointValue Then
					maxPointValue = value
				End If
			Next i
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			InitializeSeries()
			ApplyColorizer()
		End Sub
	End Class

	Friend Class PointGenerator
		Public Shared Function Generate() As List(Of SimpleDataPoint)
			Dim rnd As New Random(Date.Now.Millisecond*2)
			Dim list As New List(Of SimpleDataPoint)()
			For i As Integer = 0 To 9
				Dim x As Integer = i
				Dim y As Double = (rnd.NextDouble() - 0.5) * 2
				list.Add(New SimpleDataPoint(x, y))
			Next i
			Return list
		End Function
	End Class
	Public Class SimpleDataPoint
		Private privateX As Double
		Public Property X() As Double
			Get
				Return privateX
			End Get
			Private Set(ByVal value As Double)
				privateX = value
			End Set
		End Property
		Private privateY As Double
		Public Property Y() As Double
			Get
				Return privateY
			End Get
			Private Set(ByVal value As Double)
				privateY = value
			End Set
		End Property
		Public Sub New(ByVal arg As Double, ByVal val As Double)
			X = arg
			Y = val
		End Sub
	End Class
End Namespace