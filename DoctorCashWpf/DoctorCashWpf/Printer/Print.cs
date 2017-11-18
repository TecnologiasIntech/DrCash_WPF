using System;
using System.Collections.Generic;
using System.Text;
using PrinterUtility;
using System.Collections;
using System.Drawing;
using System.IO;

namespace DoctorCashWpf.Printer
{
    class Print
    {
        private dateService date = new dateService();
        private MoneyFormatService money = new MoneyFormatService();
        private class CrearTicket
        {
            
            StringBuilder line = new StringBuilder();            
            
            //long of ticket
            int maxCar = 64, cortar;
            
            public string LeftText(string text)
            {
                line.Clear();
                
                if (text.Length > maxCar)
                {
                    int currentcharacter = 0;
                    for (int longText = text.Length; longText > maxCar; longText -= maxCar)
                    {
                        
                        line.AppendLine(text.Substring(currentcharacter, maxCar));
                        currentcharacter += maxCar;
                    }
                    
                    line.AppendLine(text.Substring(currentcharacter, text.Length - currentcharacter));
                }
                else
                {
                    //Si no es mayor solo agregarlo.
                    line.AppendLine(text);
                }
                return line.ToString();
            }
           
            public string RightText(string text)
            {
                line.Clear();
                
                if (text.Length > maxCar)
                {
                    int currentcharacter = 0;
                    for (int longText = text.Length; longText > maxCar; longText -= maxCar)
                    {
                        
                        line.AppendLine(text.Substring(currentcharacter, maxCar));
                        currentcharacter += maxCar;
                    }
                    
                    string spaces = "";
                    
                    for (int i = 0; i < (maxCar - text.Substring(currentcharacter, text.Length - currentcharacter).Length); i++)
                    {
                        spaces += " ";
                    }

                    
                    line.AppendLine(spaces + text.Substring(currentcharacter, text.Length - currentcharacter));
                }
                else
                {
                    string spaces = "";
                    
                    for (int i = 0; i < (maxCar - text.Length); i++)
                    {
                        spaces += " ";
                    }
                   
                    line.AppendLine(spaces + text);
                }
                return line.ToString();
            }
            
            public string CenterText(string text)
            {
                line.Clear();
                if (text.Length > 32)
                {
                    int currentcharacter = 0;
                    for (int longText = text.Length; longText > 32; longText -= 32)
                    {
                       
                        line.AppendLine(text.Substring(currentcharacter, 32));
                        currentcharacter += 32;
                    }
                    
                    string spaces = "";
                    
                    int centrar = (32 - text.Substring(currentcharacter, text.Length - currentcharacter).Length) / 2;
                    
                    for (int i = 0; i < centrar; i++)
                    {
                        spaces += " ";
                    }

                    
                    line.AppendLine(spaces + text.Substring(currentcharacter, text.Length - currentcharacter));
                }
                else
                {
                    string spaces = "";
                    
                    int center = (32 - text.Length) / 2;
                    
                    for (int i = 0; i < center; i++)
                    {
                        spaces += " ";
                    }

                    
                    line.AppendLine(spaces + text);

                }
                return line.ToString();
            }
            
            public string ExtremeText(string LeftText, string RightText)
            {
                line.Clear();
                
                string leftext="", rightext="", fulltext = "", spaces = "";


                if (LeftText.Length > 30)
                {
                    cortar = LeftText.Length - 30;
                    leftext = LeftText.Remove(30, cortar);
                }
                else
                {

                    leftext = LeftText;
                    //for (int i = LeftText.Length; i < 30; i++)
                    //{
                    //    leftext += "*";
                    //}

                }
                for (int i = leftext.Length; i < 30; i++)
                {
                    spaces += " ";
                }
                leftext = spaces + leftext;

                fulltext = leftext;
                spaces = "";

                if (RightText.Length > 32)
                {
                    cortar = RightText.Length - 32;
                    rightext = RightText.Remove(32, cortar);
                }
                else
                {
                    rightext = RightText;
                    //for (int i = RightText.Length; i < 32; i++)
                    //{
                    //    rightext = "*"+rightext;
                    //}
                }

                
                int spacesnumbers = maxCar - (leftext.Length + rightext.Length);
                for (int i = 0; i < spacesnumbers; i++)
                {
                    //spaces += ".";
                    spaces += " ";
                }
                fulltext += spaces + RightText;
                line.AppendLine(fulltext);

                return line.ToString();
            }
            
