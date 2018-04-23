Imports Microsoft.VisualBasic
Imports System
Namespace ChartLineColor
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Dim lineSeriesView1 As New DevExpress.XtraCharts.LineSeriesView()
			Me.chartControl1 = New DevExpress.XtraCharts.ChartControl()
			Me.button1 = New System.Windows.Forms.Button()
			Me.textBox1 = New System.Windows.Forms.TextBox()
			CType(Me.chartControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(lineSeriesView1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' chartControl1
			' 
			Me.chartControl1.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.chartControl1.Location = New System.Drawing.Point(12, 12)
			Me.chartControl1.Name = "chartControl1"
			Me.chartControl1.SeriesSerializable = New DevExpress.XtraCharts.Series(){}
			Me.chartControl1.SeriesTemplate.View = lineSeriesView1
			Me.chartControl1.Size = New System.Drawing.Size(652, 258)
			Me.chartControl1.TabIndex = 0
'			Me.chartControl1.CustomDrawSeriesPoint += New DevExpress.XtraCharts.CustomDrawSeriesPointEventHandler(Me.chartControl1_CustomDrawSeriesPoint);
'			Me.chartControl1.BoundDataChanged += New DevExpress.XtraCharts.BoundDataChangedEventHandler(Me.chartControl1_BoundDataChanged);
			' 
			' button1
			' 
			Me.button1.Location = New System.Drawing.Point(226, 280)
			Me.button1.Name = "button1"
			Me.button1.Size = New System.Drawing.Size(75, 23)
			Me.button1.TabIndex = 1
			Me.button1.Text = "Set level"
			Me.button1.UseVisualStyleBackColor = True
'			Me.button1.Click += New System.EventHandler(Me.button1_Click);
			' 
			' textBox1
			' 
			Me.textBox1.Location = New System.Drawing.Point(351, 283)
			Me.textBox1.Name = "textBox1"
			Me.textBox1.Size = New System.Drawing.Size(100, 20)
			Me.textBox1.TabIndex = 2
			Me.textBox1.Text = "0"
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(676, 314)
			Me.Controls.Add(Me.textBox1)
			Me.Controls.Add(Me.button1)
			Me.Controls.Add(Me.chartControl1)
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(lineSeriesView1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.chartControl1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private WithEvents chartControl1 As DevExpress.XtraCharts.ChartControl
		Private WithEvents button1 As System.Windows.Forms.Button
		Private textBox1 As System.Windows.Forms.TextBox
	End Class
End Namespace

