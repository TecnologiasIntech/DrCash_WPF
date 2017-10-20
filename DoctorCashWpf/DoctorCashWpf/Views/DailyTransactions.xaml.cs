using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
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
    /// Interaction logic for DailyTransactions.xaml
    /// </summary>
    public partial class DailyTransactions : UserControl
    {
        public DailyTransactions()
        {
            InitializeComponent();
        }

        private reportService getreport = new reportService();
        MoneyFormatService moneyService = new MoneyFormatService();
        logService serviceslog = new logService();
        private dateService date = new dateService();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            if (txtbox_question.Text==""&& Patient_Name.Text==""&& fromdate.Text=="" && todate.Text == "")
            {
                labelerror.Content = "complete a field or dates";
            }
            else
            {
                var items = new log();
                items.log_Username = userInformation.user.usr_Username;
                items.log_DateTime = date.getCurrentDate();
                items.log_Actions = "Search Information by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + " in Daily Transactions, Search Data: Transaction Number= " + txtbox_question.Text + ", Dates: From= " + fromdate.Text + ", To= " + todate.Text;
                serviceslog.CreateLog(items);

                double charge = 0, cash = 0, Credit = 0, Check = 0, Change = 0;
                var list = getreport.getTransactionsByRangeAndTransactionId(txtbox_question.Text, Patient_Name.Text, fromdate.Text, todate.Text).list;
                dataGridViewDailyTransactions.ItemsSource = null;                              

                if (list.Count() == 0)
                {
                    labelerror.Content = "Data Not Found";
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Transaction ID");
                    dt.Columns.Add("User");
                    dt.Columns.Add("Date");
                    dt.Columns.Add("Patient Name");
                    dt.Columns.Add("Type");
                    dt.Columns.Add("Charge");
                    dt.Columns.Add("Cash");
                    dt.Columns.Add("Credit");
                    dt.Columns.Add("Check");
                    dt.Columns.Add("Change");
                    dt.Columns.Add("Check #");
                    dt.Columns.Add("Closed");
                    dt.Columns.Add("Register");

                    for (int i = 0; i < list.Count(); i++)
                    {
                        dt.Rows.Add(list[i].trn_id, list[i].userId, list[i].dateRegistered, list[i].patientFirstName, list[i].type, moneyService.convertToMoneyFormat(list[i].amountCharged.ToString()).txtComponent, moneyService.convertToMoneyFormat(list[i].cash.ToString()).txtComponent, moneyService.convertToMoneyFormat(list[i].credit.ToString()).txtComponent, moneyService.convertToMoneyFormat(list[i].check.ToString()).txtComponent, moneyService.convertToMoneyFormat(list[i].change.ToString()).txtComponent, list[i].checkNumber, list[i].closed, list[i].registerId);
                        charge += list[i].amountCharged;
                        cash += list[i].cash;
                        Credit += list[i].credit;
                        Check += list[i].check;
                        Change += list[i].change;
                    }


                    dt.Rows.Add("Total´s", "", "", "", "", moneyService.convertToMoneyFormat(charge.ToString()).txtComponent, moneyService.convertToMoneyFormat(cash.ToString()).txtComponent, moneyService.convertToMoneyFormat(Credit.ToString()).txtComponent, moneyService.convertToMoneyFormat(Check.ToString()).txtComponent, moneyService.convertToMoneyFormat(Change.ToString()).txtComponent, "", "", "");

                    dataGridViewDailyTransactions.ItemsSource = dt.DefaultView;

                    dataGridViewDailyTransactions.MaxHeight = 185;

                }

            }            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            if (txtbox_question.Text == "" && Patient_Name.Text == "" && fromdate.Text == "" && todate.Text == "")
            {
                labelerror.Content = "complete a field or dates";
            }
            else
            {                

                var list = getreport.getTransactionsByRangeAndTransactionId(txtbox_question.Text, Patient_Name.Text, fromdate.Text, todate.Text).list;

                if (list.Count() == 0)
                {
                    labelerror.Content = "Data Not Found";
                }
                else
                {
                    var items = new log();
                    items.log_Username = userInformation.user.usr_Username;
                    items.log_DateTime = date.getCurrentDate();
                    items.log_Actions = "Print Information by UserName= " + userInformation.user.usr_Username + ", Full Name: " + userInformation.user.usr_FirstName + " " + userInformation.user.usr_LastName + " in Daily Transactions, Search Data: Transaction Number= " + txtbox_question.Text + ", Dates: From= " + fromdate.Text + ", To= " + todate.Text;
                    serviceslog.CreateLog(items);

                    #region Create PDF
                    Random rnd = new Random();
                    string nomber = rnd.Next().ToString();
                    double charge = 0, cash = 0, Credit = 0, Check = 0, Change = 0;

                    //vamos a crear el reporte desde codigo
                    Document doc = new Document(PageSize.LETTER);

                    PdfWriter writer = PdfWriter.GetInstance(doc,
                                new FileStream(@"C:/DrCash_WPF/DoctorCashWpf/DailyTransaction" + nomber + ".pdf", FileMode.CreateNew));
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
                    PdfPCell cltransaction = new PdfPCell(new Phrase("Transaction", _standardFont));
                    cltransaction.BorderWidth = 0;
                    cltransaction.BorderWidthBottom = 0.75f;

                    PdfPCell cluser = new PdfPCell(new Phrase("User", _standardFont));
                    cluser.BorderWidth = 0;
                    cluser.BorderWidthBottom = 0.75f;

                    PdfPCell cldate = new PdfPCell(new Phrase("Date", _standardFont));
                    cldate.BorderWidth = 0;
                    cldate.BorderWidthBottom = 0.75f;

                    PdfPCell clpatientname = new PdfPCell(new Phrase("Patien Name", _standardFont));
                    clpatientname.BorderWidth = 0;
                    clpatientname.BorderWidthBottom = 0.75f;

                    PdfPCell cltype = new PdfPCell(new Phrase("Type", _standardFont));
                    cltype.BorderWidth = 0;
                    cltype.BorderWidthBottom = 0.75f;

                    PdfPCell clcharge = new PdfPCell(new Phrase("Charge", _standardFont));
                    clcharge.BorderWidth = 0;
                    clcharge.BorderWidthBottom = 0.75f;

                    PdfPCell clcash = new PdfPCell(new Phrase("Cash", _standardFont));
                    clcash.BorderWidth = 0;
                    clcash.BorderWidthBottom = 0.75f;

                    PdfPCell clcredit = new PdfPCell(new Phrase("Credit", _standardFont));
                    clcredit.BorderWidth = 0;
                    clcredit.BorderWidthBottom = 0.75f;

                    PdfPCell clcheck = new PdfPCell(new Phrase("Check", _standardFont));
                    clcheck.BorderWidth = 0;
                    clcheck.BorderWidthBottom = 0.75f;

                    PdfPCell clchange = new PdfPCell(new Phrase("Change", _standardFont));
                    clchange.BorderWidth = 0;
                    clchange.BorderWidthBottom = 0.75f;

                    PdfPCell clchecknumber = new PdfPCell(new Phrase("Check #", _standardFont));
                    clchecknumber.BorderWidth = 0;
                    clchecknumber.BorderWidthBottom = 0.75f;

                    PdfPCell clclosed = new PdfPCell(new Phrase("Closed", _standardFont));
                    clclosed.BorderWidth = 0;
                    clclosed.BorderWidthBottom = 0.75f;

                    PdfPCell clregister = new PdfPCell(new Phrase("Register", _standardFont));
                    clregister.BorderWidth = 0;
                    clregister.BorderWidthBottom = 0.75f;


                    // Añadimos las celdas a la tabla
                    tblPrueba.AddCell(cltransaction);
                    tblPrueba.AddCell(cluser);
                    tblPrueba.AddCell(cldate);
                    tblPrueba.AddCell(clpatientname);
                    tblPrueba.AddCell(cltype);
                    tblPrueba.AddCell(clcharge);
                    tblPrueba.AddCell(clcash);
                    tblPrueba.AddCell(clcredit);
                    tblPrueba.AddCell(clcheck);
                    tblPrueba.AddCell(clchange);
                    tblPrueba.AddCell(clchecknumber);
                    tblPrueba.AddCell(clclosed);
                    tblPrueba.AddCell(clregister);


                    for (int i = 0; i < list.Count(); i++)
                    {
                        if (i == (list.Count() - 1))
                        {                            
                            cltransaction = new PdfPCell(new Phrase(list[i].trn_id.ToString(), _standardFont)); cltransaction.BorderWidth = 0; cltransaction.BorderWidthBottom = 0.75f;
                            cluser = new PdfPCell(new Phrase(list[i].userId.ToString(), _standardFont)); cluser.BorderWidth = 0; cluser.BorderWidthBottom = 0.75f;
                            cldate = new PdfPCell(new Phrase(list[i].dateRegistered.ToString(), _standardFont)); cldate.BorderWidth = 0; cldate.BorderWidthBottom = 0.75f;
                            clpatientname = new PdfPCell(new Phrase(list[i].patientFirstName.ToString(), _standardFont)); clpatientname.BorderWidth = 0; clpatientname.BorderWidthBottom = 0.75f;
                            cltype = new PdfPCell(new Phrase(list[i].type.ToString(), _standardFont)); cltype.BorderWidth = 0; cltype.BorderWidthBottom = 0.75f;
                            clcharge = new PdfPCell(new Phrase(moneyService.convertToMoneyFormat(list[i].amountCharged.ToString()).txtComponent, _standardFont)); clcharge.BorderWidth = 0; clcharge.BorderWidthBottom = 0.75f;
                            clcash = new PdfPCell(new Phrase(moneyService.convertToMoneyFormat(list[i].cash.ToString()).txtComponent, _standardFont)); clcash.BorderWidth = 0; clcash.BorderWidthBottom = 0.75f;
                            clcredit = new PdfPCell(new Phrase(moneyService.convertToMoneyFormat(list[i].credit.ToString()).txtComponent, _standardFont)); clcredit.BorderWidth = 0; clcredit.BorderWidthBottom = 0.75f;
                            clcheck = new PdfPCell(new Phrase(moneyService.convertToMoneyFormat(list[i].check.ToString()).txtComponent, _standardFont)); clcheck.BorderWidth = 0; clcheck.BorderWidthBottom = 0.75f;
                            clchange = new PdfPCell(new Phrase(moneyService.convertToMoneyFormat(list[i].change.ToString()).txtComponent, _standardFont)); clchange.BorderWidth = 0; clchange.BorderWidthBottom = 0.75f;
                            clchecknumber = new PdfPCell(new Phrase(list[i].checkNumber.ToString(), _standardFont)); clchecknumber.BorderWidth = 0; clchecknumber.BorderWidthBottom = 0.75f;
                            clclosed = new PdfPCell(new Phrase(list[i].closed.ToString(), _standardFont)); clclosed.BorderWidth = 0; clclosed.BorderWidthBottom = 0.75f;
                            clregister = new PdfPCell(new Phrase(list[i].registerId.ToString(), _standardFont)); clregister.BorderWidth = 0; clregister.BorderWidthBottom = 0.75f;

                            charge += list[i].amountCharged;
                            cash += list[i].cash;
                            Credit += list[i].credit;
                            Check += list[i].check;
                            Change += list[i].change;

                            tblPrueba.AddCell(cltransaction);
                            tblPrueba.AddCell(cluser);
                            tblPrueba.AddCell(cldate);
                            tblPrueba.AddCell(clpatientname);
                            tblPrueba.AddCell(cltype);
                            tblPrueba.AddCell(clcharge);
                            tblPrueba.AddCell(clcash);
                            tblPrueba.AddCell(clcredit);
                            tblPrueba.AddCell(clcheck);
                            tblPrueba.AddCell(clchange);
                            tblPrueba.AddCell(clchecknumber);
                            tblPrueba.AddCell(clclosed);
                            tblPrueba.AddCell(clregister);

                            //datos de las sumas
                            cltransaction = new PdfPCell(new Phrase("Total´s", _standardFont2)); cltransaction.BorderWidth = 0;
                            cluser = new PdfPCell(new Phrase("", _standardFont)); cluser.BorderWidth = 0;
                            cldate = new PdfPCell(new Phrase("", _standardFont)); cldate.BorderWidth = 0;
                            clpatientname = new PdfPCell(new Phrase("", _standardFont)); clpatientname.BorderWidth = 0;
                            cltype = new PdfPCell(new Phrase("", _standardFont)); cltype.BorderWidth = 0;
                            clcharge = new PdfPCell(new Phrase(moneyService.convertToMoneyFormat(charge.ToString()).txtComponent, _standardFont2)); clcharge.BorderWidth = 0;
                            clcash = new PdfPCell(new Phrase(moneyService.convertToMoneyFormat(cash.ToString()).txtComponent, _standardFont2)); clcash.BorderWidth = 0;
                            clcredit = new PdfPCell(new Phrase(moneyService.convertToMoneyFormat(Credit.ToString()).txtComponent, _standardFont2)); clcredit.BorderWidth = 0;
                            clcheck = new PdfPCell(new Phrase(moneyService.convertToMoneyFormat(Check.ToString()).txtComponent, _standardFont2)); clcheck.BorderWidth = 0;
                            clchange = new PdfPCell(new Phrase(moneyService.convertToMoneyFormat(Change.ToString()).txtComponent, _standardFont2)); clchange.BorderWidth = 0;
                            clchecknumber = new PdfPCell(new Phrase("", _standardFont)); clchecknumber.BorderWidth = 0;
                            clclosed = new PdfPCell(new Phrase("", _standardFont)); clclosed.BorderWidth = 0;
                            clregister = new PdfPCell(new Phrase("", _standardFont)); clregister.BorderWidth = 0;

                            tblPrueba.AddCell(cltransaction);
                            tblPrueba.AddCell(cluser);
                            tblPrueba.AddCell(cldate);
                            tblPrueba.AddCell(clpatientname);
                            tblPrueba.AddCell(cltype);
                            tblPrueba.AddCell(clcharge);
                            tblPrueba.AddCell(clcash);
                            tblPrueba.AddCell(clcredit);
                            tblPrueba.AddCell(clcheck);
                            tblPrueba.AddCell(clchange);
                            tblPrueba.AddCell(clchecknumber);
                            tblPrueba.AddCell(clclosed);
                            tblPrueba.AddCell(clregister);
                        }
                        else
                        {
                            cltransaction = new PdfPCell(new Phrase(list[i].trn_id.ToString(), _standardFont)); cltransaction.BorderWidth = 0;
                            cluser = new PdfPCell(new Phrase(list[i].userId.ToString(), _standardFont)); cluser.BorderWidth = 0;
                            cldate = new PdfPCell(new Phrase(list[i].dateRegistered.ToString(), _standardFont)); cldate.BorderWidth = 0;
                            clpatientname = new PdfPCell(new Phrase(list[i].patientFirstName.ToString(), _standardFont)); clpatientname.BorderWidth = 0;
                            cltype = new PdfPCell(new Phrase(list[i].type.ToString(), _standardFont)); cltype.BorderWidth = 0;
                            clcharge = new PdfPCell(new Phrase(moneyService.convertToMoneyFormat(list[i].amountCharged.ToString()).txtComponent, _standardFont)); clcharge.BorderWidth = 0;
                            clcash = new PdfPCell(new Phrase(moneyService.convertToMoneyFormat(list[i].cash.ToString()).txtComponent, _standardFont)); clcash.BorderWidth = 0;
                            clcredit = new PdfPCell(new Phrase(moneyService.convertToMoneyFormat(list[i].credit.ToString()).txtComponent, _standardFont)); clcredit.BorderWidth = 0;
                            clcheck = new PdfPCell(new Phrase(moneyService.convertToMoneyFormat(list[i].check.ToString()).txtComponent, _standardFont)); clcheck.BorderWidth = 0;
                            clchange = new PdfPCell(new Phrase(moneyService.convertToMoneyFormat(list[i].change.ToString()).txtComponent, _standardFont)); clchange.BorderWidth = 0;
                            clchecknumber = new PdfPCell(new Phrase(list[i].checkNumber.ToString(), _standardFont)); clchecknumber.BorderWidth = 0;
                            clclosed = new PdfPCell(new Phrase(list[i].closed.ToString(), _standardFont)); clclosed.BorderWidth = 0;
                            clregister = new PdfPCell(new Phrase(list[i].registerId.ToString(), _standardFont)); clregister.BorderWidth = 0;

                            charge += list[i].amountCharged;
                            cash += list[i].cash;
                            Credit += list[i].credit;
                            Check += list[i].check;
                            Change += list[i].change;

                            tblPrueba.AddCell(cltransaction);
                            tblPrueba.AddCell(cluser);
                            tblPrueba.AddCell(cldate);
                            tblPrueba.AddCell(clpatientname);
                            tblPrueba.AddCell(cltype);
                            tblPrueba.AddCell(clcharge);
                            tblPrueba.AddCell(clcash);
                            tblPrueba.AddCell(clcredit);
                            tblPrueba.AddCell(clcheck);
                            tblPrueba.AddCell(clchange);
                            tblPrueba.AddCell(clchecknumber);
                            tblPrueba.AddCell(clclosed);
                            tblPrueba.AddCell(clregister);
                        }
                    }


                    doc.Add(tblPrueba);
                    doc.Close();
                    writer.Close();

                    Process.Start(@"C:/DrCash_WPF/DoctorCashWpf/DailyTransaction" + nomber + ".pdf");
                    #endregion                    

                }
                //////////////////////////////////////
                /*
                Reportings.Report1 miventana = new Reportings.Report1(getreport.getDailyTransactions(txtbox_question.Text, Patient_Name.Text, fromdate.Text, todate.Text).dataTable);
                miventana.ShowDialog();*/
            }
                       
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            labelerror.Content = "";
            dataGridViewDailyTransactions.ItemsSource = null;                      
            txtbox_question.Clear();
            Patient_Name.Clear();
            fromdate.Text = "";
            todate.Text = "";
        }

        private void txtbox_question_KeyUp(object sender, KeyEventArgs e)
        {
            labelerror.Content = "";
        }
    }
}