            public string EqualLines()
            {
                line.Clear();
                string equallines = "";
                for (int i = 0; i < maxCar; i++)
                {
                    equallines += "=";
                }
                line.AppendLine(equallines).ToString(); 
                return line.ToString();
            }

        }
        public void print()
        {
            PrinterUtility.EscPosEpsonCommands.EscPosEpson obj = new PrinterUtility.EscPosEpsonCommands.EscPosEpson();
            var BytesValue = Encoding.ASCII.GetBytes(@"D:\logo.bmp");
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Clinica\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth4());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("La familia\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Invoice\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Invoice No. : 12345\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Date        : 12/12/2015\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Itm                      Qty      Net   Total\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "item 1", 12, 11, 144.00));
            BytesValue = PrintExtensions.AddBytes(BytesValue, string.Format("{0,-40}{1,6}{2,9}{3,9:N2}\n", "item 2", 12, 11, 144.00));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Right());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Total\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("288.00\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.BarCode.Code128("12345"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.QrCode.Print("12345", PrinterUtility.Enums.QrCodeSize.Grande));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, "-------------------Thank you for coming------------------------\n");
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, CutPage());


            if (File.Exists(".\\tmpPrint.print"))
                File.Delete(".\\tmpPrint.print");
            File.WriteAllBytes(".\\tmpPrint.print", BytesValue);
            RawPrinterHelper.SendFileToPrinter("CITIZEN CT-S310II", ".\\tmpPrint.print");
            //RawPrinterHelper.SendStringToPrinter("CITIZEN CT-S310II", Encoding.ASCII.GetString(new byte[] { 27, 112, 1, 50, 250 }));

            try
            {
                File.Delete(".\\tmpPrint.print");
            }
            catch
            {

            }
        }

