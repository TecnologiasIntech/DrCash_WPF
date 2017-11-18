using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace DoctorCashWpf.Printer
{
    /// <summary>
    /// Lógica de interacción para Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        private reportService report = new reportService();

        public Report(int id)
        {
            InitializeComponent();
            this.id = id;
            ReportViewerDemo.Reset();
            DataTable dt = GetData();
            Microsoft.Reporting.WinForms.ReportDataSource ds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt);
            ReportViewerDemo.LocalReport.DataSources.Add(ds);
            ReportViewerDemo.LocalReport.ReportEmbeddedResource = "DoctorCashWpf.Printer.Report1.rdlc";
            ReportViewerDemo.RefreshReport();
        }
        int id;
        private DataTable GetData()
        {
            var data = new DataTable();

            data=report.getLastInformation(id.ToString());

            return data;
        }
    }
}
