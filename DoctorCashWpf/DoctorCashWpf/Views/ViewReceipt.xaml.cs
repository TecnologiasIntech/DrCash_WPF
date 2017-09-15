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
    /// Interaction logic for ViewReceipt.xaml
    /// </summary>
    public partial class ViewReceipt : UserControl
    {
        public ViewReceipt()
        {
            InitializeComponent();

            loadValuesOfSearch();
        }

        private reportService getreport = new reportService();
        private logService serviceslog = new logService();
        private int transactionID = -1;

        private void loadValuesOfSearch()
        {
            txtbox_question.Text = cashInUpdate.saveTransactionNumber;
            fromdate.Text = cashInUpdate.saveSearchFromDate;
            todate.Text = cashInUpdate.saveSearchToDate;

            Button_Click_1(null, null);

            cashInUpdate.saveTransactionNumber = "";
            cashInUpdate.saveSearchFromDate = "";
            cashInUpdate.saveSearchToDate = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            txtbox_question.Text = "";
            fromdate.Text = "";
            todate.Text = "";
            dataGridViewClosedStatement.ItemsSource = null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            if (txtbox_question.Text == "" && fromdate.Text == "" && todate.Text == "")
            {
                labelerror.Content = "Complete the Fields";
            }
            else
            {
                var items = new log();
                items.log_Username = userInformation.user.usr_Username;
                items.log_DateTime = DateTime.Now.ToString();
                items.log_Actions = "Search Information by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + " in Transactions, Search Data: Transaction Number= " + txtbox_question.Text + ", Dates: From= " + fromdate.Text + ", To= " + todate.Text;
                serviceslog.CreateLog(items);

                var list = getreport.getDailyTransactions(txtbox_question.Text, "", fromdate.Text, todate.Text).list;
                dataGridViewClosedStatement.ItemsSource = null;

                if (list.Count() == 0)
                {
                    labelerror.Content = "Data Not Found";
                }
                else
                {                    

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Transaction ID");
                    dt.Columns.Add("Patient Name");
                    dt.Columns.Add("Register");
                    dt.Columns.Add("Date");

                    for (int i = 0; i < list.Count(); i++)
                    {
                        dt.Rows.Add(list[i].trn_id, list[i].patientFirstName, list[i].modifiedById, list[i].dateRegistered);
                    }
                    dataGridViewClosedStatement.ItemsSource = dt.DefaultView;
                }
            }            
        }

        private void dataGridViewClosedStatement_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {            
            /*
            //var dato = dataGridViewClosedStatement.SelectedItem;
            DataRowView item = dataGridViewClosedStatement.SelectedItem as DataRowView;
            int dato = Convert.ToInt32(item.Row.ItemArray[0]);            
            var list = getreport.getDailyTransactions(txtbox_question.Text, "", fromdate.Text, todate.Text).list;
            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i].trn_id == dato)
                {
                    amounChange.Text = list[i].amountCharged.ToString();
                    cash.Text = list[i].cash.ToString();
                    creditCard.Text = list[i].credit.ToString();
                    check.Text = list[i].check.ToString();
                    total.Text = list[i].cash.ToString();
                    change.Text = list[i].change.ToString();
                    break;
                }
            }*/
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var dato = dataGridViewClosedStatement.SelectedItem;
            DataRowView item = dataGridViewClosedStatement.SelectedItem as DataRowView;
            transactionID = Convert.ToInt32(item.Row.ItemArray[0]);
            var list = getreport.getDailyTransactions(txtbox_question.Text, "", fromdate.Text, todate.Text).list;
            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i].trn_id == transactionID)
                {
                    amounChange.Text = list[i].amountCharged.ToString();
                    cash.Text = list[i].cash.ToString();
                    creditCard.Text = list[i].credit.ToString();
                    check.Text = list[i].check.ToString();
                    total.Text = list[i].cash.ToString();
                    change.Text = list[i].change.ToString();
                    break;
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (transactionID != -1)
            {
                cashInUpdate.isUpdate = true;
                cashInUpdate.transactionID = transactionID;
                cashInUpdate.saveTransactionNumber = txtbox_question.Text;
                cashInUpdate.saveSearchFromDate = fromdate.Text;
                cashInUpdate.saveSearchToDate = todate.Text;

                MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);                

            }
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            if (txtbox_question.Text == "" && fromdate.Text == "" && todate.Text == "")
            {
                labelerror.Content = "Complete the Fields";
            }
            else
            {
                var items = new log();
                items.log_Username = userInformation.user.usr_Username;
                items.log_DateTime = DateTime.Now.ToString();
                items.log_Actions = "Print Information by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + " in Daily Transactions, Search Data: Transaction Number= " + txtbox_question.Text + ", Dates: From= " + fromdate.Text + ", To= " + todate.Text;
                serviceslog.CreateLog(items);

                var list = getreport.getDailyTransactions(txtbox_question.Text, "", fromdate.Text, todate.Text).list;
                dataGridViewClosedStatement.ItemsSource = null;

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
                                new FileStream(@"C:/DrCash_WPF/DoctorCashWpf/Transaction" + nomber + ".pdf", FileMode.CreateNew));
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
                    PdfPTable tblPrueba = new PdfPTable(4);
                    tblPrueba.WidthPercentage = 100;

                    // Configuramos el título de las columnas de la tabla
                    PdfPCell cltransaction = new PdfPCell(new Phrase("Transaction ID", _standardFont));
                    cltransaction.BorderWidth = 0;
                    cltransaction.BorderWidthBottom = 0.75f;

                    PdfPCell clpatientname = new PdfPCell(new Phrase("Patien Name", _standardFont));
                    clpatientname.BorderWidth = 0;
                    clpatientname.BorderWidthBottom = 0.75f;

                    PdfPCell clregister = new PdfPCell(new Phrase("Register", _standardFont));
                    clregister.BorderWidth = 0;
                    clregister.BorderWidthBottom = 0.75f;

                    PdfPCell cldate = new PdfPCell(new Phrase("Date", _standardFont));
                    cldate.BorderWidth = 0;
                    cldate.BorderWidthBottom = 0.75f;                                     


                    // Añadimos las celdas a la tabla
                    tblPrueba.AddCell(cltransaction);                    
                    tblPrueba.AddCell(cldate);
                    tblPrueba.AddCell(clregister);
                    tblPrueba.AddCell(clpatientname);                                        

                    for (int i = 0; i < list.Count(); i++)
                    {
                        if (i == (list.Count() - 1))
                        {
                            cltransaction = new PdfPCell(new Phrase(list[i].trn_id.ToString(), _standardFont)); cltransaction.BorderWidth = 0; cltransaction.BorderWidthBottom = 0.75f;                                                        
                            clpatientname = new PdfPCell(new Phrase(list[i].patientFirstName.ToString(), _standardFont)); clpatientname.BorderWidth = 0; clpatientname.BorderWidthBottom = 0.75f;                                                        
                            clregister = new PdfPCell(new Phrase(list[i].registerId.ToString(), _standardFont)); clregister.BorderWidth = 0; clregister.BorderWidthBottom = 0.75f;
                            cldate = new PdfPCell(new Phrase(list[i].dateRegistered.ToString(), _standardFont)); cldate.BorderWidth = 0; cldate.BorderWidthBottom = 0.75f;


                            tblPrueba.AddCell(cltransaction);
                            tblPrueba.AddCell(clpatientname);
                            tblPrueba.AddCell(clregister);
                            tblPrueba.AddCell(cldate);
                            
                            
                        }
                        else
                        {
                            cltransaction = new PdfPCell(new Phrase(list[i].trn_id.ToString(), _standardFont)); cltransaction.BorderWidth = 0;
                            clpatientname = new PdfPCell(new Phrase(list[i].patientFirstName.ToString(), _standardFont)); clpatientname.BorderWidth = 0;
                            clregister = new PdfPCell(new Phrase(list[i].registerId.ToString(), _standardFont)); clregister.BorderWidth = 0;
                            cldate = new PdfPCell(new Phrase(list[i].dateRegistered.ToString(), _standardFont)); cldate.BorderWidth = 0;
                            

                            tblPrueba.AddCell(cltransaction);
                            tblPrueba.AddCell(clpatientname);
                            tblPrueba.AddCell(clregister);
                            tblPrueba.AddCell(cldate);
                            
                        }
                    }


                    doc.Add(tblPrueba);
                    doc.Close();
                    writer.Close();

                    Process.Start(@"C:/DrCash_WPF/DoctorCashWpf/Transaction" + nomber + ".pdf");
                    #endregion                    
                }
            }
        }
    }
}
