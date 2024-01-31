Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.XtraReports.Parameters
Imports DevExpress.XtraReports.UI
Namespace Reporting_Create_Multi_Value_Report_Parameter
    Friend Module Program
        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            CreateAndDisplayReport()
        End Sub
        Private Sub CreateAndDisplayReport()
            ' Create a report instance.
            Dim report = New XtraReport1()
            ConfigureDataSource(report)

            ' Create a multi-value parameter and specify its properties. 
            Dim parameter1 As New Parameter With {
                .Name = "CategoryIDs",
                .Type = GetType(System.Int32),
                .MultiValue = True,
                .Description = "Categories: "
            }

            ' Create a DynamicListLookUpSettings instance and set up its properties.
            Dim lookupSettings As New DynamicListLookUpSettings With {
                .DataSource = report.DataSource,
                .DataMember = "Categories",
                .DisplayMember = "CategoryName",
                .ValueMember = "CategoryId"
            }

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

            report.ShowRibbonPreviewDialog()
        End Sub

        Private Sub ConfigureDataSource(ByRef report As XtraReport1)
            Dim projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName
            Dim databasePath = Path.Combine(projectDirectory, "nwind.db")
            Dim connectionParameters = New SQLiteConnectionParameters(databasePath, "")
            Dim dataSource = New SqlDataSource(connectionParameters)

            Dim ordersQuery = New CustomSqlQuery()
            ordersQuery.Name = "Categories"
            ordersQuery.Sql = "SELECT * FROM Categories"

            dataSource.Queries.Add(ordersQuery)

            report.DataSource = dataSource
            report.DataMember = "Categories"
        End Sub
    End Module
End Namespace
