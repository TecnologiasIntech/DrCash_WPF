using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
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
        private reportService getreport = new reportService();
        MoneyComponentService moneyService = new MoneyComponentService();
        logService serviceslog = new logService();
        private dateService date = new dateService();

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
                items.log_DateTime = date.getCurrentDate();
                items.log_Actions = "Search Information by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + " in Log, Search Data: Processed by= " + txtbox_question.Text + ", Dates: From= " + fromdate.Text + ", To= " + todate.Text;
                serviceslog.CreateLog(items);                

                var list = serviceslog.getLogs(txtbox_question.Text, fromdate.Text, todate.Text).list;

                if (list.Count() == 0)
                {
                    labelerror.Content = "Data not Found";
                }
                else
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("Date");
                    table.Columns.Add("Processed by");
                    table.Columns.Add("Action");

                    for (int i = 0; i < list.Count(); i++)
                    {
                        table.Rows.Add(list[i].log_DateTime, list[i].log_Username, list[i].log_Actions);
                    }

                    dataGridViewLog.ItemsSource = table.DefaultView;
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
                    items.log_DateTime = date.getCurrentDate();
                    items.log_Actions = "Print Information by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + " in Log, Search Data: Processed by= " + txtbox_question.Text + ", Dates: From= " + fromdate.Text + ", To= " + todate.Text;
                    serviceslog.CreateLog(items);

                    #region Create PDF

                    Random rnd = new Random();
                    string nomber = rnd.Next().ToString();

                    //vamos a crear el reporte desde codigo
                    Document doc = new Document(PageSize.LETTER);

                    PdfWriter writer = PdfWriter.GetInstance(doc,
                                new FileStream(@"C:/DrCash_WPF/DoctorCashWpf/ArchiveLog" + nomber + ".pdf", FileMode.CreateNew));
                    //PdfWriter write = PdfWriter.GetInstance(doc, new FileStream("", FileMode.CreateNew));

                    //datos de titulo
                    doc.AddTitle("Archive PDF");

                    doc.AddCreator("DrCash");
                    doc.Open();

                    //tipo de letra del documento
                    iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.RED);

                    iTextSharp.text.Paragraph text1 = new iTextSharp.text.Paragraph("Table Of Results", _standardFont);
                    text1.Alignment = 1;
                    doc.Add(text1);
                    doc.Add(Chunk.NEWLINE);

                    /*
                    // si queremos agregar una imagen al documento
                    iTextSharp.text.Image jpg =iTextSharp.text.Image.GetInstance(@"C:\...\ghostsandgoblins.jpg");
                    jpg.Alignment = iTextSharp.text.Image.MIDDLE_ALIGN;
                    doc.Add(jpg);
                    //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx*/


                    //creamos una tabla para los datos
                    PdfPTable tblPrueba = new PdfPTable(3);
                    tblPrueba.WidthPercentage = 100;

                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clprocessedby = new PdfPCell(new Phrase("Processed By", _standardFont));
                    clprocessedby.BorderWidth = 0;
                    clprocessedby.BorderWidthBottom = 0.75f;

                    PdfPCell cldate = new PdfPCell(new Phrase("Date", _standardFont));
                    cldate.BorderWidth = 0;
                    cldate.BorderWidthBottom = 0.75f;

                    PdfPCell claction = new PdfPCell(new Phrase("Action", _standardFont));
                    claction.BorderWidth = 0;
                    claction.BorderWidthBottom = 0.75f;


                    // Añadimos las celdas a la tabla
                    tblPrueba.AddCell(clprocessedby);
                    tblPrueba.AddCell(cldate);
                    tblPrueba.AddCell(claction);


                    for (int i = 0; i < list.Count(); i++)
                    {
                        if (i == (list.Count() - 1))
                        {
                            clprocessedby = new PdfPCell(new Phrase(list[i].log_Username.ToString(), _standardFont)); clprocessedby.BorderWidth = 0; clprocessedby.BorderWidthBottom = 0.75f;
                            cldate = new PdfPCell(new Phrase(list[i].log_DateTime.ToString(), _standardFont)); cldate.BorderWidth = 0; cldate.BorderWidthBottom = 0.75f;
                            claction = new PdfPCell(new Phrase(list[i].log_Actions.ToString(), _standardFont)); claction.BorderWidth = 0; claction.BorderWidthBottom = 0.75f;
                            tblPrueba.AddCell(clprocessedby);
                            tblPrueba.AddCell(cldate);
                            tblPrueba.AddCell(claction);
                        }
                        else
                        {
                            clprocessedby = new PdfPCell(new Phrase(list[i].log_Username.ToString(), _standardFont)); clprocessedby.BorderWidth = 0;
                            cldate = new PdfPCell(new Phrase(list[i].log_DateTime.ToString(), _standardFont)); cldate.BorderWidth = 0;
                            claction = new PdfPCell(new Phrase(list[i].log_Actions.ToString(), _standardFont)); claction.BorderWidth = 0;
                            tblPrueba.AddCell(clprocessedby);
                            tblPrueba.AddCell(cldate);
                            tblPrueba.AddCell(claction);
                        }
                    }

                    doc.Add(tblPrueba);
                    doc.Close();
                    writer.Close();

                    Process.Start(@"C:/DrCash_WPF/DoctorCashWpf/ArchiveLog" + nomber + ".pdf");

                    #endregion
                }

                //////////////////////////////////////
                /*
                Reportings.Report1 miventana = new Reportings.Report1(getreport.getDailyTransactions(txtbox_question.Text, Patient_Name.Text, fromdate.Text, todate.Text).dataTable);
                miventana.ShowDialog();*/
            }
        }
    }
}
