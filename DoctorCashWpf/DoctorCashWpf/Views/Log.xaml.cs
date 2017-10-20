using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace DoctorCashWpf.Views
{
    /// <summary>
    /// Interaction logic for Log.xaml
    /// </summary>
    public partial class Log : UserControl
    {
        public Log()
        {
            InitializeComponent();
        }
        private reportService reportservices = new reportService();
        MoneyFormatService moneyService = new MoneyFormatService();
        logService serviceslog = new logService();
        private dateService dateservice = new dateService();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dataGridViewLog.ItemsSource = null;            
            txtbox_question.Clear();
            fromdate.Text = "";
            todate.Text = "";
            labelerror.Content = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            dataGridViewLog.ItemsSource = null;
            if (todate.Text == "" && fromdate.Text == "" && txtbox_question.Text == "")
            {
                labelerror.Content = "Complete the Date or ID fields";
            }
            else
            {
                var items = new log();
                items.log_Username = userInformation.user.usr_Username;
                items.log_DateTime = dateservice.getCurrentDate();
                items.log_Actions = "Search Information by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + " in Log, Search Data: Processed by= " + txtbox_question.Text + ", Dates: From= " + fromdate.Text + ", To= " + todate.Text;
                serviceslog.CreateLog(items);                

                var list = serviceslog.getLogs(txtbox_question.Text, fromdate.Text, todate.Text).list;

                if (list.Count() == 0)
                {
                    labelerror.Content = "Data not Found";
                }
                else
                {
                    DataTable datatable = new DataTable();
                    datatable.Columns.Add("Date");
                    datatable.Columns.Add("Processed by");
                    datatable.Columns.Add("Action");

                    for (int i = 0; i < list.Count(); i++)
                    {
                        datatable.Rows.Add(list[i].log_DateTime, list[i].log_Username, list[i].log_Actions);
                    }

                    dataGridViewLog.ItemsSource = datatable.DefaultView;
                    dataGridViewLog.MaxHeight = 300;
                }
            }           
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            if (txtbox_question.Text == "" && fromdate.Text == "" && todate.Text == "")
            {
                labelerror.Content = "complete a field or dates";
            }
            else
            {               

                var list = serviceslog.getLogs(txtbox_question.Text, fromdate.Text, todate.Text).list;

                if (list.Count() == 0)
                {
                    labelerror.Content = "Data Not Found";
                }
                else
                {
                    var items = new log();
                    items.log_Username = userInformation.user.usr_Username;
                    items.log_DateTime = dateservice.getCurrentDate();
                    items.log_Actions = "Print Information by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + " in Log, Search Data: Processed by= " + txtbox_question.Text + ", Dates: From= " + fromdate.Text + ", To= " + todate.Text;
                    serviceslog.CreateLog(items);

                    createPDF(list);
                }
            }
        }

        private void createPDF(List<log> list)
        {
            #region Create PDF

            Random rnd = new Random();
            string nomber = rnd.Next().ToString();
     
            Document documentPDF = new Document(PageSize.LETTER);

            PdfWriter writer = PdfWriter.GetInstance(documentPDF,
                        new FileStream(@"C:/DrCash_WPF/DoctorCashWpf/ArchiveLog" + nomber + ".pdf", FileMode.CreateNew));            

            //title
            documentPDF.AddTitle("Archive PDF");

            documentPDF.AddCreator("DrCash");
            documentPDF.Open();

            //type letter
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.RED);

            iTextSharp.text.Paragraph text1 = new iTextSharp.text.Paragraph("Table Of Results", _standardFont);
            text1.Alignment = 1;
            documentPDF.Add(text1);
            documentPDF.Add(Chunk.NEWLINE);


            //create a table
            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100;

            // config title and columns
            PdfPCell clprocessedby = new PdfPCell(new Phrase("Processed By", _standardFont));
            clprocessedby.BorderWidth = 0;
            clprocessedby.BorderWidthBottom = 0.75f;

            PdfPCell cldate = new PdfPCell(new Phrase("Date", _standardFont));
            cldate.BorderWidth = 0;
            cldate.BorderWidthBottom = 0.75f;

            PdfPCell claction = new PdfPCell(new Phrase("Action", _standardFont));
            claction.BorderWidth = 0;
            claction.BorderWidthBottom = 0.75f;


            // add cells at table
            table.AddCell(clprocessedby);
            table.AddCell(cldate);
            table.AddCell(claction);


            for (int i = 0; i < list.Count(); i++)
            {
                if (i == (list.Count() - 1))
                {
                    clprocessedby = new PdfPCell(new Phrase(list[i].log_Username.ToString(), _standardFont)); clprocessedby.BorderWidth = 0; clprocessedby.BorderWidthBottom = 0.75f;
                    cldate = new PdfPCell(new Phrase(list[i].log_DateTime.ToString(), _standardFont)); cldate.BorderWidth = 0; cldate.BorderWidthBottom = 0.75f;
                    claction = new PdfPCell(new Phrase(list[i].log_Actions.ToString(), _standardFont)); claction.BorderWidth = 0; claction.BorderWidthBottom = 0.75f;
                    table.AddCell(clprocessedby);
                    table.AddCell(cldate);
                    table.AddCell(claction);
                }
                else
                {
                    clprocessedby = new PdfPCell(new Phrase(list[i].log_Username.ToString(), _standardFont)); clprocessedby.BorderWidth = 0;
                    cldate = new PdfPCell(new Phrase(list[i].log_DateTime.ToString(), _standardFont)); cldate.BorderWidth = 0;
                    claction = new PdfPCell(new Phrase(list[i].log_Actions.ToString(), _standardFont)); claction.BorderWidth = 0;
                    table.AddCell(clprocessedby);
                    table.AddCell(cldate);
                    table.AddCell(claction);
                }
            }

            documentPDF.Add(table);
            documentPDF.Close();
            writer.Close();

            Process.Start(@"C:/DrCash_WPF/DoctorCashWpf/ArchiveLog" + nomber + ".pdf");

            #endregion
        }
    }
}
