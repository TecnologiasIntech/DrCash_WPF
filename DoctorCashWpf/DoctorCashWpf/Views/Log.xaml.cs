using iTextSharp.text;
using iTextSharp.text.pdf;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dataGridViewLog.ItemsSource = null;
            txtbox_question.Clear();
            fromdate.Text = "";
            todate.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

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
                var list = getreport.getDailyTransactions(txtbox_question.Text, "", fromdate.Text, todate.Text).list;

                if (list.Count() == 0)
                {
                    labelerror.Content = "Data Not Found";
                }
                else
                {
                    #region Create PDF

                    Random rnd = new Random();
                    string nomber = rnd.Next().ToString();

                    //vamos a crear el reporte desde codigo
                    Document doc = new Document(PageSize.LETTER);

                    PdfWriter writer = PdfWriter.GetInstance(doc,
                                new FileStream(@"C:/DrCash_WPF/DoctorCashWpf/Archive" + nomber + ".pdf", FileMode.CreateNew));
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
                    PdfPTable tblPrueba = new PdfPTable(13);
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
                            clprocessedby = new PdfPCell(new Phrase(list[i].trn_id.ToString(), _standardFont)); clprocessedby.BorderWidth = 0; clprocessedby.BorderWidthBottom = 0.75f;
                            cldate = new PdfPCell(new Phrase(list[i].dateRegistered.ToString(), _standardFont)); cldate.BorderWidth = 0; cldate.BorderWidthBottom = 0.75f;
                            claction = new PdfPCell(new Phrase(list[i].patientFirstName.ToString(), _standardFont)); claction.BorderWidth = 0; claction.BorderWidthBottom = 0.75f;
                            tblPrueba.AddCell(clprocessedby);
                            tblPrueba.AddCell(cldate);
                            tblPrueba.AddCell(claction);
                        }
                        else
                        {
                            clprocessedby = new PdfPCell(new Phrase(list[i].trn_id.ToString(), _standardFont)); clprocessedby.BorderWidth = 0;
                            cldate = new PdfPCell(new Phrase(list[i].dateRegistered.ToString(), _standardFont)); cldate.BorderWidth = 0;
                            claction = new PdfPCell(new Phrase(list[i].patientFirstName.ToString(), _standardFont)); claction.BorderWidth = 0;
                            tblPrueba.AddCell(clprocessedby);
                            tblPrueba.AddCell(cldate);
                            tblPrueba.AddCell(claction);
                        }
                    }

                    doc.Add(tblPrueba);
                    doc.Close();
                    writer.Close();

                    Process.Start(@"C:/DrCash_WPF/DoctorCashWpf/Archive" + nomber + ".pdf");

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
