using System;
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

namespace DoctorCashWpf.Reportings
{
    /// <summary>
    /// Lógica de interacción para Report1.xaml
    /// </summary>
    public partial class Report1 : Window
    {
        public Report1()
        {
            InitializeComponent();

            /*
            ReportViewerDemos.Reset();            
            DataTable dt = GetDataReport();
            ReportDataSource ds = new ReportDataSource("DataSet1", dt);
            ReportViewerDemos.LocalReport.DataSources.Add(ds);
            ReportViewerDemos.LocalReport.ReportEmbeddedResource = "DoctorCashWpf.Reportings.Report1.rdlc";
            ReportViewerDemos.RefreshReport();
            */
        }

        private sqlQueryService createQuery = new sqlQueryService();
        private userService user = new userService();        
    }
}
