Imports System
Imports System.Windows.Forms
Imports DevExpress.XtraReports.Parameters
Imports DevExpress.XtraReports.UI

Namespace Reporting_Create_Multi_Value_Report_Parameter
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			' Create a report instance.
			Dim report = New XtraReport1()

			' Create a multi-value parameter and specify its properties. 
			Dim parameter1 As New Parameter()
			parameter1.Name = "CategoryIDs"
			parameter1.Type = GetType(System.Int32)
			parameter1.MultiValue = True
			parameter1.Description = "Categories: "

			' Create a DynamicListLookUpSettings instance and set up its properties.
			Dim lookupSettings As New DynamicListLookUpSettings()
			lookupSettings.DataSource = report.DataSource
			lookupSettings.DataMember = "Categories"
			lookupSettings.DisplayMember = "CategoryName"
			lookupSettings.ValueMember = "CategoryId"

			' Assign the settings to the parameter's LookUpSettings property.
			parameter1.LookUpSettings = lookupSettings

			' Set the parameter's Visible and SelectAllValues properties to true to
			' make the parameter visible in the Parameters Panel and select all
			' values as defaults.
			parameter1.Visible = True
			parameter1.SelectAllValues = True

			' Add the parameter to the report's Parameters collection.
			report.Parameters.Add(parameter1)

			' Use the parameter to filter the report's data.
			report.FilterString = "CategoryID in (?CategoryIDs)"
			report.ShowPreview()
		End Sub
	End Class
End Namespace
