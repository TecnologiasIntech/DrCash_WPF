using DoctorCashWpf.Printer;
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
using System.Windows.Input;

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

        private reportService reportService = new reportService();
        private logService serviceslog = new logService();
        private dateService dateservice = new dateService();
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
                setLog("Search");

                var list = reportService.getTransactionsByRangeAndTransactionId(txtbox_question.Text, "", fromdate.Text, todate.Text).list;
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
                    dataGridViewClosedStatement.MaxHeight = 315;
                }
            }
        }

        private void dataGridViewClosedStatement_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView item = dataGridViewClosedStatement.SelectedItem as DataRowView;
            transactionID = Convert.ToInt32(item.Row.ItemArray[0]);
            var list = reportService.getTransactionsByRangeAndTransactionId(txtbox_question.Text, "", fromdate.Text, todate.Text).list;
            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i].trn_id == transactionID)
                {
                    amounChange.Text = list[i].amountCharged.ToString();
                    cash.Text = list[i].cash.ToString();
                    creditCard.Text = list[i].credit.ToString();
                    check.Text = list[i].check.ToString();
                    total.Text = (list[i].cash + list[i].credit + list[i].check).ToString();
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
            else
            {
                labelerror.Content = "Select a transaction ID";
            }

        }

        private void setLog(string stringValue)
        {
            var items = new log();
            items.log_Username = userInformation.user.usr_Username;
            items.log_DateTime = dateservice.getCurrentDate();
            items.log_Actions = stringValue + " Information by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + " in View Receipt, Search Data: Transaction Number= " + txtbox_question.Text + ", Dates: From= " + fromdate.Text + ", To= " + todate.Text;
            serviceslog.CreateLog(items);
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
                setLog("Print");

                //generamos el ticket con los totales de la seleccion
                if (transactionID != -1)
                {
                    setPrint();
                }

                var list = reportService.getTransactionsByRangeAndTransactionId(txtbox_question.Text, "", fromdate.Text, todate.Text).list;                

                if (list.Count() == 0)
                {
                    labelerror.Content = "Data Not Found";
                }
                else
                {
                    createPDF(list);
                }
            }
        }

        private void setPrint()
        {
            double total_sum = 0;
            var item = new transaction();
            item.amountCharged = (float)Convert.ToDouble(amounChange.Text);
            item.cash = (float)Convert.ToDouble(cash.Text);
            item.credit = (float)Convert.ToDouble(creditCard.Text);
            item.check = (float)Convert.ToDouble(check.Text);
            total_sum = (float)Convert.ToDouble(total.Text);
            item.change = (float)Convert.ToDouble(change.Text);

            Print printer = new Print();
            printer.printViewReciept(item, total_sum);
        }

        private void createPDF(List<transaction> list)
        {
            #region Create PDF
            Random rnd = new Random();
            string nomber = rnd.Next().ToString();
            
            Document documentPDF = new Document(PageSize.LETTER);

            PdfWriter writer = PdfWriter.GetInstance(documentPDF,
                        new FileStream(@"C:/DrCash_WPF/DoctorCashWpf/Transaction" + nomber + ".pdf", FileMode.CreateNew));            

            //title
            documentPDF.AddTitle("Archive PDF");

            documentPDF.AddCreator("DrCash");
            documentPDF.Open();

            //type of letter
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.RED);

            iTextSharp.text.Paragraph text1 = new iTextSharp.text.Paragraph("Table Of Results", _standardFont);
            text1.Alignment = 1;
            documentPDF.Add(text1);
            documentPDF.Add(Chunk.NEWLINE);

            /*
            // si queremos agregar una imagen al documento
            iTextSharp.text.Image jpg =iTextSharp.text.Image.GetInstance(@"C:\...\ghostsandgoblins.jpg");
            jpg.Alignment = iTextSharp.text.Image.MIDDLE_ALIGN;
            doc.Add(jpg);
            //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx*/


            //create table
            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;

            // config title and columns
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


            // add cells
            table.AddCell(cltransaction);
            table.AddCell(cldate);
            table.AddCell(clregister);
            table.AddCell(clpatientname);

            for (int i = 0; i < list.Count(); i++)
            {
                if (i == (list.Count() - 1))
                {
                    cltransaction = new PdfPCell(new Phrase(list[i].trn_id.ToString(), _standardFont)); cltransaction.BorderWidth = 0; cltransaction.BorderWidthBottom = 0.75f;
                    clpatientname = new PdfPCell(new Phrase(list[i].patientFirstName.ToString(), _standardFont)); clpatientname.BorderWidth = 0; clpatientname.BorderWidthBottom = 0.75f;
                    clregister = new PdfPCell(new Phrase(list[i].registerId.ToString(), _standardFont)); clregister.BorderWidth = 0; clregister.BorderWidthBottom = 0.75f;
                    cldate = new PdfPCell(new Phrase(list[i].dateRegistered.ToString(), _standardFont)); cldate.BorderWidth = 0; cldate.BorderWidthBottom = 0.75f;


                    table.AddCell(cltransaction);
                    table.AddCell(clpatientname);
                    table.AddCell(clregister);
                    table.AddCell(cldate);
                }
                else
                {
                    cltransaction = new PdfPCell(new Phrase(list[i].trn_id.ToString(), _standardFont)); cltransaction.BorderWidth = 0;
                    clpatientname = new PdfPCell(new Phrase(list[i].patientFirstName.ToString(), _standardFont)); clpatientname.BorderWidth = 0;
                    clregister = new PdfPCell(new Phrase(list[i].registerId.ToString(), _standardFont)); clregister.BorderWidth = 0;
                    cldate = new PdfPCell(new Phrase(list[i].dateRegistered.ToString(), _standardFont)); cldate.BorderWidth = 0;


                    table.AddCell(cltransaction);
                    table.AddCell(clpatientname);
                    table.AddCell(clregister);
                    table.AddCell(cldate);
                }
            }


            documentPDF.Add(table);
            documentPDF.Close();
            writer.Close();

            Process.Start(@"C:/DrCash_WPF/DoctorCashWpf/Transaction" + nomber + ".pdf");
            #endregion
        }

        private void txtbox_question_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
        }

        private void fromdate_GotFocus(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
        }
    }
}
