'Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraCharts

Namespace ChartLineColor
	Partial Public Class Form1
		Inherits Form
		Private level As Double = 0

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			PopulateData()
		End Sub

		Private Sub PopulateData()
			Dim sm As New SeriesMaker(Convert.ToDouble(textBox1.Text))

			chartControl1.DataSource = sm.Data
			chartControl1.SeriesDataMember = "S"
			chartControl1.SeriesTemplate.View = New StepLineSeriesView()
			chartControl1.SeriesTemplate.PointOptions.ValueNumericOptions.Format = NumericFormat.FixedPoint
			chartControl1.SeriesTemplate.PointOptions.ValueNumericOptions.Precision = 2
			chartControl1.SeriesTemplate.ArgumentDataMember = "X"
			chartControl1.SeriesTemplate.ValueDataMembers.AddRange(New String() { "Y" })
            TryCast(chartControl1.Diagram, XYDiagram).AxisX.Alignment = AxisAlignment.Zero
		End Sub

		Private Sub chartControl1_CustomDrawSeriesPoint(ByVal sender As Object, ByVal e As CustomDrawSeriesPointEventArgs) Handles chartControl1.CustomDrawSeriesPoint
			If e.SeriesPoint.Values(0) = level Then
                TryCast(e.SeriesDrawOptions, LineDrawOptions).Marker.Visible = False
				e.LabelText = ""
			End If
		End Sub

		Private Sub chartControl1_BoundDataChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chartControl1.BoundDataChanged
			If chartControl1.Series.Count > 1 Then
				chartControl1.Series(1).View.Color = Color.Blue
			End If
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			PopulateData()
			level = Convert.ToDouble(textBox1.Text)
		End Sub
	End Class

	Friend Class SeriesMaker
		Private data_Renamed As New DataTable("MyDataTable")
		Private level As Double

		Public Sub New(ByVal level As Double)
			Me.level = level
			Init()
		End Sub

		Public Sub Init()
			Dim rnd As New Random(DateTime.Now.Millisecond*2)

			data_Renamed.Columns.Add("S", GetType(String))
			data_Renamed.Columns.Add("X", GetType(Int32))
			data_Renamed.Columns.Add("Y", GetType(Double))

			Dim spc As List(Of SeriesPoint) = New List(Of SeriesPoint)()

			For i As Integer = 0 To 9
				Dim x As Integer = i
				Dim y As Double = (rnd.NextDouble() - 0.5) * 2

				spc.Add(New SeriesPoint(x, y))
			Next i

			Dim bLastPointBelowZero As Boolean = (spc(0).Values(0) < level)

			For i As Integer = 0 To spc.Count - 1
				'data.Rows.Add(new object[] { "ActualSeries", spc[i].Argument, spc[i].Values[0] });
				'continue;

				' Work with ActualSeries
				If spc(i).Values(0) >= level Then
					If bLastPointBelowZero AndAlso i > 0 Then
						data_Renamed.Rows.Add(New Object() { "ActualSeries", spc(i).Argument, level })
					End If

					data_Renamed.Rows.Add(New Object() { "ActualSeries", spc(i).Argument, spc(i).Values(0) })
				Else
					data_Renamed.Rows.Add(New Object() { "ActualSeries", spc(i).Argument, level })
					data_Renamed.Rows.Add(New Object() { "ActualSeries", spc(i).Argument, Nothing })
				End If

				' Work with SecondarySeries
				If spc(i).Values(0) >= level Then
					data_Renamed.Rows.Add(New Object() { "SecondarySeries", spc(i).Argument, level })
					data_Renamed.Rows.Add(New Object() { "SecondarySeries", spc(i).Argument, Nothing })
				Else
					If (Not bLastPointBelowZero) AndAlso i > 0 Then
						data_Renamed.Rows.Add(New Object() { "SecondarySeries", spc(i).Argument, level })
					End If

					data_Renamed.Rows.Add(New Object() { "SecondarySeries", spc(i).Argument, spc(i).Values(0) })
				End If

				bLastPointBelowZero = (spc(i).Values(0) < level)
			Next i
		End Sub

		Public ReadOnly Property Data() As DataTable
			Get
				Return data_Renamed
			End Get
		End Property

	End Class


End Namespace