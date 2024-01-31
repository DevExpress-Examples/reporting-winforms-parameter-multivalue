using System;
using System.IO;
using System.Windows.Forms;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;

namespace Reporting_Create_Multi_Value_Report_Parameter {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CreateAndDisplayReport();
        }
        private static void CreateAndDisplayReport() {
            // Create a report instance.
            var report = new XtraReport1();
            ConfigureDataSource(ref report);

            // Create a multi-value parameter and specify its properties. 
            Parameter parameter1 = new Parameter();
            parameter1.Name = "CategoryIDs";
            parameter1.Type = typeof(System.Int32);
            parameter1.MultiValue = true;
            parameter1.Description = "Categories: ";

            // Create a DynamicListLookUpSettings instance and set up its properties.
            DynamicListLookUpSettings lookupSettings = new DynamicListLookUpSettings();
            lookupSettings.DataSource = report.DataSource;
            lookupSettings.DataMember = "Categories";
            lookupSettings.DisplayMember = "CategoryName";
            lookupSettings.ValueMember = "CategoryId";

            // Assign the settings to the parameter's LookUpSettings property.
            parameter1.LookUpSettings = lookupSettings;

            // Set the parameter's Visible and SelectAllValues properties to true to
            // make the parameter visible in the Parameters Panel and select all
            // values as defaults.
            parameter1.Visible = true;
            parameter1.SelectAllValues = true;

            // Add the parameter to the report's Parameters collection.
            report.Parameters.Add(parameter1);

            // Use the parameter to filter the report's data.
            report.FilterString = "CategoryID in (?CategoryIDs)";

            report.ShowRibbonPreviewDialog();
        }

        private static void ConfigureDataSource(ref XtraReport1 report) {
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            var databasePath = Path.Combine(projectDirectory, "nwind.db");
            var connectionParameters = new SQLiteConnectionParameters(databasePath, "");
            var dataSource = new SqlDataSource(connectionParameters);

            var ordersQuery = new CustomSqlQuery();
            ordersQuery.Name = "Categories";
            ordersQuery.Sql = "SELECT * FROM Categories";

            dataSource.Queries.Add(ordersQuery);

            report.DataSource = dataSource;
            report.DataMember = "Categories";
        }
    }
}