        public void printCashIn(transaction transaction)
        {
            //para usar las otras funciones del metodo ticket
            CrearTicket ticket = new CrearTicket();

            //primera parte del ticket
            PrinterUtility.EscPosEpsonCommands.EscPosEpson obj = new PrinterUtility.EscPosEpsonCommands.EscPosEpson();
            var BytesValue = Encoding.ASCII.GetBytes("");
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, GetLogo(@"C:\DrCash_WPF\DoctorCashWpf\DoctorCashWpf\Resources\logoTexto.bmp"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth6());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Clinica\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth4());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("La familia\n"));            
            /*
            //donde ira todo lo que le vamos a mandar a imprimir            
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight2());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Cash In\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines()+"\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Patient Name", transaction.patientFirstName) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Amount Chanrge", money.convertToMoneyFormat(transaction.amountCharged.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Cash", money.convertToMoneyFormat(transaction.cash.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Credit", money.convertToMoneyFormat(transaction.credit.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Check", transaction.check.ToString()) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Check #", transaction.checkNumber.ToString()) + "\n"));

            //verificar de que es y poner ese nomas
            if (transaction.copayment) BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.RightText("Copayment\n")));
            if (transaction.selfPay) BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.RightText("Selfpay\n")));
            if (transaction.deductible) BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.RightText("Deductible\n")));
            if (transaction.labs) BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.RightText("Labs\n")));
            if (transaction.other) BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.RightText("Other\n")));

            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Right());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());            
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));            
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Total", money.convertToMoneyFormat(transaction.total_cash.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Amount", money.convertToMoneyFormat(transaction.total_amount.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Change", money.convertToMoneyFormat(transaction.change.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            */
            //donde ira todo lo que le vamos a mandar a imprimir            
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight2());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Reciept\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Amount Charged", money.convertToMoneyFormat(transaction.amountCharged.ToString()).txtComponent) + "\n"));

            //verificar de que es y poner ese nomas
            if (transaction.copayment) BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.RightText("Copayment\n")));
            if (transaction.selfPay) BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.RightText("Selfpay\n")));
            if (transaction.deductible) BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.RightText("Deductible\n")));
            if (transaction.labs) BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.RightText("Labs\n")));
            if (transaction.other) BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.RightText("Other\n")));

            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Total Cash", money.convertToMoneyFormat(transaction.total_cash.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Credit Card", money.convertToMoneyFormat(transaction.credit.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Check", transaction.check.ToString()) + "\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());            


            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Total Paid", money.convertToMoneyFormat(transaction.amountCharged.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Change", money.convertToMoneyFormat(transaction.change.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());

            //parte final de ticket
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());


            //numero aleatorio para el codigo QR y Codigo de barra
            Random rnd = new Random();
            //int number = rnd.Next();            
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Transaction Number\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.BarCode.Code128(transaction.trn_id.ToString()+"\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(transaction.trn_id + "\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.QrCode.Print(number.ToString(), PrinterUtility.Enums.QrCodeSize.Grande));

            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Printed by: " + userInformation.user.usr_Username + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Date: " + DateTime.Now.ToShortDateString() + " @ " + DateTime.Now.ToShortTimeString() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, CutPage());

            if (File.Exists(".\\tmpPrint.print"))
                File.Delete(".\\tmpPrint.print");
            File.WriteAllBytes(".\\tmpPrint.print", BytesValue);
            RawPrinterHelper.SendFileToPrinter("CITIZEN CT-S310II", ".\\tmpPrint.print");
            //RawPrinterHelper.SendStringToPrinter("CITIZEN CT-S310II", Encoding.ASCII.GetString(new byte[] { 27, 112, 1, 50, 250 }));

            try
            {
                File.Delete(".\\tmpPrint.print");
            }
            catch
            {

            }
        }

        public void printCashOut(transaction transaction)
        {

            //para usar las otras funciones del metodo ticket
            CrearTicket ticket = new CrearTicket();
            //primera parte del ticket
            PrinterUtility.EscPosEpsonCommands.EscPosEpson obj = new PrinterUtility.EscPosEpsonCommands.EscPosEpson();
            var BytesValue = Encoding.ASCII.GetBytes("");
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, GetLogo(@"C:\DrCash_WPF\DoctorCashWpf\DoctorCashWpf\Resources\logoTexto.bmp"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth6());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Clinica\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth4());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("La familia\n"));            

            //donde ira todo lo que le vamos a mandar a imprimir            
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight2());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Cash Out\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Cash", money.convertToMoneyFormat(transaction.cash.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));


            //parte final de ticket
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());


            //numero aleatorio para el codigo QR y Codigo de barra                    
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Transaction Number\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.BarCode.Code128(transaction.trn_id.ToString() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(transaction.trn_id + "\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.QrCode.Print(number.ToString(), PrinterUtility.Enums.QrCodeSize.Grande));

            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Printed by: " + userInformation.user.usr_Username + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Date: " + DateTime.Now.ToShortDateString() + " @ " + DateTime.Now.ToShortTimeString() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, CutPage());

            if (File.Exists(".\\tmpPrint.print"))
                File.Delete(".\\tmpPrint.print");
            File.WriteAllBytes(".\\tmpPrint.print", BytesValue);
            RawPrinterHelper.SendFileToPrinter("CITIZEN CT-S310II", ".\\tmpPrint.print");
            //RawPrinterHelper.SendStringToPrinter("CITIZEN CT-S310II", Encoding.ASCII.GetString(new byte[] { 27, 112, 1, 50, 250 }));

            try
            {
                File.Delete(".\\tmpPrint.print");
            }
            catch
            {

            }
        }

        public void printCloseDate(closeDate transaction)
        {
            //para usar las otras funciones del metodo ticket
            CrearTicket ticket = new CrearTicket();
            //primera parte del ticket
            PrinterUtility.EscPosEpsonCommands.EscPosEpson obj = new PrinterUtility.EscPosEpsonCommands.EscPosEpson();
            var BytesValue = Encoding.ASCII.GetBytes("");
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, GetLogo(@"C:\DrCash_WPF\DoctorCashWpf\DoctorCashWpf\Resources\logoTexto.bmp"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth6());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Clinica\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth4());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("La familia\n"));            

            //donde ira todo lo que le vamos a mandar a imprimir            
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight2());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Close Date\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Information\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("100's", money.convertToMoneyFormat(transaction.clt_100_bills.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("50's", money.convertToMoneyFormat(transaction.clt_50_bills.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("20's", money.convertToMoneyFormat(transaction.clt_20_bills.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("10's", money.convertToMoneyFormat(transaction.clt_10_bills.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("5's", money.convertToMoneyFormat(transaction.clt_5_bills.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("1's", money.convertToMoneyFormat(transaction.clt_1_bills.ToString()).txtComponent) + "\n"));
            
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Total Cash", money.convertToMoneyFormat(transaction.clt_total_cash.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Total Credit", money.convertToMoneyFormat(transaction.clt_total_credit.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("total Check", money.convertToMoneyFormat(transaction.clt_total_check.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));


            //parte final de ticket
           // BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());


            //numero aleatorio para el codigo QR y Codigo de barra           
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Closed Transaction Number\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.BarCode.Code128(transaction.clt_closed_ID.ToString() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(transaction.clt_closed_ID + "\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.QrCode.Print(number.ToString(), PrinterUtility.Enums.QrCodeSize.Grande));

            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Printed by: " + userInformation.user.usr_Username + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Date: " + DateTime.Now.ToShortDateString() + " @ " + DateTime.Now.ToShortTimeString() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, CutPage());

            if (File.Exists(".\\tmpPrint.print"))
                File.Delete(".\\tmpPrint.print");
            File.WriteAllBytes(".\\tmpPrint.print", BytesValue);
            RawPrinterHelper.SendFileToPrinter("CITIZEN CT-S310II", ".\\tmpPrint.print");
            //RawPrinterHelper.SendStringToPrinter("CITIZEN CT-S310II", Encoding.ASCII.GetString(new byte[] { 27, 112, 1, 50, 250 }));

            try
            {
                File.Delete(".\\tmpPrint.print");
            }
            catch
            {

            }
        }

        public void printClosedStatement(closeDate transaction)
        {

            //para usar las otras funciones del metodo ticket
            CrearTicket ticket = new CrearTicket();

            //primera parte del ticket
            PrinterUtility.EscPosEpsonCommands.EscPosEpson obj = new PrinterUtility.EscPosEpsonCommands.EscPosEpson();
            var BytesValue = Encoding.ASCII.GetBytes("");
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, GetLogo(@"C:\DrCash_WPF\DoctorCashWpf\DoctorCashWpf\Resources\logoTexto.bmp"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth6());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Clinica\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth4());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("La familia\n"));            

            //donde ira todo lo que le vamos a mandar a imprimir            
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight2());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Closing Statement\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Totals\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Initial Cash", money.convertToMoneyFormat(transaction.clt_initial_cash.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Amount Charged", money.convertToMoneyFormat(transaction.clt_checks_amount.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Cash", money.convertToMoneyFormat(transaction.clt_total_cash.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Credit Card", money.convertToMoneyFormat(transaction.clt_total_credit.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Check", transaction.clt_total_check.ToString()) + "\n"));            
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Report\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("100's", money.convertToMoneyFormat(transaction.clt_100_bills.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("50's", money.convertToMoneyFormat(transaction.clt_50_bills.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("20's", money.convertToMoneyFormat(transaction.clt_20_bills.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("10's", money.convertToMoneyFormat(transaction.clt_10_bills.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("5's", money.convertToMoneyFormat(transaction.clt_5_bills.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("1's", money.convertToMoneyFormat(transaction.clt_1_bills.ToString()).txtComponent) + "\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Total Cash", money.convertToMoneyFormat(transaction.clt_total_cash.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Total Credit", money.convertToMoneyFormat(transaction.clt_total_credit.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("total Check", money.convertToMoneyFormat(transaction.clt_total_check.ToString()).txtComponent) + "\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            ////BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Balance", money.convertToMoneyFormat(transaction.clt_balance.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));


            //parte final de ticket
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());

            /*
            //numero aleatorio para el codigo QR y Codigo de barra
            Random rnd = new Random();
            int number = rnd.Next();
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(number + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.BarCode.Code128(number.ToString()));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.QrCode.Print(number.ToString(), PrinterUtility.Enums.QrCodeSize.Grande));*/

            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Printed by: " + userInformation.user.usr_Username + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Date: " + DateTime.Now.ToShortDateString() + " @ " + DateTime.Now.ToShortTimeString() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, CutPage());

            if (File.Exists(".\\tmpPrint.print"))
                File.Delete(".\\tmpPrint.print");
            File.WriteAllBytes(".\\tmpPrint.print", BytesValue);
            RawPrinterHelper.SendFileToPrinter("CITIZEN CT-S310II", ".\\tmpPrint.print");
            //RawPrinterHelper.SendStringToPrinter("CITIZEN CT-S310II", Encoding.ASCII.GetString(new byte[] { 27, 112, 1, 50, 250 }));

            try
            {
                File.Delete(".\\tmpPrint.print");
            }
            catch
            {

            }
        }

        public void printViewReciept(transaction transaction,double total)
        {

            //para usar las otras funciones del metodo ticket
            CrearTicket ticket = new CrearTicket();

            //primera parte del ticket
            PrinterUtility.EscPosEpsonCommands.EscPosEpson obj = new PrinterUtility.EscPosEpsonCommands.EscPosEpson();
            var BytesValue = Encoding.ASCII.GetBytes("");
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, GetLogo(@"C:\DrCash_WPF\DoctorCashWpf\DoctorCashWpf\Resources\logoTexto.bmp"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth6());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Clinica\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth4());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("La familia\n"));            

            //donde ira todo lo que le vamos a mandar a imprimir            
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight2());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Reciept\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Amount Charged", money.convertToMoneyFormat(transaction.amountCharged.ToString()).txtComponent)+"\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Total Cash", money.convertToMoneyFormat(transaction.total_cash.ToString()).txtComponent) + "\n"));            
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Credit Card", money.convertToMoneyFormat(transaction.credit.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Check", transaction.check.ToString()) + "\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());            
            

            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Total Paid", money.convertToMoneyFormat(total.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Change", money.convertToMoneyFormat(transaction.change.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            


            //parte final de ticket
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());

            /*
            //numero aleatorio para el codigo QR y Codigo de barra
            Random rnd = new Random();
            int number = rnd.Next();
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(number + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.BarCode.Code128(number.ToString()));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.QrCode.Print(number.ToString(), PrinterUtility.Enums.QrCodeSize.Grande));*/

            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Printed by: " + userInformation.user.usr_Username + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Date: " + DateTime.Now.ToShortDateString() + " @ " + DateTime.Now.ToShortTimeString() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, CutPage());

            if (File.Exists(".\\tmpPrint.print"))
                File.Delete(".\\tmpPrint.print");
            File.WriteAllBytes(".\\tmpPrint.print", BytesValue);
            RawPrinterHelper.SendFileToPrinter("CITIZEN CT-S310II", ".\\tmpPrint.print");
            //RawPrinterHelper.SendStringToPrinter("CITIZEN CT-S310II", Encoding.ASCII.GetString(new byte[] { 27, 112, 1, 50, 250 }));

            try
            {
                File.Delete(".\\tmpPrint.print");
            }
            catch
            {

            }
        }

        public void printRefund(transaction transactions)
        {
            //para usar las otras funciones del metodo ticket
            CrearTicket ticket = new CrearTicket();

            //primera parte del ticket
            PrinterUtility.EscPosEpsonCommands.EscPosEpson obj = new PrinterUtility.EscPosEpsonCommands.EscPosEpson();
            var BytesValue = Encoding.ASCII.GetBytes("");
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, GetLogo(@"C:\DrCash_WPF\DoctorCashWpf\DoctorCashWpf\Resources\logoTexto.bmp"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth6());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.FontSelect.FontA());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Clinica\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleWidth4());
            //BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("La familia\n"));            

            //donde ira todo lo que le vamos a mandar a imprimir            
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight2());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Refund\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.ExtremeText("Amount Charged", money.convertToMoneyFormat(transactions.amountCharged.ToString()).txtComponent) + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(ticket.EqualLines() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());

            //parte final de ticket
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Separator());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Lf());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Center());


            //numero aleatorio para el codigo QR y Codigo de barra                    
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Transaction Number\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.DoubleHeight6());
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.BarCode.Code128(transactions.trn_id.ToString() + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes(transactions.trn_id + "\n"));
            //BytesValue = PrintExtensions.AddBytes(BytesValue, obj.QrCode.Print(number.ToString(), PrinterUtility.Enums.QrCodeSize.Grande));

            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.CharSize.Nomarl());
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Printed by: " + userInformation.user.usr_Username + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, Encoding.ASCII.GetBytes("Date: " + DateTime.Now + "\n"));
            BytesValue = PrintExtensions.AddBytes(BytesValue, obj.Alignment.Left());
            BytesValue = PrintExtensions.AddBytes(BytesValue, CutPage());

            if (File.Exists(".\\tmpPrint.print"))
                File.Delete(".\\tmpPrint.print");
            File.WriteAllBytes(".\\tmpPrint.print", BytesValue);
            RawPrinterHelper.SendFileToPrinter("CITIZEN CT-S310II", ".\\tmpPrint.print");
            //RawPrinterHelper.SendStringToPrinter("CITIZEN CT-S310II", Encoding.ASCII.GetString(new byte[] { 27, 112, 1, 50, 250 }));

            try
            {
                File.Delete(".\\tmpPrint.print");
            }
            catch
            {

            }
        }

        public byte[] CutPage()
        {
            List<byte> oby = new List<byte>();
            oby.Add(Convert.ToByte(Convert.ToChar(0x1D)));
            oby.Add(Convert.ToByte('V'));
            oby.Add((byte)66);
            oby.Add((byte)3);
            return oby.ToArray();
        }
        public byte[] GetLogo(string LogoPath)
        {
            List<byte> byteList = new List<byte>();
            if (!File.Exists(LogoPath))
                return null;
            BitmapData data = GetBitmapData(LogoPath);
            BitArray dots = data.Dots;
            byte[] width = BitConverter.GetBytes(data.Width);

            int offset = 0;
            MemoryStream stream = new MemoryStream();
            // BinaryWriter bw = new BinaryWriter(stream);
            byteList.Add(Convert.ToByte(Convert.ToChar(0x1B)));
            //bw.Write((char));
            byteList.Add(Convert.ToByte('@'));
            //bw.Write('@');
            byteList.Add(Convert.ToByte(Convert.ToChar(0x1B)));
            // bw.Write((char)0x1B);
            byteList.Add(Convert.ToByte('3'));
            //bw.Write('3');
            //bw.Write((byte)24);
            byteList.Add((byte)24);
            while (offset < data.Height)
            {
                byteList.Add(Convert.ToByte(Convert.ToChar(0x1B)));
                byteList.Add(Convert.ToByte('*'));
                //bw.Write((char)0x1B);
                //bw.Write('*');         // bit-image mode
                byteList.Add((byte)33);
                //bw.Write((byte)33);    // 24-dot double-density
                byteList.Add(width[0]);
                byteList.Add(width[1]);
                //bw.Write(width[0]);  // width low byte
                //bw.Write(width[1]);  // width high byte

                for (int x = 0; x < data.Width; ++x)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        byte slice = 0;
                        for (int b = 0; b < 8; ++b)
                        {
                            int y = (((offset / 8) + k) * 8) + b;
                            // Calculate the location of the pixel we want in the bit array.
                            // It'll be at (y * width) + x.
                            int i = (y * data.Width) + x;

                            // If the image is shorter than 24 dots, pad with zero.
                            bool v = false;
                            if (i < dots.Length)
                            {
                                v = dots[i];
                            }
                            slice |= (byte)((v ? 1 : 0) << (7 - b));
                        }
                        byteList.Add(slice);
                        //bw.Write(slice);
                    }
                }
                offset += 24;
                byteList.Add(Convert.ToByte(0x0A));
                //bw.Write((char));
            }
            // Restore the line spacing to the default of 30 dots.
            byteList.Add(Convert.ToByte(0x1B));
            byteList.Add(Convert.ToByte('3'));
            //bw.Write('3');
            byteList.Add((byte)30);
            return byteList.ToArray();
            //bw.Flush();
            //byte[] bytes = stream.ToArray();
            //return logo + Encoding.Default.GetString(bytes);
        }

        public BitmapData GetBitmapData(string bmpFileName)
        {
            using (var bitmap = (Bitmap)Bitmap.FromFile(bmpFileName))
            {
                /*obtiene el tamaño del logo*/
                var threshold = 127;
                var index = 0;
                double multiplier = 570; // this depends on your printer model. for Beiyang you should use 1000
                double scale = (double)(multiplier / (double)bitmap.Width);
                int xheight = (int)(bitmap.Height * scale);
                int xwidth = (int)(bitmap.Width * scale);
                var dimensions = xwidth * xheight;
                var dots = new BitArray(dimensions);

                for (var y = 0; y < xheight; y++)
                {
                    for (var x = 0; x < xwidth; x++)
                    {
                        var _x = (int)(x / scale);
                        var _y = (int)(y / scale);
                        var color = bitmap.GetPixel(_x, _y);
                        var luminance = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                        dots[index] = (luminance < threshold);
                        index++;
                    }
                }

                return new BitmapData()
                {
                    Dots = dots,
                    Height = (int)(bitmap.Height * scale),
                    Width = (int)(bitmap.Width * scale)
                };
            }
        }

        public class BitmapData
        {
            public BitArray Dots
            {
                get;
                set;
            }

            public int Height
            {
                get;
                set;
            }

            public int Width
            {
                get;
                set;
            }
        }
    }
}



